using System.Collections.Generic;

using Microsoft.Toolkit.Parsers.Markdown.Blocks;

using StaticSiteGenerator.Markdown.BlockElement;

namespace StaticSiteGenerator.Markdown.Parser.BlockParser
{
    public interface IMarkdownBlockParser
    {
        public IList<IBlockElement> Parse(IList<MarkdownBlock> blocks);
    }
}
