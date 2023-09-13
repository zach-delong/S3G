using NSubstitute;
using StaticSiteGenerator.SiteTemplating.SiteTemplateReading;

namespace StaticSiteGenerator.Tests.UnitTests.Doubles.SiteTemplating;

public class SiteTemplateReaderMockFactory
{
    public ISiteTemplateReader Get(string template)
    {
        var mock = Substitute.For<ISiteTemplateReader>();

        mock
            .ReadTemplate()
            .Returns(template);

        return mock;
    }
}
