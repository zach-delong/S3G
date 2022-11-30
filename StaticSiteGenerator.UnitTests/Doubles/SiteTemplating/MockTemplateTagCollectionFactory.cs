using System.Collections.Generic;
using System.Linq;
using Moq;
using StaticSiteGenerator.TemplateSubstitution.TagCollection;
using StaticSiteGenerator.TemplateSubstitution.TemplateTags;

namespace StaticSiteGenerator.UnitTests.Doubles.SiteTemplating;

public class MockTemplateTagCollectionFactory
{
    public Mock<ITemplateTagCollection> Get(TemplateTag resultTag)
    {
        var tags = new Mock<ITemplateTagCollection>();

        return Get(tags, resultTag);
    }

    public Mock<ITemplateTagCollection> Get(Mock<ITemplateTagCollection> mock, TemplateTag resultTag)
    {
        mock 
            .Setup(c => c.GetTagForType(It.IsAny<TagType>()))
            .Returns(resultTag);

        return mock;
    }

    public Mock<ITemplateTagCollection> Get(IEnumerable<TemplateTag> resultTag)
    {
        var tags = new Mock<ITemplateTagCollection>();

        return Get(tags, resultTag);
    }

    public Mock<ITemplateTagCollection> Get(Mock<ITemplateTagCollection> mock, IEnumerable<TemplateTag> resultTags)
    {
        var tagDictionary = resultTags.ToDictionary(x => x.Type, x => x);

	mock
            .Setup(c => c.GetTagForType(It.IsAny<TagType>()))
            .Returns<TagType>(x => tagDictionary[x]);

        return mock;
    }
}
