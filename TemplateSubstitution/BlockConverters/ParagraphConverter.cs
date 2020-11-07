using System;

using TanvirArjel.Extensions.Microsoft.DependencyInjection;

using Microsoft.Toolkit.Parsers.Markdown.Blocks;

namespace StaticSiteGenerator.TemplateSubstitution.BlockConverters
{
    [TransientService]
    public class ParagraphConverter: IConverter<ParagraphBlock>
    {
        InlineConverter InlineConverter;

        public ParagraphConverter(InlineConverter inlineConverter)
        {
            InlineConverter = inlineConverter;
        }
        public void Convert(ParagraphBlock block)
        {
            InlineConverter.Convert(block.Inlines);
            Console.WriteLine($"<p>{block.ToString()}</p>");
        }
    }
}
