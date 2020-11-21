using System;

using Microsoft.Toolkit.Parsers.Markdown.Blocks;
using StaticSiteGenerator.Markdown.BlockElement;

using TanvirArjel.Extensions.Microsoft.DependencyInjection;

using StaticSiteGenerator.Markdown;

namespace StaticSiteGenerator.Markdown.BlockElementConverter
{
    [TransientService]
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
