using MsMeeseeks.DIE.CodeGeneration.Nodes;
using MsMeeseeks.DIE.Nodes.Elements;
using MsMeeseeks.DIE.Nodes.Elements.FunctionCalls;
using MsMeeseeks.DIE.Nodes.Ranges;
using MsMeeseeks.DIE.Utility;
using MrMeeseeks.SourceGeneratorUtility;
using MrMeeseeks.SourceGeneratorUtility.Extensions;

namespace MsMeeseeks.DIE.Nodes.Functions;

internal interface IReturningFunctionNode : IFunctionNode;

internal abstract class ReturningFunctionNodeBase : FunctionNodeBase, IReturningFunctionNode
{
    protected readonly ITypeSymbol TypeSymbol;

    protected ReturningFunctionNodeBase(
        // parameters
        Accessibility? accessibility,
        ITypeSymbol typeSymbol,
        IReadOnlyList<ITypeSymbol> parameters,
        ImmutableDictionary<ITypeSymbol, IParameterNode> closureParameters,
        IContainerNode parentContainer,
        IRangeNode parentRange,
        
        // dependencies
        ISubDisposalNodeChooser subDisposalNodeChooser,
        ITransientScopeDisposalNodeChooser transientScopeDisposalNodeChooser,
        Lazy<IFunctionNodeGenerator> functionNodeGenerator,
        Func<ITypeSymbol, IParameterNode> parameterNodeFactory,
        Func<PlainFunctionCallNode.Params, IPlainFunctionCallNode> plainFunctionCallNodeFactory,
        Func<WrappedAsyncFunctionCallNode.Params, IWrappedAsyncFunctionCallNode> asyncFunctionCallNodeFactory,
        Func<ScopeCallNode.Params, IScopeCallNode> scopeCallNodeFactory,
        Func<TransientScopeCallNode.Params, ITransientScopeCallNode> transientScopeCallNodeFactory,
        ITypeParameterUtility typeParameterUtility,
        WellKnownTypes wellKnownTypes)
        : base(
            accessibility,
            parameters,
            closureParameters,
            parentContainer,
            parentRange,
            
            subDisposalNodeChooser,
            transientScopeDisposalNodeChooser,
            functionNodeGenerator,
            parameterNodeFactory,
            plainFunctionCallNodeFactory,
            asyncFunctionCallNodeFactory,
            scopeCallNodeFactory,
            transientScopeCallNodeFactory,
            wellKnownTypes)
    {
        TypeSymbol = typeSymbol;
        ReturnedTypeFullName = TypeSymbol.FullName();
        TypeParameters = typeParameterUtility.ExtractTypeParameters(typeSymbol);

        if (TypeSymbol is INamedTypeSymbol { IsGenericType: true } namedTypeSymbol)
        {
            if (CustomSymbolEqualityComparer.Default.Equals(wellKnownTypes.Task1, namedTypeSymbol.OriginalDefinition))
            {
                SynchronicityDecision = SynchronicityDecision.AsyncTask;
                SynchronicityDecisionKind = SynchronicityDecisionKind.AsyncNatural;
            }
            else if (CustomSymbolEqualityComparer.Default.Equals(wellKnownTypes.ValueTask1, namedTypeSymbol.OriginalDefinition))
            {
                SynchronicityDecision = SynchronicityDecision.AsyncValueTask;
                SynchronicityDecisionKind = SynchronicityDecisionKind.AsyncNatural;
            }
        }
    }

    protected override void AdjustToAsync()
    {
        var symbol = TypeSymbol is INamedTypeSymbol namedTypeSymbol
            ? namedTypeSymbol.OriginalDefinitionIfUnbound()
            : TypeSymbol;
        if (WellKnownTypes.ValueTask1 is not null)
        {
            SynchronicityDecision = SynchronicityDecision.AsyncValueTask;
            ReturnedTypeFullName =  WellKnownTypes.ValueTask1.Construct(symbol).FullName();
        }
        else
        {
            SynchronicityDecision = SynchronicityDecision.AsyncTask;
            ReturnedTypeFullName =  WellKnownTypes.Task1.Construct(symbol).FullName();
        }
    }

    public override bool CheckIfReturnedType(ITypeSymbol type) => 
        CustomSymbolEqualityComparer.IncludeNullability.Equals(type, TypeSymbol);

    public override IReadOnlyList<ITypeParameterSymbol> TypeParameters { get; }
}