using System.Collections.Generic;
using Moq.AutoMock;
using StaticSiteGenerator.TemplateSubstitution.TagCollection;
using StaticSiteGenerator.TemplateSubstitution.TemplateTags;

namespace StaticSiteGenerator.UnitTests.Doubles.SiteTemplating;

public static class SiteTemplateMockerExtensions
{

    public static void MockTemplateTagCollection(this AutoMocker stuff, TemplateTag resultTag)
    {
        var factory = new MockTemplateTagCollectionFactory();
        var mock = stuff.GetMock<ITemplateTagCollection>();

        factory.Get(mock, resultTag);
    }

    public static void MockTemplateTagCollection(this AutoMocker stuff, IEnumerable<TemplateTag> resultTags)
    {
        var factory = new MockTemplateTagCollectionFactory();
        var mock = stuff.GetMock<ITemplateTagCollection>();

        factory.Get(mock, resultTags);
    }
}
