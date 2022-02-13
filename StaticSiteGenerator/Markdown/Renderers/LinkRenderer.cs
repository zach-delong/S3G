using Markdig.Renderers;
using Markdig.Syntax.Inlines;
using StaticSiteGenerator.TemplateSubstitution.TagCollection;
using StaticSiteGenerator.TemplateSubstitution.TemplateTags;

namespace StaticSiteGenerator.Markdown.Renderers;

public class LinkRenderer : CustomRendererBase<LinkInline>
{
    private readonly ITemplateTagCollection tagCollection;

    public LinkRenderer(ITemplateTagCollection tagCollection)
    {
        this.tagCollection = tagCollection;
    }

    protected override void Write(HtmlRenderer renderer, LinkInline obj)
    {
        if(obj.IsImage) return;

        var tag = tagCollection.GetTagForType(TagType.Link);

        var tagElements = tag.Template.Split("{{url}}");

        renderer.Write(tagElements[0]);

        var url = obj.GetDynamicUrl != null
            ? obj.GetDynamicUrl() ?? obj.Url
            : obj.Url;

        renderer.WriteEscapeUrl(url);

        var remainingTag = tagElements[1].Split("{{display_text}}");

        renderer.Write(remainingTag[0]);

        renderer.WriteChildren(obj);

        renderer.Write(remainingTag[1]);
    }
}
