using System;

using TanvirArjel.Extensions.Microsoft.DependencyInjection;

using Microsoft.Toolkit.Parsers.Markdown.Blocks;

namespace StaticSiteGenerator.TemplateSubstitution.BlockConverters
{
    [TransientService]
    public class HeaderConverter : IConverter<HeaderBlock>
    {
        public void Convert(HeaderBlock block)
        {
            Console.WriteLine($"<h{block.HeaderLevel}>{block.ToString()}</h{block.HeaderLevel}>");
        }
    }
}
