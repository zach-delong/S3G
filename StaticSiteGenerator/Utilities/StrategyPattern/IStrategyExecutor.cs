using System.Collections;
using System.Collections.Generic;

namespace StaticSiteGenerator.Utilities.StrategyPattern;

public interface IStrategyExecutor<TResult, TInput>
{
    public IEnumerable<TResult> Process(IEnumerable inputs);
}
