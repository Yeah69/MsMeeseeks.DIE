using MsMeeseeks.DIE.MsContainer;
using MsMeeseeks.DIE.Nodes.Functions;

namespace MsMeeseeks.DIE.Nodes.Roots;

internal interface ICreateTransientScopeFunctionNodeRoot
{
    ICreateTransientScopeFunctionNode Function { get; }
}

internal sealed class CreateTransientScopeFunctionNodeRoot : ICreateTransientScopeFunctionNodeRoot, IScopeRoot
{
    public CreateTransientScopeFunctionNodeRoot(ICreateTransientScopeFunctionNode function)
    {
        Function = function;
    }
    
    public ICreateTransientScopeFunctionNode Function { get; }
}