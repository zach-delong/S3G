using System.Collections.Generic;

using StaticSiteGenerator.Markdown.InlineElement;
using StaticSiteGenerator.Markdown.InlineElementConverter;
using StaticSiteGenerator.Utilities.StrategyPattern;
using Markdig.Syntax.Inlines;
using Microsoft.Extensions.Logging;

namespace StaticSiteGenerator.Markdown.Parser.InlineParser
{
    public class MarkdownInlineParser: IMarkdownInlineParser
    {
        private readonly StrategyCollection<IInlineElementConverter> Strategies;

        public MarkdownInlineParser(
            StrategyCollection<IInlineElementConverter> strategies,
            ILogger<MarkdownInlineParser> logger
        )
        {
            Strategies = strategies;
            Logger = logger;
        }

        private ILogger<MarkdownInlineParser> Logger { get; }

        public IList<IInlineElement> Parse(ContainerInline inlines)
        {
            Logger.LogDebug($"Attempting to convert inlines");
            var result = new List<IInlineElement>();

            foreach (var inline in inlines)
            {
                result.Add(Parse(inline));
            }

            Logger.LogDebug("Completed converting inlines");
            return result;
        }

        public IInlineElement Parse(IInline inline)
        {
            Logger.LogDebug($"Attempting to get a converter for inline type: {inline.GetType()}.");
            var converter = Strategies.GetConverterForType(inline.GetType());

            Logger.LogDebug($"Found a converter!");
            return converter?.Convert(inline);
        }
    }
}
