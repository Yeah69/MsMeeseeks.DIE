using MsMeeseeks.DIE.MsContainer;
using MsMeeseeks.DIE.Nodes.Functions;

namespace MsMeeseeks.DIE.Nodes.Roots;

internal interface IRangedInstanceFunctionNodeRoot
{
    IRangedInstanceFunctionNode Function { get; }
}

internal class RangedInstanceFunctionNodeRoot : IRangedInstanceFunctionNodeRoot, IScopeRoot
{
    public RangedInstanceFunctionNodeRoot(IRangedInstanceFunctionNode function)
    {
        Function = function;
    }
    
    public IRangedInstanceFunctionNode Function { get; }
}