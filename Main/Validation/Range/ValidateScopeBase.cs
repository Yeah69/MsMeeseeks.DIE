
using MsMeeseeks.DIE.Logging;
using MsMeeseeks.DIE.Utility;
using MsMeeseeks.DIE.Validation.Attributes;
using MsMeeseeks.DIE.Validation.Range.UserDefined;
using MrMeeseeks.SourceGeneratorUtility;
using MrMeeseeks.SourceGeneratorUtility.Extensions;

namespace MsMeeseeks.DIE.Validation.Range;

internal interface IValidateScopeBase : IValidateRange;

internal abstract class ValidateScopeBase : ValidateRange, IValidateScopeBase
{
    private readonly WellKnownTypesMiscellaneous _wellKnownTypesMiscellaneous;
    private readonly IImmutableSet<INamedTypeSymbol> _notAllowedAttributeTypes;

    internal ValidateScopeBase(
        IValidateUserDefinedAddForDisposalSync validateUserDefinedAddForDisposalSync,
        IValidateUserDefinedAddForDisposalAsync validateUserDefinedAddForDisposalAsync,
        IValidateUserDefinedConstructorParametersInjectionMethod validateUserDefinedConstructorParametersInjectionMethod,
        IValidateUserDefinedPropertiesMethod validateUserDefinedPropertiesMethod,
        IValidateUserDefinedInitializerParametersInjectionMethod validateUserDefinedInitializerParametersInjectionMethod,
        IValidateUserDefinedFactoryMethod validateUserDefinedFactoryMethod,
        IValidateUserDefinedFactoryField validateUserDefinedFactoryField,
        IValidateAttributes validateAttributes,
        WellKnownTypes wellKnownTypes,
        WellKnownTypesAggregation wellKnownTypesAggregation,
        WellKnownTypesMiscellaneous wellKnownTypesMiscellaneous,
        ILocalDiagLogger localDiagLogger,
        IRangeUtility rangeUtility) 
        : base(
            validateUserDefinedAddForDisposalSync, 
            validateUserDefinedAddForDisposalAsync,
            validateUserDefinedConstructorParametersInjectionMethod,
            validateUserDefinedPropertiesMethod,
            validateUserDefinedInitializerParametersInjectionMethod,
            validateUserDefinedFactoryMethod,
            validateUserDefinedFactoryField,
            validateAttributes,
            wellKnownTypes,
            wellKnownTypesMiscellaneous,
            localDiagLogger,
            rangeUtility)
    {
        _wellKnownTypesMiscellaneous = wellKnownTypesMiscellaneous;

        _notAllowedAttributeTypes = ImmutableHashSet.Create<INamedTypeSymbol>(
            CustomSymbolEqualityComparer.Default,
            wellKnownTypesAggregation.ContainerInstanceAbstractionAggregationAttribute,
            wellKnownTypesAggregation.ContainerInstanceImplementationAggregationAttribute,
            wellKnownTypesAggregation.FilterContainerInstanceAbstractionAggregationAttribute,
            wellKnownTypesAggregation.FilterContainerInstanceImplementationAggregationAttribute,
            wellKnownTypesAggregation.TransientScopeInstanceAbstractionAggregationAttribute,
            wellKnownTypesAggregation.TransientScopeInstanceImplementationAggregationAttribute,
            wellKnownTypesAggregation.FilterTransientScopeInstanceAbstractionAggregationAttribute,
            wellKnownTypesAggregation.FilterTransientScopeInstanceImplementationAggregationAttribute,
            wellKnownTypesAggregation.TransientScopeRootAbstractionAggregationAttribute,
            wellKnownTypesAggregation.TransientScopeRootImplementationAggregationAttribute,
            wellKnownTypesAggregation.FilterTransientScopeRootAbstractionAggregationAttribute,
            wellKnownTypesAggregation.FilterTransientScopeRootImplementationAggregationAttribute,
            wellKnownTypesAggregation.ScopeRootAbstractionAggregationAttribute,
            wellKnownTypesAggregation.ScopeRootImplementationAggregationAttribute,
            wellKnownTypesAggregation.FilterScopeRootAbstractionAggregationAttribute,
            wellKnownTypesAggregation.FilterScopeRootImplementationAggregationAttribute);
    }

    protected abstract string DefaultScopeName { get; }
    
    protected abstract string CustomScopeName { get; }
    
    protected abstract string ScopeName { get; }

    public override void Validate(INamedTypeSymbol rangeType, INamedTypeSymbol containerType)
    {
        base.Validate(rangeType, containerType);

        if (rangeType.DeclaredAccessibility != Accessibility.Private)
            LocalDiagLogger.Error(
                ValidationErrorDiagnostic(rangeType, containerType, "Has to be declared private."),
                rangeType.Locations.FirstOrDefault() ?? Location.None);
        
        if (rangeType.Name != DefaultScopeName && !rangeType.Name.StartsWith(CustomScopeName, StringComparison.Ordinal))
            LocalDiagLogger.Error(
                ValidationErrorDiagnostic(rangeType, containerType, $"{ScopeName}'s name hast to be either \"{DefaultScopeName}\" if it is the default {ScopeName} or start with \"{CustomScopeName}\" if it is a custom {ScopeName}."),
                rangeType.Locations.FirstOrDefault() ?? Location.None);
        
        foreach (var notAllowedAttributeType in rangeType
                     .GetAttributes()
                     .Select(ad => ad.AttributeClass)
                     .Distinct(CustomSymbolEqualityComparer.Default)
                     .OfType<INamedTypeSymbol>()
                     .Where(nts => _notAllowedAttributeTypes.Contains(nts, CustomSymbolEqualityComparer.Default)))
            LocalDiagLogger.Error(
                ValidationErrorDiagnostic(rangeType, containerType, $"{ScopeName}s aren't allowed to have attributes of type \"{notAllowedAttributeType.FullName()}\"."),
                rangeType.Locations.FirstOrDefault() ?? Location.None);

        var isDefault = rangeType.Name == DefaultScopeName;
        var isCustom = rangeType.Name.StartsWith(CustomScopeName, StringComparison.Ordinal);
        
        if (rangeType.InstanceConstructors.Length > 1 
            || rangeType.InstanceConstructors.Length == 1 
            && !rangeType.InstanceConstructors[0].IsImplicitlyDeclared 
            && rangeType.InstanceConstructors[0].DeclaredAccessibility != Accessibility.Internal)
            LocalDiagLogger.Error(
                ValidationErrorDiagnostic(rangeType, containerType, "(Transient) Scope can have at most one constructor which needs to be 'internal'."),
                rangeType.Locations.FirstOrDefault() ?? Location.None);

        if (isDefault)
        {
            if (rangeType
                .GetAttributes()
                .Any(ad => CustomSymbolEqualityComparer.Default.Equals(
                    ad.AttributeClass,
                    _wellKnownTypesMiscellaneous.CustomScopeForRootTypesAttribute)))
                LocalDiagLogger.Error(
                    ValidationErrorDiagnostic(rangeType, containerType, $"A default (Transient)Scope isn't allowed to have the attribute of type \"{_wellKnownTypesMiscellaneous.CustomScopeForRootTypesAttribute.FullName()}\"."),
                    rangeType.Locations.FirstOrDefault() ?? Location.None);
        }

        if (isCustom)
        {
            if (rangeType
                .GetAttributes()
                .Count(ad => CustomSymbolEqualityComparer.Default.Equals(
                    ad.AttributeClass,
                    _wellKnownTypesMiscellaneous.CustomScopeForRootTypesAttribute)) != 1)
                LocalDiagLogger.Error(
                    ValidationErrorDiagnostic(rangeType, containerType, $"A custom (Transient)Scope has to have exactly one attribute of type \"{_wellKnownTypesMiscellaneous.CustomScopeForRootTypesAttribute.FullName()}\"."),
                    rangeType.Locations.FirstOrDefault() ?? Location.None);
        }
    }
}