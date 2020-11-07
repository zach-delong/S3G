using System;
using System.Collections.Generic;
using Microsoft.Toolkit.Parsers.Markdown;

using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace StaticSiteGenerator.TemplateSubstitution
{
    [TransientService]
    public class MarkdownConverter: IConverter<MarkdownDocument>
    {

        MarkdownBlockConverter BlockConverter;

        public MarkdownConverter(MarkdownBlockConverter blockConverter)
        {
            BlockConverter = blockConverter;
        }

        public void Convert(MarkdownDocument markdownFile)
        {
            BlockConverter.Convert(markdownFile.Blocks);
        }

    }
}
