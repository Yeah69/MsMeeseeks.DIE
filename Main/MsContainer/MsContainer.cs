using Microsoft.CodeAnalysis;
using MrMeeseeks.DIE.Configuration.Attributes;
using MsMeeseeks.DIE.Contexts;

// ReSharper disable InconsistentNaming

namespace MsMeeseeks.DIE.MsContainer;

internal sealed partial class MsContainer
{
    private readonly GeneratorExecutionContext DIE_Factory_GeneratorExecutionContext;
    private readonly Compilation DIE_Factory_Compilation;
    private readonly IContainerInfo DIE_Factory_ContainerInfo;

    private MsContainer(
        GeneratorExecutionContext context, 
        IContainerInfo dieFactoryContainerInfo)
    {
        DIE_Factory_ContainerInfo = dieFactoryContainerInfo;
        DIE_Factory_Compilation = context.Compilation;
        DIE_Factory_GeneratorExecutionContext = context;
    }

    private void DIE_ConstrParams_UserDefinedElements(out (INamedTypeSymbol? Range, INamedTypeSymbol Container) types) => 
        types = (DIE_Factory_ContainerInfo.ContainerType, DIE_Factory_ContainerInfo.ContainerType);

    private void DIE_ConstrParams_ScopeInfo(
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

    private sealed partial class DIE_TransientScope_ScopeNodeRoot
    {
        [UserDefinedConstructorParametersInjection(typeof(UserDefinedElements))]
        private void DIE_ConstrParams_UserDefinedElements(
            IContainerInfoContext containerInfoContext,
            IScopeInfo scopeInfo,
            out (INamedTypeSymbol? Range, INamedTypeSymbol Container) types) => 
            types = (scopeInfo.ScopeType, containerInfoContext.ContainerInfo.ContainerType);
    }

    private sealed partial class DIE_TransientScope_TransientScopeNodeRoot
    {
        [UserDefinedConstructorParametersInjection(typeof(UserDefinedElements))]
        private void DIE_ConstrParams_UserDefinedElements(
            IContainerInfoContext containerInfoContext,
            IScopeInfo scopeInfo,
            out (INamedTypeSymbol? Range, INamedTypeSymbol Container) types) => 
            types = (scopeInfo.ScopeType, containerInfoContext.ContainerInfo.ContainerType);
    }

    private sealed partial class DIE_Scope_CreateFunctionNodeRoot
    {
    }

    private sealed partial class DIE_Scope_CreateScopeFunctionNodeRoot
    {
    }

    private sealed partial class DIE_Scope_CreateTransientScopeFunctionNodeRoot
    {
    }

    private sealed partial class DIE_Scope_EntryFunctionNodeRoot
    {
    }

    private sealed partial class DIE_Scope_LocalFunctionNodeRoot
    {
    }

    private sealed partial class DIE_Scope_RangedInstanceFunctionNodeRoot
    {
    }

    private sealed partial class DIE_Scope_RangedInstanceInterfaceFunctionNodeRoot
    {
    }

    private sealed partial class DIE_Scope_MultiFunctionNodeRoot
    {
    }

    private sealed partial class DIE_Scope_VoidFunctionNodeRoot
    {
    }
}