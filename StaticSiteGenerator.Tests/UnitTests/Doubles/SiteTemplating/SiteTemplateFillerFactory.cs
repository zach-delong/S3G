using AutoFixture;
using NSubstitute;
using NSubstitute.Extensions;
using StaticSiteGenerator.HtmlWriting;
using StaticSiteGenerator.SiteTemplating.SiteTemplateFilling;

namespace StaticSiteGenerator.Tests.UnitTests.Doubles.SiteTemplating;

public static class SiteTemplateFillerFactory
{
    public static ISiteTemplateFiller SetupSiteTemplateFiller(this IFixture fixture, string template)
    {
        var mockSiteTemplateFiller = Substitute.For<ISiteTemplateFiller>();

        mockSiteTemplateFiller.Configure()
                              .FillSiteTemplate(Arg.Any<IHtmlFile>())
                              .Returns(template);

        fixture.Inject<ISiteTemplateFiller>(mockSiteTemplateFiller);

        return mockSiteTemplateFiller;
    }
}
