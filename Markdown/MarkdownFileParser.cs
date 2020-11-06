using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using StaticSiteGenerator.FileManipulation;
using StaticSiteGenerator.TemplateSubstitution;
using Microsoft.Toolkit.Parsers.Markdown;

using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace StaticSiteGenerator.Markdown
{

    [TransientService]
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
        public MarkdownDocument ReadFile(string filePath)
        {
            var fileContents = FileParser.ReadFile(filePath);

            var parsedContents = MarkdownParser.ParseMarkdownString(fileContents);

            return parsedContents;
        }
    }
}
