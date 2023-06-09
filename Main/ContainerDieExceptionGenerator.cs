﻿using System;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using MrMeeseeks.SourceGeneratorUtility.Extensions;
using MsMeeseeks.DIE.Contexts;
using MsMeeseeks.DIE.Logging;

namespace MsMeeseeks.DIE;

internal interface IContainerDieExceptionGenerator 
{
    void Generate(Exception? exception);
}

internal class ContainerDieExceptionGenerator : IContainerDieExceptionGenerator
{
    private readonly GeneratorExecutionContext _context;
    private readonly IDiagLogger _diagLogger;
    private readonly WellKnownTypes _wellKnownTypes;
    private readonly WellKnownTypesMiscellaneous _wellKnownTypesMiscellaneous;
    private readonly INamedTypeSymbol _containerType;
    private readonly WellKnownTypesCollections _wellKnownTypesCollections;

    internal ContainerDieExceptionGenerator(
        GeneratorExecutionContext context,
        IContainerInfoContext containerInfoContext,
        IContainerWideContext containerWideContext,
        IDiagLogger diagLogger)
    {
        _context = context;
        _diagLogger = diagLogger;
        _wellKnownTypes = containerWideContext.WellKnownTypes;
        _wellKnownTypesMiscellaneous = containerWideContext.WellKnownTypesMiscellaneous;
        _wellKnownTypesCollections = containerWideContext.WellKnownTypesCollections;
        _containerType = containerInfoContext.ContainerInfo.ContainerType;
    }

    public void Generate(Exception? exception)
    {
        var generatedContainer = new StringBuilder()
            .AppendLine($$"""
#nullable enable
namespace {{_containerType.ContainingNamespace.FullName()}}
{
partial class {{_containerType.Name}} : {{_wellKnownTypes.IAsyncDisposable.FullName()}}, {{_wellKnownTypes.IDisposable.FullName()}}
{
""");
        
        foreach (var constructor in _containerType.InstanceConstructors)
            generatedContainer.AppendLine($$"""
public static {{_containerType.FullName()}} {{Constants.CreateContainerFunctionName}}({{string.Join(", ", constructor.Parameters.Select(p => $"{ITypeSymbolExtensions.FullName(p.Type)} {p.Name}"))}})
{
return new {{_containerType.FullName()}}({{string.Join(", ", constructor.Parameters.Select(p => $"{p.Name}: {p.Name}"))}});
}
""");
        var listOfKinds = _wellKnownTypesCollections.List1.Construct(_wellKnownTypesMiscellaneous.DieExceptionKind);

        generatedContainer.AppendLine($$"""
public {{listOfKinds.FullName()}} ExceptionKinds_0_0 { get; } = new {{listOfKinds.FullName()}}() { {{string.Join(", ", _diagLogger.ErrorKinds.Select(k => $"{_wellKnownTypesMiscellaneous.DieExceptionKind.FullName()}.{k.ToString()}"))}} };
public string ExceptionToString_0_1 => @"{{exception?.ToString() ?? "no exception"}}";
public void Dispose(){}
public {{_wellKnownTypes.ValueTask.FullName()}} DisposeAsync() => new {{_wellKnownTypes.ValueTask.FullName()}}();
}
}
#nullable disable
""");

        var containerSource = CSharpSyntaxTree
            .ParseText(SourceText.From(generatedContainer.ToString(), Encoding.UTF8))
            .GetRoot()
            .NormalizeWhitespace()
            .SyntaxTree
            .GetText();
        _context.AddSource($"{_containerType.ContainingNamespace.FullName()}.{_containerType.Name}.g.cs", containerSource);
    }
}