using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using MrMeeseeks.SourceGeneratorUtility.Extensions;

namespace MsMeeseeks.DIE.Nodes.Elements;

internal interface INullNode : IElementNode
{
    
}

internal partial class NullNode : INullNode
{
    internal NullNode(
        ITypeSymbol nullableType,
        
        IReferenceGenerator referenceGenerator)
    {
        TypeFullName = nullableType.FullName();
        Reference = referenceGenerator.Generate(nullableType);
    }
    public void Build(ImmutableStack<INamedTypeSymbol> implementationStack)
    {
    }

    public string TypeFullName { get; }
    public string Reference { get; }
}