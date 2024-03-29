using MsMeeseeks.DIE.MsContainer;
using MsMeeseeks.DIE.Nodes.Ranges;

namespace MsMeeseeks.DIE.Logging;

internal interface IRangeLevelLogMessageEnhancer
{
    string Enhance(string message);
}

internal sealed class RangeLevelLogMessageEnhancer : IRangeLevelLogMessageEnhancer, ITransientScopeInstance
{
    private readonly IContainerLevelLogMessageEnhancer _parentEnhancer;
    private readonly Lazy<string> _prefix;

    internal RangeLevelLogMessageEnhancer(
        Lazy<IContainerNode> parentContainer,
        Lazy<IRangeNode> parentRange,
        IContainerLevelLogMessageEnhancer parentEnhancer)
    {
        _parentEnhancer = parentEnhancer;
        _prefix = new Lazy<string>(() => parentContainer.Value == parentRange.Value
            ? ""
            : $"[R:{parentRange.Value.Name}]");
    }
    
    public string Enhance(string message) => 
        _parentEnhancer.Enhance($"{_prefix.Value}{message}");
}