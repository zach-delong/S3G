using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

using StaticSiteGenerator.TemplateSubstitution.BlockConverterStrategies;
using StaticSiteGenerator.Markdown.BlockElement;

using TanvirArjel.Extensions.Microsoft.DependencyInjection;
using System.Linq;

namespace StaticSiteGenerator.TemplateSubstitution.MarkdownHtmlConverters
{
    [TransientService]
    public class MarkdownBlockConverter: IHtmlConverter<IList<IBlockElement>>, IHtmlConverter<IBlockElement>
    {
        private readonly IEnumerable<IBlockHtmlConverterStrategy> BlockConverters;

        public MarkdownBlockConverter(IEnumerable<IBlockHtmlConverterStrategy> blockConverters)
        {
            BlockConverters = blockConverters;
        }

        public string Convert(IList<IBlockElement> blocks)
        {
            return string.Join(Environment.NewLine, blocks.Select(b => Convert(b)));
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
