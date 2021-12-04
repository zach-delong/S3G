using System.IO.Abstractions;
using StaticSiteGenerator.HtmlWriting;
using StaticSiteGenerator.Markdown.Parser;
using StaticSiteGenerator.MarkdownHtmlConversion;
using StaticSiteGenerator.SiteTemplating.SiteTemplateFilling;
using StaticSiteGenerator.Utilities.StrategyPattern;

namespace StaticSiteGenerator.Files.FileProcessingStrategies
{
    [FileProcessorForType(nameof(MarkdownFileSystemObject))]
    public class MarkdownFileProcessingStrategy : IStrategy<object, IFileSystemObject>
    {
        private readonly IMarkdownFileParser markdownFileParser;
        private readonly IMarkdownConverter markdownConverter;
        private readonly IHtmlFileWriter fileWriter;
        private readonly ISiteTemplateFiller templateFiller;
        private readonly CliOptions options;
        private readonly IFileSystem fileSystem;

        public MarkdownFileProcessingStrategy(
            IMarkdownFileParser markdownFileParser,
            IMarkdownConverter markdownConverter,
            IHtmlFileWriter fileWriter,
            ISiteTemplateFiller templateFiller,
            CliOptions options,
            IFileSystem fileSystem
        )
        {
            this.markdownFileParser = markdownFileParser;
            this.markdownConverter = markdownConverter;
            this.fileWriter = fileWriter;
            this.templateFiller = templateFiller;
            this.options = options;
            this.fileSystem = fileSystem;
        }

        public object Execute(IFileSystemObject input)
        {
            var fileContents = markdownFileParser.ReadFile(input.FullPath);

            var htmlFile = markdownConverter.Convert(fileContents);

            htmlFile.HtmlContent = templateFiller.FillSiteTemplate(htmlFile.HtmlContent);

            var inputRoot = fileSystem.Path.GetFullPath(options.PathToMarkdownFiles);

            var fileRelativeInputRoot = fileSystem.Path.GetRelativePath(inputRoot, input.FullPath);
            var outputFilePath = fileSystem.Path.Combine(options.OutputLocation, fileRelativeInputRoot).Replace(".md", ".html");

            fileWriter.Write(outputFilePath, htmlFile.HtmlContent);

            return null;
        }
    }
}
