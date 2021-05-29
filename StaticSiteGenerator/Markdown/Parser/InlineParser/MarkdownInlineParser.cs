using System.Collections.Generic;

using StaticSiteGenerator.Markdown.InlineElement;
using StaticSiteGenerator.Markdown.InlineElementConverter;
using Microsoft.Toolkit.Parsers.Markdown.Inlines;
using StaticSiteGenerator.Utilities.StrategyPattern;

namespace StaticSiteGenerator.Markdown.Parser.InlineParser
{
    public class MarkdownInlineParser: IMarkdownInlineParser
    {
        private readonly StrategyCollection<IInlineElementConverter> Strategies;

        public MarkdownInlineParser(
            StrategyCollection<IInlineElementConverter> strategies
        )
        {
            Strategies = strategies;
        }

        public IList<IInlineElement> Parse(IList<MarkdownInline> inlines)
        {
            var result = new List<IInlineElement>();

            foreach (var inline in inlines)
            {
                result.Add(Parse(inline));
            }

            return result;
        }

        public IInlineElement Parse(MarkdownInline inline)
        {
            var converter = Strategies.GetConverterForType(inline.GetType());

            return converter?.Convert(inline);
        }
    }
}
