using System;
using MsMeeseeks.DIE.Extensions;
using MsMeeseeks.DIE.MsContainer;
using MsMeeseeks.DIE.Nodes.Ranges;

namespace MsMeeseeks.DIE.Logging;

internal interface IContainerLevelLogMessageEnhancer
{
    string Enhance(string message);
}

internal class ContainerLevelLogMessageEnhancer : IContainerLevelLogMessageEnhancer, IContainerInstance
{
    private readonly ICurrentExecutionPhase _currentExecutionPhase;
    private readonly Lazy<string> _containerPart;

    internal ContainerLevelLogMessageEnhancer(
        Lazy<IContainerNode> parentContainer,
        ICurrentExecutionPhase currentExecutionPhase)
    {
        _currentExecutionPhase = currentExecutionPhase;
        _containerPart = parentContainer.Select(c => $"[C:{c.Name}]");
    }

    public string Enhance(string message) => 
        $"[{Constants.DieAbbreviation}][P:{_currentExecutionPhase.Value.ToString()}]{_containerPart.Value}{message}";
}