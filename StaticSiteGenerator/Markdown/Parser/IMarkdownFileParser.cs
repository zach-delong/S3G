using StaticSiteGenerator.HtmlWriting;

namespace StaticSiteGenerator.Markdown.Parser;

public interface IMarkdownFileParser
{
    IHtmlFile ReadFile(string filePath);
}
