using System.IO;
using Microsoft.Toolkit.Parsers.Markdown;

namespace StaticSiteGenerator.Markdown
{
    public class MarkdownParser
    {
        public MarkdownDocument ParseMarkdownString(StreamReader markdownFileContents)
        {
            var markdownString = markdownFileContents.ReadToEnd();
            var parsedMarkdownDocument = ParseMarkdownString(markdownString);

            return parsedMarkdownDocument;
        }

        public MarkdownDocument ParseMarkdownString(string markdownFileContents)
        {
            var parsedMarkdownDocument= new MarkdownDocument();

            parsedMarkdownDocument.Parse(markdownFileContents);

            return parsedMarkdownDocument;
        }
    }
}
