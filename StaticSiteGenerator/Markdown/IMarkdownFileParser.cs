using System.Collections.Generic;
using StaticSiteGenerator.Markdown.BlockElement;

namespace StaticSiteGenerator.Markdown
{
    public interface IMarkdownFileParser
    {
        IList<IBlockElement> ReadFile(string filePath);
    }
}
