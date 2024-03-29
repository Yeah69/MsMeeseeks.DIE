using MrMeeseeks.SourceGeneratorUtility.Extensions;

namespace MsMeeseeks.DIE.Nodes.Elements;

internal interface IKeyValuePairNode : IElementNode
{
    ITypeSymbol KeyType { get; }
    object Key { get; }
    IElementNode Value { get; }
}

internal sealed partial class KeyValuePairNode : IKeyValuePairNode
{
    internal KeyValuePairNode(
        // parameters
        INamedTypeSymbol keyValuePairType,
        object key,
        IElementNode value,
        
        // dependencies
        IReferenceGenerator referenceGenerator)
    {
        Key = key;
        KeyType = keyValuePairType.TypeArguments[0];
        Value = value;
        TypeFullName = keyValuePairType.FullName();
        Reference = referenceGenerator.Generate(keyValuePairType);
    }
    public void Build(PassedContext passedContext) { }

    public string TypeFullName { get; }
    public string Reference { get; }
    public ITypeSymbol KeyType { get; }
    public object Key { get; }
    public IElementNode Value { get; }
}