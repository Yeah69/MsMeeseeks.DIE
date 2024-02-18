using MsMeeseeks.DIE.Contexts;
using MsMeeseeks.DIE.Logging;

namespace MsMeeseeks.DIE.Validation.Range.UserDefined;

internal interface IValidateUserDefinedInitializerParametersInjectionMethod : IValidateUserDefinedInjectionMethod
{
    
}

internal sealed class ValidateUserDefinedInitializerParametersInjectionMethod : ValidateUserDefinedInjectionMethod, IValidateUserDefinedInitializerParametersInjectionMethod
{
    internal ValidateUserDefinedInitializerParametersInjectionMethod(
        IContainerWideContext containerWideContext,
        ILocalDiagLogger diagLogger) 
        : base(diagLogger) => 
        InjectionAttribute = containerWideContext.WellKnownTypesMiscellaneous.UserDefinedInitializerParametersInjectionAttribute;

    protected override INamedTypeSymbol InjectionAttribute { get; }
}