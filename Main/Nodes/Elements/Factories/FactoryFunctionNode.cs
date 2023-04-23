using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using MsMeeseeks.DIE.Contexts;
using MsMeeseeks.DIE.Nodes.Functions;
using MsMeeseeks.DIE.Nodes.Mappers;

namespace MsMeeseeks.DIE.Nodes.Elements.Factories;

internal interface IFactoryFunctionNode : IFactoryNodeBase
{
    IReadOnlyList<(string Name, IElementNode Element)> Parameters { get; }
}

internal partial class FactoryFunctionNode : FactoryNodeBase, IFactoryFunctionNode
{
    private readonly IMethodSymbol _methodSymbol;
    private readonly IElementNodeMapperBase _elementNodeMapperBase;
    private readonly List<(string, IElementNode)> _parameters = new ();

    internal FactoryFunctionNode(
        IMethodSymbol methodSymbol,
        IElementNodeMapperBase elementNodeMapperBase,
        
        IFunctionNode parentFunction,
        IReferenceGenerator referenceGenerator,
        IContainerWideContext containerWideContext) 
        : base(methodSymbol.ReturnType, methodSymbol, parentFunction, referenceGenerator, containerWideContext)
    {
        _methodSymbol = methodSymbol;
        _elementNodeMapperBase = elementNodeMapperBase;
    }

    public override void Build(ImmutableStack<INamedTypeSymbol> implementationStack)
    {
        _parameters.AddRange(_methodSymbol
            .Parameters
            .Select(p => (p.Name, _elementNodeMapperBase.Map(p.Type, implementationStack))));
        base.Build(implementationStack);
    }
    
    public IReadOnlyList<(string, IElementNode)> Parameters => _parameters;
}