using System;

namespace StaticSiteGenerator.Utilities.StrategyPattern
{
    public class StrategyNotFoundException : Exception
    {
        public StrategyNotFoundException()
        {
        }

        public StrategyNotFoundException(string message) : base(message)
        {
        }
    }
}
