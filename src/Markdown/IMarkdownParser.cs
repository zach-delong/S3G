using System.Collections.Generic;

using StaticSiteGenerator.Markdown.BlockElement;

namespace StaticSiteGenerator.Markdown
{
    public interface IMarkdownParser
    {
        public IList<IBlockElement> Parse(string markdownFile);
    }
}
