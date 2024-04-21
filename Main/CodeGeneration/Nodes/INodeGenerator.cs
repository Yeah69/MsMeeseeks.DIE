namespace MsMeeseeks.DIE.CodeGeneration.Nodes;

internal interface INodeGenerator
{
    void Generate(StringBuilder code, ICodeGenerationVisitor visitor);
}