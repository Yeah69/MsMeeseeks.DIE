using MsMeeseeks.DIE.Nodes.Functions;

namespace MsMeeseeks.DIE.Nodes.Elements.Factories;

internal interface IFactoryFieldNode : IFactoryNodeBase;

internal sealed partial class FactoryFieldNode : FactoryNodeBase,  IFactoryFieldNode
{
    internal FactoryFieldNode(
        IFieldSymbol fieldSymbol, 
        
        IFunctionNode parentFunction,
        IReferenceGenerator referenceGenerator,
        WellKnownTypes wellKnownTypes) 
        : base(fieldSymbol.Type, fieldSymbol, parentFunction, referenceGenerator, wellKnownTypes)
    {
    }
}