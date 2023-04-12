using FluentAssertions;
using Moq;
using StaticSiteGenerator.HtmlWriting;
using StaticSiteGenerator.SiteTemplating.SiteTemplateFilling;
using StaticSiteGenerator.UnitTests.Doubles.SiteTemplating;
using Xunit;

namespace StaticSiteGenerator.UnitTests.SiteTemplating;

public class SiteTemplateFillerTests
{
    private SiteTemplateReaderMockFactory templateFillerMockFactory => new SiteTemplateReaderMockFactory();
    [Fact]
    public void SiteTemplateFillerShouldFillWithCachedTemplate()
    {
        var templateFillerMock = templateFillerMockFactory.Get("<html>{{}}</html>");
        var templatePropertyFillerMock = new Mock<HtmlFilePropertyFiller>(null);
        templatePropertyFillerMock
	    .Setup(s => s.FillTemplateProperties(It.IsAny<IHtmlFile>()))
	    .Returns((IHtmlFile file) => file.HtmlContent);

        var sut = new SiteTemplateFiller(templateFillerMock.Object, templatePropertyFillerMock.Object);

        var result = sut.FillSiteTemplate("asdf");

        result.Should().BeEquivalentTo("<html>asdf</html>");
    }

    [Fact]
    public void SiteTemplateShouldNotFillWithoutMustache()
    {
        var mock = templateFillerMockFactory.Get("<html></html>");
        var templatePropertyFillerMock = new Mock<HtmlFilePropertyFiller>(null);
        templatePropertyFillerMock
	    .Setup(s => s.FillTemplateProperties(It.IsAny<IHtmlFile>()))
	    .Returns((IHtmlFile file) => file.HtmlContent);

        var sut = new SiteTemplateFiller(mock.Object, templatePropertyFillerMock.Object);

        var result = sut.FillSiteTemplate("asdf");

        Assert.Equal("<html></html>", result);
    }
}
