using System.Collections.Generic;
using StaticSiteGenerator.FileManipulation;
using StaticSiteGenerator.Markdown.BlockElement;
using StaticSiteGenerator.Markdown.Parser.BlockParser;
using Microsoft.Toolkit.Parsers.Markdown;

using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace StaticSiteGenerator.Markdown
{
    [TransientService]
    public class MarkdownFileParser : IMarkdownFileParser
    {
        FileReader FileParser;
        IMarkdownBlockParser MarkdownParser;

        public MarkdownFileParser(
            FileReader fileParser,
            MarkdownBlockParser markdownParser
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

        private IList<IBlockElement> ParseMarkdownString(string markdownFileContents)
        {
            var parsedMarkdownDocument = new MarkdownDocument();

            parsedMarkdownDocument.Parse(markdownFileContents);

            return MarkdownParser.Parse(parsedMarkdownDocument.Blocks);
        }

    }
}
