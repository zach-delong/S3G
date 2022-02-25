using System.ComponentModel;

namespace StaticSiteGenerator.TemplateSubstitution.TemplateTags;

public enum TagType
{
    [Description("h1")]
    Header1,
    [Description("h2")]
    Header2,
    [Description("h3")]
    Header3,
    [Description("h4")]
    Header4,
    [Description("h5")]
    Header5,
    [Description("h6")]
    Header6,
    [Description("p")]
    Paragraph,
    [Description("link")]
    Link,
    [Description("image")]
    Image,
    [Description("ordered_list")]
    OrderedList,
    [Description("unordered_list")]
    UnorderedList,
    [Description("list_item")]
    ListItem,
    [Description("i")]
    Italic,
    [Description("b")]
    Bold
}
