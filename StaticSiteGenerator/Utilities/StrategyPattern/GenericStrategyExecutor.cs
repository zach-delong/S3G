using System.Collections;
using System.Collections.Generic;

namespace StaticSiteGenerator.Utilities.StrategyPattern;

public class GenericStrategyExecutor<TResult, TInput> : IStrategyExecutor<TResult, TInput>
{
    private readonly StrategyCollection<IStrategy<TResult, TInput>> strategies;

    public GenericStrategyExecutor(StrategyCollection<IStrategy<TResult, TInput>> strategies)
    {
        this.strategies = strategies;
    }

    public IEnumerable<TResult> Process(IEnumerable TInputs)
    {
        foreach (TInput i in TInputs)
        {
            var strategy = strategies.GetStrategyForType(i.GetType());

            yield return strategy.Execute(i);
        }
    }
}
