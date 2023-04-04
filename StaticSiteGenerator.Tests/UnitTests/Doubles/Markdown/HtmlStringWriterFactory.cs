using System;
using System.IO;
using Markdig.Renderers;

namespace StaticSiteGenerator.UnitTests.Doubles.Markdown;

public class HtmlStringWriterFactory
{
    public (HtmlRenderer, StringWriter) Get()
    {

        StringWriter writer = new StringWriter();
        var htmlRenderer = new HtmlRenderer(writer);

        return (htmlRenderer, writer);
    }
}
