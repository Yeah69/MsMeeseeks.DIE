using System.Threading.Tasks;

namespace MsMeeseeks.DIE.MsContainer;

internal interface IContainerInstance;
internal interface ITransientScopeInstance;
internal interface IScopeInstance;
internal interface ITransientScopeRoot;
internal interface IScopeRoot;
internal interface ITransient;
internal interface ISyncTransient;
internal interface IAsyncTransient;
// ReSharper disable once UnusedTypeParameter
internal interface IDecorator<T>;
// ReSharper disable once UnusedTypeParameter
internal interface IComposite<T>;
internal interface IInitializer
{
    void Initialize();
}
internal interface ITaskInitializer
{
    Task InitializeAsync();
}