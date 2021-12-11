using System.Collections;
using System.Collections.Generic;

namespace StaticSiteGenerator.Utilities.StrategyPattern;

public interface IStrategyExecutor<result, input>
{
    public IEnumerable<result> Process(IEnumerable inputs);
}
