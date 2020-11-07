using System;
using System.Text;
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

        public string Convert(IList<MarkdownBlock> blocks)
        {
            var result = new StringBuilder();
            foreach(var block in blocks)
            {
                try
                {
                    result.AppendLine(Convert(block));
                }
                catch(ArgumentException ex)
                {
                    Console.WriteLine(ex.ToString());
                    Console.WriteLine(ex.Message);
                }
            }

            return result.ToString();
        }

        public string Convert(MarkdownBlock block)
        {
            switch (block) {
                case HeaderBlock b:
                    return HeaderConverter.Convert(b);
                case ParagraphBlock b:
                    return ParagraphConveter.Convert(b);
                default:
                    throw new ArgumentException(
                        message: $"Could not convert block {block.GetType()}",
                        paramName: nameof(block));
            }

        }

    }
}
