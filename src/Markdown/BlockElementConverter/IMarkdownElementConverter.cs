using StaticSiteGenerator.Markdown.BlockElement;
using Microsoft.Toolkit.Parsers.Markdown.Blocks;

namespace StaticSiteGenerator.Markdown.MarkdownElementConverter
{
    public interface IBlockElementConverter
    {
        IBlockElement Convert(MarkdownBlock block);
    }
}
