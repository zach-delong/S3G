using System.ComponentModel;

namespace StaticSiteGenerator.TemplateSubstitution.TemplateTags;

public enum TagType
{
    [Description("h1")]
    Header1,
    [Description("p")]
    Paragraph,
    [Description("link")]
    Link
}
