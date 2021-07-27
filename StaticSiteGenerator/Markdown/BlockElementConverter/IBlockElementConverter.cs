using Markdig.Syntax;
using StaticSiteGenerator.Markdown.BlockElement;

namespace StaticSiteGenerator.Markdown.BlockElementConverter
{
    public interface IBlockElementConverter
    {
        IBlockElement Convert(IBlock block);
    }
}
