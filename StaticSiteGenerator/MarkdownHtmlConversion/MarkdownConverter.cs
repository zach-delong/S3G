using System.Collections.Generic;

using StaticSiteGenerator.Markdown.BlockElement;
using StaticSiteGenerator.MarkdownHtmlConversion.MarkdownHtmlConverters;

using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace StaticSiteGenerator.MarkdownHtmlConversion
{
    [TransientService]
    public class MarkdownConverter : IHtmlConverter<IList<IBlockElement>>
    {

        IMarkdownBlockConverter BlockConverter;

        public MarkdownConverter(IMarkdownBlockConverter blockConverter)
        {
            BlockConverter = blockConverter;
        }

        public string Convert(IList<IBlockElement> markdownFile)
        {
            return BlockConverter.Convert(markdownFile);
        }

    }
}
