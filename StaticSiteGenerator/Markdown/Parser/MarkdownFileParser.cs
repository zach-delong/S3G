using System.Collections.Generic;
using StaticSiteGenerator.FileManipulation;
using StaticSiteGenerator.Markdown.BlockElement;
using StaticSiteGenerator.Markdown.Parser.BlockParser;
using Microsoft.Toolkit.Parsers.Markdown;
using System.IO;
using StaticSiteGenerator.Markdown.YamlMetadata;
using System.Linq;
using System;

namespace StaticSiteGenerator.Markdown.Parser
{
    public class MarkdownFileParser : IMarkdownFileParser
    {
        private readonly FileReader fileParser;
        private readonly IMarkdownBlockParser markdownParser;
        private readonly IYamlMetadataProcessor yamlMetadataProcessor;

        public MarkdownFileParser(
            FileReader fileParser,
            IMarkdownBlockParser markdownParser,
            IYamlMetadataProcessor yamlMetadataProcessor
        )
        {
            this.fileParser = fileParser;
            this.markdownParser = markdownParser;
            this.yamlMetadataProcessor = yamlMetadataProcessor;
        }

        public IList<IBlockElement> ReadFile(string filePath)
        {
            var fileContents = fileParser.ReadFile(filePath);

            var parsedContents = ParseMarkdownString(fileContents);

            return parsedContents;
        }

        public IEnumerable<IMarkdownFile> ReadFiles(IEnumerable<string> filePaths)
        {
            // Console.WriteLine("Beginning converting files");
            foreach (var filePath in filePaths)
            {
                Console.WriteLine($"(0) Starting converting file {filePath}");
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

                // Console.WriteLine($"(0) Done converting file {filePath}");
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
