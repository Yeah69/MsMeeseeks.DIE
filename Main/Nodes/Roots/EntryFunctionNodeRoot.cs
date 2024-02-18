using MsMeeseeks.DIE.MsContainer;
using MsMeeseeks.DIE.Nodes.Functions;

namespace MsMeeseeks.DIE.Nodes.Roots;

internal interface IEntryFunctionNodeRoot
{
    IEntryFunctionNode Function { get; }
}

internal sealed class EntryFunctionNodeRoot : IEntryFunctionNodeRoot, IScopeRoot
{
    public EntryFunctionNodeRoot(IEntryFunctionNode function)
    {
        Function = function;
    }
    
    public IEntryFunctionNode Function { get; }
}