using System;

using Microsoft.Toolkit.Parsers.Markdown.Blocks;
using StaticSiteGenerator.Markdown.MarkdownElement;

using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace StaticSiteGenerator.Markdown.MarkdownElementConverter
{
    [TransientService]
    [MarkdownConverterFor(nameof(ParagraphBlock))]
    public class MarkdownParagraphElementConverter: IMarkdownElementConverter
    {
        public IMarkdownElement Convert(MarkdownBlock block)
        {
            ParagraphBlock b = (ParagraphBlock)block;
            return new Paragraph
            {
                // TODO: Paragraph Blocks actually have inlines and we need to process them rather than just ToString-ing
                Text = b.ToString()
            };
        }
    }
}
