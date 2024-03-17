using MsMeeseeks.DIE.Configuration;
using MrMeeseeks.DIE.Configuration.Attributes;
using MsMeeseeks.DIE.Nodes.Ranges;
using MsMeeseeks.DIE.Utility;

// ReSharper disable InconsistentNaming

namespace MsMeeseeks.DIE.MsContainer;

internal sealed partial class MsContainer
{
    private readonly GeneratorExecutionContext DIE_Factory_GeneratorExecutionContext;
    private readonly Compilation DIE_Factory_Compilation;
    private readonly ContainerInfo DIE_Factory_ContainerInfo;
    private readonly RequiredKeywordUtility DIE_Factory_RequiredKeywordUtility;

    private MsContainer(
        GeneratorExecutionContext context, 
        ContainerInfo dieFactoryContainerInfo,
        RequiredKeywordUtility dieFactoryRequiredKeywordUtility)
    {
        DIE_Factory_ContainerInfo = dieFactoryContainerInfo;
        DIE_Factory_RequiredKeywordUtility = dieFactoryRequiredKeywordUtility;
        DIE_Factory_Compilation = context.Compilation;
        DIE_Factory_GeneratorExecutionContext = context;
    }

    private void DIE_ConstrParams_UserDefinedElements(out (INamedTypeSymbol? Range, INamedTypeSymbol Container) types) => 
        types = (DIE_Factory_ContainerInfo.ContainerType, DIE_Factory_ContainerInfo.ContainerType);

    private static void DIE_ConstrParams_ScopeInfo(
        out string name,
        out INamedTypeSymbol? scopeType)
    {
        name = "";
        scopeType = null;
    }

    private WellKnownTypes DIE_Factory_WellKnownTypes() => 
        WellKnownTypes.Create(DIE_Factory_Compilation);

    private WellKnownTypesAggregation DIE_Factory_WellKnownTypesAggregation() => 
        WellKnownTypesAggregation.Create(DIE_Factory_Compilation);

    private WellKnownTypesCollections DIE_Factory_WellKnownTypesCollections() => 
        WellKnownTypesCollections.Create(DIE_Factory_Compilation);

    private WellKnownTypesChoice DIE_Factory_WellKnownTypesChoice() => 
        WellKnownTypesChoice.Create(DIE_Factory_Compilation);

    private WellKnownTypesMiscellaneous DIE_Factory_WellKnownTypesMiscellaneous() => 
        WellKnownTypesMiscellaneous.Create(DIE_Factory_Compilation);

    [InitializedInstances(typeof(ReferenceGenerator))]
    private abstract class ScopeObject;
    
    private abstract class TransientScopeBase : ScopeObject
    {
        protected static void DIE_ConstrParams_UserDefinedElements(
            IContainerInfo containerInfo,
            IScopeInfo scopeInfo,
            out (INamedTypeSymbol? Range, INamedTypeSymbol Container) types) => 
            types = (scopeInfo.ScopeType, containerInfo.ContainerType);
    }

    private sealed partial class DIE_TransientScope_ScopeNodeRoot : TransientScopeBase;

    private sealed partial class DIE_TransientScope_TransientScopeNodeRoot : TransientScopeBase;

    private abstract class ScopeBase : ScopeObject
    {
        protected readonly IRangeNode DIE_Factory_parentRange;
        protected readonly ICheckTypeProperties DIE_Factory_checkTypeProperties;

        protected ScopeBase(
            IRangeNode parentRange,
            ICheckTypeProperties checkTypeProperties)
        {
            DIE_Factory_parentRange = parentRange;
            DIE_Factory_checkTypeProperties = checkTypeProperties;
        }
    }

    private sealed partial class DIE_Scope_CreateFunctionNodeRoot : ScopeBase
    {
        internal DIE_Scope_CreateFunctionNodeRoot(IRangeNode parentRange, ICheckTypeProperties checkTypeProperties) 
            : base(parentRange, checkTypeProperties) { }
    }

    private sealed partial class DIE_Scope_CreateScopeFunctionNodeRoot : ScopeBase
    {
        internal DIE_Scope_CreateScopeFunctionNodeRoot(IRangeNode parentRange, ICheckTypeProperties checkTypeProperties) 
            : base(parentRange, checkTypeProperties) { }
    }

    private sealed partial class DIE_Scope_CreateTransientScopeFunctionNodeRoot : ScopeBase
    {
        internal DIE_Scope_CreateTransientScopeFunctionNodeRoot(IRangeNode parentRange, ICheckTypeProperties checkTypeProperties) 
            : base(parentRange, checkTypeProperties) { }
    }

    private sealed partial class DIE_Scope_EntryFunctionNodeRoot : ScopeBase
    {
        internal DIE_Scope_EntryFunctionNodeRoot(IRangeNode parentRange, ICheckTypeProperties checkTypeProperties) 
            : base(parentRange, checkTypeProperties) { }
    }

    private sealed partial class DIE_Scope_LocalFunctionNodeRoot : ScopeBase
    {
        internal DIE_Scope_LocalFunctionNodeRoot(IRangeNode parentRange, ICheckTypeProperties checkTypeProperties) 
            : base(parentRange, checkTypeProperties) { }
    }

    private sealed partial class DIE_Scope_RangedInstanceFunctionNodeRoot : ScopeBase
    {
        internal DIE_Scope_RangedInstanceFunctionNodeRoot(IRangeNode parentRange, ICheckTypeProperties checkTypeProperties) 
            : base(parentRange, checkTypeProperties) { }
    }

    private sealed partial class DIE_Scope_RangedInstanceInterfaceFunctionNodeRoot : ScopeBase
    {
        internal DIE_Scope_RangedInstanceInterfaceFunctionNodeRoot(IRangeNode parentRange, ICheckTypeProperties checkTypeProperties) 
            : base(parentRange, checkTypeProperties) { }
    }

    private sealed partial class DIE_Scope_MultiFunctionNodeRoot : ScopeBase
    {
        internal DIE_Scope_MultiFunctionNodeRoot(IRangeNode parentRange, ICheckTypeProperties checkTypeProperties) 
            : base(parentRange, checkTypeProperties) { }
    }

    private sealed partial class DIE_Scope_VoidFunctionNodeRoot : ScopeBase
    {
        internal DIE_Scope_VoidFunctionNodeRoot(IRangeNode parentRange, ICheckTypeProperties checkTypeProperties) 
            : base(parentRange, checkTypeProperties) { }
    }

    private sealed partial class DIE_Scope_MultiKeyValueFunctionNodeRoot : ScopeBase
    {
        internal DIE_Scope_MultiKeyValueFunctionNodeRoot(IRangeNode parentRange, ICheckTypeProperties checkTypeProperties) 
            : base(parentRange, checkTypeProperties) { }
    }

    private sealed partial class DIE_Scope_MultiKeyValueMultiFunctionNodeRoot : ScopeBase
    {
        internal DIE_Scope_MultiKeyValueMultiFunctionNodeRoot(IRangeNode parentRange, ICheckTypeProperties checkTypeProperties) 
            : base(parentRange, checkTypeProperties) { }
    }
}