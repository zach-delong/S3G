using System.Collections.Generic;
using StaticSiteGenerator.Markdown.InlineElement;

namespace StaticSiteGenerator.MarkdownHtmlConversion.TagModelConverters;

public class LinkInlineModelConverter
{
    public IDictionary<string, string> Convert(IInlineElement input)
    {
        return Convert((LinkElement)input);
    }

    public IDictionary<string, string> Convert(LinkElement input)
    {
        var dict = new Dictionary<string, string>();

        dict["url"] = input.Link;
        dict["display_text"] = input.Text;

        return dict;
    }
}
