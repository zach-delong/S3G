using StaticSiteGenerator.Markdown;

namespace StaticSiteGenerator.MarkdownHtmlConversion;

public interface IMarkdownConverter
{
    IHtmlFile Convert(IMarkdownFile markdownFile);
}
