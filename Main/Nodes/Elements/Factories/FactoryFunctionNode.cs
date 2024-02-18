using MsMeeseeks.DIE.Contexts;
using MsMeeseeks.DIE.Nodes.Functions;
using MsMeeseeks.DIE.Nodes.Mappers;

namespace MsMeeseeks.DIE.Nodes.Elements.Factories;

internal interface IFactoryFunctionNode : IFactoryNodeBase
{
    IReadOnlyList<(string Name, IElementNode Element)> Parameters { get; }
}

internal sealed partial class FactoryFunctionNode : FactoryNodeBase, IFactoryFunctionNode
{
    private readonly IMethodSymbol _methodSymbol;
    private readonly IElementNodeMapperBase _elementNodeMapperBase;
    private readonly List<(string, IElementNode)> _parameters = new ();

    internal FactoryFunctionNode(
        IMethodSymbol methodSymbol,
        IElementNodeMapperBase elementNodeMapperBase,
        
        IFunctionNode parentFunction,
        IReferenceGenerator referenceGenerator,
        IContainerWideContext containerWideContext) 
        : base(methodSymbol.ReturnType, methodSymbol, parentFunction, referenceGenerator, containerWideContext)
    {
        _methodSymbol = methodSymbol;
        _elementNodeMapperBase = elementNodeMapperBase;
    }

    public override void Build(PassedContext passedContext)
    {
        _parameters.AddRange(_methodSymbol
            .Parameters
            .Select(p => (p.Name, _elementNodeMapperBase.Map(p.Type, passedContext))));
        base.Build(passedContext);
    }
    
    public IReadOnlyList<(string, IElementNode)> Parameters => _parameters;
}