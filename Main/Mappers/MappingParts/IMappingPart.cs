using MsMeeseeks.DIE.Nodes;
using MsMeeseeks.DIE.Nodes.Elements;

namespace MsMeeseeks.DIE.Mappers.MappingParts;

internal interface IMappingPart
{
    IElementNode? Map(MappingPartData data);
}

internal sealed record MappingPartData(
    ITypeSymbol Type,
    PassedContext PassedContext,
    IElementNodeMapperBase Next,
    IElementNodeMapperBase NextForWraps,
    IElementNodeMapperBase Current,
    Func<MapperData> GetMapperDataForAsyncWrapping);