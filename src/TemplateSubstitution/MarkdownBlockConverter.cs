using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

using StaticSiteGenerator.TemplateSubstitution.BlockConverters;
using StaticSiteGenerator.Markdown.BlockElement;

using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace StaticSiteGenerator.TemplateSubstitution
{
    [TransientService]
    public class MarkdownBlockConverter: IHtmlConverter<IList<IBlockElement>>, IHtmlConverter<IBlockElement>
    {
        private readonly IEnumerable<IHtmlConverter<IBlockElement>> BlockConverters;

        public MarkdownBlockConverter(IEnumerable<IHtmlConverter<IBlockElement>> blockConverters)
        {
            BlockConverters = blockConverters;
        }

        public string Convert(IList<IBlockElement> blocks)
        {
            var result = new StringBuilder();
            foreach(var block in blocks)
            {
                try
                {
                    result.AppendLine(Convert(block));
                }
                catch(ArgumentException ex)
                {
                    Console.WriteLine(ex.ToString());
                    Console.WriteLine(ex.Message);
                }
            }

            return result.ToString();
        }

        public string Convert(IBlockElement block)
        {
            var blockConverter = GetConverterFor(block.GetType());

            return blockConverter.Convert(block);
        }

        private IHtmlConverter<IBlockElement> GetConverterFor(Type t)
        {
            foreach(var converter in BlockConverters)
            {
                if(ConverterMatchesAttributeType(converter, t))
                {
                    return converter;
                }
            }

            throw new Exception($"Could not find an HTML Writer for {t.Name}");
        }

        private bool ConverterMatchesAttributeType(IHtmlConverter<IBlockElement> converter, Type t)
        {
          var converterType = converter.GetType();

          var attr = (HtmlConverterForAttribute) Attribute.GetCustomAttribute(converterType, typeof(HtmlConverterForAttribute));

          return attr?.TypeName == t.Name;
        }
    }
}
