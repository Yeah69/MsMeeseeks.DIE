using MsMeeseeks.DIE.MsContainer;
using MsMeeseeks.DIE.Nodes.Elements;
using MsMeeseeks.DIE.Nodes.Functions;

namespace MsMeeseeks.DIE.Mappers.MappingParts;

internal interface IOverridesMappingPart : IMappingPart;

internal sealed class OverridesMappingPart : IOverridesMappingPart, IScopeInstance
{
    private readonly IFunctionNode _parentFunction;

    internal OverridesMappingPart(
        IFunctionNode parentFunction)
    {
        _parentFunction = parentFunction;
    }
    
    public IElementNode? Map(MappingPartData data) => 
        _parentFunction.Overrides.TryGetValue(data.Type, out var tuple) 
            ? tuple 
            : null;
}