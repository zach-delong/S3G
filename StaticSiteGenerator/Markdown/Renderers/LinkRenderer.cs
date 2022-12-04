using System;
using System.IO.Abstractions;
using Markdig.Renderers;
using Markdig.Syntax.Inlines;
using StaticSiteGenerator.TemplateSubstitution.TagCollection;
using StaticSiteGenerator.TemplateSubstitution.TemplateTags;
using StaticSiteGenerator.Utilities;

namespace StaticSiteGenerator.Markdown.Renderers;

public class LinkRenderer : CustomRendererBase<LinkInline>
{
    private readonly ITemplateTagCollection TagCollection;
    private readonly BeforeLinkWrite OnLinkWrite;
    private readonly ILinkProcessor LinkProcessor;

    public delegate void BeforeLinkWrite(string url);

    public LinkRenderer(
	ITemplateTagCollection tagCollection,
	BeforeLinkWrite beforeLinkWrite,
	ILinkProcessor linkProcessor
    )
    {
        TagCollection = tagCollection;
        OnLinkWrite = beforeLinkWrite;
        LinkProcessor = linkProcessor;
    }

    protected override void Write(HtmlRenderer renderer, LinkInline obj)
    {
        if (obj.IsImage)
            WriteImage(renderer, obj);
        else
            WriteNormalLink(renderer, obj);
    }

    private void WriteImage(HtmlRenderer renderer, LinkInline obj)
    {
        var tag = TagCollection.GetTagForType(TagType.Image);

        var tagElements = tag.Template.Split("{{url}}");

        for (var i = 0; i < tagElements.Length; i++)
        {
            renderer.Write(tagElements[i]);

            // We normally want to place a URL between the halves,
            // UNLESS this is the last element that we need to write
            if(i + 1 < tagElements.Length)
                renderer.Write(obj.Url);
        }
    }

    private void WriteNormalLink(HtmlRenderer renderer, LinkInline obj)
    {
        var tag = TagCollection.GetTagForType(TagType.Link);

        var tagElements = tag.Template.Split("{{url}}");

        renderer.Write(tagElements[0]);

        var url = obj.GetDynamicUrl != null
            ? obj.GetDynamicUrl() ?? obj.Url
            : obj.Url;

        url = LinkProcessor.Process(url);

        if(OnLinkWrite != null) 
	    OnLinkWrite(url);

        renderer.WriteEscapeUrl(url);

        var remainingTag = tagElements[1].Split("{{display_text}}");

        renderer.Write(remainingTag[0]);

        renderer.WriteChildren(obj);

        renderer.Write(remainingTag[1]);
    }
}
