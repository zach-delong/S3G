using System;

using Microsoft.Toolkit.Parsers.Markdown.Inlines;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace StaticSiteGenerator.TemplateSubstitution.InlineConverters
{
    [TransientService]
    public class TextConverter: IConverter<TextRunInline>
    {
        public void Convert(TextRunInline inline)
        {
            Console.WriteLine(inline.Text);
        }
    }
}
