using System;

using TanvirArjel.Extensions.Microsoft.DependencyInjection;

using Microsoft.Toolkit.Parsers.Markdown.Blocks;

using StaticSiteGenerator.TemplateSubstitution.TemplateTags;

namespace StaticSiteGenerator.TemplateSubstitution.BlockConverters
{
    [TransientService]
    public class HeaderConverter : IConverter<HeaderBlock>
    {

        private TemplateReader TemplateReader;

        public HeaderConverter(TemplateReader reader)
        {
            TemplateReader = reader;
        }
        public string Convert(HeaderBlock block)
        {
            var template = TemplateReader.GetTemplateTagForType(TagType.Header1);
            return template.ToHtml(block.ToString());
        }
    }
}
