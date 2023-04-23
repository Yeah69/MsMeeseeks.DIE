using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using MrMeeseeks.SourceGeneratorUtility.Extensions;
using MsMeeseeks.DIE.Nodes.Mappers;

namespace MsMeeseeks.DIE.Nodes.Elements.Tuples;

internal interface ITupleNode : IElementNode
{
    IReadOnlyList<(string Name, IElementNode Node)> Parameters { get; }
}

internal partial class TupleNode : ITupleNode
{
    private readonly INamedTypeSymbol _tupleType;
    private readonly IElementNodeMapperBase _elementNodeMapper;
    private readonly List<(string Name, IElementNode Node)> _parameters = new();

    internal TupleNode(
        INamedTypeSymbol tupleType,
        IElementNodeMapperBase elementNodeMapper,
        
        IReferenceGenerator referenceGenerator)
    {
        _tupleType = tupleType;
        _elementNodeMapper = elementNodeMapper;
        TypeFullName = tupleType.FullName();
        Reference = referenceGenerator.Generate(_tupleType);
    }

    public void Build(ImmutableStack<INamedTypeSymbol> implementationStack)
    {
        var constructor = _tupleType
            .InstanceConstructors
            // Has only one
            .First();
        _parameters.AddRange(constructor
            .Parameters
            .Select(p => (p.Name, _elementNodeMapper.Map(p.Type, implementationStack))));
    }

    public string TypeFullName { get; }
    public string Reference { get; }
    public IReadOnlyList<(string Name, IElementNode Node)> Parameters => _parameters;
}