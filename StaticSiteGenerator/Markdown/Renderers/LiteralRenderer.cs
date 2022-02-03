using Markdig.Renderers;
using Markdig.Syntax.Inlines;

namespace StaticSiteGenerator.Markdown.Renderers;

public class LiteralRenderer : CustomRendererBase<LiteralInline>
{
    protected override void Write(HtmlRenderer renderer, LiteralInline obj)
    {
        if(obj == null){
            return;
        }

        renderer.Write(obj.Content);
    }
}
