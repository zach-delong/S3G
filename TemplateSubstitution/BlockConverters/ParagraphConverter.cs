using System;

using TanvirArjel.Extensions.Microsoft.DependencyInjection;

using Microsoft.Toolkit.Parsers.Markdown.Blocks;

namespace StaticSiteGenerator.TemplateSubstitution.BlockConverters
{
    [TransientService]
    public class ParagraphConverter: IConverter<ParagraphBlock>
    {
        public void Convert(ParagraphBlock block)
        {
            Console.WriteLine($"<p>{block.ToString()}</p>");
        }
    }
}
