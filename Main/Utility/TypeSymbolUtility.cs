using System.Linq;
using Microsoft.CodeAnalysis;
using MrMeeseeks.SourceGeneratorUtility;
using MrMeeseeks.SourceGeneratorUtility.Extensions;

namespace MsMeeseeks.DIE.Utility;

internal static class TypeSymbolUtility
{
    internal static ITypeSymbol GetUnwrappedType(ITypeSymbol type, WellKnownTypes wellKnownTypes)
    {
        if ((CustomSymbolEqualityComparer.Default.Equals(type.OriginalDefinition, wellKnownTypes.ValueTask1)
             || CustomSymbolEqualityComparer.Default.Equals(type.OriginalDefinition, wellKnownTypes.Task1)
             || CustomSymbolEqualityComparer.Default.Equals(type.OriginalDefinition, wellKnownTypes.Lazy1))
            && type is INamedTypeSymbol namedType)
            return GetUnwrappedType(namedType.TypeArguments.First(), wellKnownTypes);

        if (type.TypeKind == TypeKind.Delegate 
            && type.FullName().StartsWith("global::System.Func<")
            && type is INamedTypeSymbol func)
            return GetUnwrappedType(func.TypeArguments.Last(), wellKnownTypes);

        return type;
    }
    internal static bool IsWrapType(ITypeSymbol type, WellKnownTypes wellKnownTypes) =>
        IsAsyncWrapType(type, wellKnownTypes)
        || CustomSymbolEqualityComparer.Default.Equals(type.OriginalDefinition, wellKnownTypes.Lazy1)
        || type.TypeKind == TypeKind.Delegate && type.FullName().StartsWith("global::System.Func<");

    internal static bool IsAsyncWrapType(ITypeSymbol type, WellKnownTypes wellKnownTypes) =>
        CustomSymbolEqualityComparer.Default.Equals(type.OriginalDefinition, wellKnownTypes.ValueTask1)
        || CustomSymbolEqualityComparer.Default.Equals(type.OriginalDefinition, wellKnownTypes.Task1);
}