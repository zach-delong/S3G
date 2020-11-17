using StaticSiteGenerator.Markdown.MarkdownElement;
using Microsoft.Toolkit.Parsers.Markdown.Blocks;

namespace StaticSiteGenerator.Markdown.MarkdownElementConverter
{
    public interface IMarkdownElementConverter
    {
        IMarkdownElement Convert(MarkdownBlock block);
    }
}
