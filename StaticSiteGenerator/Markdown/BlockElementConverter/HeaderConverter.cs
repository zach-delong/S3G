using System.Linq;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using StaticSiteGenerator.Markdown.BlockElement;
using StaticSiteGenerator.Markdown.InlineElement;
using StaticSiteGenerator.Utilities.StrategyPattern;

namespace StaticSiteGenerator.Markdown.BlockElementConverter;

[MarkdownConverterFor(nameof(HeadingBlock))]
public class HeaderConverter : IBlockElementConverter
{

    private readonly IStrategyExecutor<IInlineElement, IInline> InlineParser;

    public HeaderConverter(IStrategyExecutor<IInlineElement, IInline> inlineParser)
    {
        InlineParser = inlineParser;
    }

    public IBlockElement Execute(IBlock block)
    {
        Markdig.Syntax.HeadingBlock header = (Markdig.Syntax.HeadingBlock)block;
        return new Header
        {
            Level = (byte)header.Level,
            Inlines = InlineParser.Process(header.Inline)?.ToList()
        };
    }
}
