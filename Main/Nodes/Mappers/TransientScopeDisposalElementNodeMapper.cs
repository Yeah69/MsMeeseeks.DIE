using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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

internal interface ITransientScopeDisposalElementNodeMapper : IElementNodeMapperBase
{
}

internal class TransientScopeDisposalElementNodeMapper : ElementNodeMapperBase, ITransientScopeDisposalElementNodeMapper
{
    private readonly WellKnownTypes _wellKnownTypes;
    private readonly Func<INamedTypeSymbol, ITransientScopeDisposalTriggerNode> _transientScopeDisposalTriggerNodeFactory;

    public TransientScopeDisposalElementNodeMapper(
        IElementNodeMapperBase parentElementNodeMapper,
        
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
        Func<(INamedTypeSymbol, INamedTypeSymbol), IElementNodeMapperBase, IAbstractionNode> abstractionNodeFactory, 
        Func<INamedTypeSymbol, IMethodSymbol, IElementNodeMapperBase, IImplementationNode> implementationNodeFactory, 
        Func<ITypeSymbol, IOutParameterNode> outParameterNodeFactory,
        Func<string, ITypeSymbol, IErrorNode> errorNodeFactory, 
        Func<ITypeSymbol, INullNode> nullNodeFactory,
        Func<IElementNode, IReusedNode> reusedNodeFactory,
        Func<ITypeSymbol, IReadOnlyList<ITypeSymbol>, ImmutableDictionary<ITypeSymbol, IParameterNode>, ILocalFunctionNodeRoot> localFunctionNodeFactory,
        Func<IElementNodeMapperBase, ImmutableQueue<(INamedTypeSymbol, INamedTypeSymbol)>, IOverridingElementNodeMapper> overridingElementNodeMapperFactory,
        Func<INamedTypeSymbol, ITransientScopeDisposalTriggerNode> transientScopeDisposalTriggerNodeFactory) 
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
            abstractionNodeFactory,
            implementationNodeFactory, 
            outParameterNodeFactory,
            errorNodeFactory, 
            nullNodeFactory,
            reusedNodeFactory,
            localFunctionNodeFactory,
            overridingElementNodeMapperFactory)
    {
        _wellKnownTypes = containerWideContext.WellKnownTypes;
        _transientScopeDisposalTriggerNodeFactory = transientScopeDisposalTriggerNodeFactory;
        Next = parentElementNodeMapper;
    }

    protected override IElementNodeMapperBase NextForWraps => this;

    protected override IElementNodeMapperBase Next { get; }

    public override IElementNode Map(ITypeSymbol type, ImmutableStack<INamedTypeSymbol> implementationStack)
    {
        if (type is INamedTypeSymbol namedType
            && (CustomSymbolEqualityComparer.Default.Equals(namedType, _wellKnownTypes.IDisposable)
                || CustomSymbolEqualityComparer.Default.Equals(namedType, _wellKnownTypes.IAsyncDisposable)))
            return _transientScopeDisposalTriggerNodeFactory(namedType);

        return base.Map(type, implementationStack);
    }
}