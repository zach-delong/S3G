using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using NSubstitute;
using StaticSiteGenerator.TemplateSubstitution.TagCollection;
using StaticSiteGenerator.TemplateSubstitution.TemplateTags;

namespace StaticSiteGenerator.Tests.UnitTests.Doubles.SiteTemplating;

public static class MockTemplateTagCollectionFactory
{
    public static ITemplateTagCollection Get(TemplateTag resultTag)
    {
        var tags = Substitute.For<ITemplateTagCollection>();

        return Get(tags, resultTag);
    }

    public static void MockTemplateTagCollection(this IFixture fixture, TemplateTag resultTag)
    {
        var frozenMock = fixture
            .Freeze<ITemplateTagCollection>();

        Get(frozenMock, resultTag);

    }

    public static ITemplateTagCollection Get(ITemplateTagCollection mock, TemplateTag resultTag)
    {
        mock 
            .GetTagForType(Arg.Any<TagType>())
            .Returns(resultTag);

        return mock;
    }

    public static ITemplateTagCollection Get(IEnumerable<TemplateTag> resultTag)
    {
        var tags = Substitute.For<ITemplateTagCollection>();

        return Get(tags, resultTag);
    }

    public static ITemplateTagCollection Get(ITemplateTagCollection mock, IEnumerable<TemplateTag> resultTags)
    {
        var tagDictionary = resultTags.ToDictionary(x => x.Type, x => x);

	mock
            .GetTagForType(Arg.Any<TagType>())
            .Returns((x) => tagDictionary[(TagType)x[0]]);

        return mock;
    }
}
