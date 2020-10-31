using Microsoft.Toolkit.Parsers.Markdown;

namespace StaticSiteGenerator.Markdown
{
    public class MarkdownParser
    {
        public MarkdownDocument ParseMarkdownString(string markdownFileContents)
        {
            var parsedMarkdownDocument= new MarkdownDocument();
            parsedMarkdownDocument.Parse(markdownFileContents);

            return parsedMarkdownDocument;
        }
    }
}
