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
            HtmlFileWriter.Write(filePath, htmlString);
        }

        public void Write(IHtmlFile file)
        {
            Write($"{file.Name}.{file.FileExtension}", file.HtmlContent);
        }
    }
}
