using MsMeeseeks.DIE.MsContainer;
using MrMeeseeks.SourceGeneratorUtility;

namespace MsMeeseeks.DIE.Analytics;

internal interface IAnalyticsFlags
{
    bool ResolutionGraph { get; }
    bool ErrorFilteredResolutionGraph { get; }
}

internal sealed class AnalyticsFlags : IAnalyticsFlags, IContainerInstance
{
    private readonly MrMeeseeks.DIE.Configuration.Attributes.Analytics? _analytics;
    
    internal AnalyticsFlags(
        IContainerInfo containerInfo,
        WellKnownTypesMiscellaneous wellKnownTypesMiscellaneous,
        GeneratorExecutionContext context)
    {
        var attributeData = containerInfo.ContainerType
            .GetAttributes()
            .FirstOrDefault(CheckAttribute) 
            ?? context.Compilation.Assembly.GetAttributes().FirstOrDefault(CheckAttribute);

        _analytics = attributeData?.ConstructorArguments[0].Value is int analytics
            ? (MrMeeseeks.DIE.Configuration.Attributes.Analytics) analytics
            : null;
        
        bool CheckAttribute(AttributeData ad) =>
            CustomSymbolEqualityComparer.Default.Equals(ad.AttributeClass, wellKnownTypesMiscellaneous.AnalyticsAttribute) 
            && ad.ConstructorArguments.Length == 1 
            && ad.ConstructorArguments[0].Value is int;
    }

    public bool ResolutionGraph => _analytics?.HasFlag(MrMeeseeks.DIE.Configuration.Attributes.Analytics.ResolutionGraph) ?? false;

    public bool ErrorFilteredResolutionGraph => _analytics?.HasFlag(MrMeeseeks.DIE.Configuration.Attributes.Analytics.ErrorFilteredResolutionGraph) ?? false;
}