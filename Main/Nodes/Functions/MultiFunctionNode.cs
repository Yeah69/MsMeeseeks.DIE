using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using MrMeeseeks.SourceGeneratorUtility;
using MrMeeseeks.SourceGeneratorUtility.Extensions;
using MsMeeseeks.DIE.Configuration;
using MsMeeseeks.DIE.Contexts;
using MsMeeseeks.DIE.MsContainer;
using MsMeeseeks.DIE.Nodes.Elements;
using MsMeeseeks.DIE.Nodes.Elements.FunctionCalls;
using MsMeeseeks.DIE.Nodes.Mappers;
using MsMeeseeks.DIE.Nodes.Ranges;
using MsMeeseeks.DIE.Utility;

namespace MsMeeseeks.DIE.Nodes.Functions;

internal interface IMultiFunctionNode : IFunctionNode
{
    IReadOnlyList<IElementNode> ReturnedElements { get; }
    bool IsAsyncEnumerable { get; }
    string ItemTypeFullName { get; }
}

internal partial class MultiFunctionNode : ReturningFunctionNodeBase, IMultiFunctionNode, IScopeInstance
{
    private readonly INamedTypeSymbol _enumerableType;
    private readonly ICheckTypeProperties _checkTypeProperties;
    private readonly Func<IElementNodeMapper> _typeToElementNodeMapperFactory;
    private readonly Func<IElementNodeMapperBase, (INamedTypeSymbol, INamedTypeSymbol), IOverridingElementNodeWithDecorationMapper> _overridingElementNodeWithDecorationMapperFactory;
    private readonly WellKnownTypes _wellKnownTypes;

    internal MultiFunctionNode(
        // parameters
        INamedTypeSymbol enumerableType,
        IReadOnlyList<ITypeSymbol> parameters,
        IContainerNode parentContainer,
        ITransientScopeWideContext transientScopeWideContext,
        IReferenceGenerator referenceGenerator,
        
        // dependencies
        Func<ITypeSymbol, IParameterNode> parameterNodeFactory,
        Func<string?, IReadOnlyList<(IParameterNode, IParameterNode)>, IPlainFunctionCallNode> plainFunctionCallNodeFactory,
        Func<ITypeSymbol, string?, SynchronicityDecision, IReadOnlyList<(IParameterNode, IParameterNode)>, IAsyncFunctionCallNode> asyncFunctionCallNodeFactory,
        Func<(string, string), IScopeNode, IRangeNode, IReadOnlyList<(IParameterNode, IParameterNode)>, IFunctionCallNode?, IScopeCallNode> scopeCallNodeFactory,
        Func<string, ITransientScopeNode, IRangeNode, IReadOnlyList<(IParameterNode, IParameterNode)>, IFunctionCallNode?, ITransientScopeCallNode> transientScopeCallNodeFactory,
        Func<IElementNodeMapper> typeToElementNodeMapperFactory,
        Func<IElementNodeMapperBase, (INamedTypeSymbol, INamedTypeSymbol), IOverridingElementNodeWithDecorationMapper> overridingElementNodeWithDecorationMapperFactory,
        IContainerWideContext containerWideContext)
        : base(
            Microsoft.CodeAnalysis.Accessibility.Private, 
            enumerableType, 
            parameters, 
            ImmutableDictionary.Create<ITypeSymbol, IParameterNode>(CustomSymbolEqualityComparer.IncludeNullability), 
            parentContainer, 
            transientScopeWideContext.Range,
            parameterNodeFactory,
            plainFunctionCallNodeFactory,
            asyncFunctionCallNodeFactory,
            scopeCallNodeFactory,
            transientScopeCallNodeFactory,
            containerWideContext)
    {
        _enumerableType = enumerableType;
        _checkTypeProperties = transientScopeWideContext.CheckTypeProperties;
        _typeToElementNodeMapperFactory = typeToElementNodeMapperFactory;
        _overridingElementNodeWithDecorationMapperFactory = overridingElementNodeWithDecorationMapperFactory;
        _wellKnownTypes = containerWideContext.WellKnownTypes;

        ItemTypeFullName = CollectionUtility.GetCollectionsInnerType(enumerableType).FullName();

        SuppressAsync =
            CustomSymbolEqualityComparer.Default.Equals(enumerableType.OriginalDefinition, containerWideContext.WellKnownTypesCollections.IAsyncEnumerable1);
        IsAsyncEnumerable =
            CustomSymbolEqualityComparer.Default.Equals(enumerableType.OriginalDefinition, containerWideContext.WellKnownTypesCollections.IAsyncEnumerable1);

        ReturnedTypeNameNotWrapped = enumerableType.Name;

        Name = referenceGenerator.Generate("CreateMulti", enumerableType);
    }

    private IElementNodeMapperBase GetMapper(
        ITypeSymbol unwrappedType,
        ITypeSymbol concreteImplementationType)
    {
        var baseMapper = _typeToElementNodeMapperFactory();
        return concreteImplementationType is INamedTypeSymbol namedTypeSymbol && unwrappedType is INamedTypeSymbol namedUnwrappedType
            ? _overridingElementNodeWithDecorationMapperFactory(
                baseMapper,
                (namedUnwrappedType, namedTypeSymbol))
            : baseMapper;
    }

    protected override bool SuppressAsync { get; }

    private IElementNode MapToReturnedElement(IElementNodeMapperBase mapper, ITypeSymbol itemType) =>
        mapper.Map(itemType, ImmutableStack.Create<INamedTypeSymbol>());
    
    public override void Build(ImmutableStack<INamedTypeSymbol> implementationStack)
    {
        base.Build(implementationStack);
        var itemType = CollectionUtility.GetCollectionsInnerType(_enumerableType);
        var unwrappedItemType = TypeSymbolUtility.GetUnwrappedType(itemType, _wellKnownTypes);

        var concreteItemTypes = unwrappedItemType is INamedTypeSymbol namedTypeSymbol
            ? _checkTypeProperties.MapToImplementations(namedTypeSymbol)
            : (IReadOnlyList<ITypeSymbol>) new[] { unwrappedItemType };

        ReturnedElements = concreteItemTypes
            .Select(cit => MapToReturnedElement(
                GetMapper(unwrappedItemType, cit),
                itemType))
            .ToList();
    }

    public override string Name { get; protected set; }
    public override string ReturnedTypeNameNotWrapped { get; }

    public IReadOnlyList<IElementNode> ReturnedElements { get; private set; } = Array.Empty<IElementNode>();
    public bool IsAsyncEnumerable { get; }
    public string ItemTypeFullName { get; }
}