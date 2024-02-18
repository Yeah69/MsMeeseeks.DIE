using MsMeeseeks.DIE.MsContainer;
using MsMeeseeks.DIE.Nodes.Functions;

namespace MsMeeseeks.DIE.Nodes.Roots;

internal interface IMultiKeyValueMultiFunctionNodeRoot
{
    IMultiKeyValueMultiFunctionNode Function { get; }
}

internal sealed class MultiKeyValueMultiFunctionNodeRoot : IMultiKeyValueMultiFunctionNodeRoot, IScopeRoot
{
    public MultiKeyValueMultiFunctionNodeRoot(IMultiKeyValueMultiFunctionNode function)
    {
        Function = function;
    }
    
    public IMultiKeyValueMultiFunctionNode Function { get; }
}