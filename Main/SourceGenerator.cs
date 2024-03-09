﻿using MsMeeseeks.DIE.Contexts;
using MsMeeseeks.DIE.Utility;

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
            var wellKnownTypesMiscellaneous = WellKnownTypesMiscellaneous.Create(context.Compilation);
            var rangeUtility = new RangeUtility(wellKnownTypesMiscellaneous);
        
            var execute = new ExecuteImpl(
                context,
                rangeUtility,
                ContainerInfoFactory);
            execute.Execute();
                
            IContainerInfo ContainerInfoFactory(INamedTypeSymbol type) => 
                new ContainerInfo(type, wellKnownTypesMiscellaneous, rangeUtility);
        }
        catch (ValidationDieException)
        {
            // nothing to do here
        }
    }
}