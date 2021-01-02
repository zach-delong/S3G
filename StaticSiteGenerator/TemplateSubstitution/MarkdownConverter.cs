using System;
using System.Collections.Generic;

using StaticSiteGenerator.Markdown.BlockElement;
using StaticSiteGenerator.TemplateSubstitution.MarkdownHtmlConverters;

using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace StaticSiteGenerator.TemplateSubstitution
{
    [TransientService]
    public class MarkdownConverter: IHtmlConverter<IList<IBlockElement>>
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