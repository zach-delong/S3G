using System;

namespace StaticSiteGenerator.Markdown.BlockElementConverter
{
    public class MarkdownConverterForAttribute: System.Attribute
    {
        public string TypeName { get; set; }

        public MarkdownConverterForAttribute(string typeName)
        {
            TypeName = typeName;
        }
    }
}
