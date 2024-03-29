using MsMeeseeks.DIE.Nodes.Functions;

namespace MsMeeseeks.DIE.Nodes.Elements.Factories;

internal interface IFactoryPropertyNode : IFactoryNodeBase;

internal sealed partial class FactoryPropertyNode : FactoryNodeBase, IFactoryPropertyNode
{
    internal FactoryPropertyNode(
        IPropertySymbol propertySymbol, 
        
        IFunctionNode parentFunction,
        IReferenceGenerator referenceGenerator,
        WellKnownTypes wellKnownTypes) 
        : base(propertySymbol.Type, propertySymbol, parentFunction, referenceGenerator, wellKnownTypes)
    {
    }
}