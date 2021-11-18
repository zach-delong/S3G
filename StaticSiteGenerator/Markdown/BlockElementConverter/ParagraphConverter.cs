using System;

using StaticSiteGenerator.Markdown.BlockElement;
using Markdig.Syntax;
using Microsoft.Extensions.Logging;
using System.Linq;
using Markdig.Syntax.Inlines;
using StaticSiteGenerator.Utilities.StrategyPattern;
using StaticSiteGenerator.Markdown.InlineElement;

namespace StaticSiteGenerator.Markdown.BlockElementConverter
{
    [MarkdownConverterFor(nameof(ParagraphBlock))]
    public class ParagraphConverter: IBlockElementConverter
    {
        private readonly IStrategyExcecutor<IInline, IInlineElement> Parser;

        public ParagraphConverter(
            IStrategyExcecutor<IInline, IInlineElement> parser,
            ILogger<ParagraphConverter> logger)
        {
            Parser = parser;
            Logger = logger;
        }

        private ILogger<ParagraphConverter> Logger { get; }

        public IBlockElement Convert(IBlock block)
        {
            Markdig.Syntax.ParagraphBlock paragraph = (Markdig.Syntax.ParagraphBlock)block;

            Logger.LogDebug($"The paragraph being parsed: {paragraph.Lines}");

            return new Paragraph
            {
                Inlines = Parser.Process(paragraph.Inline)?.ToList()
            };
        }
    }
}
