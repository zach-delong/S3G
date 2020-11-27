using System;

using StaticSiteGenerator.Markdown.InlineElement;

using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace StaticSiteGenerator.TemplateSubstitution.InlineConverters
{
    [TransientService]
    public class TextConverter: IConverter<Text>
    {
        public string Convert(Text inline)
        {
            return inline.Content;
        }
    }
}
