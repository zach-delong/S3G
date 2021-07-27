using System.Collections.Generic;
using StaticSiteGenerator.Markdown.BlockElement;
using StaticSiteGenerator.Markdown.BlockElementConverter;
using StaticSiteGenerator.Utilities.StrategyPattern;
using Markdig.Syntax;
using Microsoft.Extensions.Logging;

namespace StaticSiteGenerator.Markdown.Parser.BlockParser
{

    public class MarkdownBlockParser: IMarkdownBlockParser
    {
        public MarkdownBlockParser(
            StrategyCollection<IBlockElementConverter> converters,
            ILogger<MarkdownBlockParser> logger)
        {
            Converters = converters;
            Logger = logger;
        }

        private ILogger<MarkdownBlockParser> Logger { get; }
        private StrategyCollection<IBlockElementConverter> Converters;

        public IList<IBlockElement> Parse(MarkdownDocument document)
        {
            Logger.LogDebug("Starting converting from 3rd party document format to internal format");
            var list = new List<IBlockElement>();

            foreach(var block in document)
            {
                list.Add(Parse(block));
            }

            Logger.LogDebug("Successfully converted document");
            return list;
        }

        public IBlockElement Parse(IBlock block)
        {
            Logger.LogDebug($"Attempting to convert block from {block.GetType()}");
            var converter = Converters.GetConverterForType(block.GetType());

            return converter.Convert(block);
        }
    }
}
