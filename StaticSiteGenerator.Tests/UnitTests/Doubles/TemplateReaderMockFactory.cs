using System.Collections.Generic;
using NSubstitute;
using StaticSiteGenerator.TemplateReading;
using StaticSiteGenerator.TemplateSubstitution.TemplateTags;

namespace StaticSiteGenerator.Tests.UnitTests.Doubles;

public class TemplateReaderMockFactory
{
    public static ITemplateReader Get(IList<TemplateTag> tagResult)
    {
        var mockReader = Substitute.For<ITemplateReader>();

        mockReader.ReadTemplate()
                  .Returns(tagResult);

        return mockReader;
    }
}
