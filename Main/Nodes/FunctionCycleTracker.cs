using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using MsMeeseeks.DIE.Logging;
using MsMeeseeks.DIE.Nodes.Functions;
using MsMeeseeks.DIE.Nodes.Ranges;

namespace MsMeeseeks.DIE.Nodes;

internal interface IFunctionCycleTracker
{
    void DetectCycle(IContainerNode containerNode);
}

internal class FunctionCycleTracker : IFunctionCycleTracker
{
    private readonly ILocalDiagLogger _localDiagLogger;

    internal FunctionCycleTracker(
        ILocalDiagLogger localDiagLogger) =>
        _localDiagLogger = localDiagLogger;

    public void DetectCycle(IContainerNode containerNode)
    {
        Queue<IFunctionNode> roots = new(containerNode.RootFunctions);
        HashSet<IFunctionNode> v = new();
        HashSet<IFunctionNode> cf = new();
        Stack<IFunctionNode> s = new();

        while (roots.Any() && roots.Dequeue() is {} next)
            DetectCycleInner(
                next, 
                v, 
                s,
                cf);

        void DetectCycleInner(
            IFunctionNode current, 
            ISet<IFunctionNode> visited, 
            Stack<IFunctionNode> stack,
            ISet<IFunctionNode> cycleFree)
        {
            if (cycleFree.Contains(current))
                return; // one of the previous roots checked this node already
            if (visited.Contains(current))
            {
                var cycleStack = ImmutableStack.Create(current.Description);
                IFunctionNode i;
                do
                {
                    i = stack.Pop();
                    cycleStack = cycleStack.Push(current.Description);
                } while (i != current && stack.Any());
                
                _localDiagLogger.Error(ErrorLogData.CircularReferenceAmongFactories(cycleStack), Location.None);
                throw new FunctionCycleDieException(cycleStack);
            }
            visited.Add(current);
            stack.Push(current);
            foreach (var neighbor in current.CalledFunctions)
                DetectCycleInner(neighbor, visited, stack, cycleFree);
            cycleFree.Add(current);
            stack.Pop();
            visited.Remove(current);
            foreach (var localFunction in current.LocalFunctions)
                roots.Enqueue(localFunction);
        }
    }
}