using System;
using StaticSiteGenerator.FileManipulation.FileWriting;

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
            string htmlFilePath = EnsureHtmlFileExtension(filePath);

            Writer.WriteFile(htmlFilePath, htmlString);
        }

        private static string EnsureHtmlFileExtension(string filePath)
        {
            var namedHtmlFilePath = filePath;

            if (!namedHtmlFilePath.EndsWith(HtmlFileExtension))
            {
                namedHtmlFilePath += HtmlFileExtension;
            }

            return namedHtmlFilePath;
        }
    }
}
