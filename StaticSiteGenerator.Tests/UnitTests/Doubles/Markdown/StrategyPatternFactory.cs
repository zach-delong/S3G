using System.Collections.Generic;
using StaticSiteGenerator.Utilities.StrategyPattern;

namespace StaticSiteGenerator.Tests.UnitTests.Doubles.Markdown;

public class StrategyPatternFactory
{
    public GenericStrategyExecutor<TInput, TResult> Get<TInput, TResult>(IEnumerable<IStrategy<TInput, TResult>> strategies)
    {
        var strategyCollection = new StrategyCollection<IStrategy<TInput, TResult>>(strategies);
        var strategyPattern = new GenericStrategyExecutor<TInput, TResult>(strategyCollection);
        return strategyPattern;
    }
}
