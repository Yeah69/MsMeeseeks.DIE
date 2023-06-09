using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using MrMeeseeks.SourceGeneratorUtility;
using MrMeeseeks.SourceGeneratorUtility.Extensions;
using MsMeeseeks.DIE.Configuration;
using MsMeeseeks.DIE.Contexts;
using MsMeeseeks.DIE.Extensions;
using MsMeeseeks.DIE.Logging;
using MsMeeseeks.DIE.Nodes.Elements;
using MsMeeseeks.DIE.Nodes.Elements.Delegates;
using MsMeeseeks.DIE.Nodes.Elements.Factories;
using MsMeeseeks.DIE.Nodes.Elements.Tuples;
using MsMeeseeks.DIE.Nodes.Functions;
using MsMeeseeks.DIE.Nodes.Ranges;
using MsMeeseeks.DIE.Nodes.Roots;

namespace MsMeeseeks.DIE.Nodes.Mappers;

internal interface IElementNodeMapperBase
{
    IElementNode Map(ITypeSymbol type, ImmutableStack<INamedTypeSymbol> implementationStack);
    IElementNode MapToImplementation(
        ImplementationMappingConfiguration config,
        INamedTypeSymbol? abstractionType,
        INamedTypeSymbol implementationType,
        ImmutableStack<INamedTypeSymbol> implementationStack);
    IElementNode MapToOutParameter(ITypeSymbol type, ImmutableStack<INamedTypeSymbol> implementationStack);
}

internal record ImplementationMappingConfiguration(
    bool CheckForScopeRoot,
    bool CheckForRangedInstance,
    bool CheckForInitializedInstance);

internal abstract class ElementNodeMapperBase : IElementNodeMapperBase
{
    protected readonly IFunctionNode ParentFunction;
    protected readonly IRangeNode ParentRange;
    private readonly IContainerNode _parentContainer;
    private readonly ILocalDiagLogger _localDiagLogger;
    private readonly IUserDefinedElements _userDefinedElements;
    private readonly ICheckTypeProperties _checkTypeProperties;
    protected readonly WellKnownTypes WellKnownTypes;
    private readonly WellKnownTypesCollections _wellKnownTypesCollections;
    private readonly Func<IFieldSymbol, IFactoryFieldNode> _factoryFieldNodeFactory;
    private readonly Func<IPropertySymbol, IFactoryPropertyNode> _factoryPropertyNodeFactory;
    private readonly Func<IMethodSymbol, IElementNodeMapperBase, IFactoryFunctionNode> _factoryFunctionNodeFactory;
    private readonly Func<INamedTypeSymbol, IElementNodeMapperBase, IValueTupleNode> _valueTupleNodeFactory;
    private readonly Func<INamedTypeSymbol, IElementNodeMapperBase, IValueTupleSyntaxNode> _valueTupleSyntaxNodeFactory;
    private readonly Func<INamedTypeSymbol, IElementNodeMapperBase, ITupleNode> _tupleNodeFactory;
    private readonly Func<INamedTypeSymbol, ILocalFunctionNode, ILazyNode> _lazyNodeFactory;
    private readonly Func<INamedTypeSymbol, ILocalFunctionNode, IFuncNode> _funcNodeFactory;
    private readonly Func<ITypeSymbol, IEnumerableBasedNode> _enumerableBasedNodeFactory;
    private readonly Func<INamedTypeSymbol?, INamedTypeSymbol, IMethodSymbol, IElementNodeMapperBase, IImplementationNode> _implementationNodeFactory;
    private readonly Func<ITypeSymbol, IOutParameterNode> _outParameterNodeFactory;
    private readonly Func<string, ITypeSymbol, IErrorNode> _errorNodeFactory;
    private readonly Func<ITypeSymbol, INullNode> _nullNodeFactory;
    private readonly Func<IElementNode, IReusedNode> _reusedNodeFactory;
    private readonly Func<ITypeSymbol, IReadOnlyList<ITypeSymbol>, ImmutableDictionary<ITypeSymbol, IParameterNode>, ILocalFunctionNodeRoot> _localFunctionNodeFactory;
    private readonly Func<IElementNodeMapperBase, ImmutableQueue<(INamedTypeSymbol, INamedTypeSymbol)>, IOverridingElementNodeMapper> _overridingElementNodeMapperFactory;
    
    internal ElementNodeMapperBase(
        IFunctionNode parentFunction,
        IRangeNode parentRange,
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
    {
        ParentFunction = parentFunction;
        ParentRange = parentRange;
        _parentContainer = parentContainer;
        _localDiagLogger = localDiagLogger;
        _userDefinedElements = transientScopeWideContext.UserDefinedElements;
        _checkTypeProperties = transientScopeWideContext.CheckTypeProperties;
        WellKnownTypes = containerWideContext.WellKnownTypes;
        _wellKnownTypesCollections = containerWideContext.WellKnownTypesCollections;
        _factoryFieldNodeFactory = factoryFieldNodeFactory;
        _factoryPropertyNodeFactory = factoryPropertyNodeFactory;
        _factoryFunctionNodeFactory = factoryFunctionNodeFactory;
        _valueTupleNodeFactory = valueTupleNodeFactory;
        _valueTupleSyntaxNodeFactory = valueTupleSyntaxNodeFactory;
        _tupleNodeFactory = tupleNodeFactory;
        _lazyNodeFactory = lazyNodeFactory;
        _funcNodeFactory = funcNodeFactory;
        _enumerableBasedNodeFactory = enumerableBasedNodeFactory;
        _implementationNodeFactory = implementationNodeFactory;
        _outParameterNodeFactory = outParameterNodeFactory;
        _errorNodeFactory = errorNodeFactory;
        _nullNodeFactory = nullNodeFactory;
        _reusedNodeFactory = reusedNodeFactory;
        _localFunctionNodeFactory = localFunctionNodeFactory;
        _overridingElementNodeMapperFactory = overridingElementNodeMapperFactory;
    }
    
    protected abstract IElementNodeMapperBase NextForWraps { get; }

    protected abstract IElementNodeMapperBase Next { get; }

    protected virtual MapperData GetMapperDataForAsyncWrapping() => 
        new VanillaMapperData();

    public virtual IElementNode Map(ITypeSymbol type, ImmutableStack<INamedTypeSymbol> implementationStack)
    {
        if (ParentFunction.Overrides.TryGetValue(type, out var tuple))
            return tuple;

        if (_userDefinedElements.GetFactoryFieldFor(type) is { } instance)
            return _factoryFieldNodeFactory(instance)
                .EnqueueBuildJobTo(_parentContainer.BuildQueue, implementationStack);

        if (_userDefinedElements.GetFactoryPropertyFor(type) is { } property)
            return _factoryPropertyNodeFactory(property)
                .EnqueueBuildJobTo(_parentContainer.BuildQueue, implementationStack);

        if (_userDefinedElements.GetFactoryMethodFor(type) is { } method)
            return _factoryFunctionNodeFactory(method, Next)
                .EnqueueBuildJobTo(_parentContainer.BuildQueue, implementationStack);

        if (CustomSymbolEqualityComparer.Default.Equals(type.OriginalDefinition, WellKnownTypes.ValueTask1)
            && type is INamedTypeSymbol valueTask)
            return ParentRange.BuildAsyncCreateCall(GetMapperDataForAsyncWrapping(), valueTask.TypeArguments[0], SynchronicityDecision.AsyncValueTask, ParentFunction);

        if (CustomSymbolEqualityComparer.Default.Equals(type.OriginalDefinition, WellKnownTypes.Task1)
            && type is INamedTypeSymbol task)
            return ParentRange.BuildAsyncCreateCall(GetMapperDataForAsyncWrapping(), task.TypeArguments[0], SynchronicityDecision.AsyncTask, ParentFunction);

        if (type.FullName().StartsWith("global::System.ValueTuple<") && type is INamedTypeSymbol valueTupleType)
            return _valueTupleNodeFactory(valueTupleType, NextForWraps)
                .EnqueueBuildJobTo(_parentContainer.BuildQueue, implementationStack);
        
        if (type.FullName().StartsWith("(") && type.FullName().EndsWith(")") && type is INamedTypeSymbol syntaxValueTupleType)
            return _valueTupleSyntaxNodeFactory(syntaxValueTupleType, NextForWraps)
                .EnqueueBuildJobTo(_parentContainer.BuildQueue, implementationStack);

        if (type.FullName().StartsWith("global::System.Tuple<") && type is INamedTypeSymbol tupleType)
            return _tupleNodeFactory(tupleType, NextForWraps)
                .EnqueueBuildJobTo(_parentContainer.BuildQueue, implementationStack);

        if (CustomSymbolEqualityComparer.Default.Equals(type.OriginalDefinition, WellKnownTypes.Lazy1)
            && type is INamedTypeSymbol lazyType)
        {
            if (lazyType.TypeArguments.SingleOrDefault() is not { } valueType)
            {
                return _errorNodeFactory(lazyType.TypeArguments.Length switch 
                        {
                            0 => "Lazy: No type argument",
                            > 1 => "Lazy: more than one type argument",
                            _ => $"Lazy: {lazyType.TypeArguments.First().FullName()} is not a type symbol",
                        },
                        type)
                    .EnqueueBuildJobTo(_parentContainer.BuildQueue, implementationStack);
            }

            var function = _localFunctionNodeFactory(
                valueType,
                Array.Empty<ITypeSymbol>(),
                ParentFunction.Overrides)
                .Function
                .EnqueueBuildJobTo(_parentContainer.BuildQueue, implementationStack);
            ParentFunction.AddLocalFunction(function);
            
            return _lazyNodeFactory(lazyType, function)
                .EnqueueBuildJobTo(_parentContainer.BuildQueue, implementationStack);
        }

        if (type.TypeKind == TypeKind.Delegate 
            && type.FullName().StartsWith("global::System.Func<")
            && type is INamedTypeSymbol funcType)
        {
            if (funcType.TypeArguments.LastOrDefault() is not { } returnType)
            {
                return _errorNodeFactory(funcType.TypeArguments.Length switch
                        {
                            0 => "Func: No type argument",
                            _ => $"Func: {funcType.TypeArguments.Last().FullName()} is not a type symbol",
                        },
                        type)
                    .EnqueueBuildJobTo(_parentContainer.BuildQueue, implementationStack);
            }
            
            var lambdaParameters = funcType
                .TypeArguments
                .Take(funcType.TypeArguments.Length - 1)
                .ToArray();

            var function = _localFunctionNodeFactory(
                returnType,
                lambdaParameters,
                ParentFunction.Overrides)
                .Function
                .EnqueueBuildJobTo(_parentContainer.BuildQueue, implementationStack);
            ParentFunction.AddLocalFunction(function);
            
            return _funcNodeFactory(funcType, function)
                .EnqueueBuildJobTo(_parentContainer.BuildQueue, implementationStack);
        }
        
        if (CustomSymbolEqualityComparer.Default.Equals(type.OriginalDefinition, _wellKnownTypesCollections.IEnumerable1)
            || CustomSymbolEqualityComparer.Default.Equals(type.OriginalDefinition, _wellKnownTypesCollections.IAsyncEnumerable1)
            || type is IArrayTypeSymbol
            || CustomSymbolEqualityComparer.Default.Equals(type.OriginalDefinition, _wellKnownTypesCollections.IList1)
            || CustomSymbolEqualityComparer.Default.Equals(type.OriginalDefinition, _wellKnownTypesCollections.ICollection1)
            || CustomSymbolEqualityComparer.Default.Equals(type.OriginalDefinition, _wellKnownTypesCollections.ReadOnlyCollection1)
            || CustomSymbolEqualityComparer.Default.Equals(type.OriginalDefinition, _wellKnownTypesCollections.IReadOnlyCollection1)
            || CustomSymbolEqualityComparer.Default.Equals(type.OriginalDefinition, _wellKnownTypesCollections.IReadOnlyList1)
            || CustomSymbolEqualityComparer.Default.Equals(type.OriginalDefinition, _wellKnownTypesCollections.ArraySegment1)
            || CustomSymbolEqualityComparer.Default.Equals(type.OriginalDefinition, _wellKnownTypesCollections.ConcurrentBag1)
            || CustomSymbolEqualityComparer.Default.Equals(type.OriginalDefinition, _wellKnownTypesCollections.ConcurrentQueue1)
            || CustomSymbolEqualityComparer.Default.Equals(type.OriginalDefinition, _wellKnownTypesCollections.ConcurrentStack1)
            || CustomSymbolEqualityComparer.Default.Equals(type.OriginalDefinition, _wellKnownTypesCollections.HashSet1)
            || CustomSymbolEqualityComparer.Default.Equals(type.OriginalDefinition, _wellKnownTypesCollections.LinkedList1)
            || CustomSymbolEqualityComparer.Default.Equals(type.OriginalDefinition, _wellKnownTypesCollections.List1)
            || CustomSymbolEqualityComparer.Default.Equals(type.OriginalDefinition, _wellKnownTypesCollections.Queue1)
            || CustomSymbolEqualityComparer.Default.Equals(type.OriginalDefinition, _wellKnownTypesCollections.SortedSet1)
            || CustomSymbolEqualityComparer.Default.Equals(type.OriginalDefinition, _wellKnownTypesCollections.Stack1)
            || CustomSymbolEqualityComparer.Default.Equals(type.OriginalDefinition, _wellKnownTypesCollections.ImmutableArray1)
            || CustomSymbolEqualityComparer.Default.Equals(type.OriginalDefinition, _wellKnownTypesCollections.ImmutableHashSet1)
            || CustomSymbolEqualityComparer.Default.Equals(type.OriginalDefinition, _wellKnownTypesCollections.ImmutableList1)
            || CustomSymbolEqualityComparer.Default.Equals(type.OriginalDefinition, _wellKnownTypesCollections.ImmutableQueue1)
            || CustomSymbolEqualityComparer.Default.Equals(type.OriginalDefinition, _wellKnownTypesCollections.ImmutableSortedSet1)
            || CustomSymbolEqualityComparer.Default.Equals(type.OriginalDefinition, _wellKnownTypesCollections.ImmutableStack1))
            return _enumerableBasedNodeFactory(type)
                .EnqueueBuildJobTo(_parentContainer.BuildQueue, implementationStack);

        if (type is ({ TypeKind: TypeKind.Interface } or { TypeKind: TypeKind.Class, IsAbstract: true })
            and INamedTypeSymbol interfaceOrAbstractType)
        {
            return SwitchInterface(interfaceOrAbstractType, implementationStack);
        }

        if (type is INamedTypeSymbol { TypeKind: TypeKind.Class or TypeKind.Struct } classOrStructType)
        {
            if (_checkTypeProperties.MapToSingleFittingImplementation(classOrStructType) is not { } chosenImplementationType)
            {
                if (classOrStructType.NullableAnnotation == NullableAnnotation.Annotated)
                {
                    _localDiagLogger.Warning(WarningLogData.NullResolutionWarning(
                        $"Interface: Multiple or no implementations where a single is required for \"{classOrStructType.FullName()}\", but injecting null instead."),
                        Location.None);
                    return _nullNodeFactory(classOrStructType)
                        .EnqueueBuildJobTo(_parentContainer.BuildQueue, implementationStack);
                }
                return _errorNodeFactory(
                        $"Interface: Multiple or no implementations where a single is required for \"{classOrStructType.FullName()}\",",
                        type)
                    .EnqueueBuildJobTo(_parentContainer.BuildQueue, implementationStack);
            }

            return SwitchImplementation(
                new(true, true, true),
                null,
                chosenImplementationType,
                implementationStack,
                Next);
        }

        return _errorNodeFactory(
                "Couldn't process in resolution tree creation.",
                type)
            .EnqueueBuildJobTo(_parentContainer.BuildQueue, implementationStack);
    }

    /// <summary>
    /// Meant as entry point for mappings where concrete implementation type is already chosen.
    /// </summary>
    public IElementNode MapToImplementation(ImplementationMappingConfiguration config,
        INamedTypeSymbol? abstractionType,
        INamedTypeSymbol implementationType,
        ImmutableStack<INamedTypeSymbol> implementationStack) =>
        SwitchImplementation(
            config,
            abstractionType,
            implementationType, 
            implementationStack, 
            NextForWraps); // Use NextForWraps, cause MapToImplementation is entry point

    public IElementNode MapToOutParameter(ITypeSymbol type, ImmutableStack<INamedTypeSymbol> implementationStack) => 
        _outParameterNodeFactory(type)
            .EnqueueBuildJobTo(_parentContainer.BuildQueue, implementationStack);

    protected IElementNode SwitchImplementation(
        ImplementationMappingConfiguration config,
        INamedTypeSymbol? abstractionType,
        INamedTypeSymbol implementationType, 
        ImmutableStack<INamedTypeSymbol> implementationSet,
        IElementNodeMapperBase nextMapper)
    {
        if (config.CheckForInitializedInstance && !ParentFunction.CheckIfReturnedType(implementationType))
        {
            if (ParentRange.GetInitializedNode(implementationType) is { } initializedInstanceNode)
            {
                ParentFunction.RegisterUsedInitializedInstance(initializedInstanceNode);
                return initializedInstanceNode;
            }
        }
        if (config.CheckForScopeRoot)
        {
            var ret = _checkTypeProperties.ShouldBeScopeRoot(implementationType) switch
            {
                ScopeLevel.TransientScope => ParentRange.BuildTransientScopeCall(implementationType, ParentFunction),
                ScopeLevel.Scope => ParentRange.BuildScopeCall(implementationType, ParentFunction),
                _ => (IElementNode?) null
            };
            if (ret is not null)
                return ret;
        }
        
        if (config.CheckForRangedInstance)
        {
            var scopeLevel = _checkTypeProperties.GetScopeLevelFor(implementationType);
            
            if (ParentFunction.TryGetReusedNode(implementationType, out var rn) && rn is not null)
                return rn;

            var ret = scopeLevel switch
            {
                ScopeLevel.Container => ParentRange.BuildContainerInstanceCall(implementationType, ParentFunction),
                ScopeLevel.TransientScope => ParentRange.BuildTransientScopeInstanceCall(implementationType, ParentFunction),
                ScopeLevel.Scope => ParentRange.BuildScopeInstanceCall(implementationType, ParentFunction),
                _ => null
            };
            if (ret is not null)
            {
                var reusedNode = _reusedNodeFactory(ret)
                    .EnqueueBuildJobTo(_parentContainer.BuildQueue, implementationSet);
                ParentFunction.AddReusedNode(implementationType, reusedNode);
                return reusedNode;
            }
        }

        if (_checkTypeProperties.GetConstructorChoiceFor(implementationType) is { } constructor)
            return _implementationNodeFactory(
                    abstractionType,
                    implementationType, 
                    constructor, 
                    nextMapper)
                .EnqueueBuildJobTo(_parentContainer.BuildQueue, implementationSet);

        if (implementationType.NullableAnnotation != NullableAnnotation.Annotated)
            return _errorNodeFactory(implementationType.InstanceConstructors.Length switch
                {
                    0 => $"Class.Constructor: No constructor found for implementation {implementationType.FullName()}",
                    > 1 =>
                        $"Class.Constructor: More than one constructor found for implementation {implementationType.FullName()}",
                    _ => $"Class.Constructor: {implementationType.InstanceConstructors[0].Name} is not a method symbol"
                },
                implementationType).EnqueueBuildJobTo(_parentContainer.BuildQueue, implementationSet);
            
        _localDiagLogger.Warning(WarningLogData.NullResolutionWarning(
            $"Interface: Multiple or no implementations where a single is required for \"{implementationType.FullName()}\", but injecting null instead."),
            Location.None);
        return _nullNodeFactory(implementationType)
            .EnqueueBuildJobTo(_parentContainer.BuildQueue, implementationSet);
    }
    
    private IElementNode SwitchInterface(INamedTypeSymbol interfaceType, ImmutableStack<INamedTypeSymbol> implementationSet)
    {
        if (_checkTypeProperties.ShouldBeComposite(interfaceType)
            && _checkTypeProperties.GetCompositeFor(interfaceType) is {} compositeImplementationType)
            return SwitchInterfaceWithPotentialDecoration(interfaceType, compositeImplementationType, implementationSet, Next);
        if (_checkTypeProperties.MapToSingleFittingImplementation(interfaceType) is not { } impType)
        {
            if (interfaceType.NullableAnnotation == NullableAnnotation.Annotated)
            {
                _localDiagLogger.Warning(WarningLogData.NullResolutionWarning(
                    $"Interface: Multiple or no implementations where a single is required for \"{interfaceType.FullName()}\", but injecting null instead."),
                    Location.None);
                return _nullNodeFactory(interfaceType)
                    .EnqueueBuildJobTo(_parentContainer.BuildQueue, implementationSet);
            }
            return _errorNodeFactory(
                    $"Interface: Multiple or no implementations where a single is required for \"{interfaceType.FullName()}\".",
                    interfaceType)
                .EnqueueBuildJobTo(_parentContainer.BuildQueue, implementationSet);
        }

        return SwitchInterfaceWithPotentialDecoration(interfaceType, impType, implementationSet, this);
    }

    protected IElementNode SwitchInterfaceWithPotentialDecoration(
        INamedTypeSymbol interfaceType,
        INamedTypeSymbol implementationType, 
        ImmutableStack<INamedTypeSymbol> implementationSet,
        IElementNodeMapperBase mapper)
    {
        var shouldBeDecorated = _checkTypeProperties.ShouldBeDecorated(interfaceType);
        if (!shouldBeDecorated)
            return SwitchImplementation(
                new(true, true, true),
                interfaceType,
                implementationType,
                implementationSet,
                mapper);

        var decoratorSequence = _checkTypeProperties.GetSequenceFor(interfaceType, implementationType)
            .Reverse()
            .Append(implementationType)
            .ToList();
        
        var decoratorTypes = ImmutableQueue.CreateRange(decoratorSequence
            .Select(t => (interfaceType, t))
            .Append((interfaceType, implementationType)));
            
        var overridingMapper = _overridingElementNodeMapperFactory(this, decoratorTypes);
        return overridingMapper.Map(interfaceType, implementationSet);
    }
}