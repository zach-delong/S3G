using AutoFixture;
using FluentAssertions;
using StaticSiteGenerator.HtmlWriting;
using StaticSiteGenerator.SiteTemplating.SiteTemplateFilling;
using StaticSiteGenerator.Tests.AutoFixture;
using StaticSiteGenerator.Tests.UnitTests.Doubles.Markdown;
using StaticSiteGenerator.Tests.UnitTests.Doubles.SiteTemplating;
using Xunit;

namespace StaticSiteGenerator.Tests.UnitTests.SiteTemplating;

public class SiteTemplateFillerTests: MockingTestBase
{
    [Fact]
    public void SiteTemplateFillerShouldFillWithCachedTemplate()
    {
        Mocker.SetupTemplateReader("<html>{{}}</html>");
        Mocker.SetupHtmlFilePropertyFiller();
	
        var sut = Mocker.Create<SiteTemplateFiller>();

        var result = sut.FillSiteTemplate("asdf");

        result.Should().BeEquivalentTo("<html>asdf</html>");
    }

    [Fact]
    public void SiteTemplateShouldNotFillWithoutMustache()
    {
        Mocker.SetupTemplateReader("<html></html>");
        Mocker.SetupHtmlFilePropertyFiller();
	
        var sut = Mocker.Create<SiteTemplateFiller>();

        var result = sut.FillSiteTemplate("asdf");

        Assert.Equal("<html></html>", result);
    }
}
