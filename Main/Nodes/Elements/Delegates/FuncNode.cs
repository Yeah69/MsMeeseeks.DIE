using MsMeeseeks.DIE.Logging;
using MsMeeseeks.DIE.Nodes.Functions;
using MsMeeseeks.DIE.Nodes.Ranges;

namespace MsMeeseeks.DIE.Nodes.Elements.Delegates;

internal interface IFuncNode : IDelegateBaseNode;

internal sealed partial class FuncNode : DelegateBaseNode, IFuncNode
{
    internal FuncNode(
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