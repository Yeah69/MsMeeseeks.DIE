using MsMeeseeks.DIE.Extensions;
using MsMeeseeks.DIE.MsContainer;
using MsMeeseeks.DIE.Nodes.Elements;
using MsMeeseeks.DIE.Nodes.Ranges;
using MsMeeseeks.DIE.Utility;

namespace MsMeeseeks.DIE.Mappers.MappingParts;

internal interface ICollectionMappingPart : IMappingPart;

internal sealed class CollectionMappingPart : ICollectionMappingPart, IScopeInstance
{
    private readonly IContainerNode _parentContainer;
    private readonly ICheckIterableTypes _checkIterableTypes;
    private readonly IUserDefinedElementsMappingPart _userDefinedElementsMappingPart;
    private readonly Func<ITypeSymbol, IEnumerableBasedNode> _enumerableBasedNodeFactory;
    private readonly Func<INamedTypeSymbol, IKeyValueBasedNode> _keyValueBasedNodeFactory;

    internal CollectionMappingPart(
        IContainerNode parentContainer, 
        ICheckIterableTypes checkIterableTypes, 
        IUserDefinedElementsMappingPart userDefinedElementsMappingPart,
        Func<ITypeSymbol, IEnumerableBasedNode> enumerableBasedNodeFactory,
        Func<INamedTypeSymbol, IKeyValueBasedNode> keyValueBasedNodeFactory)
    {
        _parentContainer = parentContainer;
        _checkIterableTypes = checkIterableTypes;
        _userDefinedElementsMappingPart = userDefinedElementsMappingPart;
        _enumerableBasedNodeFactory = enumerableBasedNodeFactory;
        _keyValueBasedNodeFactory = keyValueBasedNodeFactory;
    }

    public IElementNode? Map(MappingPartData data)
    {
        if (_checkIterableTypes.IsMapType(data.Type) && data.Type is INamedTypeSymbol mapType)
            return _userDefinedElementsMappingPart.Map(data) 
                   ?? _keyValueBasedNodeFactory(mapType)
                       .EnqueueBuildJobTo(_parentContainer.BuildQueue, data.PassedContext);

        if (_checkIterableTypes.IsCollectionType(data.Type))
            return _userDefinedElementsMappingPart.Map(data) 
                   ?? _enumerableBasedNodeFactory(data.Type)
                       .EnqueueBuildJobTo(_parentContainer.BuildQueue, data.PassedContext);

        return null;
    }
}