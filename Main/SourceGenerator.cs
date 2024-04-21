using MsMeeseeks.DIE.CodeGeneration;
using MsMeeseeks.DIE.Logging;
using MsMeeseeks.DIE.Utility;
using MrMeeseeks.SourceGeneratorUtility;

namespace MsMeeseeks.DIE;

[Generator]
public class SourceGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
        // no initialization required
    }

    public void Execute(GeneratorExecutionContext context)
    {
        try
        {
            var wellKnownTypes = WellKnownTypes.Create(context.Compilation);
            var wellKnownTypesCollections = WellKnownTypesCollections.Create(context.Compilation);
            var wellKnownTypesMiscellaneous = WellKnownTypesMiscellaneous.Create(context.Compilation);
            var rangeUtility = new RangeUtility(wellKnownTypesMiscellaneous);
            var requiredKeywordUtility = new RequiredKeywordUtility(context, new CheckInternalsVisible(context));
            var referenceGeneratorCounter = new ReferenceGeneratorCounter();
            var singularDisposeFunctionUtility = new DisposeUtility(
                new ReferenceGenerator(
                    referenceGeneratorCounter, 
                    new LocalDiagLogger(
                        new FunctionLevelLogMessageEnhancerForSourceGenerator(), 
                        new DiagLogger(new GeneratorConfiguration(context, wellKnownTypesMiscellaneous), context))), 
                wellKnownTypes,
                wellKnownTypesCollections);

            var execute = new ExecuteImpl(
                context,
                rangeUtility,
                requiredKeywordUtility,
                singularDisposeFunctionUtility,
                referenceGeneratorCounter,
                ContainerInfoFactory);
            execute.Execute();
                
            ContainerInfo ContainerInfoFactory(INamedTypeSymbol type) => 
                new(type, wellKnownTypesMiscellaneous, rangeUtility);
        }
        catch (ValidationDieException)
        {
            // nothing to do here
        }
    }
}