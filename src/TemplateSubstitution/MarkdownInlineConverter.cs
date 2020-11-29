using System;
using System.Text;
using System.Collections.Generic;

using TanvirArjel.Extensions.Microsoft.DependencyInjection;
using StaticSiteGenerator.Markdown.InlineElement;

using StaticSiteGenerator.TemplateSubstitution.InlineConverters;

namespace StaticSiteGenerator.TemplateSubstitution
{
    [TransientService]
    public class MarkdownInlineConverter: IHtmlConverter<IInlineElement>, IHtmlConverter<IList<IInlineElement>>
    {
        IEnumerable<IHtmlConverter<IInlineElement>> InlineElementConverters;

        public MarkdownInlineConverter(IEnumerable<IHtmlConverter<IInlineElement>> inlineElementConverters)
        {
            InlineElementConverters = inlineElementConverters;
        }

        public string Convert(IInlineElement inline)
        {
            var inlineConverter = GetConverterFor(inline.GetType());

            return inlineConverter.Convert(inline);
        }

        private IHtmlConverter<IInlineElement> GetConverterFor(Type t)
        {
            foreach(var converter in InlineElementConverters)
            {
                if(ConverterMatchesAttributeType(converter, t))
                {
                    return converter;
                }
            }

            throw new Exception($"Could not find an HTML Writer for {t.Name}");
        }

        private bool ConverterMatchesAttributeType(IHtmlConverter<IInlineElement> converter, Type t)
        {
            var converterType = converter.GetType();

            var attr = (HtmlConverterForAttribute) Attribute.GetCustomAttribute(converterType, typeof(HtmlConverterForAttribute));

            return attr?.TypeName == t.Name;
        }

        public string Convert(IList<IInlineElement> inlines)
        {
            var result = new StringBuilder();
            foreach(var inline in inlines)
            {
                try
                {
                    result.Append(Convert(inline));
                }
                catch(ArgumentException ex)
                {
                    Console.WriteLine(ex.ToString());
                    Console.WriteLine(ex.Message);
                }
            }

            return result.ToString();
        }
    }
}
