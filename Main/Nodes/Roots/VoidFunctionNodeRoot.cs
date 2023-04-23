using MsMeeseeks.DIE.MsContainer;
using MsMeeseeks.DIE.Nodes.Functions;

namespace MsMeeseeks.DIE.Nodes.Roots;

internal interface IVoidFunctionNodeRoot
{
    IVoidFunctionNode Function { get; }
}

internal class VoidFunctionNodeRoot : IVoidFunctionNodeRoot, IScopeRoot
{
    public VoidFunctionNodeRoot(IVoidFunctionNode function)
    {
        Function = function;
    }
    
    public IVoidFunctionNode Function { get; }
}