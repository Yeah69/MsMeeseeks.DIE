using MsMeeseeks.DIE.Mappers;
using MsMeeseeks.DIE.MsContainer;
using MsMeeseeks.DIE.Nodes.Elements;
using MsMeeseeks.DIE.Nodes.Elements.FunctionCalls;
using MsMeeseeks.DIE.Nodes.Ranges;
using MsMeeseeks.DIE.Utility;
using MrMeeseeks.SourceGeneratorUtility;

namespace MsMeeseeks.DIE.Nodes.Functions;

internal interface ICreateTransientScopeFunctionNode : ICreateFunctionNodeBase;

internal sealed partial class CreateTransientScopeFunctionNode : SingleFunctionNodeBase, ICreateTransientScopeFunctionNode, IScopeInstance
{
    private readonly INamedTypeSymbol _typeSymbol;
    private readonly Func<IElementNodeMapper> _typeToElementNodeMapperFactory;
    private readonly Func<IElementNodeMapperBase, ITransientScopeDisposalElementNodeMapper> _transientScopeDisposalElementNodeMapperFactory;

    public CreateTransientScopeFunctionNode(
        // parameters
        INamedTypeSymbol typeSymbol, 
        IReadOnlyList<ITypeSymbol> parameters,
        
        // dependencies
        IRangeNode parentRange,
        IContainerNode parentContainer, 
        IReferenceGenerator referenceGenerator, 
        Func<IElementNodeMapper> typeToElementNodeMapperFactory,
        Func<IElementNodeMapperBase, ITransientScopeDisposalElementNodeMapper> transientScopeDisposalElementNodeMapperFactory,
        Func<ITypeSymbol, string?, IReadOnlyList<(IParameterNode, IParameterNode)>, IReadOnlyList<ITypeSymbol>, IPlainFunctionCallNode> plainFunctionCallNodeFactory,
        Func<ITypeSymbol, string?, SynchronicityDecision, IReadOnlyList<(IParameterNode, IParameterNode)>, IReadOnlyList<ITypeSymbol>, IWrappedAsyncFunctionCallNode> asyncFunctionCallNodeFactory,
        Func<ITypeSymbol, (string, string), IScopeNode, IRangeNode, IReadOnlyList<(IParameterNode, IParameterNode)>, IReadOnlyList<ITypeSymbol>, IFunctionCallNode?, ScopeCallNodeOuterMapperParam, IScopeCallNode> scopeCallNodeFactory,
        Func<ITypeSymbol, string, ITransientScopeNode, IRangeNode, IReadOnlyList<(IParameterNode, IParameterNode)>, IReadOnlyList<ITypeSymbol>, IFunctionCallNode?, ScopeCallNodeOuterMapperParam, ITransientScopeCallNode> transientScopeCallNodeFactory,
        Func<ITypeSymbol, IParameterNode> parameterNodeFactory,
        ITypeParameterUtility typeParameterUtility,
        WellKnownTypes wellKnownTypes) 
        : base(
            Microsoft.CodeAnalysis.Accessibility.Internal,
            typeSymbol, 
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
        _typeSymbol = typeSymbol;
        _typeToElementNodeMapperFactory = typeToElementNodeMapperFactory;
        _transientScopeDisposalElementNodeMapperFactory = transientScopeDisposalElementNodeMapperFactory;
        Name = referenceGenerator.Generate("Create", _typeSymbol);
    }

    protected override IElementNode MapToReturnedElement(IElementNodeMapperBase mapper) => 
        mapper.MapToImplementation(
            new(false, false, false), 
            null, 
            _typeSymbol, 
            new(ImmutableStack<INamedTypeSymbol>.Empty, null));

    protected override IElementNodeMapperBase GetMapper()
    {
        var parentMapper = _typeToElementNodeMapperFactory();
        return _transientScopeDisposalElementNodeMapperFactory(parentMapper);
    }

    public override string Name { get; protected set; }
}