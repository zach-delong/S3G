using System;
using StaticSiteGenerator.Utilities.StrategyPattern;

namespace StaticSiteGenerator.TemplateSubstitution
{
    public class HtmlConverterForAttribute: StrategyForTypeAttribute
    {
        public HtmlConverterForAttribute(string typeName): base(typeName)
        {
        }
    }
}
