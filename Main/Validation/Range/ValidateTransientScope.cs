using Microsoft.CodeAnalysis;
using MsMeeseeks.DIE.Contexts;
using MsMeeseeks.DIE.Logging;
using MsMeeseeks.DIE.Validation.Attributes;
using MsMeeseeks.DIE.Validation.Range.UserDefined;

namespace MsMeeseeks.DIE.Validation.Range;

internal interface IValidateTransientScope : IValidateScopeBase
{
}

internal class ValidateTransientScope : ValidateScopeBase, IValidateTransientScope
{
    internal ValidateTransientScope(
        IValidateUserDefinedAddForDisposalSync validateUserDefinedAddForDisposalSync,
        IValidateUserDefinedAddForDisposalAsync validateUserDefinedAddForDisposalAsync,
        IValidateUserDefinedConstructorParametersInjectionMethod validateUserDefinedConstructorParametersInjectionMethod,
        IValidateUserDefinedPropertiesMethod validateUserDefinedPropertiesMethod,
        IValidateUserDefinedInitializerParametersInjectionMethod validateUserDefinedInitializerParametersInjectionMethod,
        IValidateUserDefinedFactoryMethod validateUserDefinedFactoryMethod,
        IValidateUserDefinedFactoryField validateUserDefinedFactoryField,
        IValidateAttributes validateAttributes,
        IContainerWideContext containerWideContext,
        ILocalDiagLogger localDiagLogger) 
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
            localDiagLogger)
    {
        
    }

    protected override string DefaultScopeName => Constants.DefaultTransientScopeName;
    protected override string CustomScopeName => Constants.CustomTransientScopeName;
    protected override string ScopeName => Constants.TransientScopeName;

    protected override DiagLogData ValidationErrorDiagnostic(INamedTypeSymbol rangeType, INamedTypeSymbol containerType, string specification) => 
        ErrorLogData.ValidationTransientScope(rangeType, containerType, specification);
}