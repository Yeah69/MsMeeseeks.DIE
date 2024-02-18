﻿using MrMeeseeks.DIE.Configuration.Attributes;
using MrMeeseeks.SourceGeneratorUtility.Extensions;

namespace MsMeeseeks.DIE;

internal sealed record WellKnownTypesAggregation(
    INamedTypeSymbol ImplementationAggregationAttribute,
    INamedTypeSymbol TransientAbstractionAggregationAttribute,
    INamedTypeSymbol SyncTransientAbstractionAggregationAttribute,
    INamedTypeSymbol AsyncTransientAbstractionAggregationAttribute,
    INamedTypeSymbol ContainerInstanceAbstractionAggregationAttribute,
    INamedTypeSymbol TransientScopeInstanceAbstractionAggregationAttribute,
    INamedTypeSymbol ScopeInstanceAbstractionAggregationAttribute,
    INamedTypeSymbol TransientScopeRootAbstractionAggregationAttribute,
    INamedTypeSymbol ScopeRootAbstractionAggregationAttribute,
    INamedTypeSymbol DecoratorAbstractionAggregationAttribute,
    INamedTypeSymbol CompositeAbstractionAggregationAttribute,
    INamedTypeSymbol TransientImplementationAggregationAttribute,
    INamedTypeSymbol SyncTransientImplementationAggregationAttribute,
    INamedTypeSymbol AsyncTransientImplementationAggregationAttribute,
    INamedTypeSymbol ContainerInstanceImplementationAggregationAttribute,
    INamedTypeSymbol TransientScopeInstanceImplementationAggregationAttribute,
    INamedTypeSymbol ScopeInstanceImplementationAggregationAttribute,
    INamedTypeSymbol TransientScopeRootImplementationAggregationAttribute,
    INamedTypeSymbol ScopeRootImplementationAggregationAttribute,
    INamedTypeSymbol AllImplementationsAggregationAttribute,
    INamedTypeSymbol AssemblyImplementationsAggregationAttribute,
    INamedTypeSymbol FilterImplementationAggregationAttribute,
    INamedTypeSymbol FilterTransientAbstractionAggregationAttribute,
    INamedTypeSymbol FilterSyncTransientAbstractionAggregationAttribute,
    INamedTypeSymbol FilterAsyncTransientAbstractionAggregationAttribute,
    INamedTypeSymbol FilterContainerInstanceAbstractionAggregationAttribute,
    INamedTypeSymbol FilterTransientScopeInstanceAbstractionAggregationAttribute,
    INamedTypeSymbol FilterScopeInstanceAbstractionAggregationAttribute,
    INamedTypeSymbol FilterTransientScopeRootAbstractionAggregationAttribute,
    INamedTypeSymbol FilterScopeRootAbstractionAggregationAttribute,
    INamedTypeSymbol FilterDecoratorAbstractionAggregationAttribute,
    INamedTypeSymbol FilterCompositeAbstractionAggregationAttribute,
    INamedTypeSymbol FilterTransientImplementationAggregationAttribute,
    INamedTypeSymbol FilterSyncTransientImplementationAggregationAttribute,
    INamedTypeSymbol FilterAsyncTransientImplementationAggregationAttribute,
    INamedTypeSymbol FilterContainerInstanceImplementationAggregationAttribute,
    INamedTypeSymbol FilterTransientScopeInstanceImplementationAggregationAttribute,
    INamedTypeSymbol FilterScopeInstanceImplementationAggregationAttribute,
    INamedTypeSymbol FilterTransientScopeRootImplementationAggregationAttribute,
    INamedTypeSymbol FilterScopeRootImplementationAggregationAttribute,
    INamedTypeSymbol FilterAllImplementationsAggregationAttribute,
    INamedTypeSymbol FilterAssemblyImplementationsAggregationAttribute)
{
    internal static WellKnownTypesAggregation Create(Compilation compilation) => new (
        ImplementationAggregationAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(ImplementationAggregationAttribute).FullName ?? ""),
        TransientAbstractionAggregationAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(TransientAbstractionAggregationAttribute).FullName ?? ""),
        SyncTransientAbstractionAggregationAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(SyncTransientAbstractionAggregationAttribute).FullName ?? ""),
        AsyncTransientAbstractionAggregationAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(AsyncTransientAbstractionAggregationAttribute).FullName ?? ""),
        ContainerInstanceAbstractionAggregationAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(ContainerInstanceAbstractionAggregationAttribute).FullName ?? ""),
        TransientScopeInstanceAbstractionAggregationAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(TransientScopeInstanceAbstractionAggregationAttribute).FullName ?? ""),
        ScopeInstanceAbstractionAggregationAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(ScopeInstanceAbstractionAggregationAttribute).FullName ?? ""),
        TransientScopeRootAbstractionAggregationAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(TransientScopeRootAbstractionAggregationAttribute).FullName ?? ""),
        ScopeRootAbstractionAggregationAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(ScopeRootAbstractionAggregationAttribute).FullName ?? ""),
        DecoratorAbstractionAggregationAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(DecoratorAbstractionAggregationAttribute).FullName ?? ""),
        CompositeAbstractionAggregationAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(CompositeAbstractionAggregationAttribute).FullName ?? ""),
        TransientImplementationAggregationAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(TransientImplementationAggregationAttribute).FullName ?? ""),
        SyncTransientImplementationAggregationAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(SyncTransientImplementationAggregationAttribute).FullName ?? ""),
        AsyncTransientImplementationAggregationAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(AsyncTransientImplementationAggregationAttribute).FullName ?? ""),
        ContainerInstanceImplementationAggregationAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(ContainerInstanceImplementationAggregationAttribute).FullName ?? ""),
        TransientScopeInstanceImplementationAggregationAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(TransientScopeInstanceImplementationAggregationAttribute).FullName ?? ""),
        ScopeInstanceImplementationAggregationAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(ScopeInstanceImplementationAggregationAttribute).FullName ?? ""),
        TransientScopeRootImplementationAggregationAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(TransientScopeRootImplementationAggregationAttribute).FullName ?? ""),
        ScopeRootImplementationAggregationAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(ScopeRootImplementationAggregationAttribute).FullName ?? ""),
        AllImplementationsAggregationAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(AllImplementationsAggregationAttribute).FullName ?? ""),
        AssemblyImplementationsAggregationAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(AssemblyImplementationsAggregationAttribute).FullName ?? ""),
        FilterImplementationAggregationAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(FilterImplementationAggregationAttribute).FullName ?? ""),
        FilterTransientAbstractionAggregationAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(FilterTransientAbstractionAggregationAttribute).FullName ?? ""),
        FilterSyncTransientAbstractionAggregationAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(FilterSyncTransientAbstractionAggregationAttribute).FullName ?? ""),
        FilterAsyncTransientAbstractionAggregationAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(FilterAsyncTransientAbstractionAggregationAttribute).FullName ?? ""),
        FilterContainerInstanceAbstractionAggregationAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(FilterContainerInstanceAbstractionAggregationAttribute).FullName ?? ""),
        FilterTransientScopeInstanceAbstractionAggregationAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(FilterTransientScopeInstanceAbstractionAggregationAttribute).FullName ?? ""),
        FilterScopeInstanceAbstractionAggregationAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(FilterScopeInstanceAbstractionAggregationAttribute).FullName ?? ""),
        FilterTransientScopeRootAbstractionAggregationAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(FilterTransientScopeRootAbstractionAggregationAttribute).FullName ?? ""),
        FilterScopeRootAbstractionAggregationAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(FilterScopeRootAbstractionAggregationAttribute).FullName ?? ""),
        FilterDecoratorAbstractionAggregationAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(FilterDecoratorAbstractionAggregationAttribute).FullName ?? ""),
        FilterCompositeAbstractionAggregationAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(FilterCompositeAbstractionAggregationAttribute).FullName ?? ""),
        FilterTransientImplementationAggregationAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(FilterTransientImplementationAggregationAttribute).FullName ?? ""),
        FilterSyncTransientImplementationAggregationAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(FilterSyncTransientImplementationAggregationAttribute).FullName ?? ""),
        FilterAsyncTransientImplementationAggregationAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(FilterAsyncTransientImplementationAggregationAttribute).FullName ?? ""),
        FilterContainerInstanceImplementationAggregationAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(FilterContainerInstanceImplementationAggregationAttribute).FullName ?? ""),
        FilterTransientScopeInstanceImplementationAggregationAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(FilterTransientScopeInstanceImplementationAggregationAttribute).FullName ?? ""),
        FilterScopeInstanceImplementationAggregationAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(FilterScopeInstanceImplementationAggregationAttribute).FullName ?? ""),
        FilterTransientScopeRootImplementationAggregationAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(FilterTransientScopeRootImplementationAggregationAttribute).FullName ?? ""),
        FilterScopeRootImplementationAggregationAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(FilterScopeRootImplementationAggregationAttribute).FullName ?? ""),
        FilterAllImplementationsAggregationAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(FilterAllImplementationsAggregationAttribute).FullName ?? ""),
        FilterAssemblyImplementationsAggregationAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(FilterAssemblyImplementationsAggregationAttribute).FullName ?? ""));
}