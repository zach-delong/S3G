using Markdig.Syntax;
using StaticSiteGenerator.Markdown.BlockElement;

using StaticSiteGenerator.Markdown.Parser.InlineParser;

namespace StaticSiteGenerator.Markdown.BlockElementConverter
{
    [MarkdownConverterFor(nameof(HeadingBlock))]
    public class HeaderConverter: IBlockElementConverter
    {

        private readonly IMarkdownInlineParser InlineParser;
        public HeaderConverter(IMarkdownInlineParser inlineParser)
        {
            InlineParser = inlineParser;
        }

        public IBlockElement Convert(IBlock block)
        {
            Markdig.Syntax.HeadingBlock header = (Markdig.Syntax.HeadingBlock)block;
            return new Header
            {
                Level = (byte)header.Level,
                Inlines = InlineParser.Parse(header.Inline)
        };
        }
    }
}
