using System.IO;
using System;
using Microsoft.Extensions.Logging;
using StaticSiteGenerator.Files;
using StaticSiteGenerator.HtmlWriting;

namespace StaticSiteGenerator.Markdown.Parser;

public class MarkdownFileParser : IMarkdownFileParser
{
    private readonly FileReader fileParser;
    private readonly ILogger<MarkdownFileParser> logger;
    private readonly MarkdownConverter markdownConverter;

    public MarkdownFileParser(
        FileReader fileParser,
        ILogger<MarkdownFileParser> logger,
        MarkdownConverter markdownConverter
    )
    {
        this.fileParser = fileParser;
        this.logger = logger;
        this.markdownConverter = markdownConverter;
    }

    public IHtmlFile ReadFile(string filePath)
    {
        logger.LogInformation("Starting to convert string contents to Markdown");
        logger.LogTrace($"Starting to convert file {filePath}");

        logger.LogTrace($"Reading file: {filePath}");
        var fileContents = fileParser.ReadFile(filePath);

        logger.LogTrace($"Read file contents: {fileContents?.Substring(0, ((fileContents.Length > 50) ? 50 : fileContents.Length)) ?? String.Empty}");
        var parsedFile = markdownConverter.ConvertToHtml(fileContents);

        IHtmlFile file = new HtmlFile
        {
            HtmlContent = parsedFile.Contents,
            Name = Path.GetFileNameWithoutExtension(filePath),
	    Title = parsedFile?.Properties?.Title,
	    IsPublished = parsedFile?.Properties?.Published ?? true
        };

        logger.LogTrace($"Converted file: {filePath}");

        return file;
    }
}
