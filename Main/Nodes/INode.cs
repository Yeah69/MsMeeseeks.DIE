using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace MsMeeseeks.DIE.Nodes;

internal partial interface INode
{
    void Build(ImmutableStack<INamedTypeSymbol> implementationStack);
}