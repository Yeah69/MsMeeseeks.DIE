using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using MrMeeseeks.SourceGeneratorUtility.Extensions;

namespace MsMeeseeks.DIE.Nodes.Elements;

internal interface IParameterNode : IElementNode
{
}

internal partial class ParameterNode : IParameterNode
{
    internal ParameterNode(
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