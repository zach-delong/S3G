using System;
using StaticSiteGenerator.Markdown;
using StaticSiteGenerator.MarkdownHtmlConversion;
using StaticSiteGenerator.FileManipulation;

using TanvirArjel.Extensions.Microsoft.DependencyInjection;
using StaticSiteGenerator.HtmlWriting;
using StaticSiteGenerator.SiteTemplating.SiteTemplateFilling;

namespace StaticSiteGenerator
{
    [TransientService]
    public class StaticSiteGenerator
    {
        private FileIterator fileIterator;
        private IMarkdownFileParser MarkdownFileParser;
        private IMarkdownConverter MarkdownConverter;

        private CliOptions Options;

        public readonly IHtmlFileWriter HtmlFileWriter;

        public ISiteTemplateFiller SiteTemplateFiller { get; }

        public StaticSiteGenerator(
            FileIterator fileIterator,
            IMarkdownFileParser markdownFileParser,
            IMarkdownConverter markdownConverter,
            CliOptions options,
            IHtmlFileWriter fileWriter,
            ISiteTemplateFiller templateFiller
        ) {
            this.fileIterator = fileIterator;
            this.MarkdownFileParser = markdownFileParser;
            this.MarkdownConverter = markdownConverter;
            this.Options = options;
            this.HtmlFileWriter = fileWriter;
            this.SiteTemplateFiller = templateFiller;
        }

        public void Start()
        {
            try
            {
                var files = fileIterator.GetFilesInDirectory(Options.PathToMarkdownFiles);

                var fileContents = MarkdownFileParser.ReadFiles(files);

                var htmlFiles = MarkdownConverter.Convert(fileContents);

                htmlFiles = SiteTemplateFiller.FillSiteTemplate(htmlFiles);

                HtmlFileWriter.Write(htmlFiles);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception was thrown");
                Console.Write(ex);
            }
        }

    }
}
