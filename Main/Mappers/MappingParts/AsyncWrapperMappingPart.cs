using MsMeeseeks.DIE.MsContainer;
using MsMeeseeks.DIE.Nodes.Elements;
using MsMeeseeks.DIE.Nodes.Functions;
using MsMeeseeks.DIE.Nodes.Ranges;
using MrMeeseeks.SourceGeneratorUtility;

namespace MsMeeseeks.DIE.Mappers.MappingParts;

internal interface IAsyncWrapperMappingPart : IMappingPart;

internal sealed class AsyncWrapperMappingPart : IAsyncWrapperMappingPart, IScopeInstance
{
    private readonly IFunctionNode _parentFunction;
    private readonly IUserDefinedElementsMappingPart _userDefinedElementsMappingPart;
    private readonly IRangeNode _parentRange;
    private readonly WellKnownTypes _wellKnownTypes;

    internal AsyncWrapperMappingPart(
        IFunctionNode parentFunction,
        IRangeNode parentRange,
        WellKnownTypes wellKnownTypes,
        IUserDefinedElementsMappingPart userDefinedElementsMappingPart)
    {
        _parentFunction = parentFunction;
        _userDefinedElementsMappingPart = userDefinedElementsMappingPart;
        _parentRange = parentRange;
        _wellKnownTypes = wellKnownTypes;
    }
    
    public IElementNode? Map(MappingPartData data)
    {
        if (_wellKnownTypes.ValueTask1 is not null 
            && CustomSymbolEqualityComparer.Default.Equals(data.Type.OriginalDefinition, _wellKnownTypes.ValueTask1)
            && data.Type is INamedTypeSymbol valueTask)
            return _userDefinedElementsMappingPart.Map(data) 
                   ?? _parentRange.BuildAsyncCreateCall(data.GetMapperDataForAsyncWrapping(), valueTask.TypeArguments[0], SynchronicityDecision.AsyncValueTask, _parentFunction);

        if (CustomSymbolEqualityComparer.Default.Equals(data.Type.OriginalDefinition, _wellKnownTypes.Task1)
            && data.Type is INamedTypeSymbol task)
            return _userDefinedElementsMappingPart.Map(data) 
                   ?? _parentRange.BuildAsyncCreateCall(data.GetMapperDataForAsyncWrapping(), task.TypeArguments[0], SynchronicityDecision.AsyncTask, _parentFunction);

        return null;
    }
}