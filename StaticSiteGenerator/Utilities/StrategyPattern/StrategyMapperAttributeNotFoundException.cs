using System;

namespace StaticSiteGenerator.Utilities.StrategyPattern
{
    public class StrategyMapperAttributeNotFoundException : Exception
    {
        public StrategyMapperAttributeNotFoundException(string message) : base($"Unable to find a {nameof(StrategyForTypeAttribute)} {message}")
        {
        }
    }
}
