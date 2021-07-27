using System.Collections.Generic;
using Markdig.Syntax;

using StaticSiteGenerator.Markdown.BlockElement;

namespace StaticSiteGenerator.Markdown.Parser.BlockParser
{
    public interface IMarkdownBlockParser
    {
        public IList<IBlockElement> Parse(MarkdownDocument document);
    }
}
