

using MsMeeseeks.DIE.MsContainer;

namespace MsMeeseeks.DIE.Contexts;

internal interface IContainerInfoContext
{
    IContainerInfo ContainerInfo { get; }
}

internal sealed class ContainerInfoContext : IContainerInfoContext, IContainerInstance
{
    public ContainerInfoContext(
        IContainerInfo containerInfo)
    {
        ContainerInfo = containerInfo;
    }

    public IContainerInfo ContainerInfo { get; }
}