using StaticSiteGenerator.MarkdownHtmlConversion;

namespace StaticSiteGenerator.Markdown.Parser;

public interface IMarkdownFileParser
{
    IHtmlFile ReadFile(string filePath);
}
