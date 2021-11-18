using System.Collections;
using System.Collections.Generic;

namespace StaticSiteGenerator.Utilities.StrategyPattern {
    public interface IStrategyExcecutor<input, result>
    {
        public IEnumerable<result> Process(IEnumerable inputs);
    }
}
