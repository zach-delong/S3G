using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using Markdig;
using Markdig.Renderers;
using StaticSiteGenerator.Markdown;
using StaticSiteGenerator.Markdown.Renderers;
using Xunit;

namespace StaticSiteGenerator.Tests.UnitTests.Markdown;

public class CustomExtensionTests
{
    [Fact]
    public void EnsureRenderersGetInitializedWhenPluginIsEnabled()
    {
        var renderer = new HtmlRenderer(new StringWriter());

        IMarkdownExtension extension = new CustomExtension(new List<ICustomRenderer> {new LiteralRenderer()});

        extension.Setup(null, renderer);

        // Assert that the extension exists in the pipeline
        renderer.ObjectRenderers
	    .Find<LiteralRenderer>()
	    .Should()
	    .NotBeNull();
    }
}
