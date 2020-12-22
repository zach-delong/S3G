using System.Collections.Generic;

using StaticSiteGenerator.Markdown.InlineElement;
using StaticSiteGenerator.Markdown.InlineElementConverter;
using Microsoft.Toolkit.Parsers.Markdown.Inlines;
using StaticSiteGenerator.Utilities.StrategyPattern;

using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace StaticSiteGenerator.Markdown.Parser.InlineParser
{

    [TransientService]
    public class MarkdownInlineParser: IMarkdownInlineParser
    {
        private readonly StrategyCollection Strategies;

        public MarkdownInlineParser(
            IEnumerable<IInlineElementConverter> converters,
            StrategyCollection strategies
        )
        {
            Strategies = strategies;
            Strategies.SetCollection(converters);
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
