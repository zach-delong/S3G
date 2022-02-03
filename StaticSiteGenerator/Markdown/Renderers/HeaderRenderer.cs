using Markdig.Renderers;
using Markdig.Syntax;
using StaticSiteGenerator.TemplateSubstitution.TagCollection;
using StaticSiteGenerator.TemplateSubstitution.TemplateTags;

namespace StaticSiteGenerator.Markdown.Renderers;

public class HeaderRenderer : CustomRendererBase<HeadingBlock>
{
    private readonly ITemplateTagCollection tagCollection;

    public HeaderRenderer(ITemplateTagCollection tagCollection)
    {
        this.tagCollection = tagCollection;
    }

    protected override void Write(HtmlRenderer renderer, HeadingBlock obj)
    {
        // TODO: what about other header sizes?
        var tag = tagCollection.GetTagForType(TagType.Header1);

        var foo = tag.Template.Split("{{}}");

        renderer.Write(foo[0]);

        renderer.WriteLeafInline(obj);

        renderer.Write(foo[1]);
    }
}
