using MsMeeseeks.DIE.Contexts;
using MsMeeseeks.DIE.Logging;

namespace MsMeeseeks.DIE.Validation.Range.UserDefined;

internal interface IValidateUserDefinedConstructorParametersInjectionMethod : IValidateUserDefinedInjectionMethod
{
    
}

internal sealed class ValidateUserDefinedConstructorParametersInjectionMethod : ValidateUserDefinedInjectionMethod, IValidateUserDefinedConstructorParametersInjectionMethod
{
    internal ValidateUserDefinedConstructorParametersInjectionMethod(
        IContainerWideContext containerWideContext,
        ILocalDiagLogger diagLogger) 
        : base(diagLogger) => 
        InjectionAttribute = containerWideContext.WellKnownTypesMiscellaneous.UserDefinedConstructorParametersInjectionAttribute;

    protected override INamedTypeSymbol InjectionAttribute { get; }
}