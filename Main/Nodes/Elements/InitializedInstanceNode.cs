using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using MrMeeseeks.SourceGeneratorUtility.Extensions;
using MsMeeseeks.DIE.Nodes.Elements.FunctionCalls;
using MsMeeseeks.DIE.Nodes.Functions;
using MsMeeseeks.DIE.Nodes.Ranges;

namespace MsMeeseeks.DIE.Nodes.Elements;

internal interface IInitializedInstanceNode : IElementNode
{
    bool IsReferenceType { get; }
    IFunctionCallNode BuildCall(IRangeNode range, IFunctionNode callingFunction);
}

internal partial class InitializedInstanceNode : IInitializedInstanceNode
{
    private readonly INamedTypeSymbol _type;

    internal InitializedInstanceNode(
        INamedTypeSymbol type,
        
        IReferenceGenerator referenceGenerator)
    {
        _type = type;
        TypeFullName = type.FullName();
        Reference = referenceGenerator.Generate("_initialized", type);
        IsReferenceType = type.IsReferenceType;
    }

    public void Build(ImmutableStack<INamedTypeSymbol> implementationStack)
    {
    }

    public string TypeFullName { get; }
    public string Reference { get; }
    public bool IsReferenceType { get; }
    
    public IFunctionCallNode BuildCall(IRangeNode range, IFunctionNode callingFunction) => 
        range.BuildCreateCall(_type, callingFunction);
}