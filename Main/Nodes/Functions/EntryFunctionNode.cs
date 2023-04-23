using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using MrMeeseeks.SourceGeneratorUtility;
using MsMeeseeks.DIE.Contexts;
using MsMeeseeks.DIE.MsContainer;
using MsMeeseeks.DIE.Nodes.Elements;
using MsMeeseeks.DIE.Nodes.Elements.FunctionCalls;
using MsMeeseeks.DIE.Nodes.Mappers;
using MsMeeseeks.DIE.Nodes.Ranges;

namespace MsMeeseeks.DIE.Nodes.Functions;

internal interface IEntryFunctionNode : ISingleFunctionNode
{
}

internal partial class EntryFunctionNode : SingleFunctionNodeBase, IEntryFunctionNode, IScopeInstance
{
    private readonly Func<IElementNodeMapper> _typeToElementNodeMapperFactory;
    private readonly Func<IElementNodeMapperBase, INonWrapToCreateElementNodeMapper> _nonWrapToCreateElementNodeMapperFactory;

    public EntryFunctionNode(
        // parameters
        ITypeSymbol typeSymbol, 
        string prefix, 
        IReadOnlyList<ITypeSymbol> parameters,
        
        // dependencies
        ITransientScopeWideContext transientScopeWideContext,
        IContainerNode parentContainer, 
        IContainerWideContext containerWideContext,
        Func<IElementNodeMapper> typeToElementNodeMapperFactory, 
        Func<IElementNodeMapperBase, INonWrapToCreateElementNodeMapper> nonWrapToCreateElementNodeMapperFactory,
        Func<string?, IReadOnlyList<(IParameterNode, IParameterNode)>, IPlainFunctionCallNode> plainFunctionCallNodeFactory,
        Func<ITypeSymbol, string?, SynchronicityDecision, IReadOnlyList<(IParameterNode, IParameterNode)>, IAsyncFunctionCallNode> asyncFunctionCallNodeFactory,
        Func<(string, string), IScopeNode, IRangeNode, IReadOnlyList<(IParameterNode, IParameterNode)>, IFunctionCallNode?, IScopeCallNode> scopeCallNodeFactory,
        Func<string, ITransientScopeNode, IRangeNode, IReadOnlyList<(IParameterNode, IParameterNode)>, IFunctionCallNode?, ITransientScopeCallNode> transientScopeCallNodeFactory,
        Func<ITypeSymbol, IParameterNode> parameterNodeFactory) 
        : base(
            Microsoft.CodeAnalysis.Accessibility.Internal,
            typeSymbol, 
            parameters,
            ImmutableDictionary.Create<ITypeSymbol, IParameterNode>(CustomSymbolEqualityComparer.IncludeNullability), 
            transientScopeWideContext.Range, 
            parentContainer, 
            parameterNodeFactory,
            plainFunctionCallNodeFactory,
            asyncFunctionCallNodeFactory,
            scopeCallNodeFactory,
            transientScopeCallNodeFactory,
            containerWideContext)
    {
        _typeToElementNodeMapperFactory = typeToElementNodeMapperFactory;
        _nonWrapToCreateElementNodeMapperFactory = nonWrapToCreateElementNodeMapperFactory;
        Name = prefix;
    }

    protected override IElementNodeMapperBase GetMapper()
    {
        var dummyMapper = _typeToElementNodeMapperFactory();

        return _nonWrapToCreateElementNodeMapperFactory(dummyMapper);
    }

    public override string Name { get; protected set; }
}