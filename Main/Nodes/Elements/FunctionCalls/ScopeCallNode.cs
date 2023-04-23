using System.Collections.Generic;
using MsMeeseeks.DIE.Configuration;
using MsMeeseeks.DIE.Nodes.Functions;
using MsMeeseeks.DIE.Nodes.Ranges;

namespace MsMeeseeks.DIE.Nodes.Elements.FunctionCalls;

internal interface IScopeCallNode : IFunctionCallNode
{
    string ContainerParameter { get; }
    string TransientScopeInterfaceParameter { get; }
    string ScopeFullName { get; }
    string ScopeReference { get; }
    DisposalType DisposalType { get; }
    string? DisposableCollectionReference { get; }
    IFunctionCallNode? Initialization { get; }
}

internal partial class ScopeCallNode : FunctionCallNode, IScopeCallNode
{
    private readonly IScopeNode _scope;
    private readonly IRangeNode _callingRange;

    public ScopeCallNode(
        (string ContainerParamter, string TransientScopeInterfaceParameter) stringParams,
        IScopeNode scope,
        IRangeNode callingRange,
        IFunctionNode calledFunction, 
        IReadOnlyList<(IParameterNode, IParameterNode)> parameters, 
        IFunctionCallNode? initialization,
        
        IReferenceGenerator referenceGenerator) 
        : base(null, calledFunction, parameters, referenceGenerator)
    {
        _scope = scope;
        _callingRange = callingRange;
        ContainerParameter = stringParams.ContainerParamter;
        TransientScopeInterfaceParameter = stringParams.TransientScopeInterfaceParameter;
        Initialization = initialization;
        ScopeFullName = scope.FullName;
        ScopeReference = referenceGenerator.Generate("scopeRoot");
        callingRange.DisposalHandling.RegisterSyncDisposal();
    }

    public override string OwnerReference => ScopeReference;

    public string ContainerParameter { get; }
    public string TransientScopeInterfaceParameter { get; }
    public IFunctionCallNode? Initialization { get; }
    public string ScopeFullName { get; }
    public string ScopeReference { get; }
    public DisposalType DisposalType => _scope.DisposalType;

    public string? DisposableCollectionReference => DisposalType switch
    {
        DisposalType.Async | DisposalType.Sync or DisposalType.Async => _callingRange.DisposalHandling.AsyncCollectionReference,
        DisposalType.Sync => _callingRange.DisposalHandling.SyncCollectionReference,
        _ => null
    };
}