#nullable enable
namespace MsMeeseeks.DIE.MsContainer
{
    sealed partial class ExecuteLevelContainer : MsMeeseeks.DIE.IDisposableRange_0_4, global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer.ITransientScope_1349_13
    {
        public static global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer DIE_CreateContainer(global::Microsoft.CodeAnalysis.GeneratorExecutionContext context)
        {
            global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer executeLevelContainer_1349_12 = new global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer(context: context);
            return executeLevelContainer_1349_12;
        }

        internal global::MsMeeseeks.DIE.IExecute Create()
        {
            global::System.Collections.Generic.List<global::System.Object> transientScopeSubDisposal_1350_1 = new global::System.Collections.Generic.List<global::System.Object>(0);
            global::System.Collections.Generic.List<global::System.Object> subDisposal_1350_0 = new global::System.Collections.Generic.List<global::System.Object>(0);
            if (Disposed_1349_2)
                throw new System.ObjectDisposedException("global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer", $"[DIE] This scope \"global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer\" is already disposed, so it can't create a \"global::MsMeeseeks.DIE.IExecute\" instance anymore.");
            try
            {
                global::System.Threading.Interlocked.Increment(ref resolutionCounter_1349_5);
                global::MsMeeseeks.DIE.IExecute functionCallResult_1351_3 = (global::MsMeeseeks.DIE.IExecute)CreateIExecute_1351_2(list_1351_0: subDisposal_1350_0, list_1351_1: transientScopeSubDisposal_1350_1);
                if (Disposed_1349_2)
                    throw new System.ObjectDisposedException("global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer", $"[DIE] This scope \"global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer\" is already disposed, so it can't create a \"global::MsMeeseeks.DIE.IExecute\" instance anymore.");
                _disposal_1349_4.Add(subDisposal_1350_0);
                transientScopeDisposal_1349_10.AddRange(transientScopeSubDisposal_1350_1);
                return functionCallResult_1351_3;
            }
            catch (global::System.Exception exception)
            {
                throw MsMeeseeks.DIE.DisposeUtility_0_0.DisposeExceptionHandling_0_10((MsMeeseeks.DIE.IDisposableRange_0_4)this, exception, subDisposal_1350_0, transientScopeSubDisposal_1350_1);
            }
            finally
            {
                global::System.Threading.Interlocked.Decrement(ref resolutionCounter_1349_5);
            }
        }

        private global::System.Int32 resolutionCounter_1349_5;
        private global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::System.Object>> _disposal_1349_4 = new global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::System.Object>>();
        private global::MsMeeseeks.DIE.IExecute CreateIExecute_1351_2(global::System.Collections.Generic.List<global::System.Object> list_1351_0, global::System.Collections.Generic.List<global::System.Object> list_1351_1)
        {
            global::Microsoft.CodeAnalysis.GeneratorExecutionContext generatorExecutionContext_1351_5 = (global::Microsoft.CodeAnalysis.GeneratorExecutionContext)DIE_Factory_GeneratorExecutionContext;
            global::MsMeeseeks.DIE.WellKnownTypesMiscellaneous functionCallResult_1356_3 = (global::MsMeeseeks.DIE.WellKnownTypesMiscellaneous)GetContainerInstanceWellKnownTypesMiscellaneous_1356_2();
            global::MsMeeseeks.DIE.Utility.IRangeUtility rangeUtility_1351_6 = (global::MsMeeseeks.DIE.Utility.IRangeUtility)new global::MsMeeseeks.DIE.Utility.RangeUtility(wellKnownTypesMiscellaneous: functionCallResult_1356_3);
            global::MsMeeseeks.DIE.CodeGeneration.RequiredKeywordUtility functionCallResult_1352_3 = (global::MsMeeseeks.DIE.CodeGeneration.RequiredKeywordUtility)GetContainerInstanceRequiredKeywordUtility_1352_2();
            global::MsMeeseeks.DIE.CodeGeneration.DisposeUtility functionCallResult_1353_3 = (global::MsMeeseeks.DIE.CodeGeneration.DisposeUtility)GetContainerInstanceDisposeUtility_1353_2();
            global::System.Func<global::Microsoft.CodeAnalysis.INamedTypeSymbol, global::MsMeeseeks.DIE.ContainerInfo> func_1351_7 = LocalContainerInfo_1354_3;
            global::System.Func<global::MsMeeseeks.DIE.ContainerInfo, global::MsMeeseeks.DIE.IExecuteContainerContext> func_1351_8 = LocalIExecuteContainerContext_1355_3;
            global::MsMeeseeks.DIE.IExecute executeImpl_1351_4 = (global::MsMeeseeks.DIE.IExecute)new global::MsMeeseeks.DIE.ExecuteImpl(context: generatorExecutionContext_1351_5, rangeUtility: rangeUtility_1351_6, requiredKeywordUtility: functionCallResult_1352_3, disposeUtility: functionCallResult_1353_3, containerInfoFactory: func_1351_7, executeContainerContextFactory: func_1351_8);
            return executeImpl_1351_4;
            global::MsMeeseeks.DIE.ContainerInfo LocalContainerInfo_1354_3(global::Microsoft.CodeAnalysis.INamedTypeSymbol iNamedTypeSymbol_1354_0)
            {
                global::System.Collections.Generic.List<global::System.Object> transientScopeSubDisposal_1354_2 = new global::System.Collections.Generic.List<global::System.Object>(0);
                global::System.Collections.Generic.List<global::System.Object> subDisposal_1354_1 = new global::System.Collections.Generic.List<global::System.Object>(0);
                if (Disposed_1349_2)
                    throw new System.ObjectDisposedException("global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer", $"[DIE] This scope \"global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer\" is already disposed, so it can't create a \"global::MsMeeseeks.DIE.ContainerInfo\" instance anymore.");
                try
                {
                    global::System.Threading.Interlocked.Increment(ref resolutionCounter_1349_5);
                    global::MsMeeseeks.DIE.ContainerInfo functionCallResult_1357_4 = (global::MsMeeseeks.DIE.ContainerInfo)CreateContainerInfo_1357_3(iNamedTypeSymbol_1357_0: iNamedTypeSymbol_1354_0, list_1357_1: subDisposal_1354_1, list_1357_2: transientScopeSubDisposal_1354_2);
                    if (Disposed_1349_2)
                        throw new System.ObjectDisposedException("global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer", $"[DIE] This scope \"global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer\" is already disposed, so it can't create a \"global::MsMeeseeks.DIE.ContainerInfo\" instance anymore.");
                    _disposal_1349_4.Add(subDisposal_1354_1);
                    transientScopeDisposal_1349_10.AddRange(transientScopeSubDisposal_1354_2);
                    return functionCallResult_1357_4;
                }
                catch (global::System.Exception exception)
                {
                    throw MsMeeseeks.DIE.DisposeUtility_0_0.DisposeExceptionHandling_0_10((MsMeeseeks.DIE.IDisposableRange_0_4)this, exception, subDisposal_1354_1, transientScopeSubDisposal_1354_2);
                }
                finally
                {
                    global::System.Threading.Interlocked.Decrement(ref resolutionCounter_1349_5);
                }
            }

            global::MsMeeseeks.DIE.IExecuteContainerContext LocalIExecuteContainerContext_1355_3(global::MsMeeseeks.DIE.ContainerInfo containerInfo_1355_0)
            {
                global::System.Collections.Generic.List<global::System.Object> transientScopeSubDisposal_1355_2 = new global::System.Collections.Generic.List<global::System.Object>(1);
                global::System.Collections.Generic.List<global::System.Object> subDisposal_1355_1 = new global::System.Collections.Generic.List<global::System.Object>(0);
                if (Disposed_1349_2)
                    throw new System.ObjectDisposedException("global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer", $"[DIE] This scope \"global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer\" is already disposed, so it can't create a \"global::MsMeeseeks.DIE.IExecuteContainerContext\" instance anymore.");
                try
                {
                    global::System.Threading.Interlocked.Increment(ref resolutionCounter_1349_5);
                    global::MsMeeseeks.DIE.IExecuteContainerContext functionCallResult_1358_4 = (global::MsMeeseeks.DIE.IExecuteContainerContext)CreateIExecuteContainerContext_1358_3(containerInfo_1358_0: containerInfo_1355_0, list_1358_1: subDisposal_1355_1, list_1358_2: transientScopeSubDisposal_1355_2);
                    if (Disposed_1349_2)
                        throw new System.ObjectDisposedException("global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer", $"[DIE] This scope \"global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer\" is already disposed, so it can't create a \"global::MsMeeseeks.DIE.IExecuteContainerContext\" instance anymore.");
                    _disposal_1349_4.Add(subDisposal_1355_1);
                    transientScopeDisposal_1349_10.AddRange(transientScopeSubDisposal_1355_2);
                    return functionCallResult_1358_4;
                }
                catch (global::System.Exception exception)
                {
                    throw MsMeeseeks.DIE.DisposeUtility_0_0.DisposeExceptionHandling_0_10((MsMeeseeks.DIE.IDisposableRange_0_4)this, exception, subDisposal_1355_1, transientScopeSubDisposal_1355_2);
                }
                finally
                {
                    global::System.Threading.Interlocked.Decrement(ref resolutionCounter_1349_5);
                }
            }
        }

        private global::MsMeeseeks.DIE.ContainerInfo CreateContainerInfo_1357_3(global::Microsoft.CodeAnalysis.INamedTypeSymbol iNamedTypeSymbol_1357_0, global::System.Collections.Generic.List<global::System.Object> list_1357_1, global::System.Collections.Generic.List<global::System.Object> list_1357_2)
        {
            global::MsMeeseeks.DIE.WellKnownTypesMiscellaneous functionCallResult_1356_5 = (global::MsMeeseeks.DIE.WellKnownTypesMiscellaneous)GetContainerInstanceWellKnownTypesMiscellaneous_1356_2();
            global::MsMeeseeks.DIE.Utility.IRangeUtility rangeUtility_1357_6 = (global::MsMeeseeks.DIE.Utility.IRangeUtility)new global::MsMeeseeks.DIE.Utility.RangeUtility(wellKnownTypesMiscellaneous: functionCallResult_1356_5);
            global::MsMeeseeks.DIE.ContainerInfo containerInfo_1357_5 = new global::MsMeeseeks.DIE.ContainerInfo(containerClass: iNamedTypeSymbol_1357_0, wellKnownTypesMiscellaneous: functionCallResult_1356_5, rangeUtility: rangeUtility_1357_6);
            return containerInfo_1357_5;
        }

        private global::MsMeeseeks.DIE.IExecuteContainerContext CreateIExecuteContainerContext_1358_3(global::MsMeeseeks.DIE.ContainerInfo containerInfo_1358_0, global::System.Collections.Generic.List<global::System.Object> list_1358_1, global::System.Collections.Generic.List<global::System.Object> list_1358_2)
        {
            global::Microsoft.CodeAnalysis.GeneratorExecutionContext generatorExecutionContext_1358_6 = (global::Microsoft.CodeAnalysis.GeneratorExecutionContext)DIE_Factory_GeneratorExecutionContext;
            global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer.DIE_DefaultTransientScope dIE_DefaultTransientScope_1358_5 = new global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer.DIE_DefaultTransientScope(context: generatorExecutionContext_1358_6)
            {
                Container_1362_10 = this
            };
            transientScopeDisposal_1349_10.Add(dIE_DefaultTransientScope_1358_5);
            global::MsMeeseeks.DIE.ExecuteContainerContext functionCallResult_1363_4 = (global::MsMeeseeks.DIE.ExecuteContainerContext)dIE_DefaultTransientScope_1358_5.CreateExecuteContainerContext_1363_3(containerInfo_1363_0: containerInfo_1358_0, list_1363_2: list_1358_2);
            return functionCallResult_1363_4;
        }

        private global::MsMeeseeks.DIE.CodeGeneration.RequiredKeywordUtility? _containerInstanceFieldRequiredKeywordUtility_1349_14;
        private global::System.Threading.SemaphoreSlim? _containerInstanceLock_1349_15 = new global::System.Threading.SemaphoreSlim(1);
        private global::MsMeeseeks.DIE.CodeGeneration.RequiredKeywordUtility GetContainerInstanceRequiredKeywordUtility_1352_2()
        {
            global::System.Collections.Generic.List<global::System.Object> transientScopeSubDisposal_1352_1 = new global::System.Collections.Generic.List<global::System.Object>(0);
            global::System.Collections.Generic.List<global::System.Object> subDisposal_1352_0 = new global::System.Collections.Generic.List<global::System.Object>(0);
            if (!global::System.Object.ReferenceEquals(_containerInstanceFieldRequiredKeywordUtility_1349_14, null))
                return _containerInstanceFieldRequiredKeywordUtility_1349_14;
            this._containerInstanceLock_1349_15?.Wait();
            try
            {
                if (!global::System.Object.ReferenceEquals(_containerInstanceFieldRequiredKeywordUtility_1349_14, null))
                    return _containerInstanceFieldRequiredKeywordUtility_1349_14;
                if (Disposed_1349_2)
                    throw new System.ObjectDisposedException("global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer", $"[DIE] This scope \"global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer\" is already disposed, so it can't create a \"global::MsMeeseeks.DIE.CodeGeneration.RequiredKeywordUtility\" instance anymore.");
                try
                {
                    global::System.Threading.Interlocked.Increment(ref resolutionCounter_1349_5);
                    global::Microsoft.CodeAnalysis.GeneratorExecutionContext generatorExecutionContext_1352_5 = (global::Microsoft.CodeAnalysis.GeneratorExecutionContext)DIE_Factory_GeneratorExecutionContext;
                    global::Microsoft.CodeAnalysis.GeneratorExecutionContext generatorExecutionContext_1352_7 = (global::Microsoft.CodeAnalysis.GeneratorExecutionContext)DIE_Factory_GeneratorExecutionContext;
                    global::MrMeeseeks.SourceGeneratorUtility.ICheckInternalsVisible checkInternalsVisible_1352_6 = (global::MrMeeseeks.SourceGeneratorUtility.ICheckInternalsVisible)new global::MrMeeseeks.SourceGeneratorUtility.CheckInternalsVisible(generatorExecutionContext: generatorExecutionContext_1352_7);
                    global::MsMeeseeks.DIE.CodeGeneration.RequiredKeywordUtility requiredKeywordUtility_1352_4 = new global::MsMeeseeks.DIE.CodeGeneration.RequiredKeywordUtility(generatorExecutionContext: generatorExecutionContext_1352_5, checkInternalsVisible: checkInternalsVisible_1352_6);
                    if (Disposed_1349_2)
                        throw new System.ObjectDisposedException("global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer", $"[DIE] This scope \"global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer\" is already disposed, so it can't create a \"global::MsMeeseeks.DIE.CodeGeneration.RequiredKeywordUtility\" instance anymore.");
                    _disposal_1349_4.Add(subDisposal_1352_0);
                    transientScopeDisposal_1349_10.AddRange(transientScopeSubDisposal_1352_1);
                    _containerInstanceFieldRequiredKeywordUtility_1349_14 = requiredKeywordUtility_1352_4;
                }
                catch (global::System.Exception exception)
                {
                    throw MsMeeseeks.DIE.DisposeUtility_0_0.DisposeExceptionHandling_0_10((MsMeeseeks.DIE.IDisposableRange_0_4)this, exception, subDisposal_1352_0, transientScopeSubDisposal_1352_1);
                }
                finally
                {
                    global::System.Threading.Interlocked.Decrement(ref resolutionCounter_1349_5);
                }
            }
            finally
            {
                this._containerInstanceLock_1349_15?.Release();
            }

            this._containerInstanceLock_1349_15 = null;
            return this._containerInstanceFieldRequiredKeywordUtility_1349_14;
        }

        private global::MsMeeseeks.DIE.CodeGeneration.DisposeUtility? _containerInstanceFieldDisposeUtility_1349_16;
        private global::System.Threading.SemaphoreSlim? _containerInstanceLock_1349_17 = new global::System.Threading.SemaphoreSlim(1);
        private global::MsMeeseeks.DIE.CodeGeneration.DisposeUtility GetContainerInstanceDisposeUtility_1353_2()
        {
            global::System.Collections.Generic.List<global::System.Object> transientScopeSubDisposal_1353_1 = new global::System.Collections.Generic.List<global::System.Object>(0);
            global::System.Collections.Generic.List<global::System.Object> subDisposal_1353_0 = new global::System.Collections.Generic.List<global::System.Object>(0);
            if (!global::System.Object.ReferenceEquals(_containerInstanceFieldDisposeUtility_1349_16, null))
                return _containerInstanceFieldDisposeUtility_1349_16;
            this._containerInstanceLock_1349_17?.Wait();
            try
            {
                if (!global::System.Object.ReferenceEquals(_containerInstanceFieldDisposeUtility_1349_16, null))
                    return _containerInstanceFieldDisposeUtility_1349_16;
                if (Disposed_1349_2)
                    throw new System.ObjectDisposedException("global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer", $"[DIE] This scope \"global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer\" is already disposed, so it can't create a \"global::MsMeeseeks.DIE.CodeGeneration.DisposeUtility\" instance anymore.");
                try
                {
                    global::System.Threading.Interlocked.Increment(ref resolutionCounter_1349_5);
                    global::MsMeeseeks.DIE.ReferenceGenerator functionCallResult_1359_3 = (global::MsMeeseeks.DIE.ReferenceGenerator)GetScopeInstanceReferenceGenerator_1359_2();
                    global::MsMeeseeks.DIE.WellKnownTypes functionCallResult_1360_3 = (global::MsMeeseeks.DIE.WellKnownTypes)GetContainerInstanceWellKnownTypes_1360_2();
                    global::MsMeeseeks.DIE.WellKnownTypesCollections functionCallResult_1361_3 = (global::MsMeeseeks.DIE.WellKnownTypesCollections)GetContainerInstanceWellKnownTypesCollections_1361_2();
                    global::MsMeeseeks.DIE.CodeGeneration.DisposeUtility disposeUtility_1353_4 = new global::MsMeeseeks.DIE.CodeGeneration.DisposeUtility(referenceGenerator: functionCallResult_1359_3, wellKnownTypes: functionCallResult_1360_3, wellKnownTypesCollections: functionCallResult_1361_3);
                    if (Disposed_1349_2)
                        throw new System.ObjectDisposedException("global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer", $"[DIE] This scope \"global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer\" is already disposed, so it can't create a \"global::MsMeeseeks.DIE.CodeGeneration.DisposeUtility\" instance anymore.");
                    _disposal_1349_4.Add(subDisposal_1353_0);
                    transientScopeDisposal_1349_10.AddRange(transientScopeSubDisposal_1353_1);
                    _containerInstanceFieldDisposeUtility_1349_16 = disposeUtility_1353_4;
                }
                catch (global::System.Exception exception)
                {
                    throw MsMeeseeks.DIE.DisposeUtility_0_0.DisposeExceptionHandling_0_10((MsMeeseeks.DIE.IDisposableRange_0_4)this, exception, subDisposal_1353_0, transientScopeSubDisposal_1353_1);
                }
                finally
                {
                    global::System.Threading.Interlocked.Decrement(ref resolutionCounter_1349_5);
                }
            }
            finally
            {
                this._containerInstanceLock_1349_17?.Release();
            }

            this._containerInstanceLock_1349_17 = null;
            return this._containerInstanceFieldDisposeUtility_1349_16;
        }

        private global::MsMeeseeks.DIE.WellKnownTypesMiscellaneous? _containerInstanceFieldWellKnownTypesMiscellaneous_1349_18;
        private global::System.Threading.SemaphoreSlim? _containerInstanceLock_1349_19 = new global::System.Threading.SemaphoreSlim(1);
        private global::MsMeeseeks.DIE.WellKnownTypesMiscellaneous GetContainerInstanceWellKnownTypesMiscellaneous_1356_2()
        {
            global::System.Collections.Generic.List<global::System.Object> transientScopeSubDisposal_1356_1 = new global::System.Collections.Generic.List<global::System.Object>(0);
            global::System.Collections.Generic.List<global::System.Object> subDisposal_1356_0 = new global::System.Collections.Generic.List<global::System.Object>(0);
            if (!global::System.Object.ReferenceEquals(_containerInstanceFieldWellKnownTypesMiscellaneous_1349_18, null))
                return _containerInstanceFieldWellKnownTypesMiscellaneous_1349_18;
            this._containerInstanceLock_1349_19?.Wait();
            try
            {
                if (!global::System.Object.ReferenceEquals(_containerInstanceFieldWellKnownTypesMiscellaneous_1349_18, null))
                    return _containerInstanceFieldWellKnownTypesMiscellaneous_1349_18;
                if (Disposed_1349_2)
                    throw new System.ObjectDisposedException("global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer", $"[DIE] This scope \"global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer\" is already disposed, so it can't create a \"global::MsMeeseeks.DIE.WellKnownTypesMiscellaneous\" instance anymore.");
                try
                {
                    global::System.Threading.Interlocked.Increment(ref resolutionCounter_1349_5);
                    global::MsMeeseeks.DIE.WellKnownTypesMiscellaneous wellKnownTypesMiscellaneous_1356_4 = (global::MsMeeseeks.DIE.WellKnownTypesMiscellaneous)DIE_Factory_WellKnownTypesMiscellaneous();
                    if (Disposed_1349_2)
                        throw new System.ObjectDisposedException("global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer", $"[DIE] This scope \"global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer\" is already disposed, so it can't create a \"global::MsMeeseeks.DIE.WellKnownTypesMiscellaneous\" instance anymore.");
                    _disposal_1349_4.Add(subDisposal_1356_0);
                    transientScopeDisposal_1349_10.AddRange(transientScopeSubDisposal_1356_1);
                    _containerInstanceFieldWellKnownTypesMiscellaneous_1349_18 = wellKnownTypesMiscellaneous_1356_4;
                }
                catch (global::System.Exception exception)
                {
                    throw MsMeeseeks.DIE.DisposeUtility_0_0.DisposeExceptionHandling_0_10((MsMeeseeks.DIE.IDisposableRange_0_4)this, exception, subDisposal_1356_0, transientScopeSubDisposal_1356_1);
                }
                finally
                {
                    global::System.Threading.Interlocked.Decrement(ref resolutionCounter_1349_5);
                }
            }
            finally
            {
                this._containerInstanceLock_1349_19?.Release();
            }

            this._containerInstanceLock_1349_19 = null;
            return this._containerInstanceFieldWellKnownTypesMiscellaneous_1349_18;
        }

        private global::MsMeeseeks.DIE.ReferenceGenerator? _scopeInstanceFieldReferenceGenerator_1349_20;
        private global::System.Threading.SemaphoreSlim? _scopeInstanceLock_1349_21 = new global::System.Threading.SemaphoreSlim(1);
        private global::MsMeeseeks.DIE.ReferenceGenerator GetScopeInstanceReferenceGenerator_1359_2()
        {
            global::System.Collections.Generic.List<global::System.Object> transientScopeSubDisposal_1359_1 = new global::System.Collections.Generic.List<global::System.Object>(0);
            global::System.Collections.Generic.List<global::System.Object> subDisposal_1359_0 = new global::System.Collections.Generic.List<global::System.Object>(0);
            if (!global::System.Object.ReferenceEquals(_scopeInstanceFieldReferenceGenerator_1349_20, null))
                return _scopeInstanceFieldReferenceGenerator_1349_20;
            this._scopeInstanceLock_1349_21?.Wait();
            try
            {
                if (!global::System.Object.ReferenceEquals(_scopeInstanceFieldReferenceGenerator_1349_20, null))
                    return _scopeInstanceFieldReferenceGenerator_1349_20;
                if (Disposed_1349_2)
                    throw new System.ObjectDisposedException("global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer", $"[DIE] This scope \"global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer\" is already disposed, so it can't create a \"global::MsMeeseeks.DIE.ReferenceGenerator\" instance anymore.");
                try
                {
                    global::System.Threading.Interlocked.Increment(ref resolutionCounter_1349_5);
                    global::MsMeeseeks.DIE.ReferenceGeneratorCounter functionCallResult_1364_3 = (global::MsMeeseeks.DIE.ReferenceGeneratorCounter)GetContainerInstanceReferenceGeneratorCounter_1364_2();
                    global::MsMeeseeks.DIE.Logging.LocalDiagLogger functionCallResult_1365_3 = (global::MsMeeseeks.DIE.Logging.LocalDiagLogger)GetScopeInstanceLocalDiagLogger_1365_2();
                    global::MsMeeseeks.DIE.ReferenceGenerator referenceGenerator_1359_4 = new global::MsMeeseeks.DIE.ReferenceGenerator(referenceGeneratorCounter: functionCallResult_1364_3, localDiagLogger: functionCallResult_1365_3);
                    if (Disposed_1349_2)
                        throw new System.ObjectDisposedException("global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer", $"[DIE] This scope \"global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer\" is already disposed, so it can't create a \"global::MsMeeseeks.DIE.ReferenceGenerator\" instance anymore.");
                    _disposal_1349_4.Add(subDisposal_1359_0);
                    transientScopeDisposal_1349_10.AddRange(transientScopeSubDisposal_1359_1);
                    _scopeInstanceFieldReferenceGenerator_1349_20 = referenceGenerator_1359_4;
                }
                catch (global::System.Exception exception)
                {
                    throw MsMeeseeks.DIE.DisposeUtility_0_0.DisposeExceptionHandling_0_10((MsMeeseeks.DIE.IDisposableRange_0_4)this, exception, subDisposal_1359_0, transientScopeSubDisposal_1359_1);
                }
                finally
                {
                    global::System.Threading.Interlocked.Decrement(ref resolutionCounter_1349_5);
                }
            }
            finally
            {
                this._scopeInstanceLock_1349_21?.Release();
            }

            this._scopeInstanceLock_1349_21 = null;
            return this._scopeInstanceFieldReferenceGenerator_1349_20;
        }

        private global::MsMeeseeks.DIE.WellKnownTypes? _containerInstanceFieldWellKnownTypes_1349_22;
        private global::System.Threading.SemaphoreSlim? _containerInstanceLock_1349_23 = new global::System.Threading.SemaphoreSlim(1);
        private global::MsMeeseeks.DIE.WellKnownTypes GetContainerInstanceWellKnownTypes_1360_2()
        {
            global::System.Collections.Generic.List<global::System.Object> transientScopeSubDisposal_1360_1 = new global::System.Collections.Generic.List<global::System.Object>(0);
            global::System.Collections.Generic.List<global::System.Object> subDisposal_1360_0 = new global::System.Collections.Generic.List<global::System.Object>(0);
            if (!global::System.Object.ReferenceEquals(_containerInstanceFieldWellKnownTypes_1349_22, null))
                return _containerInstanceFieldWellKnownTypes_1349_22;
            this._containerInstanceLock_1349_23?.Wait();
            try
            {
                if (!global::System.Object.ReferenceEquals(_containerInstanceFieldWellKnownTypes_1349_22, null))
                    return _containerInstanceFieldWellKnownTypes_1349_22;
                if (Disposed_1349_2)
                    throw new System.ObjectDisposedException("global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer", $"[DIE] This scope \"global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer\" is already disposed, so it can't create a \"global::MsMeeseeks.DIE.WellKnownTypes\" instance anymore.");
                try
                {
                    global::System.Threading.Interlocked.Increment(ref resolutionCounter_1349_5);
                    global::MsMeeseeks.DIE.WellKnownTypes wellKnownTypes_1360_4 = (global::MsMeeseeks.DIE.WellKnownTypes)DIE_Factory_WellKnownTypes();
                    if (Disposed_1349_2)
                        throw new System.ObjectDisposedException("global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer", $"[DIE] This scope \"global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer\" is already disposed, so it can't create a \"global::MsMeeseeks.DIE.WellKnownTypes\" instance anymore.");
                    _disposal_1349_4.Add(subDisposal_1360_0);
                    transientScopeDisposal_1349_10.AddRange(transientScopeSubDisposal_1360_1);
                    _containerInstanceFieldWellKnownTypes_1349_22 = wellKnownTypes_1360_4;
                }
                catch (global::System.Exception exception)
                {
                    throw MsMeeseeks.DIE.DisposeUtility_0_0.DisposeExceptionHandling_0_10((MsMeeseeks.DIE.IDisposableRange_0_4)this, exception, subDisposal_1360_0, transientScopeSubDisposal_1360_1);
                }
                finally
                {
                    global::System.Threading.Interlocked.Decrement(ref resolutionCounter_1349_5);
                }
            }
            finally
            {
                this._containerInstanceLock_1349_23?.Release();
            }

            this._containerInstanceLock_1349_23 = null;
            return this._containerInstanceFieldWellKnownTypes_1349_22;
        }

        private global::MsMeeseeks.DIE.WellKnownTypesCollections? _containerInstanceFieldWellKnownTypesCollections_1349_24;
        private global::System.Threading.SemaphoreSlim? _containerInstanceLock_1349_25 = new global::System.Threading.SemaphoreSlim(1);
        private global::MsMeeseeks.DIE.WellKnownTypesCollections GetContainerInstanceWellKnownTypesCollections_1361_2()
        {
            global::System.Collections.Generic.List<global::System.Object> transientScopeSubDisposal_1361_1 = new global::System.Collections.Generic.List<global::System.Object>(0);
            global::System.Collections.Generic.List<global::System.Object> subDisposal_1361_0 = new global::System.Collections.Generic.List<global::System.Object>(0);
            if (!global::System.Object.ReferenceEquals(_containerInstanceFieldWellKnownTypesCollections_1349_24, null))
                return _containerInstanceFieldWellKnownTypesCollections_1349_24;
            this._containerInstanceLock_1349_25?.Wait();
            try
            {
                if (!global::System.Object.ReferenceEquals(_containerInstanceFieldWellKnownTypesCollections_1349_24, null))
                    return _containerInstanceFieldWellKnownTypesCollections_1349_24;
                if (Disposed_1349_2)
                    throw new System.ObjectDisposedException("global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer", $"[DIE] This scope \"global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer\" is already disposed, so it can't create a \"global::MsMeeseeks.DIE.WellKnownTypesCollections\" instance anymore.");
                try
                {
                    global::System.Threading.Interlocked.Increment(ref resolutionCounter_1349_5);
                    global::MsMeeseeks.DIE.WellKnownTypesCollections wellKnownTypesCollections_1361_4 = (global::MsMeeseeks.DIE.WellKnownTypesCollections)DIE_Factory_WellKnownTypesCollections();
                    if (Disposed_1349_2)
                        throw new System.ObjectDisposedException("global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer", $"[DIE] This scope \"global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer\" is already disposed, so it can't create a \"global::MsMeeseeks.DIE.WellKnownTypesCollections\" instance anymore.");
                    _disposal_1349_4.Add(subDisposal_1361_0);
                    transientScopeDisposal_1349_10.AddRange(transientScopeSubDisposal_1361_1);
                    _containerInstanceFieldWellKnownTypesCollections_1349_24 = wellKnownTypesCollections_1361_4;
                }
                catch (global::System.Exception exception)
                {
                    throw MsMeeseeks.DIE.DisposeUtility_0_0.DisposeExceptionHandling_0_10((MsMeeseeks.DIE.IDisposableRange_0_4)this, exception, subDisposal_1361_0, transientScopeSubDisposal_1361_1);
                }
                finally
                {
                    global::System.Threading.Interlocked.Decrement(ref resolutionCounter_1349_5);
                }
            }
            finally
            {
                this._containerInstanceLock_1349_25?.Release();
            }

            this._containerInstanceLock_1349_25 = null;
            return this._containerInstanceFieldWellKnownTypesCollections_1349_24;
        }

        private global::MsMeeseeks.DIE.ReferenceGeneratorCounter? _containerInstanceFieldReferenceGeneratorCounter_1349_26;
        private global::System.Threading.SemaphoreSlim? _containerInstanceLock_1349_27 = new global::System.Threading.SemaphoreSlim(1);
        private global::MsMeeseeks.DIE.ReferenceGeneratorCounter GetContainerInstanceReferenceGeneratorCounter_1364_2()
        {
            global::System.Collections.Generic.List<global::System.Object> transientScopeSubDisposal_1364_1 = new global::System.Collections.Generic.List<global::System.Object>(0);
            global::System.Collections.Generic.List<global::System.Object> subDisposal_1364_0 = new global::System.Collections.Generic.List<global::System.Object>(0);
            if (!global::System.Object.ReferenceEquals(_containerInstanceFieldReferenceGeneratorCounter_1349_26, null))
                return _containerInstanceFieldReferenceGeneratorCounter_1349_26;
            this._containerInstanceLock_1349_27?.Wait();
            try
            {
                if (!global::System.Object.ReferenceEquals(_containerInstanceFieldReferenceGeneratorCounter_1349_26, null))
                    return _containerInstanceFieldReferenceGeneratorCounter_1349_26;
                if (Disposed_1349_2)
                    throw new System.ObjectDisposedException("global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer", $"[DIE] This scope \"global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer\" is already disposed, so it can't create a \"global::MsMeeseeks.DIE.ReferenceGeneratorCounter\" instance anymore.");
                try
                {
                    global::System.Threading.Interlocked.Increment(ref resolutionCounter_1349_5);
                    global::MsMeeseeks.DIE.ReferenceGeneratorCounter referenceGeneratorCounter_1364_4 = new global::MsMeeseeks.DIE.ReferenceGeneratorCounter();
                    if (Disposed_1349_2)
                        throw new System.ObjectDisposedException("global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer", $"[DIE] This scope \"global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer\" is already disposed, so it can't create a \"global::MsMeeseeks.DIE.ReferenceGeneratorCounter\" instance anymore.");
                    _disposal_1349_4.Add(subDisposal_1364_0);
                    transientScopeDisposal_1349_10.AddRange(transientScopeSubDisposal_1364_1);
                    _containerInstanceFieldReferenceGeneratorCounter_1349_26 = referenceGeneratorCounter_1364_4;
                }
                catch (global::System.Exception exception)
                {
                    throw MsMeeseeks.DIE.DisposeUtility_0_0.DisposeExceptionHandling_0_10((MsMeeseeks.DIE.IDisposableRange_0_4)this, exception, subDisposal_1364_0, transientScopeSubDisposal_1364_1);
                }
                finally
                {
                    global::System.Threading.Interlocked.Decrement(ref resolutionCounter_1349_5);
                }
            }
            finally
            {
                this._containerInstanceLock_1349_27?.Release();
            }

            this._containerInstanceLock_1349_27 = null;
            return this._containerInstanceFieldReferenceGeneratorCounter_1349_26;
        }

        private global::MsMeeseeks.DIE.Logging.LocalDiagLogger? _scopeInstanceFieldLocalDiagLogger_1349_28;
        private global::System.Threading.SemaphoreSlim? _scopeInstanceLock_1349_29 = new global::System.Threading.SemaphoreSlim(1);
        private global::MsMeeseeks.DIE.Logging.LocalDiagLogger GetScopeInstanceLocalDiagLogger_1365_2()
        {
            global::System.Collections.Generic.List<global::System.Object> transientScopeSubDisposal_1365_1 = new global::System.Collections.Generic.List<global::System.Object>(0);
            global::System.Collections.Generic.List<global::System.Object> subDisposal_1365_0 = new global::System.Collections.Generic.List<global::System.Object>(0);
            if (!global::System.Object.ReferenceEquals(_scopeInstanceFieldLocalDiagLogger_1349_28, null))
                return _scopeInstanceFieldLocalDiagLogger_1349_28;
            this._scopeInstanceLock_1349_29?.Wait();
            try
            {
                if (!global::System.Object.ReferenceEquals(_scopeInstanceFieldLocalDiagLogger_1349_28, null))
                    return _scopeInstanceFieldLocalDiagLogger_1349_28;
                if (Disposed_1349_2)
                    throw new System.ObjectDisposedException("global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer", $"[DIE] This scope \"global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer\" is already disposed, so it can't create a \"global::MsMeeseeks.DIE.Logging.LocalDiagLogger\" instance anymore.");
                try
                {
                    global::System.Threading.Interlocked.Increment(ref resolutionCounter_1349_5);
                    global::MsMeeseeks.DIE.Logging.BaseLogEnhancer functionCallResult_1367_3 = (global::MsMeeseeks.DIE.Logging.BaseLogEnhancer)GetScopeInstanceBaseLogEnhancer_1367_2();
                    global::MsMeeseeks.DIE.Logging.ILogEnhancer executeLevelLogEnhancerDecorator_1365_5 = (global::MsMeeseeks.DIE.Logging.ILogEnhancer)new global::MsMeeseeks.DIE.Logging.ExecuteLevelLogEnhancerDecorator(decoratedEnhancer: functionCallResult_1367_3);
                    global::MsMeeseeks.DIE.Logging.DiagLogger functionCallResult_1366_3 = (global::MsMeeseeks.DIE.Logging.DiagLogger)GetContainerInstanceDiagLogger_1366_2();
                    global::MsMeeseeks.DIE.Logging.LocalDiagLogger localDiagLogger_1365_4 = new global::MsMeeseeks.DIE.Logging.LocalDiagLogger(logEnhancer: executeLevelLogEnhancerDecorator_1365_5, diagLogger: functionCallResult_1366_3);
                    if (Disposed_1349_2)
                        throw new System.ObjectDisposedException("global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer", $"[DIE] This scope \"global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer\" is already disposed, so it can't create a \"global::MsMeeseeks.DIE.Logging.LocalDiagLogger\" instance anymore.");
                    _disposal_1349_4.Add(subDisposal_1365_0);
                    transientScopeDisposal_1349_10.AddRange(transientScopeSubDisposal_1365_1);
                    _scopeInstanceFieldLocalDiagLogger_1349_28 = localDiagLogger_1365_4;
                }
                catch (global::System.Exception exception)
                {
                    throw MsMeeseeks.DIE.DisposeUtility_0_0.DisposeExceptionHandling_0_10((MsMeeseeks.DIE.IDisposableRange_0_4)this, exception, subDisposal_1365_0, transientScopeSubDisposal_1365_1);
                }
                finally
                {
                    global::System.Threading.Interlocked.Decrement(ref resolutionCounter_1349_5);
                }
            }
            finally
            {
                this._scopeInstanceLock_1349_29?.Release();
            }

            this._scopeInstanceLock_1349_29 = null;
            return this._scopeInstanceFieldLocalDiagLogger_1349_28;
        }

        private global::MsMeeseeks.DIE.Logging.DiagLogger? _containerInstanceFieldDiagLogger_1349_30;
        private global::System.Threading.SemaphoreSlim? _containerInstanceLock_1349_31 = new global::System.Threading.SemaphoreSlim(1);
        private global::MsMeeseeks.DIE.Logging.DiagLogger GetContainerInstanceDiagLogger_1366_2()
        {
            global::System.Collections.Generic.List<global::System.Object> transientScopeSubDisposal_1366_1 = new global::System.Collections.Generic.List<global::System.Object>(0);
            global::System.Collections.Generic.List<global::System.Object> subDisposal_1366_0 = new global::System.Collections.Generic.List<global::System.Object>(0);
            if (!global::System.Object.ReferenceEquals(_containerInstanceFieldDiagLogger_1349_30, null))
                return _containerInstanceFieldDiagLogger_1349_30;
            this._containerInstanceLock_1349_31?.Wait();
            try
            {
                if (!global::System.Object.ReferenceEquals(_containerInstanceFieldDiagLogger_1349_30, null))
                    return _containerInstanceFieldDiagLogger_1349_30;
                if (Disposed_1349_2)
                    throw new System.ObjectDisposedException("global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer", $"[DIE] This scope \"global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer\" is already disposed, so it can't create a \"global::MsMeeseeks.DIE.Logging.DiagLogger\" instance anymore.");
                try
                {
                    global::System.Threading.Interlocked.Increment(ref resolutionCounter_1349_5);
                    global::MsMeeseeks.DIE.GeneratorConfiguration functionCallResult_1368_3 = (global::MsMeeseeks.DIE.GeneratorConfiguration)GetContainerInstanceGeneratorConfiguration_1368_2();
                    global::Microsoft.CodeAnalysis.GeneratorExecutionContext generatorExecutionContext_1366_5 = (global::Microsoft.CodeAnalysis.GeneratorExecutionContext)DIE_Factory_GeneratorExecutionContext;
                    global::MsMeeseeks.DIE.Logging.DiagLogger diagLogger_1366_4 = new global::MsMeeseeks.DIE.Logging.DiagLogger(generatorConfiguration: functionCallResult_1368_3, context: generatorExecutionContext_1366_5);
                    if (Disposed_1349_2)
                        throw new System.ObjectDisposedException("global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer", $"[DIE] This scope \"global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer\" is already disposed, so it can't create a \"global::MsMeeseeks.DIE.Logging.DiagLogger\" instance anymore.");
                    _disposal_1349_4.Add(subDisposal_1366_0);
                    transientScopeDisposal_1349_10.AddRange(transientScopeSubDisposal_1366_1);
                    _containerInstanceFieldDiagLogger_1349_30 = diagLogger_1366_4;
                }
                catch (global::System.Exception exception)
                {
                    throw MsMeeseeks.DIE.DisposeUtility_0_0.DisposeExceptionHandling_0_10((MsMeeseeks.DIE.IDisposableRange_0_4)this, exception, subDisposal_1366_0, transientScopeSubDisposal_1366_1);
                }
                finally
                {
                    global::System.Threading.Interlocked.Decrement(ref resolutionCounter_1349_5);
                }
            }
            finally
            {
                this._containerInstanceLock_1349_31?.Release();
            }

            this._containerInstanceLock_1349_31 = null;
            return this._containerInstanceFieldDiagLogger_1349_30;
        }

        private global::MsMeeseeks.DIE.Logging.BaseLogEnhancer? _scopeInstanceFieldBaseLogEnhancer_1349_32;
        private global::System.Threading.SemaphoreSlim? _scopeInstanceLock_1349_33 = new global::System.Threading.SemaphoreSlim(1);
        private global::MsMeeseeks.DIE.Logging.BaseLogEnhancer GetScopeInstanceBaseLogEnhancer_1367_2()
        {
            global::System.Collections.Generic.List<global::System.Object> transientScopeSubDisposal_1367_1 = new global::System.Collections.Generic.List<global::System.Object>(0);
            global::System.Collections.Generic.List<global::System.Object> subDisposal_1367_0 = new global::System.Collections.Generic.List<global::System.Object>(0);
            if (!global::System.Object.ReferenceEquals(_scopeInstanceFieldBaseLogEnhancer_1349_32, null))
                return _scopeInstanceFieldBaseLogEnhancer_1349_32;
            this._scopeInstanceLock_1349_33?.Wait();
            try
            {
                if (!global::System.Object.ReferenceEquals(_scopeInstanceFieldBaseLogEnhancer_1349_32, null))
                    return _scopeInstanceFieldBaseLogEnhancer_1349_32;
                if (Disposed_1349_2)
                    throw new System.ObjectDisposedException("global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer", $"[DIE] This scope \"global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer\" is already disposed, so it can't create a \"global::MsMeeseeks.DIE.Logging.BaseLogEnhancer\" instance anymore.");
                try
                {
                    global::System.Threading.Interlocked.Increment(ref resolutionCounter_1349_5);
                    global::MsMeeseeks.DIE.Logging.BaseLogEnhancer baseLogEnhancer_1367_4 = new global::MsMeeseeks.DIE.Logging.BaseLogEnhancer();
                    if (Disposed_1349_2)
                        throw new System.ObjectDisposedException("global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer", $"[DIE] This scope \"global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer\" is already disposed, so it can't create a \"global::MsMeeseeks.DIE.Logging.BaseLogEnhancer\" instance anymore.");
                    _disposal_1349_4.Add(subDisposal_1367_0);
                    transientScopeDisposal_1349_10.AddRange(transientScopeSubDisposal_1367_1);
                    _scopeInstanceFieldBaseLogEnhancer_1349_32 = baseLogEnhancer_1367_4;
                }
                catch (global::System.Exception exception)
                {
                    throw MsMeeseeks.DIE.DisposeUtility_0_0.DisposeExceptionHandling_0_10((MsMeeseeks.DIE.IDisposableRange_0_4)this, exception, subDisposal_1367_0, transientScopeSubDisposal_1367_1);
                }
                finally
                {
                    global::System.Threading.Interlocked.Decrement(ref resolutionCounter_1349_5);
                }
            }
            finally
            {
                this._scopeInstanceLock_1349_33?.Release();
            }

            this._scopeInstanceLock_1349_33 = null;
            return this._scopeInstanceFieldBaseLogEnhancer_1349_32;
        }

        private global::MsMeeseeks.DIE.GeneratorConfiguration? _containerInstanceFieldGeneratorConfiguration_1349_34;
        private global::System.Threading.SemaphoreSlim? _containerInstanceLock_1349_35 = new global::System.Threading.SemaphoreSlim(1);
        private global::MsMeeseeks.DIE.GeneratorConfiguration GetContainerInstanceGeneratorConfiguration_1368_2()
        {
            global::System.Collections.Generic.List<global::System.Object> transientScopeSubDisposal_1368_1 = new global::System.Collections.Generic.List<global::System.Object>(0);
            global::System.Collections.Generic.List<global::System.Object> subDisposal_1368_0 = new global::System.Collections.Generic.List<global::System.Object>(0);
            if (!global::System.Object.ReferenceEquals(_containerInstanceFieldGeneratorConfiguration_1349_34, null))
                return _containerInstanceFieldGeneratorConfiguration_1349_34;
            this._containerInstanceLock_1349_35?.Wait();
            try
            {
                if (!global::System.Object.ReferenceEquals(_containerInstanceFieldGeneratorConfiguration_1349_34, null))
                    return _containerInstanceFieldGeneratorConfiguration_1349_34;
                if (Disposed_1349_2)
                    throw new System.ObjectDisposedException("global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer", $"[DIE] This scope \"global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer\" is already disposed, so it can't create a \"global::MsMeeseeks.DIE.GeneratorConfiguration\" instance anymore.");
                try
                {
                    global::System.Threading.Interlocked.Increment(ref resolutionCounter_1349_5);
                    global::Microsoft.CodeAnalysis.GeneratorExecutionContext generatorExecutionContext_1368_5 = (global::Microsoft.CodeAnalysis.GeneratorExecutionContext)DIE_Factory_GeneratorExecutionContext;
                    global::MsMeeseeks.DIE.WellKnownTypesMiscellaneous functionCallResult_1356_6 = (global::MsMeeseeks.DIE.WellKnownTypesMiscellaneous)GetContainerInstanceWellKnownTypesMiscellaneous_1356_2();
                    global::MsMeeseeks.DIE.GeneratorConfiguration generatorConfiguration_1368_4 = new global::MsMeeseeks.DIE.GeneratorConfiguration(context: generatorExecutionContext_1368_5, wellKnownTypesMiscellaneous: functionCallResult_1356_6);
                    if (Disposed_1349_2)
                        throw new System.ObjectDisposedException("global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer", $"[DIE] This scope \"global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer\" is already disposed, so it can't create a \"global::MsMeeseeks.DIE.GeneratorConfiguration\" instance anymore.");
                    _disposal_1349_4.Add(subDisposal_1368_0);
                    transientScopeDisposal_1349_10.AddRange(transientScopeSubDisposal_1368_1);
                    _containerInstanceFieldGeneratorConfiguration_1349_34 = generatorConfiguration_1368_4;
                }
                catch (global::System.Exception exception)
                {
                    throw MsMeeseeks.DIE.DisposeUtility_0_0.DisposeExceptionHandling_0_10((MsMeeseeks.DIE.IDisposableRange_0_4)this, exception, subDisposal_1368_0, transientScopeSubDisposal_1368_1);
                }
                finally
                {
                    global::System.Threading.Interlocked.Decrement(ref resolutionCounter_1349_5);
                }
            }
            finally
            {
                this._containerInstanceLock_1349_35?.Release();
            }

            this._containerInstanceLock_1349_35 = null;
            return this._containerInstanceFieldGeneratorConfiguration_1349_34;
        }

        global::System.Object[] MsMeeseeks.DIE.IDisposableRange_0_4.TransientScopes_0_6 => System.Linq.Enumerable.ToArray(transientScopeDisposal_1349_10);

        global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::System.Object>> MsMeeseeks.DIE.IDisposableRange_0_4.Disposables_0_7 => _disposal_1349_4;

        global::System.Collections.Concurrent.ConcurrentBag<global::System.IDisposable> MsMeeseeks.DIE.IDisposableRange_0_4.UserDefinedSyncDisposables_0_8 => concurrentBag_1349_3;

        bool MsMeeseeks.DIE.IDisposableRange_0_4.ShouldBeDisposed_0_5(global::System.Object disposable)
        {
            var genericTypeDefinition = disposable.GetType().IsGenericType ? disposable.GetType().GetGenericTypeDefinition() : null;
            return disposable is IScope_1349_11;
        }

        private global::System.Collections.Concurrent.ConcurrentBag<global::System.IDisposable> concurrentBag_1349_3 = new global::System.Collections.Concurrent.ConcurrentBag<global::System.IDisposable>();
        private int _disposed_1349_0 = 0;
        private bool Disposed_1349_2 => _disposed_1349_0 != 0;

        public void Dispose()
        {
            var disposed_1349_1 = global::System.Threading.Interlocked.Exchange(ref _disposed_1349_0, 1);
            if (disposed_1349_1 != 0)
                return;
            System.Threading.SpinWait.SpinUntil(() => resolutionCounter_1349_5 == 0);
            MsMeeseeks.DIE.DisposeUtility_0_0.Dispose_0_1((MsMeeseeks.DIE.IDisposableRange_0_4)this);
        }

        private interface IScope_1349_11 : global::System.IDisposable
        {
        }

        private global::System.Collections.Generic.List<global::System.Object> transientScopeDisposal_1349_10 = new global::System.Collections.Generic.List<global::System.Object>();
        private interface ITransientScope_1349_13 : global::System.IDisposable
        {
        }

        private sealed partial class DIE_DefaultTransientScope : MsMeeseeks.DIE.IDisposableRange_0_4, ITransientScope_1349_13
        {
            internal required global::MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer Container_1362_10 { private get; init; }

            private global::System.Int32 resolutionCounter_1362_5;
            private global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::System.Object>> _disposal_1362_4 = new global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::System.Object>>();
            internal global::MsMeeseeks.DIE.ExecuteContainerContext CreateExecuteContainerContext_1363_3(global::MsMeeseeks.DIE.ContainerInfo containerInfo_1363_0, global::System.Collections.Generic.List<global::System.Object> list_1363_2)
            {
                global::System.Collections.Generic.List<global::System.Object> subDisposal_1363_1 = new global::System.Collections.Generic.List<global::System.Object>(1);
                if (Disposed_1362_2)
                    throw new System.ObjectDisposedException("MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer.DIE_DefaultTransientScope", $"[DIE] This scope \"MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer.DIE_DefaultTransientScope\" is already disposed, so it can't create a \"global::MsMeeseeks.DIE.ExecuteContainerContext\" instance anymore.");
                try
                {
                    global::System.Threading.Interlocked.Increment(ref resolutionCounter_1362_5);
                    global::MsMeeseeks.DIE.CodeGeneration.RequiredKeywordUtility functionCallResult_1352_8 = (global::MsMeeseeks.DIE.CodeGeneration.RequiredKeywordUtility)Container_1362_10.GetContainerInstanceRequiredKeywordUtility_1352_2();
                    global::MsMeeseeks.DIE.CodeGeneration.DisposeUtility functionCallResult_1353_5 = (global::MsMeeseeks.DIE.CodeGeneration.DisposeUtility)Container_1362_10.GetContainerInstanceDisposeUtility_1353_2();
                    global::MsMeeseeks.DIE.ReferenceGeneratorCounter functionCallResult_1364_5 = (global::MsMeeseeks.DIE.ReferenceGeneratorCounter)Container_1362_10.GetContainerInstanceReferenceGeneratorCounter_1364_2();
                    global::MsMeeseeks.DIE.IExecuteContainer iExecuteContainer_1363_6 = (global::MsMeeseeks.DIE.IExecuteContainer)DIE_Factory_IExecuteContainer(containerInfo: containerInfo_1363_0, requiredKeywordUtility: functionCallResult_1352_8, disposeUtility: functionCallResult_1353_5, referenceGeneratorCounter: functionCallResult_1364_5);
                    global::System.IDisposable iDisposable_1363_7 = this as global::System.IDisposable;
                    global::MsMeeseeks.DIE.ExecuteContainerContext executeContainerContext_1363_5 = new global::MsMeeseeks.DIE.ExecuteContainerContext(executeContainer: iExecuteContainer_1363_6, eagerDisposalTrigger: iDisposable_1363_7);
                    subDisposal_1363_1.Add(executeContainerContext_1363_5);
                    if (Disposed_1362_2)
                        throw new System.ObjectDisposedException("MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer.DIE_DefaultTransientScope", $"[DIE] This scope \"MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer.DIE_DefaultTransientScope\" is already disposed, so it can't create a \"global::MsMeeseeks.DIE.ExecuteContainerContext\" instance anymore.");
                    _disposal_1362_4.Add(subDisposal_1363_1);
                    return executeContainerContext_1363_5;
                }
                catch (global::System.Exception exception)
                {
                    throw MsMeeseeks.DIE.DisposeUtility_0_0.DisposeExceptionHandling_0_10((MsMeeseeks.DIE.IDisposableRange_0_4)this, exception, subDisposal_1363_1);
                }
                finally
                {
                    global::System.Threading.Interlocked.Decrement(ref resolutionCounter_1362_5);
                }
            }

            private partial void DIE_AddForDisposal(global::System.IDisposable disposable)
            {
                global::System.Threading.Interlocked.Increment(ref resolutionCounter_1362_5);
                try
                {
                    if (Disposed_1362_2)
                        throw new System.ObjectDisposedException("MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer.DIE_DefaultTransientScope", $"[DIE] This scope \"MsMeeseeks.DIE.MsContainer.ExecuteLevelContainer.DIE_DefaultTransientScope\" is already disposed, so it can't manage another disposable.");
                    concurrentBag_1362_3.Add((global::System.IDisposable)disposable);
                }
                finally
                {
                    global::System.Threading.Interlocked.Decrement(ref resolutionCounter_1362_5);
                }
            }

            global::System.Object[] MsMeeseeks.DIE.IDisposableRange_0_4.TransientScopes_0_6 => new object[0];

            global::System.Collections.Generic.List<global::System.Collections.Generic.List<global::System.Object>> MsMeeseeks.DIE.IDisposableRange_0_4.Disposables_0_7 => _disposal_1362_4;

            global::System.Collections.Concurrent.ConcurrentBag<global::System.IDisposable> MsMeeseeks.DIE.IDisposableRange_0_4.UserDefinedSyncDisposables_0_8 => concurrentBag_1362_3;

            bool MsMeeseeks.DIE.IDisposableRange_0_4.ShouldBeDisposed_0_5(global::System.Object disposable)
            {
                var genericTypeDefinition = disposable.GetType().IsGenericType ? disposable.GetType().GetGenericTypeDefinition() : null;
                return disposable is global::MsMeeseeks.DIE.ExecuteContainerContext || disposable is IScope_1349_11;
            }

            private global::System.Collections.Concurrent.ConcurrentBag<global::System.IDisposable> concurrentBag_1362_3 = new global::System.Collections.Concurrent.ConcurrentBag<global::System.IDisposable>();
            private int _disposed_1362_0 = 0;
            private bool Disposed_1362_2 => _disposed_1362_0 != 0;

            public void Dispose()
            {
                var disposed_1362_1 = global::System.Threading.Interlocked.Exchange(ref _disposed_1362_0, 1);
                if (disposed_1362_1 != 0)
                    return;
                System.Threading.SpinWait.SpinUntil(() => resolutionCounter_1362_5 == 0);
                Container_1362_10.transientScopeDisposal_1349_10.Remove(this);
                Container_1362_10.transientScopeDisposal_1349_10.TrimExcess();
                MsMeeseeks.DIE.DisposeUtility_0_0.Dispose_0_1((MsMeeseeks.DIE.IDisposableRange_0_4)this);
            }
        }
    }
}
#nullable disable
