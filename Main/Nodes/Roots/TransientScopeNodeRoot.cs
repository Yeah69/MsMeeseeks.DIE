using MsMeeseeks.DIE.MsContainer;
using MsMeeseeks.DIE.Nodes.Ranges;

namespace MsMeeseeks.DIE.Nodes.Roots;

internal interface ITransientScopeNodeRoot
{
    ITransientScopeNode TransientScope { get; }
}

internal class TransientScopeNodeRoot : ITransientScopeNodeRoot, ITransientScopeRoot
{
    public ITransientScopeNode TransientScope { get; }

    public TransientScopeNodeRoot(
        ITransientScopeNode transientScope)
    {
        TransientScope = transientScope;
    }
}