using StaticSiteGenerator.Utilities.StrategyPattern;

namespace StaticSiteGenerator.MarkdownHtmlConversion
{
    public interface IHtmlConverter<input>: IStrategy<string, input>
    {}
}
