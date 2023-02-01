namespace StaticSiteGenerator.Utilities.StrategyPattern;

public interface IStrategy<TResult, TInput>
{
    TResult Execute(TInput input);
}
