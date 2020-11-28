using System;
using System.Collections.Generic;

using StaticSiteGenerator.Markdown.BlockElement;

using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace StaticSiteGenerator.TemplateSubstitution
{
    [TransientService]
    public class MarkdownConverter: IHtmlConverter<IList<IBlockElement>>
    {

        MarkdownBlockConverter BlockConverter;

        public MarkdownConverter(MarkdownBlockConverter blockConverter)
        {
            BlockConverter = blockConverter;
        }

        public string Convert(IList<IBlockElement> markdownFile)
        {
            return BlockConverter.Convert(markdownFile);
        }

    }
}
