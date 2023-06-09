using System.Linq;
using Microsoft.CodeAnalysis;
using MrMeeseeks.SourceGeneratorUtility;
using MsMeeseeks.DIE.Contexts;
using MsMeeseeks.DIE.MsContainer;

namespace MsMeeseeks.DIE;

internal interface IGeneratorConfiguration
{
    bool ErrorDescriptionInsteadOfBuildFailure { get; }
}

internal class GeneratorConfiguration : IGeneratorConfiguration, IContainerInstance
{
    public GeneratorConfiguration(
        GeneratorExecutionContext context,
        IContainerWideContext containerWideContext) =>
        ErrorDescriptionInsteadOfBuildFailure = context.Compilation.Assembly.GetAttributes()
            .Any(ad => CustomSymbolEqualityComparer.Default.Equals(
                containerWideContext.WellKnownTypesMiscellaneous.ErrorDescriptionInsteadOfBuildFailureAttribute, 
                ad.AttributeClass));

    public bool ErrorDescriptionInsteadOfBuildFailure { get; }
}