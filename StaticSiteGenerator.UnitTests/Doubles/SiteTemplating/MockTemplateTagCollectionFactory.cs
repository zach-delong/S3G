using Moq;
using StaticSiteGenerator.TemplateSubstitution.TagCollection;
using StaticSiteGenerator.TemplateSubstitution.TemplateTags;

namespace StaticSiteGenerator.UnitTests.Doubles.SiteTemplating;

public class MockTemplateTagCollectionFactory
{
    public Mock<ITemplateTagCollection> Get(TemplateTag resultTag)
    {
        var tags = new Mock<ITemplateTagCollection>();

        tags
            .Setup(c => c.GetTagForType(It.IsAny<TagType>()))
            .Returns(resultTag);

        return tags;
    }
}
