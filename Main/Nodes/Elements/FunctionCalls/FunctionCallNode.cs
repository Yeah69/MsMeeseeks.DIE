using MsMeeseeks.DIE.Nodes.Functions;
using MsMeeseeks.DIE.Visitors;
using MrMeeseeks.SourceGeneratorUtility.Extensions;

namespace MsMeeseeks.DIE.Nodes.Elements.FunctionCalls;

internal interface IFunctionCallNode : IElementNode, IAwaitableNode
{
    string? OwnerReference { get; }
    string FunctionName { get; }
    IReadOnlyList<(IParameterNode, IParameterNode)> Parameters { get; }
    (IElementNode Calling, IElementNode Called)? SubDisposalParameter { get; }
    (IElementNode Calling, IElementNode Called)? TransientScopeDisposalParameter { get; }
    IReadOnlyList<ITypeSymbol> TypeParameters { get; }
    IFunctionNode CalledFunction { get; }
}

internal abstract class FunctionCallNode : IFunctionCallNode
{
    protected FunctionCallNode(
        string? ownerReference,
        ITypeSymbol callSideType,
        IReadOnlyList<(IParameterNode, IParameterNode)> parameters,
        IReadOnlyList<ITypeSymbol> typeParameters,
        IElementNode? callingSubDisposal,
        IElementNode callingTransientScopeDisposal,
        
        IFunctionNode calledFunction,
        IReferenceGenerator referenceGenerator)
    {
        CalledFunction = calledFunction;
        OwnerReference = ownerReference;
        Parameters = parameters;
        TypeParameters = typeParameters;
        FunctionName = calledFunction.Name;
        TypeFullName = callSideType.FullName();
        Reference = referenceGenerator.Generate("functionCallResult");
        SubDisposalParameter = callingSubDisposal is null || !calledFunction.IsSubDisposalAsParameter
            ? null 
            : (callingSubDisposal, calledFunction.SubDisposalNode);
        TransientScopeDisposalParameter = !calledFunction.IsTransientScopeDisposalAsParameter
            ? null
            : (callingTransientScopeDisposal, calledFunction.TransientScopeDisposalNode);
    }

    public virtual void Build(PassedContext passedContext) { }

    public abstract void Accept(INodeVisitor nodeVisitor);

    public string TypeFullName { get; }
    public string Reference { get; }
    public string FunctionName { get; }
    public virtual string? OwnerReference { get; }
    public IReadOnlyList<(IParameterNode, IParameterNode)> Parameters { get; }
    public (IElementNode Calling, IElementNode Called)? SubDisposalParameter { get; }
    public (IElementNode Calling, IElementNode Called)? TransientScopeDisposalParameter { get; }
    public IReadOnlyList<ITypeSymbol> TypeParameters { get; }
    public IFunctionNode CalledFunction { get; }

    public bool Awaited => CalledFunction.SynchronicityDecision is not SynchronicityDecision.Sync;
}