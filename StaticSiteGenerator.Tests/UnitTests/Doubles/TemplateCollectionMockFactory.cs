using System.Collections.Generic;
using Moq;
using StaticSiteGenerator.TemplateSubstitution.TemplateTags;
using System.Linq;
using StaticSiteGenerator.TemplateSubstitution.TagCollection;

namespace StaticSiteGenerator.Tests.UnitTests.Doubles;

public class TemplateCollectionMockFactory
{
    public Mock<ITemplateTagCollection> Get(IList<TemplateTag> templateTagList)
    {
        var tagDefinitions = templateTagList.ToDictionary(m => m.Type);
        var templateReader = new Mock<ITemplateTagCollection>();

        templateReader
            .Setup(r => r.GetTagForType(It.IsAny<TagType>()))
            .Returns<TagType>(p => tagDefinitions[p]);

        return templateReader;
    }
}
