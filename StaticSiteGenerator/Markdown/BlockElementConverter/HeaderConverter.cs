using System;
using System.Linq;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using StaticSiteGenerator.Markdown.BlockElement;
using StaticSiteGenerator.Markdown.InlineElement;
using StaticSiteGenerator.Utilities.StrategyPattern;

namespace StaticSiteGenerator.Markdown.BlockElementConverter
{
    [MarkdownConverterFor(nameof(HeadingBlock))]
    public class HeaderConverter: IBlockElementConverter
    {

        private readonly IStrategyExcecutor<IInline, IInlineElement> InlineParser;

        public HeaderConverter(IStrategyExcecutor<IInline, IInlineElement> inlineParser)
        {
            InlineParser = inlineParser;
        }

        public IBlockElement Convert(IBlock block)
        {
            Markdig.Syntax.HeadingBlock header = (Markdig.Syntax.HeadingBlock)block;
            return new Header
            {
                Level = (byte)header.Level,
                Inlines = InlineParser.Process(header.Inline)?.ToList()
            };
        }
    }
}
