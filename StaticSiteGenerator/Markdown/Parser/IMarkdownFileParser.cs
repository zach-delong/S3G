using System.Collections.Generic;
using StaticSiteGenerator.Markdown.BlockElement;

namespace StaticSiteGenerator.Markdown.Parser
{
    public interface IMarkdownFileParser
    {
        IMarkdownFile ReadFile(string filePath);
    }
}
