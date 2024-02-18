using MsMeeseeks.DIE.Contexts;
using MsMeeseeks.DIE.Logging;
using MsMeeseeks.DIE.Utility;
using MsMeeseeks.DIE.Validation.Attributes;
using MsMeeseeks.DIE.Validation.Range.UserDefined;

namespace MsMeeseeks.DIE.Validation.Range;

internal interface IValidateScope : IValidateScopeBase
{
}

internal sealed class ValidateScope : ValidateScopeBase, IValidateScope
{
    internal ValidateScope(
        IValidateUserDefinedAddForDisposalSync validateUserDefinedAddForDisposalSync,
        IValidateUserDefinedAddForDisposalAsync validateUserDefinedAddForDisposalAsync,
        IValidateUserDefinedConstructorParametersInjectionMethod validateUserDefinedConstructorParametersInjectionMethod,
        IValidateUserDefinedPropertiesMethod validateUserDefinedPropertiesMethod,
        IValidateUserDefinedInitializerParametersInjectionMethod validateUserDefinedInitializerParametersInjectionMethod,
        IValidateUserDefinedFactoryMethod validateUserDefinedFactoryMethod,
        IValidateUserDefinedFactoryField validateUserDefinedFactoryField,
        IValidateAttributes validateAttributes,
        IContainerWideContext containerWideContext,
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
            containerWideContext,
            localDiagLogger,
            rangeUtility)
    {
        
    }

    protected override string DefaultScopeName => Constants.DefaultScopeName;
    protected override string CustomScopeName => Constants.CustomScopeName;
    protected override string ScopeName => Constants.ScopeName;

    protected override DiagLogData ValidationErrorDiagnostic(INamedTypeSymbol rangeType, INamedTypeSymbol containerType, string specification) => 
        ErrorLogData.ValidationScope(rangeType, containerType, specification);
}