using StaticSiteGenerator.Utilities.StrategyPattern;

namespace StaticSiteGenerator.Markdown
{
    public class MarkdownConverterForAttribute : StrategyForTypeAttribute
    {
        public MarkdownConverterForAttribute(string typeName) : base(typeName) { }
    }
}
