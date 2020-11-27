using System;
using System.Text;
using System.Collections.Generic;

using StaticSiteGenerator.TemplateSubstitution.BlockConverters;
using StaticSiteGenerator.Markdown.BlockElement;

using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace StaticSiteGenerator.TemplateSubstitution
{
    [TransientService]
    public class MarkdownBlockConverter: IConverter<IList<IBlockElement>>, IConverter<IBlockElement>
    {
        HeaderConverter HeaderConverter;
        ParagraphConverter ParagraphConveter;

        public MarkdownBlockConverter(HeaderConverter headerConverter, ParagraphConverter paragraphConverter)
        {
            HeaderConverter = headerConverter;
            ParagraphConveter = paragraphConverter;
        }

        public string Convert(IList<IBlockElement> blocks)
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

        public string Convert(IBlockElement block)
        {
            switch (block) {
                case Header b:
                    return HeaderConverter.Convert(b);
                case Paragraph b:
                    return ParagraphConveter.Convert(b);
                default:
                    throw new ArgumentException(
                        message: $"Could not convert block {block.GetType()}",
                        paramName: nameof(block));
            }

        }

    }
}
