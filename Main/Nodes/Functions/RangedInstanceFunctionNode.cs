using MsMeeseeks.DIE.Configuration;
using MsMeeseeks.DIE.Mappers;
using MsMeeseeks.DIE.MsContainer;
using MsMeeseeks.DIE.Nodes.Elements;
using MsMeeseeks.DIE.Nodes.Elements.FunctionCalls;
using MsMeeseeks.DIE.Nodes.Ranges;
using MsMeeseeks.DIE.Utility;
using MrMeeseeks.SourceGeneratorUtility;

namespace MsMeeseeks.DIE.Nodes.Functions;

internal interface IRangedInstanceFunctionNode : ISingleFunctionNode;

internal interface IRangedInstanceFunctionNodeInitializer
{
    /// <summary>
    /// Only intended for transient scope instance function, cause they need to synchronize identifier with their interface function
    /// </summary>
    void Initialize(string name, string explicitInterfaceFullName);
}

internal sealed partial class RangedInstanceFunctionNode : SingleFunctionNodeBase, IRangedInstanceFunctionNode, IRangedInstanceFunctionNodeInitializer, IScopeInstance
{
    private readonly INamedTypeSymbol _type;
    private readonly Func<IElementNodeMapper> _typeToElementNodeMapperFactory;

    public RangedInstanceFunctionNode(
        // parameters
        ScopeLevel level,
        INamedTypeSymbol type, 
        IReadOnlyList<ITypeSymbol> parameters,
        
        // dependencies
        IRangeNode parentRange,
        IContainerNode parentContainer, 
        IReferenceGenerator referenceGenerator, 
        Func<IElementNodeMapper> typeToElementNodeMapperFactory,
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
            parentRange, 
            parentContainer, 
            parameterNodeFactory,
            plainFunctionCallNodeFactory,
            asyncFunctionCallNodeFactory,
            scopeCallNodeFactory,
            transientScopeCallNodeFactory,
            typeParameterUtility,
            wellKnownTypes)
    {
        _type = type;
        _typeToElementNodeMapperFactory = typeToElementNodeMapperFactory;
        Name = referenceGenerator.Generate($"Get{level.ToString()}Instance", _type);
    }

    protected override IElementNodeMapperBase GetMapper() =>
        _typeToElementNodeMapperFactory();

    protected override IElementNode MapToReturnedElement(IElementNodeMapperBase mapper) => 
        // "MapToImplementation" instead of "Map", because latter would cause an infinite recursion ever trying to create a new ranged instance function
        mapper.MapToImplementation(
            new(true, false, false), 
            null, 
            _type,
            new(ImmutableStack<INamedTypeSymbol>.Empty, null)); 

    public override string Name { get; protected set; }

    void IRangedInstanceFunctionNodeInitializer.Initialize(
        string name, 
        string explicitInterfaceFullName)
    {
        Name = name;
        ExplicitInterfaceFullName = explicitInterfaceFullName;
    }
}