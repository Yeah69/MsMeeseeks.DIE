using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using MsMeeseeks.DIE.Nodes;
using MsMeeseeks.DIE.Nodes.Ranges;

namespace MsMeeseeks.DIE.Extensions;

// ReSharper disable once InconsistentNaming
internal static class TExtensions
{
    internal static T EnqueueBuildJobTo<T>(this T item, Queue<BuildJob> queue, ImmutableStack<INamedTypeSymbol> implementationSet) 
        where T : INode
    {
        var buildJob = new BuildJob(item, implementationSet);
        queue.Enqueue(buildJob);
        return item;
    }
}