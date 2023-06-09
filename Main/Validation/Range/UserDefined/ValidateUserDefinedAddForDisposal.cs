using System.Linq;
using Microsoft.CodeAnalysis;
using MrMeeseeks.SourceGeneratorUtility;
using MrMeeseeks.SourceGeneratorUtility.Extensions;
using MsMeeseeks.DIE.Contexts;
using MsMeeseeks.DIE.Logging;

namespace MsMeeseeks.DIE.Validation.Range.UserDefined;

internal interface IValidateUserDefinedAddForDisposalSync : IValidateUserDefinedAddForDisposalBase
{
    
}

internal class ValidateUserDefinedAddForDisposalSync : ValidateUserDefinedAddForDisposalBase,
    IValidateUserDefinedAddForDisposalSync
{
    internal ValidateUserDefinedAddForDisposalSync(
        IContainerWideContext containerWideContext,
        ILocalDiagLogger localDiagLogger)
        : base(localDiagLogger) => 
        DisposableType = containerWideContext.WellKnownTypes.IDisposable;

    protected override INamedTypeSymbol DisposableType { get; }
}

internal interface IValidateUserDefinedAddForDisposalAsync : IValidateUserDefinedAddForDisposalBase
{
    
}

internal class ValidateUserDefinedAddForDisposalAsync : ValidateUserDefinedAddForDisposalBase,
    IValidateUserDefinedAddForDisposalAsync
{
    internal ValidateUserDefinedAddForDisposalAsync(
        IContainerWideContext containerWideContext,
        ILocalDiagLogger localDiagLogger)
        : base(localDiagLogger) => 
        DisposableType = containerWideContext.WellKnownTypes.IAsyncDisposable;

    protected override INamedTypeSymbol DisposableType { get; }
}

internal interface IValidateUserDefinedAddForDisposalBase : IValidateUserDefinedMethod
{
    
}

internal abstract class ValidateUserDefinedAddForDisposalBase : ValidateUserDefinedMethod, IValidateUserDefinedAddForDisposalBase
{
    internal ValidateUserDefinedAddForDisposalBase(
        ILocalDiagLogger localDiagLogger) 
        : base(localDiagLogger)
    {
    }
    protected abstract INamedTypeSymbol DisposableType { get; }

    public override void Validate(IMethodSymbol method, INamedTypeSymbol rangeType, INamedTypeSymbol containerType)
    {
        base.Validate(method, rangeType, containerType);

        if (method is
            {
                ReturnsVoid: true,
                IsPartialDefinition: true,
                Parameters.Length: 1,
                MethodKind: MethodKind.Ordinary,
                CanBeReferencedByName: true
            }
            && method.Parameters[0] is
            {
                IsDiscard: false,
                IsOptional: false,
                IsParams: false,
                IsThis: false,
                RefKind: RefKind.None,
                HasExplicitDefaultValue: false
            }
            && CustomSymbolEqualityComparer.Default.Equals(method.Parameters[0].Type, DisposableType))
        {
        }

        if (!method.ReturnsVoid)
            LocalDiagLogger.Error(
                ValidationErrorDiagnostic(method, rangeType, containerType, "Isn't allowed to have a return type."),
                method.Locations.FirstOrDefault() ?? Location.None);
        
        if (!method.IsPartialDefinition)
            LocalDiagLogger.Error(
                ValidationErrorDiagnostic(method, rangeType, containerType, "User-defined part has to have the partial definition only."),
                method.Locations.FirstOrDefault() ?? Location.None);
        
        if (method.Parameters.Length != 1 || !CustomSymbolEqualityComparer.Default.Equals(method.Parameters[0].Type, DisposableType))
            LocalDiagLogger.Error(
                ValidationErrorDiagnostic(method, rangeType, containerType, $"Has to have a single parameter which is of type \"{DisposableType.FullName()}\"."),
                method.Locations.FirstOrDefault() ?? Location.None);
        
        if (method.MethodKind != MethodKind.Ordinary)
            LocalDiagLogger.Error(
                ValidationErrorDiagnostic(method, rangeType, containerType, "Has to be an ordinary method."),
                method.Locations.FirstOrDefault() ?? Location.None);
        
        if (!method.CanBeReferencedByName)
            LocalDiagLogger.Error(
                ValidationErrorDiagnostic(method, rangeType, containerType, "Should be able to be referenced by name."),
                method.Locations.FirstOrDefault() ?? Location.None);

        if (method.Parameters.Length == 1 && method.Parameters[0] is {} parameter)
        {
            if (parameter.IsDiscard)
                LocalDiagLogger.Error(
                ValidationErrorDiagnostic(method, rangeType, containerType, "Parameter isn't allowed to be discard parameter."),
                method.Locations.FirstOrDefault() ?? Location.None);
            
            if (parameter.IsOptional)
                LocalDiagLogger.Error(
                ValidationErrorDiagnostic(method, rangeType, containerType, "Parameter isn't allowed to be optional."),
                method.Locations.FirstOrDefault() ?? Location.None);
            
            if (parameter.IsParams)
                LocalDiagLogger.Error(
                ValidationErrorDiagnostic(method, rangeType, containerType, "Parameter isn't allowed to be params parameter."),
                method.Locations.FirstOrDefault() ?? Location.None);
            
            if (parameter.IsThis)
                LocalDiagLogger.Error(
                ValidationErrorDiagnostic(method, rangeType, containerType, "Parameter isn't allowed to be this parameter."),
                method.Locations.FirstOrDefault() ?? Location.None);
            
            if (parameter.RefKind != RefKind.None)
                LocalDiagLogger.Error(
                ValidationErrorDiagnostic(method, rangeType, containerType, "Parameter isn't allowed to be of a special ref kind."),
                method.Locations.FirstOrDefault() ?? Location.None);
            
            if (parameter.HasExplicitDefaultValue)
                LocalDiagLogger.Error(
                ValidationErrorDiagnostic(method, rangeType, containerType, "Parameter isn't allowed to have explicit default value."),
                method.Locations.FirstOrDefault() ?? Location.None);
        }
    }
}