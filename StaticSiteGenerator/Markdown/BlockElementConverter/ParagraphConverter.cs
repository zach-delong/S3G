using System;

using Microsoft.Toolkit.Parsers.Markdown.Blocks;
using StaticSiteGenerator.Markdown.Parser.InlineParser;
using StaticSiteGenerator.Markdown.BlockElement;

namespace StaticSiteGenerator.Markdown.BlockElementConverter
{
    [MarkdownConverterFor(nameof(ParagraphBlock))]
    public class ParagraphConverter: IBlockElementConverter
    {
        private readonly IMarkdownInlineParser Parser;

        public ParagraphConverter(IMarkdownInlineParser parser)
        {
            Parser = parser;
        }

        public IBlockElement Convert(MarkdownBlock block)
        {
            ParagraphBlock b = (ParagraphBlock)block;
            return new Paragraph
            {
                Inlines = Parser.Parse(b.Inlines)
            };
        }
    }
}
