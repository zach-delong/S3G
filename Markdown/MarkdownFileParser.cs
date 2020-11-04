using System;
using System.IO;
using System.Text;
using StaticSiteGenerator.FileManipulation;

namespace StaticSiteGenerator.Markdown
{
    public class MarkdownFileParser
    {
        FileReader FileParser;
        MarkdownParser MarkdownParser;

        public MarkdownFileParser(
            FileReader fileParser,
            MarkdownParser markdownParser
        ){
            FileParser = fileParser;
            MarkdownParser = markdownParser;
        }
        public string ReadFile(string filePath)
        {
            var fileContents = FileParser.ReadFile(filePath);

            var parsedContents = MarkdownParser.ParseMarkdownString(fileContents);

            return fileContents.ToString();
        }
    }
}
