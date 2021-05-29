using System;

using Microsoft.Toolkit.Parsers.Markdown.Blocks;
using StaticSiteGenerator.Markdown.BlockElement;

using StaticSiteGenerator.Markdown.Parser.InlineParser;

namespace StaticSiteGenerator.Markdown.BlockElementConverter
{
    [MarkdownConverterFor(nameof(HeaderBlock))]
    public class HeaderConverter: IBlockElementConverter
    {

        private readonly IMarkdownInlineParser InlineParser;
        public HeaderConverter(IMarkdownInlineParser inlineParser)
        {
            InlineParser = inlineParser;
        }

        public IBlockElement Convert(MarkdownBlock block)
        {
            HeaderBlock b = (HeaderBlock)block;
            return new Header
            {
                Level = (byte)b.HeaderLevel,

                Inlines = InlineParser.Parse(b.Inlines)
            };
        }
    }
}
