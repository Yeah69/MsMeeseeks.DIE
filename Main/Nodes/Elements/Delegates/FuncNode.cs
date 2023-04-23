using Microsoft.CodeAnalysis;
using MsMeeseeks.DIE.Logging;
using MsMeeseeks.DIE.Nodes.Functions;
using MsMeeseeks.DIE.Nodes.Ranges;

namespace MsMeeseeks.DIE.Nodes.Elements.Delegates;

internal interface IFuncNode : IDelegateBaseNode
{
    
}

internal partial class FuncNode : DelegateBaseNode, IFuncNode
{
    internal FuncNode(
        INamedTypeSymbol funcType,
        ILocalFunctionNode function,
        
        ILocalDiagLogger localDiagLogger,
        IContainerNode parentContainer,
        IReferenceGenerator referenceGenerator) 
        : base(funcType, function, localDiagLogger, parentContainer, referenceGenerator)
    {
    }
}