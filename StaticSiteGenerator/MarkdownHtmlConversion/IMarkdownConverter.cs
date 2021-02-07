using System.Collections.Generic;
using StaticSiteGenerator.Markdown;
using StaticSiteGenerator.Markdown.BlockElement;

namespace StaticSiteGenerator.MarkdownHtmlConversion
{
    public interface IMarkdownConverter
    {
        string Convert(IList<IBlockElement> markdownFile);
        IEnumerable<IHtmlFile> Convert(IEnumerable<IMarkdownFile> markdownFiles);
    }
}
