using System;
using System.Collections.Generic;
using StaticSiteGenerator.FileManipulation.FileWriting;
using StaticSiteGenerator.MarkdownHtmlConversion;

namespace StaticSiteGenerator.HtmlWriting
{
    public class FileSystemHtmlWriter: IHtmlFileWriter
    {
        public FileSystemHtmlWriter(IFileWriter writer)
        {
            Writer = writer;
        }

        private const string HtmlFileExtension = ".html";
        private readonly IFileWriter Writer;

        public void Write(string filePath, string htmlString)
        {
            Writer.WriteFile(filePath, htmlString);
        }

        public void Write(IEnumerable<IHtmlFile> files)
        {
            foreach (var file in files)
            {
                Write($"{ file.Name }.{ file.FileExtension }", file.HtmlContent);
            }
        }
    }
}
