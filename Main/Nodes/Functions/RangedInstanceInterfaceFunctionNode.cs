using MsMeeseeks.DIE.MsContainer;
using MsMeeseeks.DIE.Nodes.Elements;
using MsMeeseeks.DIE.Nodes.Elements.FunctionCalls;
using MsMeeseeks.DIE.Nodes.Ranges;
using MsMeeseeks.DIE.Utility;
using MrMeeseeks.SourceGeneratorUtility;

namespace MsMeeseeks.DIE.Nodes.Functions;

internal interface IRangedInstanceInterfaceFunctionNode : IFunctionNode
{
    void AddConsideredRange(IRangeNode range);
}

internal sealed partial class RangedInstanceInterfaceFunctionNode : ReturningFunctionNodeBase, IRangedInstanceInterfaceFunctionNode, IScopeInstance
{
    private readonly INamedTypeSymbol _type;
    private readonly IContainerNode _parentContainer;
    private readonly List<IRangedInstanceFunctionNode> _implementations = []; 
    
    public RangedInstanceInterfaceFunctionNode(
        // parameters
        INamedTypeSymbol type, 
        IReadOnlyList<ITypeSymbol> parameters,
        
        // dependencies
        IContainerNode parentContainer,
        IRangeNode parentRange,
        IReferenceGenerator referenceGenerator, 
        Func<ITypeSymbol, string?, IReadOnlyList<(IParameterNode, IParameterNode)>, IReadOnlyList<ITypeSymbol>, IPlainFunctionCallNode> plainFunctionCallNodeFactory,
        Func<ITypeSymbol, string?, SynchronicityDecision, IReadOnlyList<(IParameterNode, IParameterNode)>, IReadOnlyList<ITypeSymbol>, IWrappedAsyncFunctionCallNode> asyncFunctionCallNodeFactory,
        Func<ITypeSymbol, (string, string), IScopeNode, IRangeNode, IReadOnlyList<(IParameterNode, IParameterNode)>, IReadOnlyList<ITypeSymbol>, IFunctionCallNode?, ScopeCallNodeOuterMapperParam, IScopeCallNode> scopeCallNodeFactory,
        Func<ITypeSymbol, string, ITransientScopeNode, IRangeNode, IReadOnlyList<(IParameterNode, IParameterNode)>, IReadOnlyList<ITypeSymbol>, IFunctionCallNode?, ScopeCallNodeOuterMapperParam, ITransientScopeCallNode> transientScopeCallNodeFactory,
        Func<ITypeSymbol, IParameterNode> parameterNodeFactory,
        ITypeParameterUtility typeParameterUtility,
        WellKnownTypes wellKnownTypes) 
        : base(
            Microsoft.CodeAnalysis.Accessibility.Private,
            type, 
            parameters,
            ImmutableDictionary.Create<ITypeSymbol, IParameterNode>(CustomSymbolEqualityComparer.IncludeNullability), 
            parentContainer, 
            parentRange, 
            parameterNodeFactory,
            plainFunctionCallNodeFactory,
            asyncFunctionCallNodeFactory,
            scopeCallNodeFactory,
            transientScopeCallNodeFactory,
            typeParameterUtility,
            wellKnownTypes)
    {
        _type = type;
        _parentContainer = parentContainer;
        ReturnedTypeNameNotWrapped = type.Name;
        Name = referenceGenerator.Generate("GetTransientScopeInstance", _type);
    }

    public override string Name { get; protected set; }
    public override string ReturnedTypeNameNotWrapped { get; }

    public void AddConsideredRange(IRangeNode range)
    {
        var implementation = range.BuildTransientScopeFunction(_type, this);
        (implementation as IRangedInstanceFunctionNodeInitializer)?.Initialize(Name, _parentContainer.TransientScopeInterface.FullName);
        implementation.RegisterCallingFunction(this);
        _implementations.Add(implementation);
    }

    protected override void OnBecameAsync()
    {
        base.OnBecameAsync();
        foreach (var implementation in _implementations)
            implementation.ForceToAsync();
    }
}