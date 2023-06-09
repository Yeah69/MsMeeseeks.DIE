using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using MrMeeseeks.SourceGeneratorUtility.Extensions;

namespace MsMeeseeks.DIE.Nodes.Elements;

internal interface IOutParameterNode : IElementNode
{
}

internal partial class OutParameterNode : IOutParameterNode
{
    internal OutParameterNode(
        ITypeSymbol type,
        
        IReferenceGenerator referenceGenerator)
    {
        TypeFullName = type.FullName();
        Reference = referenceGenerator.Generate(type);
    }

    public void Build(ImmutableStack<INamedTypeSymbol> implementationStack)
    {
    }

    public string TypeFullName { get; }
    public string Reference { get; }
}