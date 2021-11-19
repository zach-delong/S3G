using Markdig.Syntax;
using StaticSiteGenerator.Markdown.BlockElement;
using StaticSiteGenerator.Utilities.StrategyPattern;

namespace StaticSiteGenerator.Markdown.BlockElementConverter
{
    public interface IBlockElementConverter: IStrategy<IBlockElement, IBlock>
    {
    }
}
