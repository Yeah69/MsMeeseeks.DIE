using MsMeeseeks.DIE.MsContainer;
using MsMeeseeks.DIE.Nodes.Functions;

namespace MsMeeseeks.DIE.Nodes.Roots;

internal interface IRangedInstanceInterfaceFunctionNodeRoot
{
    IRangedInstanceInterfaceFunctionNode Function { get; }
}

internal sealed class RangedInstanceInterfaceFunctionNodeRoot : IRangedInstanceInterfaceFunctionNodeRoot, IScopeRoot
{
    public RangedInstanceInterfaceFunctionNodeRoot(IRangedInstanceInterfaceFunctionNode function)
    {
        Function = function;
    }
    
    public IRangedInstanceInterfaceFunctionNode Function { get; }
}