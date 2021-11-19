namespace StaticSiteGenerator.Utilities.StrategyPattern
{
    public interface IStrategy<result, input>
    {
        result Execute(input input);
    }
}
