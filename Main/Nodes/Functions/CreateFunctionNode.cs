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

internal interface ICreateFunctionNodeBase : ISingleFunctionNode
{
}

internal interface ICreateFunctionNode : ICreateFunctionNodeBase
{
}

internal partial class CreateFunctionNode : SingleFunctionNodeBase, ICreateFunctionNode, IScopeInstance
{
    private readonly MapperData _mapperData;
    private readonly IMapperFactory _mapperFactory;

    public CreateFunctionNode(
        // parameters
        MapperData mapperData,
        ITypeSymbol typeSymbol, 
        IReadOnlyList<ITypeSymbol> parameters,
        
        // dependencies
        ITransientScopeWideContext transientScopeWideContext,
        IContainerNode parentContainer,
        IReferenceGenerator referenceGenerator, 
        IMapperFactory mapperFactory,
        Func<string?, IReadOnlyList<(IParameterNode, IParameterNode)>, IPlainFunctionCallNode> plainFunctionCallNodeFactory,
        Func<ITypeSymbol, string?, SynchronicityDecision, IReadOnlyList<(IParameterNode, IParameterNode)>, IAsyncFunctionCallNode> asyncFunctionCallNodeFactory,
        Func<(string, string), IScopeNode, IRangeNode, IReadOnlyList<(IParameterNode, IParameterNode)>, IFunctionCallNode?, IScopeCallNode> scopeCallNodeFactory,
        Func<string, ITransientScopeNode, IRangeNode, IReadOnlyList<(IParameterNode, IParameterNode)>, IFunctionCallNode?, ITransientScopeCallNode> transientScopeCallNodeFactory,
        Func<ITypeSymbol, IParameterNode> parameterNodeFactory,
        IContainerWideContext containerWideContext) 
        : base(
            Microsoft.CodeAnalysis.Accessibility.Private,
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
        _mapperData = mapperData;
        _mapperFactory = mapperFactory;
        Name = referenceGenerator.Generate("Create", typeSymbol);
    }

    protected override IElementNodeMapperBase GetMapper() => _mapperFactory.Create(_mapperData);
    
    public override string Name { get; protected set; }
}