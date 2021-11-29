using System;
using StaticSiteGenerator.MarkdownHtmlConversion;

using StaticSiteGenerator.HtmlWriting;
using StaticSiteGenerator.SiteTemplating.SiteTemplateFilling;
using StaticSiteGenerator.Markdown.Parser;
using Microsoft.Extensions.Logging;
using StaticSiteGenerator.Files.FileListing;

namespace StaticSiteGenerator
{
    public class Generator
    {
        private readonly IDirectoryEnumerator directoryLister;
        private readonly IMarkdownFileParser MarkdownFileParser;
        private readonly IMarkdownConverter MarkdownConverter;

        private CliOptions Options;

        public readonly IHtmlFileWriter HtmlFileWriter;
        private readonly ILogger<Generator> logger;

        public ISiteTemplateFiller SiteTemplateFiller { get; }

        public Generator(
            IDirectoryEnumerator directoryLister,
            IMarkdownFileParser markdownFileParser,
            IMarkdownConverter markdownConverter,
            CliOptions options,
            IHtmlFileWriter fileWriter,
            ISiteTemplateFiller templateFiller,
            ILogger<Generator> logger
        ) {
            this.directoryLister = directoryLister;
            this.MarkdownFileParser = markdownFileParser;
            this.MarkdownConverter = markdownConverter;
            this.Options = options;
            this.HtmlFileWriter = fileWriter;
            this.SiteTemplateFiller = templateFiller;
            this.logger = logger;
        }

        public void Start()
        {
            try
            {
                logger.LogTrace("Starting conversion of static site.");
                var fileNames = directoryLister.GetFiles(Options.PathToMarkdownFiles, "*.md");

                var fileContents = MarkdownFileParser.ReadFiles(fileNames);

                var htmlFiles = MarkdownConverter.Convert(fileContents);

                htmlFiles = SiteTemplateFiller.FillSiteTemplate(htmlFiles);

                HtmlFileWriter.Write(htmlFiles);
                logger.LogTrace("Finished conversion of static site.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception was thrown");
                Console.Write(ex);
            }
        }

    }
}
