using System.Linq;
using System.Reflection.Metadata;
using Microsoft.CodeAnalysis;
using MsMeeseeks.DIE.Logging;

namespace MsMeeseeks.DIE.Validation.Range.UserDefined;

internal interface IValidateUserDefinedMethod
{
    void Validate(IMethodSymbol method, INamedTypeSymbol rangeType, INamedTypeSymbol containerType);
}

internal abstract class ValidateUserDefinedMethod : IValidateUserDefinedMethod
{
    protected readonly ILocalDiagLogger LocalDiagLogger;

    internal ValidateUserDefinedMethod(
        ILocalDiagLogger localDiagLogger) =>
        LocalDiagLogger = localDiagLogger;

    public virtual void Validate(IMethodSymbol method, INamedTypeSymbol rangeType, INamedTypeSymbol containerType)
    {
        if (method is
            {
                DeclaredAccessibility: Accessibility.Private,
                Arity: 0,
                CallingConvention: SignatureCallingConvention.Default,
                IsAsync: false,
                IsConditional: false,
                IsVararg: false,
                IsExtensionMethod: false,
                IsGenericMethod: false,
                IsInitOnly: false,
                IsStatic: false,
                IsImplicitlyDeclared: false
            })
        {
        }
        
        if (method.DeclaredAccessibility != Accessibility.Private)
            LocalDiagLogger.Error(
                ValidationErrorDiagnostic(method, rangeType, containerType, "Has to be private."),
                method.Locations.FirstOrDefault() ?? Location.None);
        
        if (method.Arity != 0)
            LocalDiagLogger.Error(
                ValidationErrorDiagnostic(method, rangeType, containerType, "Has to have an arity of zero."),
                method.Locations.FirstOrDefault() ?? Location.None);
        
        if (method.CallingConvention != SignatureCallingConvention.Default)
            LocalDiagLogger.Error(
                ValidationErrorDiagnostic(method, rangeType, containerType, "Has to have a default calling signature."),
                method.Locations.FirstOrDefault() ?? Location.None);
        
        if (method.IsAsync)
            LocalDiagLogger.Error(
                ValidationErrorDiagnostic(method, rangeType, containerType, "Isn't allowed to be async."),
                method.Locations.FirstOrDefault() ?? Location.None);
        
        if (method.IsConditional)
            LocalDiagLogger.Error(
                ValidationErrorDiagnostic(method, rangeType, containerType, "Isn't allowed to be marked with the ConditionalAttribute."),
                method.Locations.FirstOrDefault() ?? Location.None);
        
        if (method.IsVararg)
            LocalDiagLogger.Error(
                ValidationErrorDiagnostic(method, rangeType, containerType, "Isn't allowed to be CLI VARAG."),
                method.Locations.FirstOrDefault() ?? Location.None);
        
        if (method.IsExtensionMethod)
            LocalDiagLogger.Error(
                ValidationErrorDiagnostic(method, rangeType, containerType, "Isn't allowed to be an extension method."),
                method.Locations.FirstOrDefault() ?? Location.None);
        
        if (method.IsInitOnly)
            LocalDiagLogger.Error(
                ValidationErrorDiagnostic(method, rangeType, containerType, "Isn't allowed to be an init method."),
                method.Locations.FirstOrDefault() ?? Location.None);
        
        if (method.IsStatic)
            LocalDiagLogger.Error(
                ValidationErrorDiagnostic(method, rangeType, containerType, "Isn't allowed to be static."),
                method.Locations.FirstOrDefault() ?? Location.None);
        
        if (method.IsImplicitlyDeclared)
            LocalDiagLogger.Error(
                ValidationErrorDiagnostic(method, rangeType, containerType, "Isn't allowed to be implicitly declared."),
                method.Locations.FirstOrDefault() ?? Location.None);
    }

    protected static DiagLogData ValidationErrorDiagnostic(IMethodSymbol method, INamedTypeSymbol rangeType, INamedTypeSymbol container, string specification) =>
        ErrorLogData.ValidationUserDefinedElement(method, rangeType, container, specification);
}