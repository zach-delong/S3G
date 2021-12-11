namespace StaticSiteGenerator.Markdown.Parser;

public interface IMarkdownFileParser
{
    IMarkdownFile ReadFile(string filePath);
}
