using MrMeeseeks.Visitor;
using MsMeeseeks.DIE.Nodes;
using MsMeeseeks.DIE.Visitors;

[assembly:VisitorInterfacePair(typeof(INodeVisitor), typeof(INode))]

namespace MsMeeseeks.DIE.Visitors;

internal partial interface INodeVisitor
{
}