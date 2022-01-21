using System;
using Markdig.Renderers;

namespace StaticSiteGenerator.Markdown.Renderers;

public interface ICustomRenderer: IMarkdownObjectRenderer
{
    Type  Replaces { get; }
}
