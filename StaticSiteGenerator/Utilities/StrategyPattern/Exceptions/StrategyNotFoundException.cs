using System;

namespace StaticSiteGenerator.Utilities.StrategyPattern.Exceptions;

public class StrategyNotFoundException : Exception
{
    public StrategyNotFoundException()
    {
    }

    public StrategyNotFoundException(string message) : base(message)
    {
    }
}
