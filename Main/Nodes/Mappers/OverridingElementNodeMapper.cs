using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using MrMeeseeks.SourceGeneratorUtility;
using MsMeeseeks.DIE.Contexts;
using MsMeeseeks.DIE.Logging;
using MsMeeseeks.DIE.Nodes.Elements;
using MsMeeseeks.DIE.Nodes.Elements.Delegates;
using MsMeeseeks.DIE.Nodes.Elements.Factories;
using MsMeeseeks.DIE.Nodes.Elements.Tuples;
using MsMeeseeks.DIE.Nodes.Functions;
using MsMeeseeks.DIE.Nodes.Ranges;
using MsMeeseeks.DIE.Nodes.Roots;

namespace MsMeeseeks.DIE.Nodes.Mappers;

internal interface IOverridingElementNodeMapper : IElementNodeMapperBase
{
}

internal class OverridingElementNodeMapper : ElementNodeMapperBase, IOverridingElementNodeMapper
{
    private readonly ImmutableQueue<(INamedTypeSymbol InterfaceType, INamedTypeSymbol ImplementationType)> _override;
    private readonly Func<IElementNodeMapperBase, ImmutableQueue<(INamedTypeSymbol, INamedTypeSymbol)>, IOverridingElementNodeMapper> _overridingElementNodeMapperFactory;

    public OverridingElementNodeMapper(
        IElementNodeMapperBase parentElementNodeMapper,
        ImmutableQueue<(INamedTypeSymbol, INamedTypeSymbol)> @override,
        
        IFunctionNode parentFunction,
        IContainerNode parentContainer,
        ITransientScopeWideContext transientScopeWideContext,
        ILocalDiagLogger localDiagLogger,
        IContainerWideContext containerWideContext,
        Func<IFieldSymbol, IFactoryFieldNode> factoryFieldNodeFactory, 
        Func<IPropertySymbol, IFactoryPropertyNode> factoryPropertyNodeFactory, 
        Func<IMethodSymbol, IElementNodeMapperBase, IFactoryFunctionNode> factoryFunctionNodeFactory, 
        Func<INamedTypeSymbol, IElementNodeMapperBase, IValueTupleNode> valueTupleNodeFactory, 
        Func<INamedTypeSymbol, IElementNodeMapperBase, IValueTupleSyntaxNode> valueTupleSyntaxNodeFactory, 
        Func<INamedTypeSymbol, IElementNodeMapperBase, ITupleNode> tupleNodeFactory, 
        Func<INamedTypeSymbol, ILocalFunctionNode, ILazyNode> lazyNodeFactory, 
        Func<INamedTypeSymbol, ILocalFunctionNode, IFuncNode> funcNodeFactory, 
        Func<ITypeSymbol, IEnumerableBasedNode> enumerableBasedNodeFactory,
        Func<INamedTypeSymbol?, INamedTypeSymbol, IMethodSymbol, IElementNodeMapperBase, IImplementationNode> implementationNodeFactory, 
        Func<ITypeSymbol, IOutParameterNode> outParameterNodeFactory,
        Func<string, ITypeSymbol, IErrorNode> errorNodeFactory, 
        Func<ITypeSymbol, INullNode> nullNodeFactory,
        Func<IElementNode, IReusedNode> reusedNodeFactory,
        Func<ITypeSymbol, IReadOnlyList<ITypeSymbol>, ImmutableDictionary<ITypeSymbol, IParameterNode>, ILocalFunctionNodeRoot> localFunctionNodeFactory,
        Func<IElementNodeMapperBase, ImmutableQueue<(INamedTypeSymbol, INamedTypeSymbol)>, IOverridingElementNodeMapper> overridingElementNodeMapperFactory) 
        : base(parentFunction, 
            transientScopeWideContext.Range, 
            parentContainer, 
            transientScopeWideContext, 
            localDiagLogger, 
            containerWideContext,
            factoryFieldNodeFactory, 
            factoryPropertyNodeFactory, 
            factoryFunctionNodeFactory, 
            valueTupleNodeFactory, 
            valueTupleSyntaxNodeFactory, 
            tupleNodeFactory, 
            lazyNodeFactory, 
            funcNodeFactory, 
            enumerableBasedNodeFactory,
            implementationNodeFactory, 
            outParameterNodeFactory,
            errorNodeFactory, 
            nullNodeFactory,
            reusedNodeFactory,
            localFunctionNodeFactory,
            overridingElementNodeMapperFactory)
    {
        Next = parentElementNodeMapper;
        _override = @override;
        _overridingElementNodeMapperFactory = overridingElementNodeMapperFactory;
    }

    protected override IElementNodeMapperBase NextForWraps => this;

    protected override IElementNodeMapperBase Next { get; }

    public override IElementNode Map(ITypeSymbol type, ImmutableStack<INamedTypeSymbol> implementationStack)
    {
        if (_override.Any() 
            && type is INamedTypeSymbol abstraction 
            && CustomSymbolEqualityComparer.Default.Equals(_override.Peek().InterfaceType, type))
        {
            var nextOverride = _override.Dequeue(out var currentOverride);
            var mapper = _overridingElementNodeMapperFactory(this, nextOverride);
            return SwitchImplementation(
                new(true, true, true),
                abstraction,
                currentOverride.ImplementationType,
                implementationStack,
                mapper);
        }
        return base.Map(type, implementationStack);
    }

    protected override MapperData GetMapperDataForAsyncWrapping() => new OverridingMapperData(_override);
}