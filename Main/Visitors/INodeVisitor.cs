using MrMeeseeks.Visitor;
using MsMeeseeks.DIE.Nodes;

namespace MsMeeseeks.DIE.Visitors;

[VisitorInterface(typeof(INode))]
internal partial interface INodeVisitor
{
}