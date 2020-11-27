using System;

using StaticSiteGenerator.Markdown.BlockElement;
using StaticSiteGenerator.TemplateSubstitution.TemplateTags;

using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace StaticSiteGenerator.TemplateSubstitution.BlockConverters
{
    [TransientService]
    public class ParagraphConverter: IConverter<Paragraph>
    {
        private InlineConverter InlineConverter;
        private TemplateReader TemplateReader;

        public ParagraphConverter(InlineConverter inlineConverter, TemplateReader reader)
        {
            InlineConverter = inlineConverter;
            TemplateReader = reader;
        }
        public string Convert(Paragraph block)
        {
            var inlineText = InlineConverter.Convert(block.Inlines);

            var template = TemplateReader.GetTemplateTagForType(TagType.Paragraph); 

            return template.ToHtml(inlineText);
        }
    }
}
