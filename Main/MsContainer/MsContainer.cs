﻿using MsMeeseeks.DIE.Configuration;
using MrMeeseeks.DIE.Configuration.Attributes;
using MsMeeseeks.DIE.Contexts;
using MsMeeseeks.DIE.Logging;
using MsMeeseeks.DIE.Nodes.Functions;
using MsMeeseeks.DIE.Nodes.Ranges;
using MsMeeseeks.DIE.Nodes.Roots;
using MsMeeseeks.DIE.Utility;

// ReSharper disable InconsistentNaming

namespace MsMeeseeks.DIE.MsContainer;

//[ContainerInstanceImplementationAggregation(typeof(Compilation))]
[ImplementationChoice(typeof(IRangeNode), typeof(ContainerNode))]
[ImplementationChoice(typeof(ICheckTypeProperties), typeof(ContainerCheckTypeProperties))]
[ImplementationChoice(typeof(IFunctionLevelLogMessageEnhancer), typeof(FunctionLevelLogMessageEnhancerForRanges))]
[CreateFunction(typeof(IExecuteContainer), "Create")]
internal sealed partial class MsContainer
{
    private readonly GeneratorExecutionContext DIE_Factory_GeneratorExecutionContext;
    private readonly Compilation DIE_Factory_Compilation;
    private readonly IContainerInfo DIE_Factory_ContainerInfo;
    private readonly RequiredKeywordUtility DIE_Factory_RequiredKeywordUtility;

    private MsContainer(
        GeneratorExecutionContext context, 
        IContainerInfo dieFactoryContainerInfo,
        RequiredKeywordUtility dieFactoryRequiredKeywordUtility)
    {
        DIE_Factory_ContainerInfo = dieFactoryContainerInfo;
        DIE_Factory_RequiredKeywordUtility = dieFactoryRequiredKeywordUtility;
        DIE_Factory_Compilation = context.Compilation;
        DIE_Factory_GeneratorExecutionContext = context;
    }

    [UserDefinedConstructorParametersInjection(typeof(UserDefinedElements))]
    private void DIE_ConstrParams_UserDefinedElements(out (INamedTypeSymbol? Range, INamedTypeSymbol Container) types) => 
        types = (DIE_Factory_ContainerInfo.ContainerType, DIE_Factory_ContainerInfo.ContainerType);

    [UserDefinedConstructorParametersInjection(typeof(ScopeInfo))]
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

    [ImplementationChoice(typeof(IRangeNode), typeof(ScopeNode))]
    [ImplementationChoice(typeof(ICheckTypeProperties), typeof(ScopeCheckTypeProperties))]
    [CustomScopeForRootTypes(typeof(ScopeNodeRoot))]
    [InitializedInstances(typeof(ScopeInfo), typeof(ReferenceGenerator))]
    private sealed partial class DIE_TransientScope_ScopeNodeRoot
    {
        [UserDefinedConstructorParametersInjection(typeof(UserDefinedElements))]
        private static void DIE_ConstrParams_UserDefinedElements(
            IContainerInfoContext containerInfoContext,
            IScopeInfo scopeInfo,
            out (INamedTypeSymbol? Range, INamedTypeSymbol Container) types) => 
            types = (scopeInfo.ScopeType, containerInfoContext.ContainerInfo.ContainerType);
    }

    [ImplementationChoice(typeof(IRangeNode), typeof(TransientScopeNode))]
    [ImplementationChoice(typeof(ICheckTypeProperties), typeof(ScopeCheckTypeProperties))]
    [CustomScopeForRootTypes(typeof(TransientScopeNodeRoot))]
    [InitializedInstances(typeof(ScopeInfo), typeof(ReferenceGenerator))]
    private sealed partial class DIE_TransientScope_TransientScopeNodeRoot
    {
        [UserDefinedConstructorParametersInjection(typeof(UserDefinedElements))]
        private static void DIE_ConstrParams_UserDefinedElements(
            IContainerInfoContext containerInfoContext,
            IScopeInfo scopeInfo,
            out (INamedTypeSymbol? Range, INamedTypeSymbol Container) types) => 
            types = (scopeInfo.ScopeType, containerInfoContext.ContainerInfo.ContainerType);
    }

    [ImplementationChoice(typeof(IFunctionNode), typeof(CreateFunctionNode))]
    [ImplementationChoice(typeof(IFunctionLevelLogMessageEnhancer), typeof(FunctionLevelLogMessageEnhancer))]
    [CustomScopeForRootTypes(typeof(CreateFunctionNodeRoot))]
    [InitializedInstances(typeof(ReferenceGenerator), typeof(CreateFunctionNode))]
    private sealed partial class DIE_Scope_CreateFunctionNodeRoot;

    [ImplementationChoice(typeof(IFunctionNode), typeof(CreateScopeFunctionNode))]
    [ImplementationChoice(typeof(IFunctionLevelLogMessageEnhancer), typeof(FunctionLevelLogMessageEnhancer))]
    [CustomScopeForRootTypes(typeof(CreateScopeFunctionNodeRoot))]
    [InitializedInstances(typeof(ReferenceGenerator), typeof(CreateScopeFunctionNode))]
    private sealed partial class DIE_Scope_CreateScopeFunctionNodeRoot;

    [ImplementationChoice(typeof(IFunctionNode), typeof(CreateTransientScopeFunctionNode))]
    [ImplementationChoice(typeof(IFunctionLevelLogMessageEnhancer), typeof(FunctionLevelLogMessageEnhancer))]
    [CustomScopeForRootTypes(typeof(CreateTransientScopeFunctionNodeRoot))]
    [InitializedInstances(typeof(ReferenceGenerator), typeof(CreateTransientScopeFunctionNode))]
    private sealed partial class DIE_Scope_CreateTransientScopeFunctionNodeRoot;

    [ImplementationChoice(typeof(IFunctionNode), typeof(EntryFunctionNode))]
    [ImplementationChoice(typeof(IFunctionLevelLogMessageEnhancer), typeof(FunctionLevelLogMessageEnhancer))]
    [CustomScopeForRootTypes(typeof(EntryFunctionNodeRoot))]
    [InitializedInstances(typeof(ReferenceGenerator), typeof(EntryFunctionNode))]
    private sealed partial class DIE_Scope_EntryFunctionNodeRoot;

    [ImplementationChoice(typeof(IFunctionNode), typeof(LocalFunctionNode))]
    [ImplementationChoice(typeof(IFunctionLevelLogMessageEnhancer), typeof(FunctionLevelLogMessageEnhancer))]
    [CustomScopeForRootTypes(typeof(LocalFunctionNodeRoot))]
    [InitializedInstances(typeof(ReferenceGenerator), typeof(LocalFunctionNode))]
    private sealed partial class DIE_Scope_LocalFunctionNodeRoot;

    [ImplementationChoice(typeof(IFunctionNode), typeof(RangedInstanceFunctionNode))]
    [ImplementationChoice(typeof(IFunctionLevelLogMessageEnhancer), typeof(FunctionLevelLogMessageEnhancer))]
    [CustomScopeForRootTypes(typeof(RangedInstanceFunctionNodeRoot))]
    [InitializedInstances(typeof(ReferenceGenerator), typeof(RangedInstanceFunctionNode))]
    private sealed partial class DIE_Scope_RangedInstanceFunctionNodeRoot;

    [ImplementationChoice(typeof(IFunctionNode), typeof(RangedInstanceInterfaceFunctionNode))]
    [ImplementationChoice(typeof(IFunctionLevelLogMessageEnhancer), typeof(FunctionLevelLogMessageEnhancer))]
    [CustomScopeForRootTypes(typeof(RangedInstanceInterfaceFunctionNodeRoot))]
    [InitializedInstances(typeof(ReferenceGenerator), typeof(RangedInstanceInterfaceFunctionNode))]
    private sealed partial class DIE_Scope_RangedInstanceInterfaceFunctionNodeRoot;

    [ImplementationChoice(typeof(IFunctionNode), typeof(MultiFunctionNode))]
    [ImplementationChoice(typeof(IFunctionLevelLogMessageEnhancer), typeof(FunctionLevelLogMessageEnhancer))]
    [CustomScopeForRootTypes(typeof(MultiFunctionNodeRoot))]
    [InitializedInstances(typeof(ReferenceGenerator), typeof(MultiFunctionNode))]
    private sealed partial class DIE_Scope_MultiFunctionNodeRoot;

    [ImplementationChoice(typeof(IFunctionNode), typeof(VoidFunctionNode))]
    [ImplementationChoice(typeof(IFunctionLevelLogMessageEnhancer), typeof(FunctionLevelLogMessageEnhancer))]
    [CustomScopeForRootTypes(typeof(VoidFunctionNodeRoot))]
    [InitializedInstances(typeof(ReferenceGenerator), typeof(VoidFunctionNode))]
    private sealed partial class DIE_Scope_VoidFunctionNodeRoot;

    [ImplementationChoice(typeof(IFunctionNode), typeof(MultiKeyValueFunctionNode))]
    [ImplementationChoice(typeof(IFunctionLevelLogMessageEnhancer), typeof(FunctionLevelLogMessageEnhancer))]
    [CustomScopeForRootTypes(typeof(MultiKeyValueFunctionNodeRoot))]
    [InitializedInstances(typeof(ReferenceGenerator), typeof(MultiKeyValueFunctionNode))]
    private sealed partial class DIE_Scope_MultiKeyValueFunctionNodeRoot;

    [ImplementationChoice(typeof(IFunctionNode), typeof(MultiKeyValueMultiFunctionNode))]
    [ImplementationChoice(typeof(IFunctionLevelLogMessageEnhancer), typeof(FunctionLevelLogMessageEnhancer))]
    [CustomScopeForRootTypes(typeof(MultiKeyValueMultiFunctionNodeRoot))]
    [InitializedInstances(typeof(ReferenceGenerator), typeof(MultiKeyValueMultiFunctionNode))]
    private sealed partial class DIE_Scope_MultiKeyValueMultiFunctionNodeRoot;
}