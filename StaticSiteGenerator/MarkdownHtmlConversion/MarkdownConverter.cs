using System;
using System.Collections.Generic;
using System.IO;
using StaticSiteGenerator.Markdown;
using StaticSiteGenerator.Markdown.BlockElement;
using StaticSiteGenerator.MarkdownHtmlConversion.MarkdownHtmlConverters;

namespace StaticSiteGenerator.MarkdownHtmlConversion
{
    public class MarkdownConverter : IMarkdownConverter
    {

        readonly IMarkdownBlockConverter BlockConverter;

        public readonly CliOptions Options;

        public MarkdownConverter(IMarkdownBlockConverter blockConverter, CliOptions options)
        {
            BlockConverter = blockConverter;
            Options = options;
        }

        public string Convert(IList<IBlockElement> markdownFile)
        {
            return BlockConverter.Convert(markdownFile);
        }

        public IEnumerable<IHtmlFile> Convert(IEnumerable<IMarkdownFile> markdownFiles)
        {
            // Console.WriteLine("Starting conversion of files");
            foreach (var file in markdownFiles)
            {
                // Console.WriteLine($"(1) Starting conversion of file {file.Name}");
                yield return new HtmlFile
                {
                    HtmlContent = Convert(file.Elements),
                    Name = Path.Combine(Options.OutputLocation ?? String.Empty,
                                        Path.GetFileName(file.Name))
                };
                // Console.WriteLine($"(1) Done converting file {file.Name}");
            }

            // Console.WriteLine("Done converting files");
        }
    }
}
