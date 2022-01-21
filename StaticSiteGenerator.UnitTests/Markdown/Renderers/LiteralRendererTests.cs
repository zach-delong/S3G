using System;
using System.Collections.Generic;
using System.IO;
using Markdig.Helpers;
using Markdig.Renderers;
using Markdig.Syntax.Inlines;
using Moq;
using StaticSiteGenerator.Markdown.Renderers;
using Xunit;

namespace StaticSiteGenerator.UnitTests.Markdown.Renderers;

public class LiteralRendererTests
{
    [Theory]
    [MemberData(nameof(TestData))]
    public void Test(LiteralInline input)
    {
        var writer = new StringWriter();
        var sut = new LiteralRenderer();

        var rendererMock = new Mock<HtmlRenderer>(writer);

        sut.Write(rendererMock.Object, input);

        if(input != null)
            Assert.Equal(input?.Content.ToString(), writer.ToString());
        else
            Assert.True(true);
    }

    public static IEnumerable<object[]> TestData
    {
        get
        {
            yield return new object[] { null };
            yield return new object[] { new LiteralInline { Content = StringSlice.Empty } };
            yield return new object[] { new LiteralInline { Content = new StringSlice("ASDF") } };
        }
    }
}
