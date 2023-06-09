using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using MrMeeseeks.SourceGeneratorUtility;
using MsMeeseeks.DIE.Configuration;
using MsMeeseeks.DIE.Contexts;
using MsMeeseeks.DIE.Extensions;
using MsMeeseeks.DIE.Nodes.Elements;
using MsMeeseeks.DIE.Nodes.Elements.FunctionCalls;
using MsMeeseeks.DIE.Nodes.Functions;
using MsMeeseeks.DIE.Nodes.Mappers;
using MsMeeseeks.DIE.Nodes.Roots;
using MsMeeseeks.DIE.Utility;
using MsMeeseeks.DIE.Visitors;

namespace MsMeeseeks.DIE.Nodes.Ranges;

internal interface IRangeNode : INode
{
    string FullName { get; }
    string Name { get; }
    DisposalType DisposalType { get; }
    IDisposalHandlingNode DisposalHandling { get; }
    bool AddForDisposal { get; }
    bool AddForDisposalAsync { get; }
    string? ContainerReference { get; }
    IEnumerable<IInitializedInstanceNode> InitializedInstances { get; }

    IFunctionCallNode BuildCreateCall(ITypeSymbol type, IFunctionNode callingFunction);
    IAsyncFunctionCallNode BuildAsyncCreateCall(
        MapperData mapperData, 
        ITypeSymbol type,
        SynchronicityDecision synchronicity, 
        IFunctionNode callingFunction);
    ITransientScopeCallNode BuildTransientScopeCall(INamedTypeSymbol type, IFunctionNode callingFunction);
    IFunctionCallNode BuildContainerInstanceCall(INamedTypeSymbol type, IFunctionNode callingFunction);
    IFunctionCallNode BuildTransientScopeInstanceCall(INamedTypeSymbol type, IFunctionNode callingFunction);
    IRangedInstanceFunctionNode BuildTransientScopeFunction(INamedTypeSymbol type, IFunctionNode callingFunction);
    IFunctionCallNode BuildScopeInstanceCall(INamedTypeSymbol type, IFunctionNode callingFunction);
    IFunctionCallNode BuildEnumerableCall(INamedTypeSymbol type, IFunctionNode callingFunction);
    IEnumerable<ICreateFunctionNodeBase> CreateFunctions { get; }
    IEnumerable<IRangedInstanceFunctionGroupNode> RangedInstanceFunctionGroups { get; }
    IEnumerable<IMultiFunctionNode> MultiFunctions { get; }
    IEnumerable<IVoidFunctionNode> InitializationFunctions { get; }
    IScopeCallNode BuildScopeCall(INamedTypeSymbol type, IFunctionNode callingFunction);
    IInitializedInstanceNode? GetInitializedNode(INamedTypeSymbol type);
    IFunctionCallNode BuildInitializationCall(IFunctionNode callingFunction);

    void CycleDetectionAndReorderingOfInitializedInstances();
}

internal abstract class RangeNode : IRangeNode
{
    private readonly IMapperDataToFunctionKeyTypeConverter _mapperDataToFunctionKeyTypeConverter;
    private readonly Func<MapperData, ITypeSymbol, IReadOnlyList<ITypeSymbol>, ICreateFunctionNodeRoot> _createFunctionNodeFactory;
    private readonly Func<INamedTypeSymbol, IReadOnlyList<ITypeSymbol>, IMultiFunctionNodeRoot> _multiFunctionNodeFactory;
    private readonly Func<ScopeLevel, INamedTypeSymbol, IRangedInstanceFunctionGroupNode> _rangedInstanceFunctionGroupNodeFactory;
    protected readonly Func<IReadOnlyList<IInitializedInstanceNode>, IReadOnlyList<ITypeSymbol>, IVoidFunctionNodeRoot> VoidFunctionNodeFactory;
    protected readonly Dictionary<ITypeSymbol, List<ICreateFunctionNodeBase>> _createFunctions = new(CustomSymbolEqualityComparer.IncludeNullability);
    private readonly Dictionary<ITypeSymbol, List<IMultiFunctionNode>> _multiFunctions = new(CustomSymbolEqualityComparer.IncludeNullability);

    private readonly Dictionary<ITypeSymbol, IRangedInstanceFunctionGroupNode> _rangedInstanceFunctionGroupNodes = new(CustomSymbolEqualityComparer.IncludeNullability);

    protected readonly List<IVoidFunctionNode> _initializationFunctions = new();

    protected readonly Dictionary<INamedTypeSymbol, IInitializedInstanceNode> InitializedInstanceNodesMap = new(CustomSymbolEqualityComparer.IncludeNullability);

    public abstract string FullName { get; }
    public string Name { get; }
    public abstract DisposalType DisposalType { get; }
    public IDisposalHandlingNode DisposalHandling { get; }
    public bool AddForDisposal { get; }
    public bool AddForDisposalAsync { get; }
    public abstract string? ContainerReference { get; }

    public IEnumerable<IInitializedInstanceNode> InitializedInstances => InitializedInstanceNodesMap.Values;

    public IFunctionCallNode BuildEnumerableCall(INamedTypeSymbol type, IFunctionNode callingFunction) =>
        FunctionResolutionUtility.GetOrCreateFunctionCall(
            type,
            callingFunction,
            _multiFunctions,
            () => _multiFunctionNodeFactory(
                type,
                callingFunction.Overrides.Select(kvp => kvp.Key).ToList())
                .Function
                .EnqueueBuildJobTo(ParentContainer.BuildQueue, ImmutableStack<INamedTypeSymbol>.Empty),
            f => f.CreateCall(null, callingFunction));

    public IEnumerable<ICreateFunctionNodeBase> CreateFunctions => _createFunctions.Values.SelectMany(l => l);

    public IEnumerable<IRangedInstanceFunctionGroupNode> RangedInstanceFunctionGroups =>
        _rangedInstanceFunctionGroupNodes.Values;

    public IEnumerable<IMultiFunctionNode> MultiFunctions => _multiFunctions.Values.SelectMany(l => l);
    public IEnumerable<IVoidFunctionNode> InitializationFunctions => _initializationFunctions;

    internal RangeNode(
        string name,
        INamedTypeSymbol? rangeType,
        IUserDefinedElements userDefinedElements,
        IMapperDataToFunctionKeyTypeConverter mapperDataToFunctionKeyTypeConverter,
        IContainerWideContext containerWideContext,
        Func<MapperData, ITypeSymbol, IReadOnlyList<ITypeSymbol>, ICreateFunctionNodeRoot> createFunctionNodeFactory,
        Func<INamedTypeSymbol, IReadOnlyList<ITypeSymbol>, IMultiFunctionNodeRoot> multiFunctionNodeFactory,
        Func<ScopeLevel, INamedTypeSymbol, IRangedInstanceFunctionGroupNode> rangedInstanceFunctionGroupNodeFactory,
        Func<IReadOnlyList<IInitializedInstanceNode>, IReadOnlyList<ITypeSymbol>, IVoidFunctionNodeRoot> voidFunctionNodeFactory, 
        Func<IDisposalHandlingNode> disposalHandlingNodeFactory,
        Func<INamedTypeSymbol, IInitializedInstanceNode> initializedInstanceNodeFactory)
    {
        _mapperDataToFunctionKeyTypeConverter = mapperDataToFunctionKeyTypeConverter;
        _createFunctionNodeFactory = createFunctionNodeFactory;
        _multiFunctionNodeFactory = multiFunctionNodeFactory;
        _rangedInstanceFunctionGroupNodeFactory = rangedInstanceFunctionGroupNodeFactory;
        VoidFunctionNodeFactory = voidFunctionNodeFactory;
        Name = name;

        DisposalHandling = disposalHandlingNodeFactory();

        if (userDefinedElements.AddForDisposal is { })
        {
            AddForDisposal = true;
            DisposalHandling.RegisterSyncDisposal();
        }

        if (userDefinedElements.AddForDisposalAsync is { })
        {
            AddForDisposalAsync = true;
            DisposalHandling.RegisterAsyncDisposal();
        }
        
        if (rangeType is { })
        {
            var types = rangeType
                .GetAttributes()
                .Where(ad =>
                    CustomSymbolEqualityComparer.Default.Equals(ad.AttributeClass,
                        containerWideContext.WellKnownTypesMiscellaneous.InitializedInstancesAttribute))
                .Where(ad =>
                    ad is { ConstructorArguments.Length: 1 } &&
                    ad.ConstructorArguments[0].Kind == TypedConstantKind.Array)
                .SelectMany(ad => ad
                    .ConstructorArguments[0]
                    .Values
                    .Select(tc => tc.Value)
                    .OfType<INamedTypeSymbol>());
            foreach (var type in types)
                InitializedInstanceNodesMap[type] = initializedInstanceNodeFactory(type);
        }
    }
    
    protected abstract IScopeManager ScopeManager { get; }
    
    protected abstract IContainerNode ParentContainer { get; }
    
    protected abstract string ContainerParameterForScope { get; }

    protected virtual string TransientScopeInterfaceParameterForScope => Constants.ThisKeyword;

    public virtual void Build(ImmutableStack<INamedTypeSymbol> implementationStack) {}

    public abstract void Accept(INodeVisitor nodeVisitor);
    public IFunctionCallNode BuildCreateCall(ITypeSymbol type, IFunctionNode callingFunction) =>
        FunctionResolutionUtility.GetOrCreateFunctionCall(
            type,
            callingFunction,
            _createFunctions,
            () => _createFunctionNodeFactory(
                    new VanillaMapperData(),
                    type,
                    callingFunction.Overrides.Select(kvp => kvp.Key).ToList())
                .Function
                .EnqueueBuildJobTo(ParentContainer.BuildQueue, ImmutableStack<INamedTypeSymbol>.Empty),
            f => f.CreateCall(null, callingFunction));

    public IAsyncFunctionCallNode BuildAsyncCreateCall(
        MapperData mapperData, 
        ITypeSymbol type, 
        SynchronicityDecision synchronicity,
        IFunctionNode callingFunction) =>
        FunctionResolutionUtility.GetOrCreateFunctionCall(
            _mapperDataToFunctionKeyTypeConverter.Convert(mapperData, type),
            callingFunction,
            _createFunctions,
            () => _createFunctionNodeFactory(
                    mapperData,
                    type,
                    callingFunction.Overrides.Select(kvp => kvp.Key).ToList())
                .Function
                .EnqueueBuildJobTo(ParentContainer.BuildQueue, ImmutableStack<INamedTypeSymbol>.Empty),
            f => f.CreateAsyncCall(type, null, synchronicity, callingFunction));

    public IFunctionCallNode BuildInitializationCall(IFunctionNode callingFunction)
    {
        var voidFunction = FunctionResolutionUtility.GetOrCreateFunction(
            callingFunction,
            _initializationFunctions,
            () => VoidFunctionNodeFactory(
                    InitializedInstances.ToList(),
                    callingFunction.Overrides.Select(kvp => kvp.Key).ToList())
                .Function
                .EnqueueBuildJobTo(ParentContainer.BuildQueue, ImmutableStack<INamedTypeSymbol>.Empty));

        return voidFunction.CreateCall(null, callingFunction);
    }

    public void CycleDetectionAndReorderingOfInitializedInstances()
    {
        foreach (var initializationFunction in _initializationFunctions)
            initializationFunction.ReorderOrDetectCycle();
    }

    public ITransientScopeCallNode BuildTransientScopeCall(INamedTypeSymbol type, IFunctionNode callingFunction) => 
        ScopeManager.GetTransientScope(type).BuildTransientScopeCallFunction(ContainerParameterForScope, type, this, callingFunction);

    public IScopeCallNode BuildScopeCall(INamedTypeSymbol type, IFunctionNode callingFunction) => 
        ScopeManager.GetScope(type).BuildScopeCallFunction(ContainerParameterForScope, TransientScopeInterfaceParameterForScope, type, this, callingFunction);

    public IInitializedInstanceNode? GetInitializedNode(INamedTypeSymbol type) => 
        InitializedInstanceNodesMap.TryGetValue(type, out var initializedInstanceNode) 
            ? initializedInstanceNode
            : null;

    protected IFunctionCallNode BuildRangedInstanceCall(string? ownerReference, INamedTypeSymbol type, IFunctionNode callingFunction, ScopeLevel level)
    {
        if (!_rangedInstanceFunctionGroupNodes.TryGetValue(type, out var rangedInstanceFunctionGroupNode))
        {
            rangedInstanceFunctionGroupNode = _rangedInstanceFunctionGroupNodeFactory(
                level,
                type)
                .EnqueueBuildJobTo(ParentContainer.BuildQueue, ImmutableStack<INamedTypeSymbol>.Empty);
            _rangedInstanceFunctionGroupNodes[type] = rangedInstanceFunctionGroupNode;
        }
        var function = rangedInstanceFunctionGroupNode.BuildFunction(callingFunction);
        return function.CreateCall(ownerReference, callingFunction);
    }

    public abstract IFunctionCallNode BuildContainerInstanceCall(
        INamedTypeSymbol type, 
        IFunctionNode callingFunction);

    public abstract IFunctionCallNode BuildTransientScopeInstanceCall(
        INamedTypeSymbol type,
        IFunctionNode callingFunction);

    public IRangedInstanceFunctionNode BuildTransientScopeFunction(
        INamedTypeSymbol type, 
        IFunctionNode callingFunction)
    {
        if (!_rangedInstanceFunctionGroupNodes.TryGetValue(type, out var rangedInstanceFunctionGroupNode))
        {
            rangedInstanceFunctionGroupNode = _rangedInstanceFunctionGroupNodeFactory(
                ScopeLevel.TransientScope,
                type)
                .EnqueueBuildJobTo(ParentContainer.BuildQueue, ImmutableStack<INamedTypeSymbol>.Empty);
            _rangedInstanceFunctionGroupNodes[type] = rangedInstanceFunctionGroupNode;
        }
        var function = rangedInstanceFunctionGroupNode.BuildFunction(callingFunction);
        function.CreateCall(null, callingFunction);
        return function;
    }

    public IFunctionCallNode BuildScopeInstanceCall(
        INamedTypeSymbol type, 
        IFunctionNode callingFunction) =>
        BuildRangedInstanceCall(null, type, callingFunction, ScopeLevel.Scope);
}