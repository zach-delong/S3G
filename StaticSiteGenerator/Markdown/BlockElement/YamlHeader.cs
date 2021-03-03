using System.Collections.Generic;

namespace StaticSiteGenerator.Markdown.BlockElement
{
    public class YamlHeader: IBlockElement
    {
        public YamlHeader() { }

        public IDictionary<YamlAttributeType, string> Attributes { get; set; }

        public string Content => throw new System.NotImplementedException();
    }
}
