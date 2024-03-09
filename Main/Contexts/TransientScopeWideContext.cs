using MsMeeseeks.DIE.Configuration;
using MsMeeseeks.DIE.MsContainer;
using MsMeeseeks.DIE.Nodes.Ranges;

namespace MsMeeseeks.DIE.Contexts;

internal interface ITransientScopeWideContext
{
    IRangeNode Range { get; }
    ICheckTypeProperties CheckTypeProperties { get; }
}

internal sealed class TransientScopeWideContext : ITransientScopeWideContext, ITransientScopeInstance
{
    public IRangeNode Range { get; }
    public ICheckTypeProperties CheckTypeProperties { get; }

    internal TransientScopeWideContext(
        IRangeNode range,
        ICheckTypeProperties checkTypeProperties)
    {
        Range = range;
        CheckTypeProperties = checkTypeProperties;
    }
}