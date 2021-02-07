using System.Collections.Generic;
using StaticSiteGenerator.MarkdownHtmlConversion;

namespace StaticSiteGenerator.HtmlWriting
{
    public interface IHtmlFileWriter
    {
        void Write(string filePath, string htmlString);
        void Write(IEnumerable<IHtmlFile> files);
    }
}
