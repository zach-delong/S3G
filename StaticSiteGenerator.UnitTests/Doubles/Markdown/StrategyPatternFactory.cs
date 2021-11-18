using System.Collections.Generic;
using StaticSiteGenerator.Utilities.StrategyPattern;

namespace StaticSiteGenerator.UnitTests.Doubles.Markdown
{
    public class StrategyPatternFactory {
        public GenericStrategyExecutor<input, result> Get<input, result>(IEnumerable<IStrategy<input, result>> strategies)
        {
            var strategyCollection = new StrategyCollection<IStrategy<input, result>>(strategies);
            var strategyPattern = new GenericStrategyExecutor<input, result>(strategyCollection);
            return strategyPattern;
        }
    }
}
