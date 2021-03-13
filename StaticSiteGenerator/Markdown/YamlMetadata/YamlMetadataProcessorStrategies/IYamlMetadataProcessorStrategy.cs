using System.Collections.Generic;
using StaticSiteGenerator.Markdown.BlockElement;

namespace StaticSiteGenerator.Markdown.YamlMetadata.YamlMetadataProcessorStrategies
{
    public interface IYamlMetadataProcessorStrategy
    {
        public IMarkdownFile Process(IMarkdownFile file, IList<YamlHeader> headers);
    }
}
