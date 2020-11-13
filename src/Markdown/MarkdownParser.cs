using System.IO;
using Microsoft.Toolkit.Parsers.Markdown;

using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace StaticSiteGenerator.Markdown
{

    [TransientService]
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
