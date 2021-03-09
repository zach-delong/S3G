using System.Collections.Generic;
using System.Linq;
using StaticSiteGenerator.Markdown.BlockElement;
using StaticSiteGenerator.Markdown.YamlMetadata.YamlMetadataProcessorStrategies;

namespace StaticSiteGenerator.Markdown.YamlMetadata
{
    public class YamlMetadataProcessor: IYamlMetadataProcessor
    {
        private readonly IEnumerable<IYamlMetadataProcessorStrategy> processorStrategies;

        public YamlMetadataProcessor(IEnumerable<IYamlMetadataProcessorStrategy> processorStrategies)
        {
            this.processorStrategies = processorStrategies;
        }

        public IMarkdownFile ParseYamlMetadata(IMarkdownFile file)
        {
            var stuff = file.Elements.Where(e => e.GetType().Name == nameof(YamlHeader))
                                     .Select(e => (YamlHeader)e)
                                     .ToList<YamlHeader>();

            foreach(var processorStrategy in processorStrategies){
                file = processorStrategy.Process(file, stuff);
            }

            return file;
        }
    }
}
