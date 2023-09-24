using System.IO.Abstractions;
using StaticSiteGenerator.Files;
using StaticSiteGenerator.HtmlWriting;
using StaticSiteGenerator.Markdown.Parser;
using StaticSiteGenerator.SiteTemplating.SiteTemplateFilling;
using StaticSiteGenerator.Utilities.StrategyPattern;

namespace StaticSiteGenerator.FileProcessingStrategies;

[FileProcessorForType(nameof(MarkdownFileSystemObject))]
public class MarkdownFileProcessingStrategy : IStrategy<object, IFileSystemObject>
{
    private readonly IMarkdownFileParser markdownFileParser;
    private readonly IHtmlFileWriter fileWriter;
    private readonly ISiteTemplateFiller templateFiller;
    private readonly CliOptions options;
    private readonly IFileSystem fileSystem;

    public MarkdownFileProcessingStrategy(
        IMarkdownFileParser markdownFileParser,
        IHtmlFileWriter fileWriter,
        ISiteTemplateFiller templateFiller,
        CliOptions options,
        IFileSystem fileSystem
    )
    {
        this.markdownFileParser = markdownFileParser;
        this.fileWriter = fileWriter;
        this.templateFiller = templateFiller;
        this.options = options;
        this.fileSystem = fileSystem;
    }

    public object Execute(IFileSystemObject input)
    {
        var htmlFile = markdownFileParser.ReadFile(input.FullPath);

	if(!htmlFile.IsPublished)
            return null;

        htmlFile.HtmlContent = templateFiller.FillSiteTemplate(htmlFile);

	WriteOutputfile(input, htmlFile);

        return null;
    }

    private string GetOutputFilePath(IFileSystemObject file)
    {
        var inputRoot = fileSystem.Path.GetFullPath(options.PathToMarkdownFiles);

	var fileRelativeInputRoot = fileSystem.Path.GetRelativePath(inputRoot, file.FullPath);

	return fileSystem.Path.Combine(options.OutputLocation, fileRelativeInputRoot).Replace(".md", ".html");
    }

    private void WriteOutputfile(IFileSystemObject input, IHtmlFile htmlFile)
    {
            var outputFilePath = GetOutputFilePath(input);

	    fileWriter.Write(outputFilePath, htmlFile.HtmlContent);
    }
}
