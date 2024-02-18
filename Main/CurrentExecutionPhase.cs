using MsMeeseeks.DIE.MsContainer;

namespace MsMeeseeks.DIE;

internal interface ICurrentExecutionPhase
{
    ExecutionPhase Value { get; }
}

internal interface ICurrentExecutionPhaseSetter
{
    ExecutionPhase Value { set; }
}

internal sealed class CurrentExecutionPhase : ICurrentExecutionPhase, ICurrentExecutionPhaseSetter, IContainerInstance
{
    public ExecutionPhase Value { get; set; } = ExecutionPhase.ContainerValidation;
}