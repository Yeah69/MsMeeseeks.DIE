using System.Collections.Generic;
using MsMeeseeks.DIE.Configuration;
using MsMeeseeks.DIE.Nodes.Functions;
using MsMeeseeks.DIE.Nodes.Ranges;

namespace MsMeeseeks.DIE.Nodes.Elements.FunctionCalls;

internal interface ITransientScopeCallNode : IFunctionCallNode
{
    string ContainerParameter { get; }
    string? ContainerReference { get; }
    string TransientScopeFullName { get; }
    string TransientScopeReference { get; }
    DisposalType DisposalType { get; }
    string TransientScopeDisposalReference { get; }
    IFunctionCallNode? Initialization { get; }
}

internal partial class TransientScopeCallNode : FunctionCallNode, ITransientScopeCallNode
{
    private readonly ITransientScopeNode _scope;

    public TransientScopeCallNode(
        string containerParameter, 
        ITransientScopeNode scope,
        IContainerNode parentContainer,
        IRangeNode callingRange,
        IFunctionNode calledFunction,
        IReadOnlyList<(IParameterNode, IParameterNode)> parameters,
        IFunctionCallNode? initialization,
        
        IReferenceGenerator referenceGenerator) 
        : base(null, calledFunction, parameters, referenceGenerator)
    {
        _scope = scope;
        ContainerParameter = containerParameter;
        Initialization = initialization;
        TransientScopeFullName = scope.FullName;
        TransientScopeReference = referenceGenerator.Generate("transientScopeRoot");
        ContainerReference = callingRange.ContainerReference;
        TransientScopeDisposalReference = parentContainer.TransientScopeDisposalReference;
    }

    public override string OwnerReference => TransientScopeReference;

    public string ContainerParameter { get; }
    public string? ContainerReference { get; }
    public string TransientScopeFullName { get; }
    public string TransientScopeReference { get; }
    public DisposalType DisposalType => _scope.DisposalType;
    public string TransientScopeDisposalReference { get; }
    public IFunctionCallNode? Initialization { get; }
}