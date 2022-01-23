using System;
using Markdig.Renderers.Html;
using Markdig.Syntax;

namespace StaticSiteGenerator.Markdown.Renderers;

public abstract class CustomRendererBase<T>: HtmlObjectRenderer<T>, ICustomRenderer where T: MarkdownObject
{
    public Type Replaces => typeof(T);
}
