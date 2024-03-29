﻿using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using MsMeeseeks.DIE.Utility;

namespace MsMeeseeks.DIE;

internal interface IExecute 
{
    void Execute();
}

internal sealed class ExecuteImpl : IExecute
{
    private readonly GeneratorExecutionContext _context;
    private readonly IRangeUtility _rangeUtility;
    private readonly RequiredKeywordUtility _requiredKeywordUtility;
    private readonly Func<INamedTypeSymbol, ContainerInfo> _containerInfoFactory;

    internal ExecuteImpl(
        GeneratorExecutionContext context,
        IRangeUtility rangeUtility,
        RequiredKeywordUtility requiredKeywordUtility,
        Func<INamedTypeSymbol, ContainerInfo> containerInfoFactory)
    {
        _context = context;
        _rangeUtility = rangeUtility;
        _requiredKeywordUtility = requiredKeywordUtility;
        _containerInfoFactory = containerInfoFactory;
    }

    public void Execute()
    {
        foreach (var syntaxTree in _context.Compilation.SyntaxTrees)
        {
            var semanticModel = _context.Compilation.GetSemanticModel(syntaxTree);
            var containerInfos = syntaxTree
                .GetRoot()
                .DescendantNodesAndSelf()
                .OfType<ClassDeclarationSyntax>()
                .Select(x => ModelExtensions.GetDeclaredSymbol(semanticModel, x))
                .Where(x => x is not null)
                .OfType<INamedTypeSymbol>()
                .Where(x => _rangeUtility.IsAContainer(x))
                .Select(_containerInfoFactory)
                .ToList();
            foreach (var containerInfo in containerInfos)
            {
                using var msContainer = MsContainer.MsContainer.DIE_CreateContainer(_context, containerInfo, _requiredKeywordUtility);
                var executeContainer = msContainer.Create();
                executeContainer.Execute();
            }
        }
        
        var requiredKeywordTypesFile = _requiredKeywordUtility.GenerateRequiredKeywordTypesFile();
        if (requiredKeywordTypesFile is not null)
        {
            var requiredSource = CSharpSyntaxTree
                .ParseText(SourceText.From(requiredKeywordTypesFile, Encoding.UTF8))
                .GetRoot()
                .NormalizeWhitespace()
                .SyntaxTree
                .GetText();
            
            _context.AddSource("RequiredKeywordTypes.cs", requiredSource);
        }
    }
}