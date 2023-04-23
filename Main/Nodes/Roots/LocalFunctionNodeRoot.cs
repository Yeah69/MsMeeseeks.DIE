using MsMeeseeks.DIE.MsContainer;
using MsMeeseeks.DIE.Nodes.Functions;

namespace MsMeeseeks.DIE.Nodes.Roots;

internal interface ILocalFunctionNodeRoot
{
    ILocalFunctionNode Function { get; }
}

internal class LocalFunctionNodeRoot : ILocalFunctionNodeRoot, IScopeRoot
{
    public LocalFunctionNodeRoot(ILocalFunctionNode function)
    {
        Function = function;
    }
    
    public ILocalFunctionNode Function { get; }
}