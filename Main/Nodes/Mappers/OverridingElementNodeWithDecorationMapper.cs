using MsMeeseeks.DIE.Contexts;
using MsMeeseeks.DIE.Logging;
using MsMeeseeks.DIE.Nodes.Elements;
using MsMeeseeks.DIE.Nodes.Elements.Delegates;
using MsMeeseeks.DIE.Nodes.Elements.Factories;
using MsMeeseeks.DIE.Nodes.Elements.Tuples;
using MsMeeseeks.DIE.Nodes.Functions;
using MsMeeseeks.DIE.Nodes.Ranges;
using MsMeeseeks.DIE.Nodes.Roots;
using MsMeeseeks.DIE.Utility;
using MrMeeseeks.SourceGeneratorUtility;

namespace MsMeeseeks.DIE.Nodes.Mappers;

internal interface IOverridingElementNodeWithDecorationMapper : IElementNodeMapperBase
{
}

internal sealed class OverridingElementNodeWithDecorationMapper : ElementNodeMapperBase, IOverridingElementNodeWithDecorationMapper
{
    private readonly (INamedTypeSymbol InterfaceType, INamedTypeSymbol ImplementationType) _override;

    public OverridingElementNodeWithDecorationMapper(
        IElementNodeMapperBase parentElementNodeMapper,
        (INamedTypeSymbol, INamedTypeSymbol) @override,
        
        IFunctionNode parentFunction,
        IContainerNode parentContainer,
        ITransientScopeWideContext transientScopeWideContext,
        ILocalDiagLogger localDiagLogger,
        ITypeParameterUtility typeParameterUtility,
        IContainerWideContext containerWideContext,
        ICheckIterableTypes checkIterableTypes,
        Func<IFieldSymbol, IFactoryFieldNode> factoryFieldNodeFactory, 
        Func<IPropertySymbol, IFactoryPropertyNode> factoryPropertyNodeFactory, 
        Func<IMethodSymbol, IElementNodeMapperBase, IFactoryFunctionNode> factoryFunctionNodeFactory, 
        Func<INamedTypeSymbol, IElementNodeMapperBase, IValueTupleNode> valueTupleNodeFactory, 
        Func<INamedTypeSymbol, IElementNodeMapperBase, IValueTupleSyntaxNode> valueTupleSyntaxNodeFactory, 
        Func<INamedTypeSymbol, IElementNodeMapperBase, ITupleNode> tupleNodeFactory, 
        Func<(INamedTypeSymbol Outer, INamedTypeSymbol Inner), ILocalFunctionNode, IReadOnlyList<ITypeSymbol>, ILazyNode> lazyNodeFactory, 
        Func<(INamedTypeSymbol Outer, INamedTypeSymbol Inner), ILocalFunctionNode, IReadOnlyList<ITypeSymbol>, IThreadLocalNode> threadLocalNodeFactory,
        Func<(INamedTypeSymbol Outer, INamedTypeSymbol Inner), ILocalFunctionNode, IReadOnlyList<ITypeSymbol>, IFuncNode> funcNodeFactory, 
        Func<ITypeSymbol, IEnumerableBasedNode> enumerableBasedNodeFactory,
        Func<INamedTypeSymbol, IKeyValueBasedNode> keyValueBasedNodeFactory,
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
            typeParameterUtility,
            containerWideContext,
            checkIterableTypes,
            factoryFieldNodeFactory, 
            factoryPropertyNodeFactory, 
            factoryFunctionNodeFactory, 
            valueTupleNodeFactory, 
            valueTupleSyntaxNodeFactory, 
            tupleNodeFactory, 
            lazyNodeFactory, 
            threadLocalNodeFactory,
            funcNodeFactory, 
            enumerableBasedNodeFactory,
            keyValueBasedNodeFactory,
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
    }

    protected override IElementNodeMapperBase NextForWraps => this;

    protected override IElementNodeMapperBase Next { get; }

    public override IElementNode Map(ITypeSymbol type, PassedContext passedContext) =>
        CustomSymbolEqualityComparer.Default.Equals(_override.InterfaceType, type) 
        && type is INamedTypeSymbol abstractionType
            ? SwitchInterfaceWithPotentialDecoration(
                abstractionType, 
                _override.ImplementationType, 
                passedContext,
                Next)
            : base.Map(type, passedContext);

    protected override MapperData GetMapperDataForAsyncWrapping() => new OverridingWithDecorationMapperData(_override);
}