﻿using MrMeeseeks.DIE.Configuration.Attributes;
using MsMeeseeks.DIE.MsContainer;
using MrMeeseeks.SourceGeneratorUtility.Extensions;

namespace MsMeeseeks.DIE;

internal sealed record WellKnownTypesChoice(
    INamedTypeSymbol GenericParameterSubstitutesChoiceAttribute,
    INamedTypeSymbol GenericParameterChoiceAttribute,
    INamedTypeSymbol DecoratorSequenceChoiceAttribute,
    INamedTypeSymbol ConstructorChoiceAttribute,
    INamedTypeSymbol PropertyChoiceAttribute,
    INamedTypeSymbol ImplementationChoiceAttribute,
    INamedTypeSymbol ImplementationCollectionChoiceAttribute,
    INamedTypeSymbol InjectionKeyChoiceAttribute,
    INamedTypeSymbol DecorationOrdinalChoiceAttribute,
    INamedTypeSymbol FilterGenericParameterSubstitutesChoiceAttribute,
    INamedTypeSymbol FilterGenericParameterChoiceAttribute,
    INamedTypeSymbol FilterDecoratorSequenceChoiceAttribute,
    INamedTypeSymbol FilterConstructorChoiceAttribute,
    INamedTypeSymbol FilterPropertyChoiceAttribute,
    INamedTypeSymbol FilterImplementationChoiceAttribute,
    INamedTypeSymbol FilterImplementationCollectionChoiceAttribute,
    INamedTypeSymbol FilterInjectionKeyChoiceAttribute,
    INamedTypeSymbol FilterDecorationOrdinalChoiceAttribute)
    : IContainerInstance
{
    internal static WellKnownTypesChoice Create(Compilation compilation) => new (
        ImplementationChoiceAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(ImplementationChoiceAttribute).FullName ?? ""),
        ImplementationCollectionChoiceAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(ImplementationCollectionChoiceAttribute).FullName ?? ""),
        GenericParameterSubstitutesChoiceAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(GenericParameterSubstitutesChoiceAttribute).FullName ?? ""),
        GenericParameterChoiceAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(GenericParameterChoiceAttribute).FullName ?? ""),
        DecoratorSequenceChoiceAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(DecoratorSequenceChoiceAttribute).FullName ?? ""),
        ConstructorChoiceAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(ConstructorChoiceAttribute).FullName ?? ""),
        PropertyChoiceAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(PropertyChoiceAttribute).FullName ?? ""),
        InjectionKeyChoiceAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(InjectionKeyChoiceAttribute).FullName ?? ""),
        DecorationOrdinalChoiceAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(DecorationOrdinalChoiceAttribute).FullName ?? ""),
        FilterImplementationChoiceAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(FilterImplementationChoiceAttribute).FullName ?? ""),
        FilterImplementationCollectionChoiceAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(FilterImplementationCollectionChoiceAttribute).FullName ?? ""),
        FilterGenericParameterSubstitutesChoiceAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(FilterGenericParameterSubstitutesChoiceAttribute).FullName ?? ""),
        FilterGenericParameterChoiceAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(FilterGenericParameterChoiceAttribute).FullName ?? ""),
        FilterDecoratorSequenceChoiceAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(FilterDecoratorSequenceChoiceAttribute).FullName ?? ""),
        FilterConstructorChoiceAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(FilterConstructorChoiceAttribute).FullName ?? ""),
        FilterPropertyChoiceAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(FilterPropertyChoiceAttribute).FullName ?? ""),
        FilterInjectionKeyChoiceAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(FilterInjectionKeyChoiceAttribute).FullName ?? ""),
        FilterDecorationOrdinalChoiceAttribute: compilation.GetTypeByMetadataNameOrThrow(typeof(FilterDecorationOrdinalChoiceAttribute).FullName ?? ""));
}