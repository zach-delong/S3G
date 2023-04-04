using System.Collections.Generic;
using Moq;
using StaticSiteGenerator.TemplateReading;
using StaticSiteGenerator.TemplateSubstitution.TemplateTags;

namespace StaticSiteGenerator.UnitTests.Doubles;

public class TemplateReaderMockFactory
{
    public static Mock<ITemplateReader> Get(IList<TemplateTag> tagResult)
    {
        var mockReader = new Mock<ITemplateReader>();

        mockReader.Setup(m => m.ReadTemplate())
                  .Returns(tagResult);

        return mockReader;
    }
}
