using System.Collections.Generic;
using Markdig;
using Markdig.Renderers;
using StaticSiteGenerator.Markdown.Renderers;
using StaticSiteGenerator.Utilities.Extensions;

namespace StaticSiteGenerator.Markdown;

public class CustomExtension : IMarkdownExtension
{
    private readonly IEnumerable<ICustomRenderer> customRenderers;

    public CustomExtension(IEnumerable<ICustomRenderer> customRenderers)
    {
        this.customRenderers = customRenderers;
    }
    public void Setup(MarkdownPipelineBuilder pipeline)
    {
        return;
    }

    public void Setup(MarkdownPipeline pipeline, IMarkdownRenderer renderer)
    {
        var htmlRenderer = (renderer as HtmlRenderer);

        foreach(var r in customRenderers)
        {
            htmlRenderer.ObjectRenderers.Replace(r.Replaces, r);
        }
    }
}
