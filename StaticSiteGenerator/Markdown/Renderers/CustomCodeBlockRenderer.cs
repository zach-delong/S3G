using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Syntax;
using StaticSiteGenerator.TemplateSubstitution.TagCollection;
using StaticSiteGenerator.TemplateSubstitution.TemplateTags;

namespace StaticSiteGenerator.Markdown.Renderers;

public class CustomCodeBlockRenderer: CustomRendererBase<CodeBlock>
{
    private readonly ITemplateTagCollection tagCollection;

    public CustomCodeBlockRenderer(ITemplateTagCollection tagCollection)
    {
        this.tagCollection = tagCollection;
    }

    protected override void Write(HtmlRenderer renderer, CodeBlock obj)
    {
        var tag = tagCollection.GetTagForType(TagType.CodeBlock);
        var foo = tag.Template.Split("{{}}");

        renderer.Write(foo[0]);

        var attributes = obj.TryGetAttributes();

        foreach (var c in attributes.Classes)
        {
	    System.Console.WriteLine(c);
        }

        renderer.WriteLeafRawLines(obj, true, true, true);

        renderer.Write(foo[1]);
    }
}
