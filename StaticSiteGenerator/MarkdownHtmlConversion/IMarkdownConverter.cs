using System.Collections.Generic;

using StaticSiteGenerator.Markdown.BlockElement;

namespace StaticSiteGenerator.MarkdownHtmlConversion
{
    public interface IMarkdownConverter: IHtmlConverter<IList<IBlockElement>>
    {
    }
}
