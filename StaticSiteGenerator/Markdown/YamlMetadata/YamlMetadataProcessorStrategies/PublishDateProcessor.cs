using System;
using System.Collections.Generic;
using StaticSiteGenerator.Markdown.BlockElement;
using StaticSiteGenerator.Utilities.Date;

namespace StaticSiteGenerator.Markdown.YamlMetadata.YamlMetadataProcessorStrategies
{
    public class PublishDateProcessor: IYamlMetadataProcessorStrategy
    {
        public PublishDateProcessor(IDateParser dateProcesssor)
        {
            DateProcesssor = dateProcesssor;
        }

        public IDateParser DateProcesssor { get; }

        public IMarkdownFile Process(IMarkdownFile file, IList<YamlHeader> headers)
        {
            foreach(var header in headers){
                if(header.Attributes.ContainsKey(YamlAttributeType.PublishDate))
                {
                    var attribute = header.Attributes[YamlAttributeType.PublishDate];

                    DateTime result;
                    if(DateProcesssor.TryParse(attribute, out result))
                        file.PublishedDate = result;

                    return file;
                }
            }

            return file;
        }
    }
}
 
