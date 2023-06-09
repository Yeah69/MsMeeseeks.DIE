using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using MrMeeseeks.SourceGeneratorUtility;
using MrMeeseeks.SourceGeneratorUtility.Extensions;
using MsMeeseeks.DIE.Contexts;
using MsMeeseeks.DIE.Nodes.Elements;
using MsMeeseeks.DIE.Nodes.Elements.FunctionCalls;
using MsMeeseeks.DIE.Nodes.Ranges;

namespace MsMeeseeks.DIE.Nodes.Functions;

internal abstract class ReturningFunctionNodeBase : FunctionNodeBase
{
    protected readonly ITypeSymbol TypeSymbol;

    public ReturningFunctionNodeBase(
        // parameters
        Accessibility? accessibility,
        ITypeSymbol typeSymbol,
        IReadOnlyList<ITypeSymbol> parameters,
        ImmutableDictionary<ITypeSymbol, IParameterNode> closureParameters,
        IContainerNode parentContainer,
        IRangeNode parentRange,
        
        // dependencies
        Func<ITypeSymbol, IParameterNode> parameterNodeFactory,
        Func<string?, IReadOnlyList<(IParameterNode, IParameterNode)>, IPlainFunctionCallNode> plainFunctionCallNodeFactory,
        Func<ITypeSymbol, string?, SynchronicityDecision, IReadOnlyList<(IParameterNode, IParameterNode)>, IAsyncFunctionCallNode> asyncFunctionCallNodeFactory,
        Func<(string, string), IScopeNode, IRangeNode, IReadOnlyList<(IParameterNode, IParameterNode)>, IFunctionCallNode?, IScopeCallNode> scopeCallNodeFactory,
        Func<string, ITransientScopeNode, IRangeNode, IReadOnlyList<(IParameterNode, IParameterNode)>, IFunctionCallNode?, ITransientScopeCallNode> transientScopeCallNodeFactory,
        IContainerWideContext containerWideContext)
        : base(
            accessibility,
            parameters,
            closureParameters,
            parentContainer,
            parentRange,
            
            parameterNodeFactory,
            plainFunctionCallNodeFactory,
            asyncFunctionCallNodeFactory,
            scopeCallNodeFactory,
            transientScopeCallNodeFactory,
            containerWideContext)
    {
        TypeSymbol = typeSymbol;
        ReturnedTypeFullName = typeSymbol.FullName();
    }

    protected override string GetAsyncTypeFullName() => TypeSymbol.FullName();

    protected override string GetReturnedTypeFullName() => WellKnownTypes.ValueTask1.Construct(TypeSymbol).FullName();

    public override bool CheckIfReturnedType(ITypeSymbol type) => 
        CustomSymbolEqualityComparer.IncludeNullability.Equals(type, TypeSymbol);
}