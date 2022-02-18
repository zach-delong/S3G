using System;
using Markdig.Renderers;
using Markdig.Syntax;
using StaticSiteGenerator.TemplateSubstitution.TagCollection;
using StaticSiteGenerator.TemplateSubstitution.TemplateTags;

namespace StaticSiteGenerator.Markdown.Renderers;

public class ListRenderer : CustomRendererBase<ListBlock>
{
    private readonly ITemplateTagCollection tagCollection;

    public ListRenderer(ITemplateTagCollection tagCollection)
    {
        this.tagCollection = tagCollection;
    }

    protected override void Write(HtmlRenderer renderer, ListBlock obj)
    {
        if(obj.IsOrdered)
        {
            WriteOrderedList(renderer, obj);
        }
        else
        {
            WriteUnorderedList(renderer, obj);
        }
    }

    private void WriteOrderedList(HtmlRenderer renderer, ListBlock obj)
    {
        var tag = tagCollection.GetTagForType(TagType.OrderedList);
        Write(renderer, obj, tag);
    }

    private void WriteUnorderedList(HtmlRenderer renderer, ListBlock obj)
    {
        var tag = tagCollection.GetTagForType(TagType.UnorderedList);
        Write(renderer, obj, tag);
    }

    private void Write(HtmlRenderer renderer, ListBlock obj, TemplateTag tag)
    {
        var templateParts = tag.Template.Split("{{}}");

        renderer.Write(templateParts[0]);

        WriteChildren(renderer, obj);

        renderer.Write(templateParts[1]);
    }

    private void WriteChildren(HtmlRenderer renderer, ListBlock obj)
    {
        var tag = tagCollection.GetTagForType(TagType.ListItem);

        var elements = tag.Template.Split("{{}}");

        foreach (var child in obj)
        {
            renderer.Write(elements[0]);

            renderer.WriteChildren((ListItemBlock)child);

            renderer.Write(elements[1]);
        }
    }
}
