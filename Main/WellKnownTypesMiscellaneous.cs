﻿using Microsoft.CodeAnalysis;
using MrMeeseeks.DIE.Configuration.Attributes;
using MrMeeseeks.SourceGeneratorUtility.Extensions;

namespace MsMeeseeks.DIE;

internal record WellKnownTypesMiscellaneous(
    INamedTypeSymbol InitializerAttribute,
    INamedTypeSymbol FilterInitializerAttribute,
    INamedTypeSymbol CustomScopeForRootTypesAttribute,
    INamedTypeSymbol InitializedInstancesAttribute,
    INamedTypeSymbol UserDefinedConstructorParametersInjectionAttribute,
    INamedTypeSymbol UserDefinedPropertiesInjectionAttribute,
    INamedTypeSymbol UserDefinedInitializerParametersInjectionAttribute,
    INamedTypeSymbol CreateFunctionAttribute,
    INamedTypeSymbol ErrorDescriptionInsteadOfBuildFailureAttribute,
    INamedTypeSymbol AnalyticsAttribute,
    INamedTypeSymbol DieExceptionKind)
{
    internal static WellKnownTypesMiscellaneous Create(Compilation compilation) => new (
        InitializerAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(InitializerAttribute).FullName ?? ""),
        FilterInitializerAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(FilterInitializerAttribute).FullName ?? ""),
        CustomScopeForRootTypesAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(CustomScopeForRootTypesAttribute).FullName ?? ""),
        InitializedInstancesAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(InitializedInstancesAttribute).FullName ?? ""),
        UserDefinedConstructorParametersInjectionAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(UserDefinedConstructorParametersInjectionAttribute).FullName ?? ""),
        UserDefinedPropertiesInjectionAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(UserDefinedPropertiesInjectionAttribute).FullName ?? ""),
        UserDefinedInitializerParametersInjectionAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(UserDefinedInitializerParametersInjectionAttribute).FullName ?? ""), 
        CreateFunctionAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(CreateFunctionAttribute).FullName ?? ""),
        ErrorDescriptionInsteadOfBuildFailureAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(ErrorDescriptionInsteadOfBuildFailureAttribute).FullName ?? ""),
        AnalyticsAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(AnalyticsAttribute).FullName ?? ""),
        DieExceptionKind: compilation.GetTypeByMetadataNameOrThrow(typeof(DieExceptionKind).FullName ?? ""));
}