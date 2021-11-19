using System.Collections;
using System.Collections.Generic;

namespace StaticSiteGenerator.Utilities.StrategyPattern
{
    public class GenericStrategyExecutor<result, input> : IStrategyExcecutor<result, input>
    {
        private readonly StrategyCollection<IStrategy<result, input>> strategies;

        public GenericStrategyExecutor(StrategyCollection<IStrategy<result, input>> strategies)
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
