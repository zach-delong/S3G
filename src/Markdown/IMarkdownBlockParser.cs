using System.Collections.Generic;

using Microsoft.Toolkit.Parsers.Markdown.Blocks;

using StaticSiteGenerator.Markdown.BlockElement;

namespace StaticSiteGenerator.Markdown
{
    public interface IMarkdownBlockParser
    {
        public IList<IBlockElement> Parse(IList<MarkdownBlock> blocks);
    }
}
