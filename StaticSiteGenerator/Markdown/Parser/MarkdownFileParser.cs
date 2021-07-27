using System.Collections.Generic;
using StaticSiteGenerator.FileManipulation;
using StaticSiteGenerator.Markdown.BlockElement;
using StaticSiteGenerator.Markdown.Parser.BlockParser;
using System.IO;
using System;
using Microsoft.Extensions.Logging;

namespace StaticSiteGenerator.Markdown.Parser
{
    public class MarkdownFileParser : IMarkdownFileParser
    {
        private readonly FileReader fileParser;
        private readonly IMarkdownBlockParser markdownParser;
        private readonly ILogger<MarkdownFileParser> logger;

        public MarkdownFileParser(
            FileReader fileParser,
            IMarkdownBlockParser markdownParser,
            ILogger<MarkdownFileParser> logger
        )
        {
            this.fileParser = fileParser;
            this.markdownParser = markdownParser;
            this.logger = logger;
        }

        public IList<IBlockElement> ReadFile(string filePath)
        {
            logger.LogTrace($"Reading file: {filePath}");
            var fileContents = fileParser.ReadFile(filePath);

            logger.LogTrace($"Read file contents: {fileContents?.Substring(0, ((fileContents.Length > 50) ? 50 : fileContents.Length)) ?? String.Empty}");
            var parsedContents = ParseMarkdownString(fileContents);

            logger.LogTrace($"Succesfully converted {filePath} into Block Elements");
            return parsedContents;
        }

        public IEnumerable<IMarkdownFile> ReadFiles(IEnumerable<string> filePaths)
        {
            logger.LogInformation("Starting to convert string contents to Markdown");
            foreach (var filePath in filePaths)
            {
                logger.LogTrace($"Starting to convert file {filePath}");
                IMarkdownFile file = new MarkdownFile
                {
                    Elements = ReadFile(filePath),
                    Name = Path.GetFileNameWithoutExtension(filePath)
                };

                yield return file;

                logger.LogTrace($"Converted file: {filePath}");
            }
        }

        private IList<IBlockElement> ParseMarkdownString(string markdownFileContents)
        {
            var document = Markdig.Markdown.Parse(markdownFileContents);

            var interalMarkdownTypedFile = markdownParser.Parse(document);

            return interalMarkdownTypedFile;
        }

    }
}
