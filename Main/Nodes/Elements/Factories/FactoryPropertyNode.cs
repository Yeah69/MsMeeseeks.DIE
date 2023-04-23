using Microsoft.CodeAnalysis;
using MsMeeseeks.DIE.Contexts;
using MsMeeseeks.DIE.Nodes.Functions;

namespace MsMeeseeks.DIE.Nodes.Elements.Factories;

internal interface IFactoryPropertyNode : IFactoryNodeBase
{
}

internal partial class FactoryPropertyNode : FactoryNodeBase, IFactoryPropertyNode
{
    internal FactoryPropertyNode(
        IPropertySymbol propertySymbol, 
        
        IFunctionNode parentFunction,
        IReferenceGenerator referenceGenerator,
        IContainerWideContext containerWideContext) 
        : base(propertySymbol.Type, propertySymbol, parentFunction, referenceGenerator, containerWideContext)
    {
    }
}