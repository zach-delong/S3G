using Microsoft.Toolkit.Parsers.Markdown.Blocks;
using StaticSiteGenerator.Markdown.BlockElement;
using System.Collections.Generic;

namespace StaticSiteGenerator.Markdown.BlockElementConverter
{
    [MarkdownConverterFor(nameof(YamlHeaderBlock))]
    public class YamlConverter: IBlockElementConverter
    {
        public YamlConverter() {  }

        public IBlockElement Convert(MarkdownBlock block)
        {
            return Convert((YamlHeaderBlock)block);
        }

        private IBlockElement Convert(YamlHeaderBlock headerBlock)
        {
            var result = new YamlHeader();

            result.Attributes = Convert(headerBlock.Children);

            return result;
        }

        private IDictionary<YamlAttributeType, string> Convert(Dictionary<string, string> children)
        {
            var d = new Dictionary<YamlAttributeType, string>();

            foreach(var attribute in children)
            {
                var valueType = YamlAttributeType.GetFromStringOrDefault(attribute.Key);

                if(valueType == null)
                {
                    continue;
                }

                d.Add(valueType, attribute.Value);
            }

            return d;
        }
    }
}
