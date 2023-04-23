using Microsoft.CodeAnalysis;
using MsMeeseeks.DIE.Contexts;
using MsMeeseeks.DIE.Nodes.Functions;

namespace MsMeeseeks.DIE.Nodes.Elements.Factories;

internal interface IFactoryFieldNode : IFactoryNodeBase
{
}

internal partial class FactoryFieldNode : FactoryNodeBase,  IFactoryFieldNode
{
    internal FactoryFieldNode(
        IFieldSymbol fieldSymbol, 
        
        IFunctionNode parentFunction,
        IReferenceGenerator referenceGenerator,
        IContainerWideContext containerWideContext) 
        : base(fieldSymbol.Type, fieldSymbol, parentFunction, referenceGenerator, containerWideContext)
    {
    }
}