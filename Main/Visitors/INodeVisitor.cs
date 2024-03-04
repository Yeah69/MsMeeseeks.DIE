using MsMeeseeks.DIE.Nodes;
using MrMeeseeks.Visitor;


namespace MsMeeseeks.DIE.Visitors;

[VisitorInterface(typeof(INode))]
internal partial interface INodeVisitor;