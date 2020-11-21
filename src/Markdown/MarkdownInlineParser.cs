using System;
using System.Collections.Generic;

using StaticSiteGenerator.Markdown.InlineElement;
using StaticSiteGenerator.Markdown.InlineElementConverter;
using Microsoft.Toolkit.Parsers.Markdown.Inlines;

using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace StaticSiteGenerator.Markdown
{

    [TransientService]
    public class MarkdownInlineParser: IMarkdownInlineParser
    {
        private readonly IList<IInlineElementConverter> Converters;

        public MarkdownInlineParser(IList<IInlineElementConverter> converters)
        {
            Converters = converters;
        }

        public IList<IInlineElement> Parse(IList<MarkdownInline> inlines)
        {
            var result = new List<IInlineElement>();

            foreach(var inline in inlines)
            {
                result.Add(Parse(inline));
            }

            return result;
        }

        public IInlineElement Parse(MarkdownInline inline)
        {
            var converter = GetConvertersForType(inline.GetType());

            return converter.Convert(inline);

        }

        public IInlineElementConverter GetConvertersForType(Type t)
        {
            foreach(var converter in Converters)
            {
                if(ConverterHasMatchingAttributeType(converter, t)){
                    return converter;
                }
            }

            // TODO: should probably thow a custom exception type
            throw new Exception($"Converter for type {t.Name} not found");
        }

        private bool ConverterHasMatchingAttributeType(IInlineElementConverter converter, Type type)
        {
            var converterType = converter.GetType();

            var attribute = (MarkdownConverterForAttribute) Attribute.GetCustomAttribute(converterType, typeof(MarkdownConverterForAttribute));

            return attribute?.TypeName == type.Name;
        }
    }
}
