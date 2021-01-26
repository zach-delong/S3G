using System;

namespace StaticSiteGenerator.HtmlWriting
{
    public class MarkdownHtmlFileWriter : IHtmlFileWriter
    {
        private readonly IHtmlFileWriter HtmlFileWriter;
        public MarkdownHtmlFileWriter(IFileSystemHtmlWriter fileSystemHtmlWriter)
        {
            HtmlFileWriter = fileSystemHtmlWriter;
        }
        public void Write(string filePath, string htmlString)
        {
            var pathWithoutMarkdown = filePath.Replace(".md", "", StringComparison.CurrentCultureIgnoreCase);

            HtmlFileWriter.Write(pathWithoutMarkdown, htmlString);
        }
    }
}
