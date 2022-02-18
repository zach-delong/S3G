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

        tags
            .Setup(c => c.GetTagForType(It.IsAny<TagType>()))
            .Returns(resultTag);

        return tags;
    }

    public Mock<ITemplateTagCollection> Get(IEnumerable<TemplateTag> resultTag)
    {
        var tags = new Mock<ITemplateTagCollection>();

        var tagDictionary = resultTag.ToDictionary(x => x.Type, x => x);

        tags
            .Setup(c => c.GetTagForType(It.IsAny<TagType>()))
            .Returns<TagType>(x => tagDictionary[x]);

        return tags;
    }
}
