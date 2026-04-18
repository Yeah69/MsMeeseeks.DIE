#nullable enable
namespace MsMeeseeks.DIE
{
    internal interface IDisposableRange_0_4
    {
        internal global::System.Object[] TransientScopes_0_6 { get; }
        internal global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::System.Object>> Disposables_0_7 { get; }
        internal global::System.Collections.Concurrent.ConcurrentBag<global::System.IDisposable> UserDefinedSyncDisposables_0_8 { get; }

        internal bool ShouldBeDisposed_0_5(global::System.Object disposable);
    }

    internal static class DisposeUtility_0_0
    {
        internal static void Dispose_0_1(MsMeeseeks.DIE.IDisposableRange_0_4 disposableRange_0_11)
        {
            if (MsMeeseeks.DIE.DisposeUtility_0_0.AggregateExceptionRoutine_0_3(Inner())is { } aggregateException)
                throw aggregateException;
            return;
            System.Collections.Generic.IEnumerable<System.Exception> Inner()
            {
                foreach (var transientScope in disposableRange_0_11.TransientScopes_0_6)
                {
                    if (transientScope is global::System.IDisposable disposable && MsMeeseeks.DIE.DisposeUtility_0_0.DisposeSingular_0_2(disposable)is global::System.Exception exception)
                        yield return exception;
                }

                for (var i = disposableRange_0_11.Disposables_0_7.Count - 1; i >= 0; i--)
                {
                    foreach (var exception in MsMeeseeks.DIE.DisposeUtility_0_0.DisposeChunk_0_9((MsMeeseeks.DIE.IDisposableRange_0_4)disposableRange_0_11, disposableRange_0_11.Disposables_0_7[i]))
                    {
                        yield return exception;
                    }
                }

                foreach (var disposable in disposableRange_0_11.UserDefinedSyncDisposables_0_8)
                {
                    if (MsMeeseeks.DIE.DisposeUtility_0_0.DisposeSingular_0_2(disposable)is global::System.Exception exception)
                    {
                        yield return exception;
                    }
                }
            }
        }

        private static global::System.Collections.Generic.IEnumerable<global::System.Exception> DisposeChunk_0_9(MsMeeseeks.DIE.IDisposableRange_0_4 disposableElement_0_12, global::System.Collections.Generic.List<global::System.Object> disposables)
        {
            for (var i = disposables.Count - 1; i >= 0; i--)
            {
                if (disposableElement_0_12.ShouldBeDisposed_0_5(disposables[i]) && disposables[i] is global::System.IDisposable disposable_0_13 && DisposeSingular_0_2(disposable_0_13)is System.Exception exception_0_14)
                {
                    yield return exception_0_14;
                }
            }
        }

        internal static global::System.Exception DisposeExceptionHandling_0_10(MsMeeseeks.DIE.IDisposableRange_0_4 disposableElement_0_15, global::System.Exception exception, global::System.Collections.Generic.List<global::System.Object> subDisposal, global::System.Collections.Generic.List<global::System.Object>? transientScopeDisposal = null)
        {
            if (MsMeeseeks.DIE.DisposeUtility_0_0.AggregateExceptionRoutine_0_3(Inner())is { } aggregateException)
                return aggregateException;
            else
                return exception;
            System.Collections.Generic.IEnumerable<System.Exception> Inner()
            {
                yield return exception;
                if (transientScopeDisposal is not null)
                {
                    foreach (var transientScope in transientScopeDisposal)
                    {
                        if (transientScope is global::System.IDisposable disposable && MsMeeseeks.DIE.DisposeUtility_0_0.DisposeSingular_0_2(disposable)is global::System.Exception transientException)
                        {
                            yield return transientException;
                        }
                    }
                }

                foreach (var subException in MsMeeseeks.DIE.DisposeUtility_0_0.DisposeChunk_0_9(disposableElement_0_15, subDisposal))
                {
                    yield return subException;
                }
            }
        }

        private static global::System.Exception? DisposeSingular_0_2(global::System.IDisposable disposable)
        {
            try
            {
                disposable.Dispose();
            }
            catch (global::System.Exception e)
            {
                return e;
            }

            return null;
        }

        private static global::System.AggregateException? AggregateExceptionRoutine_0_3(global::System.Collections.Generic.IEnumerable<global::System.Exception> exceptions)
        {
            global::System.AggregateException aggregateException = new global::System.AggregateException(exceptions);
            if (aggregateException.InnerExceptions.Count > 0)
                return aggregateException;
            return null;
        }
    }
}
#nullable disable
