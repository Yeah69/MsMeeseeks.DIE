using MsMeeseeks.DIE.MsContainer;
using MsMeeseeks.DIE.Nodes.Ranges;

namespace MsMeeseeks.DIE.Nodes.Roots;

internal interface IScopeNodeRoot
{
    IScopeNode Scope { get; }
}

internal class ScopeNodeRoot : IScopeNodeRoot, ITransientScopeRoot
{
    public IScopeNode Scope { get; }

    public ScopeNodeRoot(
        IScopeNode scope)
    {
        Scope = scope;
    }
}