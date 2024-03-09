using MsMeeseeks.DIE.Logging;

namespace MsMeeseeks.DIE.Validation.Range.UserDefined;

internal interface IValidateUserDefinedPropertiesMethod: IValidateUserDefinedInjectionMethod;

internal sealed class ValidateUserDefinedPropertiesInjectionMethod : ValidateUserDefinedInjectionMethod, IValidateUserDefinedPropertiesMethod
{
    internal ValidateUserDefinedPropertiesInjectionMethod(
        WellKnownTypesMiscellaneous wellKnownTypesMiscellaneous,
        ILocalDiagLogger diagLogger) 
        : base(diagLogger) => 
        InjectionAttribute = wellKnownTypesMiscellaneous.UserDefinedPropertiesInjectionAttribute;

    protected override INamedTypeSymbol InjectionAttribute { get; }
}