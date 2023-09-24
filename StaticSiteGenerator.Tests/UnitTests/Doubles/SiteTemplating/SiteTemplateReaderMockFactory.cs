using AutoFixture;
using NSubstitute;
using StaticSiteGenerator.SiteTemplating.SiteTemplateReading;

namespace StaticSiteGenerator.Tests.UnitTests.Doubles.SiteTemplating;

public static class SiteTemplateReaderMockFactory
{
    public static ISiteTemplateReader SetupTemplateReader(this IFixture fixture, string template)
    {
        var mock = Substitute.For<ISiteTemplateReader>();

        mock
            .ReadTemplate()
            .Returns(template);

        fixture.Inject<ISiteTemplateReader>(mock);

        return mock;
    }

}
