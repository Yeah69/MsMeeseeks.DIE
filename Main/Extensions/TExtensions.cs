using MsMeeseeks.DIE.Nodes;
using MsMeeseeks.DIE.Nodes.Ranges;

namespace MsMeeseeks.DIE.Extensions;

// ReSharper disable once InconsistentNaming
internal static class TExtensions
{
    internal static T EnqueueBuildJobTo<T>(this T item, Queue<BuildJob> queue, PassedContext passedContext) 
        where T : INode
    {
        var buildJob = new BuildJob(item, passedContext);
        queue.Enqueue(buildJob);
        return item;
    }
}