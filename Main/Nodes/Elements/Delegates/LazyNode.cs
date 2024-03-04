using MsMeeseeks.DIE.Logging;
using MsMeeseeks.DIE.Nodes.Functions;
using MsMeeseeks.DIE.Nodes.Ranges;

namespace MsMeeseeks.DIE.Nodes.Elements.Delegates;

internal interface ILazyNode : IDelegateBaseNode;

internal sealed partial class LazyNode : DelegateBaseNode, ILazyNode
{
    internal LazyNode(
        (INamedTypeSymbol Outer, INamedTypeSymbol Inner) delegateTypes,
        ILocalFunctionNode function,
        IReadOnlyList<ITypeSymbol> typeParameters,
        
        ILocalDiagLogger localDiagLogger,
        IContainerNode parentContainer,
        IReferenceGenerator referenceGenerator) 
        : base(delegateTypes, function, typeParameters, localDiagLogger, parentContainer, referenceGenerator)
    {
    }
}