using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using MsMeeseeks.DIE.Contexts;
using MsMeeseeks.DIE.Nodes.Elements;
using MsMeeseeks.DIE.Nodes.Elements.FunctionCalls;
using MsMeeseeks.DIE.Nodes.Mappers;
using MsMeeseeks.DIE.Nodes.Ranges;

namespace MsMeeseeks.DIE.Nodes.Functions;

internal interface ISingleFunctionNode : IFunctionNode
{
    IElementNode ReturnedElement { get; }
}

internal abstract class SingleFunctionNodeBase : ReturningFunctionNodeBase, ISingleFunctionNode
{
    public SingleFunctionNodeBase(
        // parameters
        Accessibility? accessibility,
        ITypeSymbol typeSymbol,
        IReadOnlyList<ITypeSymbol> parameters,
        ImmutableDictionary<ITypeSymbol, IParameterNode> closureParameters,
        IRangeNode parentRange,
        IContainerNode parentContainer,
        
        // dependencies
        Func<ITypeSymbol, IParameterNode> parameterNodeFactory,
        Func<string?, IReadOnlyList<(IParameterNode, IParameterNode)>, IPlainFunctionCallNode> plainFunctionCallNodeFactory,
        Func<ITypeSymbol, string?, SynchronicityDecision, IReadOnlyList<(IParameterNode, IParameterNode)>, IAsyncFunctionCallNode> asyncFunctionCallNodeFactory,
        Func<(string, string), IScopeNode, IRangeNode, IReadOnlyList<(IParameterNode, IParameterNode)>, IFunctionCallNode?, IScopeCallNode> scopeCallNodeFactory,
        Func<string, ITransientScopeNode, IRangeNode, IReadOnlyList<(IParameterNode, IParameterNode)>, IFunctionCallNode?, ITransientScopeCallNode> transientScopeCallNodeFactory,
        IContainerWideContext containerWideContext)
        : base(
            accessibility, 
            typeSymbol, 
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
        ReturnedTypeNameNotWrapped = typeSymbol.Name;
    }

    protected abstract IElementNodeMapperBase GetMapper();

    protected virtual IElementNode MapToReturnedElement(IElementNodeMapperBase mapper) =>
        mapper.Map(TypeSymbol, ImmutableStack.Create<INamedTypeSymbol>());
    
    public override void Build(ImmutableStack<INamedTypeSymbol> implementationStack)
    {
        base.Build(implementationStack);
        ReturnedElement = MapToReturnedElement(GetMapper());
    }

    public IElementNode ReturnedElement { get; private set; } = null!;
    public override string ReturnedTypeNameNotWrapped { get; }
}