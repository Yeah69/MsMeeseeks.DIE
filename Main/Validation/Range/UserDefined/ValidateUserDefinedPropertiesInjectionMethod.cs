using Microsoft.CodeAnalysis;
using MsMeeseeks.DIE.Contexts;
using MsMeeseeks.DIE.Logging;

namespace MsMeeseeks.DIE.Validation.Range.UserDefined;

internal interface IValidateUserDefinedPropertiesMethod: IValidateUserDefinedInjectionMethod
{
    
}

internal class ValidateUserDefinedPropertiesInjectionMethod : ValidateUserDefinedInjectionMethod, IValidateUserDefinedPropertiesMethod
{
    internal ValidateUserDefinedPropertiesInjectionMethod(
        IContainerWideContext containerWideContext,
        ILocalDiagLogger diagLogger) 
        : base(diagLogger) => 
        InjectionAttribute = containerWideContext.WellKnownTypesMiscellaneous.UserDefinedPropertiesInjectionAttribute;

    protected override INamedTypeSymbol InjectionAttribute { get; }
}