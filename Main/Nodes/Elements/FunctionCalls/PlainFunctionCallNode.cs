using MsMeeseeks.DIE.Nodes.Functions;

namespace MsMeeseeks.DIE.Nodes.Elements.FunctionCalls;

internal interface IPlainFunctionCallNode : IFunctionCallNode;

internal sealed partial class PlainFunctionCallNode : FunctionCallNode, IPlainFunctionCallNode
{
    public PlainFunctionCallNode(
        string? ownerReference,
        IFunctionNode calledFunction,
        ITypeSymbol callSideType,
        IReadOnlyList<(IParameterNode, IParameterNode)> parameters,
        IReadOnlyList<ITypeSymbol> typeParameters,
        
        IReferenceGenerator referenceGenerator)
        : base(ownerReference, calledFunction, callSideType, parameters, typeParameters, referenceGenerator)
    {
    }
}