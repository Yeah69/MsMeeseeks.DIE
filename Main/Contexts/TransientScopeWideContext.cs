using MsMeeseeks.DIE.Configuration;
using MsMeeseeks.DIE.MsContainer;
using MsMeeseeks.DIE.Nodes.Ranges;

namespace MsMeeseeks.DIE.Contexts;

internal interface ITransientScopeWideContext
{
    IRangeNode Range { get; }
    IUserDefinedElements UserDefinedElements { get; }
    ICheckTypeProperties CheckTypeProperties { get; }
}

internal class TransientScopeWideContext : ITransientScopeWideContext, ITransientScopeInstance
{
    public IRangeNode Range { get; }
    public IUserDefinedElements UserDefinedElements { get; }
    public ICheckTypeProperties CheckTypeProperties { get; }

    internal TransientScopeWideContext(
        IRangeNode range,
        IUserDefinedElements userDefinedElements,
        ICheckTypeProperties checkTypeProperties)
    {
        Range = range;
        UserDefinedElements = userDefinedElements;
        CheckTypeProperties = checkTypeProperties;
    }
}