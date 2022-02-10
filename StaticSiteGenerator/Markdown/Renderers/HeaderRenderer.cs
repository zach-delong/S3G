using Markdig.Renderers;
using Markdig.Syntax;
using StaticSiteGenerator.TemplateSubstitution.TagCollection;
using StaticSiteGenerator.TemplateSubstitution.TemplateTags;
using StaticSiteGenerator.Utilities;

namespace StaticSiteGenerator.Markdown.Renderers;

public class HeaderRenderer : CustomRendererBase<HeadingBlock>
{
    private readonly ITemplateTagCollection tagCollection;
    private readonly HeaderLevelHelper headerLevelHelper;

    public HeaderRenderer(ITemplateTagCollection tagCollection, HeaderLevelHelper headerLevelHelper)
    {
        this.tagCollection = tagCollection;
        this.headerLevelHelper = headerLevelHelper;
    }

    protected override void Write(HtmlRenderer renderer, HeadingBlock obj)
    {
        // TODO: what about other header sizes?
        var tag = tagCollection.GetTagForType(headerLevelHelper.GetHeaderTagTypeFor(obj.Level));

        var foo = tag.Template.Split("{{}}");

        renderer.Write(foo[0]);

        renderer.WriteLeafInline(obj);

        renderer.Write(foo[1]);
    }
}
