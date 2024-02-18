using MsMeeseeks.DIE.MsContainer;
using MsMeeseeks.DIE.Nodes.Functions;

namespace MsMeeseeks.DIE.Nodes.Roots;

internal interface IMultiKeyValueFunctionNodeRoot
{
    IMultiKeyValueFunctionNode Function { get; }
}

internal sealed class MultiKeyValueFunctionNodeRoot : IMultiKeyValueFunctionNodeRoot, IScopeRoot
{
    public MultiKeyValueFunctionNodeRoot(IMultiKeyValueFunctionNode function)
    {
        Function = function;
    }
    
    public IMultiKeyValueFunctionNode Function { get; }
}