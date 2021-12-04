using System;
using System.Collections.Generic;
using System.IO;
using StaticSiteGenerator.Markdown;
using StaticSiteGenerator.Markdown.BlockElement;
using StaticSiteGenerator.Utilities.StrategyPattern;

namespace StaticSiteGenerator.MarkdownHtmlConversion
{
    public class MarkdownConverter : IMarkdownConverter
    {

        readonly IStrategyExecutor<string, IBlockElement> BlockConverter;

        public readonly CliOptions Options;

        public MarkdownConverter(IStrategyExecutor<string, IBlockElement> blockConverter, CliOptions options)
        {
            BlockConverter = blockConverter;
            Options = options;
        }

        private string Convert(IList<IBlockElement> markdownFile)
        {
            return String.Join(Environment.NewLine, BlockConverter.Process(markdownFile));
        }

        public IHtmlFile Convert(IMarkdownFile markdownFile)
        {
            // Console.WriteLine("Starting conversion of files");
            // Console.WriteLine($"(1) Starting conversion of file {file.Name}");
            return new HtmlFile
            {
                HtmlContent = Convert(markdownFile.Elements),
                Name = Path.Combine(Options.OutputLocation ?? String.Empty,
                                            Path.GetFileName(markdownFile.Name))
            };
            // Console.WriteLine($"(1) Done converting file {file.Name}");
        }

        // Console.WriteLine("Done converting files");
    }
}
