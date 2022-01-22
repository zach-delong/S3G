using StaticSiteGenerator.Markdown.BlockElement;
using System.IO;
using System;
using Microsoft.Extensions.Logging;
using StaticSiteGenerator.Utilities.StrategyPattern;
using Markdig.Syntax;
using StaticSiteGenerator.Files;
using StaticSiteGenerator.MarkdownHtmlConversion;

namespace StaticSiteGenerator.Markdown.Parser;

public class MarkdownFileParser : IMarkdownFileParser
{
    private readonly FileReader fileParser;
    private readonly IStrategyExecutor<IBlockElement, IBlock> markdownParser;
    private readonly ILogger<MarkdownFileParser> logger;
    private readonly MarkdownConverter markdownConverter;

    public MarkdownFileParser(
        FileReader fileParser,
        IStrategyExecutor<IBlockElement, IBlock> markdownParser,
        ILogger<MarkdownFileParser> logger,
        MarkdownConverter markdownConverter
    )
    {
        this.fileParser = fileParser;
        this.markdownParser = markdownParser;
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
        var parsedContents = markdownConverter.ConvertToHtml(fileContents);

        IHtmlFile file = new HtmlFile
        {
            HtmlContent = parsedContents,
            Name = Path.GetFileNameWithoutExtension(filePath)
        };

        logger.LogTrace($"Converted file: {filePath}");

        return file;
    }
}
