using System.Collections.Generic;
using StaticSiteGenerator.FileManipulation;
using StaticSiteGenerator.Markdown.BlockElement;
using StaticSiteGenerator.Markdown.Parser.BlockParser;
using Microsoft.Toolkit.Parsers.Markdown;
using System.IO;

namespace StaticSiteGenerator.Markdown.Parser
{
    public class MarkdownFileParser : IMarkdownFileParser
    {
        FileReader FileParser;
        IMarkdownBlockParser MarkdownParser;

        public MarkdownFileParser(
            FileReader fileParser,
            IMarkdownBlockParser markdownParser
        )
        {
            FileParser = fileParser;
            MarkdownParser = markdownParser;
        }

        public IList<IBlockElement> ReadFile(string filePath)
        {
            var fileContents = FileParser.ReadFile(filePath);

            var parsedContents = ParseMarkdownString(fileContents);

            return parsedContents;
        }

        public IEnumerable<IMarkdownFile> ReadFiles(IEnumerable<string> filePaths)
        {
            // Console.WriteLine("Beginning converting files");
            foreach (var filePath in filePaths)
            {
                // Console.WriteLine($"(0) Starting converting file {filePath}");
                var file = new MarkdownFile
                {
                    Elements = ReadFile(filePath),
                    Name = Path.GetFileNameWithoutExtension(filePath)
                };

                yield return file;

                // Console.WriteLine($"(0) Done converting file {filePath}");
            }
        }

        private IList<IBlockElement> ParseMarkdownString(string markdownFileContents)
        {
            var parsedMarkdownDocument = new MarkdownDocument();

            parsedMarkdownDocument.Parse(markdownFileContents);

            var interalMarkdownTypedFile = MarkdownParser.Parse(parsedMarkdownDocument.Blocks);

            return interalMarkdownTypedFile;
        }

    }
}
