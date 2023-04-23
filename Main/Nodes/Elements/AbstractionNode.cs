using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using MrMeeseeks.SourceGeneratorUtility.Extensions;
using MsMeeseeks.DIE.Nodes.Mappers;

namespace MsMeeseeks.DIE.Nodes.Elements;

internal interface IAbstractionNode : IElementNode
{
    IElementNode Implementation { get; }
}

internal partial class AbstractionNode : IAbstractionNode
{
    private readonly INamedTypeSymbol _implementationType;
    private readonly IElementNodeMapperBase _mapper;

    internal AbstractionNode(
        (INamedTypeSymbol Abstraction, INamedTypeSymbol Implementation) types, 
        IElementNodeMapperBase mapper,
        
        IReferenceGenerator referenceGenerator)
    {
        _implementationType = types.Implementation;
        _mapper = mapper;
        TypeFullName = types.Abstraction.FullName();
        Reference = referenceGenerator.Generate(types.Abstraction);
    }

    public void Build(ImmutableStack<INamedTypeSymbol> implementationStack)
    {
        Implementation = _mapper.MapToImplementation(
            new(true, true, true), 
            _implementationType, 
            implementationStack);
    }

    public string TypeFullName { get; }
    public string Reference { get; }
    public IElementNode Implementation { get; private set; } = null!;
}