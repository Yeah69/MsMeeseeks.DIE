using MsMeeseeks.DIE.MsContainer;
using MsMeeseeks.DIE.Nodes.Functions;

namespace MsMeeseeks.DIE.Nodes.Roots;

internal interface ICreateFunctionNodeRoot
{
    ICreateFunctionNode Function { get; }
}

internal sealed class CreateFunctionNodeRoot : ICreateFunctionNodeRoot, IScopeRoot
{
    internal CreateFunctionNodeRoot(
        ICreateFunctionNode function)
    {
        Function = function;
    }
    
    public ICreateFunctionNode Function { get; }
}