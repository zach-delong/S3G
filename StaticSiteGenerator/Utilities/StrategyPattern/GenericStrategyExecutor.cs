using System.Collections;
using System.Collections.Generic;

namespace StaticSiteGenerator.Utilities.StrategyPattern
{
    public class GenericStrategyExecutor<input, result> : IStrategyExcecutor<input, result>
    {
        private readonly StrategyCollection<IStrategy<input, result>> strategies;

        public GenericStrategyExecutor(StrategyCollection<IStrategy<input, result>> strategies)
        {
            this.strategies = strategies;
        }

        public IEnumerable<result> Process(IEnumerable inputs)
        {
            foreach (input i in inputs)
            {
                var strategy = strategies.GetStrategyForType(i.GetType());

                yield return strategy.Execute(i);
            }
        }
    }
}
