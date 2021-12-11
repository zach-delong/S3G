using StaticSiteGenerator.Utilities.StrategyPattern;

namespace StaticSiteGenerator.MarkdownHtmlConversion;

public class HtmlConverterForAttribute : StrategyForTypeAttribute
{
    public HtmlConverterForAttribute(string typeName) : base(typeName)
    {
    }
}
