using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using MrMeeseeks.SourceGeneratorUtility.Extensions;
using MsMeeseeks.DIE.Nodes.Mappers;

namespace MsMeeseeks.DIE.Nodes.Elements.Tuples;

internal interface IValueTupleNode : IElementNode
{
    IReadOnlyList<(string Name, IElementNode Node)> Parameters { get; }
}

internal partial class ValueTupleNode : IValueTupleNode
{
    private readonly INamedTypeSymbol _valueTupleType;
    private readonly IElementNodeMapperBase _elementNodeMapper;
    private readonly List<(string Name, IElementNode Node)> _parameters = new();

    internal ValueTupleNode(
        INamedTypeSymbol valueTupleType,
        IElementNodeMapperBase elementNodeMapper,
        
        IReferenceGenerator referenceGenerator)
    {
        _valueTupleType = valueTupleType;
        _elementNodeMapper = elementNodeMapper;
        TypeFullName = valueTupleType.FullName();
        Reference = referenceGenerator.Generate(_valueTupleType);
    }

    public void Build(ImmutableStack<INamedTypeSymbol> implementationStack)
    {
        var constructor = _valueTupleType
            .InstanceConstructors
            // Don't take the parameterless (struct)-constructor
            .First(c => c.Parameters.Length > 0);
        _parameters.AddRange(constructor
            .Parameters
            .Select(p => (p.Name, _elementNodeMapper.Map(p.Type, implementationStack))));
    }

    public string TypeFullName { get; }
    public string Reference { get; }
    public IReadOnlyList<(string Name, IElementNode Node)> Parameters => _parameters;
}