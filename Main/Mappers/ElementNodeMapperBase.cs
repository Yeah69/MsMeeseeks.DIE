using MsMeeseeks.DIE.Extensions;
using MsMeeseeks.DIE.Mappers.MappingParts;
using MsMeeseeks.DIE.Nodes;
using MsMeeseeks.DIE.Nodes.Elements;
using MsMeeseeks.DIE.Nodes.Ranges;

namespace MsMeeseeks.DIE.Mappers;

internal interface IElementNodeMapperBase
{
    IElementNode Map(ITypeSymbol type, PassedContext passedContext);
    IElementNode MapToImplementation(
        ImplementationMappingConfiguration config,
        INamedTypeSymbol? abstractionType,
        INamedTypeSymbol implementationType,
        PassedContext passedContext);
    IElementNode MapToOutParameter(ITypeSymbol type, PassedContext passedContext);
    IElementNode MapToScopeWithImplementationType(
        INamedTypeSymbol type,
        (string Name, string Reference)[] additionalProperties,
        PassedContext passedContext);
    
    IElementNode MapToImplicitScope(
        string typeFullName, (string Name, string Reference)[] properties, PassedContext passedContext);
}

internal sealed record ImplementationMappingConfiguration(
    bool CheckForScopeRoot,
    bool CheckForRangedInstance,
    bool CheckForInitializedInstance);

internal abstract class ElementNodeMapperBase : IElementNodeMapperBase
{
    private readonly IContainerNode _parentContainer;
    private readonly IOverridesMappingPart _overridesMappingPart;
    private readonly IUserDefinedElementsMappingPart _userDefinedElementsMappingPart;
    private readonly IAsyncWrapperMappingPart _asyncWrapperMappingPart;
    private readonly ITupleMappingPart _tupleMappingPart;
    private readonly IDelegateMappingPart _delegateMappingPart;
    private readonly ICollectionMappingPart _collectionMappingPart;
    private readonly IAbstractionImplementationMappingPart _abstractionImplementationMappingPart;
    private readonly Func<ITypeSymbol, IOutParameterNode> _outParameterNodeFactory;
    private readonly Func<string, (string Name, IElementNode Element)[], IImplicitScopeImplementationNode> _implicitScopeImplementationNodeFactory;
    private readonly Func<string, IReferenceNode> _referenceNodeFactory;
    private readonly Func<string, ITypeSymbol, IErrorNode> _errorNodeFactory;
    
    internal ElementNodeMapperBase(
        IContainerNode parentContainer,
        IOverridesMappingPart overridesMappingPart,
        IUserDefinedElementsMappingPart userDefinedElementsMappingPart,
        IAsyncWrapperMappingPart asyncWrapperMappingPart,
        ITupleMappingPart tupleMappingPart,
        IDelegateMappingPart delegateMappingPart,
        ICollectionMappingPart collectionMappingPart,
        IAbstractionImplementationMappingPart abstractionImplementationMappingPart,
        Func<ITypeSymbol, IOutParameterNode> outParameterNodeFactory,
        Func<string, (string Name, IElementNode Element)[], IImplicitScopeImplementationNode> implicitScopeImplementationNodeFactory,
        Func<string, IReferenceNode> referenceNodeFactory,
        Func<string, ITypeSymbol, IErrorNode> errorNodeFactory)
    {
        _parentContainer = parentContainer;
        _overridesMappingPart = overridesMappingPart;
        _userDefinedElementsMappingPart = userDefinedElementsMappingPart;
        _asyncWrapperMappingPart = asyncWrapperMappingPart;
        _tupleMappingPart = tupleMappingPart;
        _delegateMappingPart = delegateMappingPart;
        _collectionMappingPart = collectionMappingPart;
        _abstractionImplementationMappingPart = abstractionImplementationMappingPart;
        _outParameterNodeFactory = outParameterNodeFactory;
        _implicitScopeImplementationNodeFactory = implicitScopeImplementationNodeFactory;
        _referenceNodeFactory = referenceNodeFactory;
        _errorNodeFactory = errorNodeFactory;
    }
    
    protected abstract IElementNodeMapperBase NextForWraps { get; }

    protected abstract IElementNodeMapperBase Next { get; }

    protected virtual MapperData GetMapperDataForAsyncWrapping() => 
        new VanillaMapperData();

    public virtual IElementNode Map(ITypeSymbol type, PassedContext passedContext)
    {
        var data = GetMappingPartData(type, passedContext);

        return _overridesMappingPart.Map(data) 
               ?? _asyncWrapperMappingPart.Map(data)
               ?? _tupleMappingPart.Map(data)
               ?? _delegateMappingPart.Map(data)
               ?? _collectionMappingPart.Map(data)
               ?? _abstractionImplementationMappingPart.Map(data)
               ?? _userDefinedElementsMappingPart.Map(data)
               ?? _errorNodeFactory(
                "Couldn't process in resolution tree creation.",
                type).EnqueueBuildJobTo(_parentContainer.BuildQueue, passedContext);
    }

    /// <summary>
    /// Meant as entry point for mappings where concrete implementation type is already chosen.
    /// </summary>
    public IElementNode MapToImplementation(ImplementationMappingConfiguration config,
        INamedTypeSymbol? abstractionType,
        INamedTypeSymbol implementationType,
        PassedContext passedContext)
    {
        if (abstractionType is not null 
            && _userDefinedElementsMappingPart.Map(GetMappingPartData(abstractionType, passedContext)) 
                is { } byAbstractionNode)
            return byAbstractionNode;
        return _userDefinedElementsMappingPart.Map(GetMappingPartData(implementationType, passedContext))
               ?? SwitchImplementation(
                   config,
                   abstractionType,
                   implementationType,
                   passedContext,
                   // Use NextForWraps, cause MapToImplementation is entry point
                   NextForWraps);
    }
    
    private MappingPartData GetMappingPartData(ITypeSymbol type, PassedContext passedContext) =>
        new(type, passedContext, Next, NextForWraps, this, GetMapperDataForAsyncWrapping);

    public IElementNode MapToOutParameter(ITypeSymbol type, PassedContext passedContext) => 
        _outParameterNodeFactory(type)
            .EnqueueBuildJobTo(_parentContainer.BuildQueue, passedContext);

    public IElementNode MapToScopeWithImplementationType(
        INamedTypeSymbol type, 
        (string Name, string Reference)[] additionalProperties, 
        PassedContext passedContext) =>
        _abstractionImplementationMappingPart.ForScopeWithImplementationType(
            type,
            additionalProperties,
            passedContext,
            this);

    public IElementNode MapToImplicitScope(
        string typeFullName,
        (string Name, string Reference)[] properties,
        PassedContext passedContext) =>
        _implicitScopeImplementationNodeFactory(typeFullName,
                properties.Select(t => (t.Name,
                    Element: (IElementNode)_referenceNodeFactory(t.Reference)
                        .EnqueueBuildJobTo(_parentContainer.BuildQueue, passedContext))).ToArray())
            .EnqueueBuildJobTo(_parentContainer.BuildQueue, passedContext);

    protected IElementNode SwitchImplementation(
        ImplementationMappingConfiguration config,
        INamedTypeSymbol? abstractionType,
        INamedTypeSymbol implementationType, 
        PassedContext passedContext,
        IElementNodeMapperBase nextMapper) =>
        _abstractionImplementationMappingPart.SwitchImplementation(
            config,
            abstractionType,
            implementationType,
            passedContext,
            nextMapper);

    protected IElementNode SwitchInterfaceWithPotentialDecoration(
        INamedTypeSymbol interfaceType,
        INamedTypeSymbol implementationType, 
        PassedContext passedContext,
        IElementNodeMapperBase mapper) =>
        _abstractionImplementationMappingPart.SwitchInterfaceWithPotentialDecoration(
            interfaceType,
            implementationType,
            passedContext,
            mapper,
            this);
}