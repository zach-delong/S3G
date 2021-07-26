using System.Collections.Generic;
using StaticSiteGenerator.FileManipulation;
using StaticSiteGenerator.Markdown.BlockElement;
using StaticSiteGenerator.Markdown.Parser.BlockParser;
using Microsoft.Toolkit.Parsers.Markdown;
using System.IO;
using StaticSiteGenerator.Markdown.YamlMetadata;
using System.Linq;
using System;
using Microsoft.Extensions.Logging;

namespace StaticSiteGenerator.Markdown.Parser
{
    public class MarkdownFileParser : IMarkdownFileParser
    {
        private readonly FileReader fileParser;
        private readonly IMarkdownBlockParser markdownParser;
        private readonly IYamlMetadataProcessor yamlMetadataProcessor;
        private readonly ILogger<MarkdownFileParser> logger;

        public MarkdownFileParser(
            FileReader fileParser,
            IMarkdownBlockParser markdownParser,
            IYamlMetadataProcessor yamlMetadataProcessor,
            ILogger<MarkdownFileParser> logger
        )
        {
            this.fileParser = fileParser;
            this.markdownParser = markdownParser;
            this.yamlMetadataProcessor = yamlMetadataProcessor;
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

                file = yamlMetadataProcessor.ParseYamlMetadata(file);


                // TODO: I would like to put the yaml metadata in the HTML
                // files, but I need to do that in a separate step. For now,
                // lets just discard the information so the HTML converters
                // don't fail
                if (file?.Elements != null)
                {
                    file.Elements = file.Elements.Where(e => e.GetType().Name != nameof(YamlHeader))
                                                 .ToList();
                }

                yield return file;

                logger.LogTrace($"Converted file: {filePath}");
            }
        }

        private IList<IBlockElement> ParseMarkdownString(string markdownFileContents)
        {
            var parsedMarkdownDocument = new MarkdownDocument();

            parsedMarkdownDocument.Parse(markdownFileContents);

            var interalMarkdownTypedFile = markdownParser.Parse(parsedMarkdownDocument.Blocks);

            return interalMarkdownTypedFile;
        }

    }
}
