using System;
using System.Collections.Generic;
using StaticSiteGenerator.MarkdownHtmlConversion;

namespace StaticSiteGenerator.HtmlWriting
{
    public class MarkdownHtmlFileWriter : IHtmlFileWriter
    {
        private readonly IHtmlFileWriter HtmlFileWriter;

        public MarkdownHtmlFileWriter(IHtmlFileWriter fileSystemHtmlWriter)
        {
            HtmlFileWriter = fileSystemHtmlWriter;
        }

        public void Write(string filePath, string htmlString)
        {
            var pathWithoutMarkdown = filePath.Replace(".md", "", StringComparison.CurrentCultureIgnoreCase);

            HtmlFileWriter.Write(pathWithoutMarkdown, htmlString);
        }

        public void Write(IEnumerable<IHtmlFile> files)
        {
            foreach(var file in files)
            {
                Write(file.Name, file.HtmlContent);
            }
        }
    }
}
