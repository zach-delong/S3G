using System;

namespace StaticSiteGenerator.TemplateSubstitution
{
    public class HtmlConverterForAttribute: Attribute
    {
        public string TypeName { get; set; }

        public HtmlConverterForAttribute(string typeName)
        {
            TypeName = typeName;
        }
    }
}
