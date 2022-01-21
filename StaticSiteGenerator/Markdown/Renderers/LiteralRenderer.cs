using System;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Renderers.Html.Inlines;
using Markdig.Syntax.Inlines;

namespace StaticSiteGenerator.Markdown.Renderers;

public class LiteralRenderer : HtmlObjectRenderer<LiteralInline>, ICustomRenderer
{
    public Type Replaces => typeof(LiteralInlineRenderer);

    protected override void Write(HtmlRenderer renderer, LiteralInline obj)
    {
        if(obj == null){
            return;
        }

        renderer.Write(obj.Content);
    }
}
