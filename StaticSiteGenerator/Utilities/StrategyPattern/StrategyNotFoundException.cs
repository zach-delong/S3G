using System;

namespace StaticSiteGenerator.Utilities.StrategyPattern
{
    public class StrategyNotFoundException : Exception
    {
        public StrategyNotFoundException(string message) : base(message)
        {
        }
    }
}
