using System;
using System.Collections.Generic;
using Microsoft.Toolkit.Parsers.Markdown.Blocks;

using TanvirArjel.Extensions.Microsoft.DependencyInjection;

using StaticSiteGenerator.TemplateSubstitution.BlockConverters;

namespace StaticSiteGenerator.TemplateSubstitution
{
    [TransientService]
    public class MarkdownBlockConverter: IConverter<IList<MarkdownBlock>>, IConverter<MarkdownBlock>
    {
        HeaderConverter HeaderConverter;
        ParagraphConverter ParagraphConveter;

        public MarkdownBlockConverter(HeaderConverter headerConverter, ParagraphConverter paragraphConverter)
        {
            HeaderConverter = headerConverter;
            ParagraphConveter = paragraphConverter;
        }

        public void Convert(IList<MarkdownBlock> blocks)
        {
            foreach(var block in blocks)
            {
                Convert(block);
            }
        }

        public void Convert(MarkdownBlock block)
        {
            switch (block) {
                case HeaderBlock b:
                    HeaderConverter.Convert(b);
                    break;
                case ParagraphBlock b:
                    ParagraphConveter.Convert(b);
                    break;
            }

        }

    }
}
