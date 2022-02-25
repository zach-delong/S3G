using System;
using Markdig.Renderers;
using Markdig.Syntax.Inlines;
using StaticSiteGenerator.TemplateSubstitution.TagCollection;
using StaticSiteGenerator.TemplateSubstitution.TemplateTags;

namespace StaticSiteGenerator.Markdown.Renderers;

// A renderer for emphasis elements (italics or bold)
// TODO: Can I separate this out into two renderers?
public class EmphasisRenderer : CustomRendererBase<EmphasisInline>
{
    private readonly ITemplateTagCollection tagCollection;

    public EmphasisRenderer(ITemplateTagCollection tagCollection)
    {
        this.tagCollection = tagCollection;
    }

    protected override void Write(HtmlRenderer renderer, EmphasisInline obj)
    {
        if(obj.DelimiterCount == 2)
        {
            WriteBold(renderer, obj);
        }
        else{
            WriteItalic(renderer, obj);
        }
    }

    private void WriteBold(HtmlRenderer renderer, EmphasisInline obj)
    {
        var tag = tagCollection.GetTagForType(TagType.Bold);

        Write(renderer, obj, tag);
    }

    private void WriteItalic(HtmlRenderer renderer, EmphasisInline obj)
    {
        var tag = tagCollection.GetTagForType(TagType.Italic);

        Write(renderer, obj, tag);
    }

    private static void Write(HtmlRenderer renderer, EmphasisInline obj, TemplateTag tag)
    {
        var foo = tag.Template.Split("{{}}");

        renderer.Write(foo[0]);

        renderer.WriteChildren(obj);

        renderer.Write(foo[1]);
    }

}
