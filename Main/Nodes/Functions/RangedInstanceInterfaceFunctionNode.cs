using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using MrMeeseeks.SourceGeneratorUtility;
using MsMeeseeks.DIE.Contexts;
using MsMeeseeks.DIE.MsContainer;
using MsMeeseeks.DIE.Nodes.Elements;
using MsMeeseeks.DIE.Nodes.Elements.FunctionCalls;
using MsMeeseeks.DIE.Nodes.Ranges;

namespace MsMeeseeks.DIE.Nodes.Functions;

internal interface IRangedInstanceInterfaceFunctionNode : IFunctionNode
{
    void AddConsideredRange(IRangeNode range);
}

internal partial class RangedInstanceInterfaceFunctionNode : ReturningFunctionNodeBase, IRangedInstanceInterfaceFunctionNode, IScopeInstance
{
    private readonly INamedTypeSymbol _type;
    private readonly IContainerNode _parentContainer;
    private readonly List<IRangedInstanceFunctionNode> _implementations = new(); 
    
    public RangedInstanceInterfaceFunctionNode(
        // parameters
        INamedTypeSymbol type, 
        IReadOnlyList<ITypeSymbol> parameters,
        
        // dependencies
        IContainerNode parentContainer,
        ITransientScopeWideContext transientScopeWideContext,
        IReferenceGenerator referenceGenerator, 
        Func<string?, IReadOnlyList<(IParameterNode, IParameterNode)>, IPlainFunctionCallNode> plainFunctionCallNodeFactory,
        Func<ITypeSymbol, string?, SynchronicityDecision, IReadOnlyList<(IParameterNode, IParameterNode)>, IAsyncFunctionCallNode> asyncFunctionCallNodeFactory,
        Func<(string, string), IScopeNode, IRangeNode, IReadOnlyList<(IParameterNode, IParameterNode)>, IFunctionCallNode?, IScopeCallNode> scopeCallNodeFactory,
        Func<string, ITransientScopeNode, IRangeNode, IReadOnlyList<(IParameterNode, IParameterNode)>, IFunctionCallNode?, ITransientScopeCallNode> transientScopeCallNodeFactory,
        Func<ITypeSymbol, IParameterNode> parameterNodeFactory,
        IContainerWideContext containerWideContext) 
        : base(
            Microsoft.CodeAnalysis.Accessibility.Private,
            type, 
            parameters,
            ImmutableDictionary.Create<ITypeSymbol, IParameterNode>(CustomSymbolEqualityComparer.IncludeNullability), 
            parentContainer, 
            transientScopeWideContext.Range,
            parameterNodeFactory,
            plainFunctionCallNodeFactory,
            asyncFunctionCallNodeFactory,
            scopeCallNodeFactory,
            transientScopeCallNodeFactory,
            containerWideContext)
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