using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using MsMeeseeks.DIE.Configuration;
using MsMeeseeks.DIE.Contexts;
using MsMeeseeks.DIE.Extensions;
using MsMeeseeks.DIE.MsContainer;
using MsMeeseeks.DIE.Nodes.Elements;
using MsMeeseeks.DIE.Nodes.Elements.FunctionCalls;
using MsMeeseeks.DIE.Nodes.Functions;
using MsMeeseeks.DIE.Nodes.Mappers;
using MsMeeseeks.DIE.Nodes.Roots;
using MsMeeseeks.DIE.Utility;

namespace MsMeeseeks.DIE.Nodes.Ranges;

internal interface IScopeNode : IScopeNodeBase
{
    string TransientScopeInterfaceFullName { get; }
    string TransientScopeInterfaceReference { get; }
    string TransientScopeInterfaceParameterReference { get; }
    IScopeCallNode BuildScopeCallFunction(string containerParameter, string transientScopeInterfaceParameter, INamedTypeSymbol type, IRangeNode callingRange, IFunctionNode callingFunction);
}

internal partial class ScopeNode : ScopeNodeBase, IScopeNode, ITransientScopeInstance
{
    private readonly Func<INamedTypeSymbol, IReadOnlyList<ITypeSymbol>, ICreateScopeFunctionNodeRoot> _createScopeFunctionNodeFactory;

    internal ScopeNode(
        IScopeInfo scopeInfo,
        IContainerNode parentContainer,
        ITransientScopeInterfaceNode transientScopeInterfaceNode,
        IScopeManager scopeManager,
        IUserDefinedElements userDefinedElements,
        IReferenceGenerator referenceGenerator,
        IContainerWideContext containerWideContext,
        IMapperDataToFunctionKeyTypeConverter mapperDataToFunctionKeyTypeConverter,
        Func<MapperData, ITypeSymbol, IReadOnlyList<ITypeSymbol>, ICreateFunctionNodeRoot> createFunctionNodeFactory,
        Func<INamedTypeSymbol, IReadOnlyList<ITypeSymbol>, IMultiFunctionNodeRoot> multiFunctionNodeFactory,
        Func<INamedTypeSymbol, IReadOnlyList<ITypeSymbol>, ICreateScopeFunctionNodeRoot> createScopeFunctionNodeFactory,
        Func<ScopeLevel, INamedTypeSymbol, IRangedInstanceFunctionGroupNode> rangedInstanceFunctionGroupNodeFactory,
        Func<IReadOnlyList<IInitializedInstanceNode>, IReadOnlyList<ITypeSymbol>, IVoidFunctionNodeRoot> voidFunctionNodeFactory, 
        Func<IDisposalHandlingNode> disposalHandlingNodeFactory,
        Func<INamedTypeSymbol, IInitializedInstanceNode> initializedInstanceNodeFactory)
        : base(
            scopeInfo, 
            parentContainer,
            scopeManager,
            userDefinedElements, 
            referenceGenerator, 
            containerWideContext,
            mapperDataToFunctionKeyTypeConverter,
            createFunctionNodeFactory,  
            multiFunctionNodeFactory,
            rangedInstanceFunctionGroupNodeFactory,
            voidFunctionNodeFactory,
            disposalHandlingNodeFactory,
            initializedInstanceNodeFactory)
    {
        _createScopeFunctionNodeFactory = createScopeFunctionNodeFactory;
        TransientScopeInterfaceFullName = $"{parentContainer.Namespace}.{parentContainer.Name}.{transientScopeInterfaceNode.Name}";
        TransientScopeInterfaceReference = referenceGenerator.Generate("_transientScope");
        TransientScopeInterfaceParameterReference = referenceGenerator.Generate("transientScope");
    }
    protected override string ContainerParameterForScope => ContainerReference;
    protected override string TransientScopeInterfaceParameterForScope => TransientScopeInterfaceReference;

    public override IFunctionCallNode BuildContainerInstanceCall(INamedTypeSymbol type, IFunctionNode callingFunction) => 
        ParentContainer.BuildContainerInstanceCall(ContainerReference, type, callingFunction);

    public override IFunctionCallNode BuildTransientScopeInstanceCall(INamedTypeSymbol type, IFunctionNode callingFunction) =>
        ParentContainer.TransientScopeInterface.BuildTransientScopeInstanceCall(
            TransientScopeInterfaceReference, 
            type,
            callingFunction);

    public IScopeCallNode BuildScopeCallFunction(string containerParameter, string transientScopeInterfaceParameter, INamedTypeSymbol type, IRangeNode callingRange, IFunctionNode callingFunction) =>
        FunctionResolutionUtility.GetOrCreateFunctionCall(
            type,
            callingFunction,
            _createFunctions,
            () => _createScopeFunctionNodeFactory(
                type,
                callingFunction.Overrides.Select(kvp => kvp.Key).ToList())
                .Function
                .EnqueueBuildJobTo(ParentContainer.BuildQueue, ImmutableStack<INamedTypeSymbol>.Empty),
            f => f.CreateScopeCall(containerParameter, transientScopeInterfaceParameter, callingRange, callingFunction, this));

    public string TransientScopeInterfaceFullName { get; }
    public string TransientScopeInterfaceReference { get; }
    public string TransientScopeInterfaceParameterReference { get; }
}