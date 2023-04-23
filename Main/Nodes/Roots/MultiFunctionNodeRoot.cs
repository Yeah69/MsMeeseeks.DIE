using MsMeeseeks.DIE.MsContainer;
using MsMeeseeks.DIE.Nodes.Functions;

namespace MsMeeseeks.DIE.Nodes.Roots;

internal interface IMultiFunctionNodeRoot
{
    IMultiFunctionNode Function { get; }
}

internal class MultiFunctionNodeRoot : IMultiFunctionNodeRoot, IScopeRoot
{
    public MultiFunctionNodeRoot(IMultiFunctionNode function)
    {
        Function = function;
    }
    
    public IMultiFunctionNode Function { get; }
}