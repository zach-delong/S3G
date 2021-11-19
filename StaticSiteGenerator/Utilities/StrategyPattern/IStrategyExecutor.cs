using System.Collections;
using System.Collections.Generic;

namespace StaticSiteGenerator.Utilities.StrategyPattern {
    public interface IStrategyExcecutor<result, input>
    {
        public IEnumerable<result> Process(IEnumerable inputs);
    }
}
