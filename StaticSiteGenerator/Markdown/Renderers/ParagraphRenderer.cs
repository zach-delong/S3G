using Markdig.Renderers;
using Markdig.Syntax;
using StaticSiteGenerator.TemplateSubstitution.TagCollection;
using StaticSiteGenerator.TemplateSubstitution.TemplateTags;

namespace StaticSiteGenerator.Markdown.Renderers;

public class ParagraphRenderer : CustomRendererBase<ParagraphBlock>
{
    private readonly ITemplateTagCollection tagCollection;

    public ParagraphRenderer(ITemplateTagCollection tagCollection)
    {
        this.tagCollection = tagCollection;
    }

    protected override void Write(HtmlRenderer renderer, ParagraphBlock obj)
    {
        var tag = tagCollection.GetTagForType(TagType.Paragraph);

        var foo = tag.Template.Split("{{}}");

        renderer.Write(foo[0]);

        renderer.WriteLeafInline(obj);

        renderer.Write(foo[1]);
    }
}
