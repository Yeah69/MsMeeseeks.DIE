using MsMeeseeks.DIE.MsContainer;
using MsMeeseeks.DIE.Nodes.Functions;

namespace MsMeeseeks.DIE.Nodes.Roots;

internal interface ICreateScopeFunctionNodeRoot
{
    ICreateScopeFunctionNode Function { get; }
}

internal sealed class CreateScopeFunctionNodeRoot : ICreateScopeFunctionNodeRoot, IScopeRoot
{
    public CreateScopeFunctionNodeRoot(ICreateScopeFunctionNode function)
    {
        Function = function;
    }
    
    public ICreateScopeFunctionNode Function { get; }
}