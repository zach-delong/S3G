using System;

using TanvirArjel.Extensions.Microsoft.DependencyInjection;

using Microsoft.Toolkit.Parsers.Markdown.Blocks;

using StaticSiteGenerator.TemplateSubstitution.TemplateTags;

namespace StaticSiteGenerator.TemplateSubstitution.BlockConverters
{
    [TransientService]
    public class ParagraphConverter: IConverter<ParagraphBlock>
    {
        private InlineConverter InlineConverter;
        private TemplateReader TemplateReader;

        public ParagraphConverter(InlineConverter inlineConverter, TemplateReader reader)
        {
            InlineConverter = inlineConverter;
            TemplateReader = reader;
        }
        public string Convert(ParagraphBlock block)
        {
            var inlineText = InlineConverter.Convert(block.Inlines);

            var template = TemplateReader.GetTemplateTagForType(TagType.Paragraph); 

            return template.ToHtml(inlineText);
        }
    }
}
