using System.Collections.Generic;
using MsMeeseeks.DIE.Nodes.Functions;

namespace MsMeeseeks.DIE.Nodes.Elements.FunctionCalls;

internal interface IPlainFunctionCallNode : IFunctionCallNode
{
}

internal partial class PlainFunctionCallNode : FunctionCallNode, IPlainFunctionCallNode
{
    public PlainFunctionCallNode(
        string? ownerReference,
        IFunctionNode calledFunction,
        IReadOnlyList<(IParameterNode, IParameterNode)> parameters,
        
        IReferenceGenerator referenceGenerator)
        : base(ownerReference, calledFunction, parameters, referenceGenerator)
    {
    }
}