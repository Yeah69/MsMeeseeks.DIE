using MsMeeseeks.DIE.Configuration;
using MsMeeseeks.DIE.Logging;
using MsMeeseeks.DIE.Mappers;
using MsMeeseeks.DIE.MsContainer;
using MsMeeseeks.DIE.Nodes.Elements;
using MsMeeseeks.DIE.Nodes.Elements.FunctionCalls;
using MsMeeseeks.DIE.Nodes.Ranges;
using MsMeeseeks.DIE.Utility;

namespace MsMeeseeks.DIE.Nodes.Functions;

internal interface IMultiKeyValueFunctionNode : IMultiFunctionNodeBase;

internal sealed partial class MultiKeyValueFunctionNode : MultiFunctionNodeBase, IMultiKeyValueFunctionNode, IScopeInstance
{
    private readonly INamedTypeSymbol _enumerableType;
    private readonly ILocalDiagLogger _localDiagLogger;
    private readonly Func<INamedTypeSymbol, object, IElementNode, IKeyValuePairNode> _keyValuePairNodeFactory;
    private readonly ICheckTypeProperties _checkTypeProperties;
    private readonly WellKnownTypes _wellKnownTypes;

    internal MultiKeyValueFunctionNode(
        // parameters
        INamedTypeSymbol enumerableType,
        IReadOnlyList<ITypeSymbol> parameters,
        
        // dependencies
        IContainerNode parentContainer,
        IRangeNode parentRange,
        IReferenceGenerator referenceGenerator,
        ILocalDiagLogger localDiagLogger,
        Func<ITypeSymbol, IParameterNode> parameterNodeFactory,
        Func<ITypeSymbol, string?, IReadOnlyList<(IParameterNode, IParameterNode)>, IReadOnlyList<ITypeSymbol>, IPlainFunctionCallNode> plainFunctionCallNodeFactory,
        Func<ITypeSymbol, string?, SynchronicityDecision, IReadOnlyList<(IParameterNode, IParameterNode)>, IReadOnlyList<ITypeSymbol>, IWrappedAsyncFunctionCallNode> asyncFunctionCallNodeFactory,
        Func<ITypeSymbol, (string, string), IScopeNode, IRangeNode, IReadOnlyList<(IParameterNode, IParameterNode)>, IReadOnlyList<ITypeSymbol>, IFunctionCallNode?, ScopeCallNodeOuterMapperParam, IScopeCallNode> scopeCallNodeFactory,
        Func<ITypeSymbol, string, ITransientScopeNode, IRangeNode, IReadOnlyList<(IParameterNode, IParameterNode)>, IReadOnlyList<ITypeSymbol>, IFunctionCallNode?, ScopeCallNodeOuterMapperParam, ITransientScopeCallNode> transientScopeCallNodeFactory,
        Func<IElementNodeMapper> typeToElementNodeMapperFactory,
        Func<IElementNodeMapperBase, (INamedTypeSymbol, INamedTypeSymbol), IOverridingElementNodeWithDecorationMapper> overridingElementNodeWithDecorationMapperFactory,
        Func<INamedTypeSymbol, object, IElementNode, IKeyValuePairNode> keyValuePairNodeFactory,
        ITypeParameterUtility typeParameterUtility,
        ICheckTypeProperties checkTypeProperties,
        WellKnownTypes wellKnownTypes,
        WellKnownTypesCollections wellKnownTypesCollections)
        : base(
            enumerableType, 
            parameters, 
            parentContainer, 
            parameterNodeFactory,
            plainFunctionCallNodeFactory,
            asyncFunctionCallNodeFactory,
            scopeCallNodeFactory,
            transientScopeCallNodeFactory,
            typeToElementNodeMapperFactory,
            overridingElementNodeWithDecorationMapperFactory,
            typeParameterUtility,
            parentRange,
            wellKnownTypes,
            wellKnownTypesCollections)
    {
        _enumerableType = enumerableType;
        _localDiagLogger = localDiagLogger;
        _keyValuePairNodeFactory = keyValuePairNodeFactory;
        _checkTypeProperties = checkTypeProperties;
        _wellKnownTypes = wellKnownTypes;

        Name = referenceGenerator.Generate("CreateMultiKeyValue", _enumerableType);
    }

    private static IElementNode MapToReturnedElement(IElementNodeMapperBase mapper, ITypeSymbol itemType) =>
        mapper.Map(itemType, new(ImmutableStack<INamedTypeSymbol>.Empty, null));
    
    public override void Build(PassedContext passedContext)
    {
        base.Build(passedContext);
        var keyValueType = (INamedTypeSymbol) _enumerableType.TypeArguments[0];
        var itemType = keyValueType.TypeArguments[1];
        if (TypeSymbolUtility.GetUnwrappedType(itemType, _wellKnownTypes) is not INamedTypeSymbol unwrappedItemType)
        {
            _localDiagLogger.Error(ErrorLogData.ResolutionException("The value type of the keyed map is non-iterable, therefore it has to be a named type (class, struct or interface).", _enumerableType, ImmutableStack<INamedTypeSymbol>.Empty), Location.None);
            throw new InvalidOperationException();
        }
        var concreteItemTypesMap = _checkTypeProperties.MapToKeyedImplementations(unwrappedItemType, keyValueType.TypeArguments[0]);

        ReturnedElements = concreteItemTypesMap
            .Select(kvp => _keyValuePairNodeFactory(
                    keyValueType,
                    kvp.Key,
                    MapToReturnedElement(
                        GetMapper(unwrappedItemType, kvp.Value),
                        itemType)))
            .ToList();
    }

    public override string Name { get; protected set; }
}