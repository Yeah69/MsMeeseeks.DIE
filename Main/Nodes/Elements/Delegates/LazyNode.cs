using Microsoft.CodeAnalysis;
using MsMeeseeks.DIE.Logging;
using MsMeeseeks.DIE.Nodes.Functions;
using MsMeeseeks.DIE.Nodes.Ranges;

namespace MsMeeseeks.DIE.Nodes.Elements.Delegates;

internal interface ILazyNode : IDelegateBaseNode
{
}

internal partial class LazyNode : DelegateBaseNode, ILazyNode
{
    internal LazyNode(
        INamedTypeSymbol lazyType,
        ILocalFunctionNode function,
        
        ILocalDiagLogger localDiagLogger,
        IContainerNode parentContainer,
        IReferenceGenerator referenceGenerator) 
        : base(lazyType, function, localDiagLogger, parentContainer, referenceGenerator)
    {
    }
}