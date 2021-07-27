using System;

using StaticSiteGenerator.Markdown.Parser.InlineParser;
using StaticSiteGenerator.Markdown.BlockElement;
using Markdig.Syntax;
using Microsoft.Extensions.Logging;

namespace StaticSiteGenerator.Markdown.BlockElementConverter
{
    [MarkdownConverterFor(nameof(ParagraphBlock))]
    public class ParagraphConverter: IBlockElementConverter
    {
        private readonly IMarkdownInlineParser Parser;

        public ParagraphConverter(
            IMarkdownInlineParser parser,
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
                Inlines = Parser.Parse(paragraph.Inline)
            };
        }
    }
}
