using System;
using StaticSiteGenerator.Markdown;
using StaticSiteGenerator.MarkdownHtmlConversion;
using StaticSiteGenerator.FileManipulation;

using StaticSiteGenerator.HtmlWriting;
using StaticSiteGenerator.SiteTemplating.SiteTemplateFilling;
using StaticSiteGenerator.Markdown.Parser;
using StaticSiteGenerator.FileManipulation.FileListing;

namespace StaticSiteGenerator
{
    public class StaticSiteGenerator
    {
        private readonly IDirectoryEnumerator directoryLister;
        private readonly IMarkdownFileParser MarkdownFileParser;
        private readonly IMarkdownConverter MarkdownConverter;

        private CliOptions Options;

        public readonly IHtmlFileWriter HtmlFileWriter;

        public ISiteTemplateFiller SiteTemplateFiller { get; }

        public StaticSiteGenerator(
            IDirectoryEnumerator directoryLister,
            IMarkdownFileParser markdownFileParser,
            IMarkdownConverter markdownConverter,
            CliOptions options,
            IHtmlFileWriter fileWriter,
            ISiteTemplateFiller templateFiller
        ) {
            this.directoryLister = directoryLister;
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
                var fileNames = directoryLister.GetFiles(Options.PathToMarkdownFiles, "*.md");

                var fileContents = MarkdownFileParser.ReadFiles(fileNames);

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
