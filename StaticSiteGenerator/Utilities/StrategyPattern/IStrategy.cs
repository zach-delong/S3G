namespace StaticSiteGenerator.Utilities.StrategyPattern
{
    public interface IStrategy<input, result>
    {
        result Execute(input input);
    }
}
