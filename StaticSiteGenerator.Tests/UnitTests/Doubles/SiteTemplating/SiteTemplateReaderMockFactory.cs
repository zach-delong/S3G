using Moq;
using StaticSiteGenerator.SiteTemplating.SiteTemplateReading;

namespace StaticSiteGenerator.UnitTests.Doubles.SiteTemplating;

public class SiteTemplateReaderMockFactory
{
    public Mock<ISiteTemplateReader> Get(string template)
    {
        var mock = new Mock<ISiteTemplateReader>();

        mock
            .Setup(m => m.ReadTemplate())
            .Returns(template);

        return mock;
    }
}
