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
using MsMeeseeks.DIE.Nodes.Elements.Delegates;
using MsMeeseeks.DIE.Nodes.Elements.FunctionCalls;
using MsMeeseeks.DIE.Nodes.Functions;
using MsMeeseeks.DIE.Nodes.Mappers;
using MsMeeseeks.DIE.Nodes.Roots;

namespace MsMeeseeks.DIE.Nodes.Ranges;

internal interface IContainerNode : IRangeNode
{
    string Namespace { get; }
    Queue<BuildJob> BuildQueue { get; }
    Queue<IFunctionNode> AsyncCheckQueue { get; }
    IReadOnlyList<IEntryFunctionNode> RootFunctions { get; }
    IEnumerable<IScopeNode> Scopes { get; }
    IEnumerable<ITransientScopeNode> TransientScopes { get; }
    ITransientScopeInterfaceNode TransientScopeInterface { get; }
    string TransientScopeDisposalReference { get; }
    string TransientScopeDisposalElement { get; }
    IFunctionCallNode BuildContainerInstanceCall(string? ownerReference, INamedTypeSymbol type, IFunctionNode callingFunction);
    IReadOnlyList<ICreateContainerFunctionNode> CreateContainerFunctions { get; }

    void RegisterDelegateBaseNode(IDelegateBaseNode delegateBaseNode);
}

internal record BuildJob(INode Node, ImmutableStack<INamedTypeSymbol> PreviousImplementations);

internal partial class ContainerNode : RangeNode, IContainerNode, IContainerInstance
{
    private readonly IContainerInfo _containerInfo;
    private readonly IFunctionCycleTracker _functionCycleTracker;
    private readonly ICurrentExecutionPhaseSetter _currentExecutionPhaseSetter;
    private readonly Lazy<ITransientScopeInterfaceNode> _lazyTransientScopeInterfaceNode;
    private readonly Func<ITypeSymbol, string, IReadOnlyList<ITypeSymbol>, IEntryFunctionNodeRoot> _entryFunctionNodeFactory;
    private readonly Func<IMethodSymbol, IVoidFunctionNode?, ICreateContainerFunctionNode> _creatContainerFunctionNodeFactory;
    private readonly List<IEntryFunctionNode> _rootFunctions = new();
    private readonly Lazy<IScopeManager> _lazyScopeManager;
    private readonly Lazy<DisposalType> _lazyDisposalType;
    private readonly List<IDelegateBaseNode> _delegateBaseNodes = new();

    public override string FullName { get; }
    public override DisposalType DisposalType => _lazyDisposalType.Value;
    public string Namespace { get; }
    public Queue<BuildJob> BuildQueue { get; } = new();
    public Queue<IFunctionNode> AsyncCheckQueue { get; } = new();
    public IReadOnlyList<IEntryFunctionNode> RootFunctions => _rootFunctions;
    public IEnumerable<IScopeNode> Scopes => ScopeManager.Scopes;
    public IEnumerable<ITransientScopeNode> TransientScopes => ScopeManager.TransientScopes;
    public ITransientScopeInterfaceNode TransientScopeInterface => _lazyTransientScopeInterfaceNode.Value;
    public string TransientScopeDisposalReference { get; }
    public string TransientScopeDisposalElement { get; }

    public IFunctionCallNode BuildContainerInstanceCall(
        string? ownerReference, 
        INamedTypeSymbol type,
        IFunctionNode callingFunction) =>
        BuildRangedInstanceCall(ownerReference, type, callingFunction, ScopeLevel.Container);

    public IReadOnlyList<ICreateContainerFunctionNode> CreateContainerFunctions { get; private set; } = null!;
    public void RegisterDelegateBaseNode(IDelegateBaseNode delegateBaseNode) => 
        _delegateBaseNodes.Add(delegateBaseNode);

    internal ContainerNode(
        IContainerInfoContext containerInfoContext,
        Func<(INamedTypeSymbol?, INamedTypeSymbol), IUserDefinedElements> userDefinedElementsFactory,
        IReferenceGenerator referenceGenerator,
        IFunctionCycleTracker functionCycleTracker,
        IMapperDataToFunctionKeyTypeConverter mapperDataToFunctionKeyTypeConverter,
        IContainerWideContext containerWideContext,
        ICurrentExecutionPhaseSetter currentExecutionPhaseSetter,
        Lazy<ITransientScopeInterfaceNode> lazyTransientScopeInterfaceNode,
        Lazy<IScopeManager> lazyScopeManager,
        Func<MapperData, ITypeSymbol, IReadOnlyList<ITypeSymbol>, ICreateFunctionNodeRoot> createFunctionNodeFactory,
        Func<INamedTypeSymbol, IReadOnlyList<ITypeSymbol>, IMultiFunctionNodeRoot> multiFunctionNodeFactory,
        Func<ScopeLevel, INamedTypeSymbol, IRangedInstanceFunctionGroupNode> rangedInstanceFunctionGroupNodeFactory,
        Func<ITypeSymbol, string, IReadOnlyList<ITypeSymbol>, IEntryFunctionNodeRoot> entryFunctionNodeFactory,
        Func<IReadOnlyList<IInitializedInstanceNode>, IReadOnlyList<ITypeSymbol>, IVoidFunctionNodeRoot> voidFunctionNodeFactory, 
        Func<IDisposalHandlingNode> disposalHandlingNodeFactory,
        Func<INamedTypeSymbol, IInitializedInstanceNode> initializedInstanceNodeFactory,
        Func<IMethodSymbol, IVoidFunctionNode?, ICreateContainerFunctionNode> creatContainerFunctionNodeFactory)
        : base (
            containerInfoContext.ContainerInfo.Name, 
            containerInfoContext.ContainerInfo.ContainerType,
            userDefinedElementsFactory((containerInfoContext.ContainerInfo.ContainerType, containerInfoContext.ContainerInfo.ContainerType)), 
            mapperDataToFunctionKeyTypeConverter,
            containerWideContext,
            createFunctionNodeFactory,  
            multiFunctionNodeFactory,
            rangedInstanceFunctionGroupNodeFactory,
            voidFunctionNodeFactory,
            disposalHandlingNodeFactory,
            initializedInstanceNodeFactory)
    {
        _containerInfo = containerInfoContext.ContainerInfo;
        _functionCycleTracker = functionCycleTracker;
        _currentExecutionPhaseSetter = currentExecutionPhaseSetter;
        _lazyTransientScopeInterfaceNode = lazyTransientScopeInterfaceNode;
        _entryFunctionNodeFactory = entryFunctionNodeFactory;
        _creatContainerFunctionNodeFactory = creatContainerFunctionNodeFactory;
        Namespace = _containerInfo.Namespace;
        FullName = _containerInfo.FullName;
        
        _lazyScopeManager = lazyScopeManager;
        _lazyDisposalType = new(() => _lazyScopeManager.Value
            .Scopes.Select(s => s.DisposalHandling)
            .Concat(_lazyScopeManager.Value.TransientScopes.Select(ts => ts.DisposalHandling))
            .Prepend(DisposalHandling)
            .Aggregate(DisposalType.None, (agg, next) =>
            {
                if (next.HasSyncDisposables) agg |= DisposalType.Sync;
                if (next.HasAsyncDisposables) agg |= DisposalType.Async;
                return agg;
            }));
        
        TransientScopeDisposalReference = referenceGenerator.Generate("transientScopeDisposal");
        TransientScopeDisposalElement = referenceGenerator.Generate("transientScopeToDispose");
    }

    protected override IScopeManager ScopeManager => _lazyScopeManager.Value;
    protected override IContainerNode ParentContainer => this;
    protected override string ContainerParameterForScope =>
        Constants.ThisKeyword;

    public override void Build(ImmutableStack<INamedTypeSymbol> implementationStack)
    {
        var initializedInstancesFunction = InitializedInstances.Any()
            ? VoidFunctionNodeFactory(
                    InitializedInstances.ToList(),
                    Array.Empty<ITypeSymbol>())
                .Function
                .EnqueueBuildJobTo(ParentContainer.BuildQueue, ImmutableStack<INamedTypeSymbol>.Empty)
            : null;
        
        if (initializedInstancesFunction is {})
            _initializationFunctions.Add(initializedInstancesFunction);

        CreateContainerFunctions = _containerInfo
            .ContainerType
            .InstanceConstructors
            .Select(ic => _creatContainerFunctionNodeFactory(ic, initializedInstancesFunction))
            .ToList();

        TransientScopeInterface.RegisterRange(this);
        base.Build(implementationStack);
        foreach (var (typeSymbol, methodNamePrefix, parameterTypes) in _containerInfo.CreateFunctionData)
        {
            var functionNode = _entryFunctionNodeFactory(
                typeSymbol,
                methodNamePrefix,
                parameterTypes)
                .Function;
            _rootFunctions.Add(functionNode);
            BuildQueue.Enqueue(new(functionNode, implementationStack));
        }

        var asyncCallNodes = new List<IAsyncFunctionCallNode>();
        while (BuildQueue.Any() && BuildQueue.Dequeue() is { } buildJob)
        {
            buildJob.Node.Build(buildJob.PreviousImplementations);
            if (buildJob.Node is IAsyncFunctionCallNode call)
                asyncCallNodes.Add(call);
        }

        _currentExecutionPhaseSetter.Value = ExecutionPhase.ResolutionValidation;
        
        while (AsyncCheckQueue.Any() && AsyncCheckQueue.Dequeue() is { } function)
            function.CheckSynchronicity();
        
        foreach (var call in asyncCallNodes)
            call.AdjustToCurrentCalledFunctionSynchronicity();
        
        _functionCycleTracker.DetectCycle(this);
        
        CycleDetectionAndReorderingOfInitializedInstances();
        foreach (var scope in Scopes)
            scope.CycleDetectionAndReorderingOfInitializedInstances();
        foreach (var transientScope in TransientScopes)
            transientScope.CycleDetectionAndReorderingOfInitializedInstances();
        
        foreach (var delegateBaseNode in _delegateBaseNodes)
            delegateBaseNode.CheckSynchronicity();
    }

    public override string? ContainerReference => null;

    public override IFunctionCallNode BuildContainerInstanceCall(INamedTypeSymbol type, IFunctionNode callingFunction) => 
        BuildContainerInstanceCall(null, type, callingFunction);

    public override IFunctionCallNode BuildTransientScopeInstanceCall(INamedTypeSymbol type, IFunctionNode callingFunction) => 
        TransientScopeInterface.BuildTransientScopeInstanceCall($"({Constants.ThisKeyword} as {TransientScopeInterface.FullName})", type, callingFunction);
}