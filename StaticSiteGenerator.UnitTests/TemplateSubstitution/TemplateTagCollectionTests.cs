using System.Collections.Generic;
using StaticSiteGenerator.TemplateSubstitution.TagCollection;
using StaticSiteGenerator.TemplateSubstitution.TemplateTags;
using StaticSiteGenerator.UnitTests.Doubles;
using Xunit;

namespace StaticSiteGenerator.UnitTests.TemplateSubstitution.TagCollection
{
    public class TemplateTagCollectionTests
    {
        IList<TemplateTag> SingleTemplate => new List<TemplateTag>
        {
            new TemplateTag
            {
                Template = "<h1>{{}}</h1>",
                Type = TagType.Header1
            },
        };

        IList<TemplateTag> MultipleTemplate => new List<TemplateTag>
        {
            new TemplateTag
            {
                Template = "<h1>{{}}</h1>",
                Type = TagType.Header1
            },
            new TemplateTag
            {
                Template = "<p>{{}}</p>",
                Type = TagType.Paragraph
            }
        };

        [Fact]
        void TagCollectionShouldNotErrorWithoutTemplates()
        {
            var mockReader = TemplateReaderMockFactory.Get(new List<TemplateTag>());

            ITemplateTagCollection bar = new TemplateTagCollection(mockReader.Object);

            // ASSERT: this just shouldn't throw an exception
        }

        [Fact]
        void TagCollectionshouldProcessWithOneResult()
        {
            var mockReader = TemplateReaderMockFactory.Get(SingleTemplate);

            ITemplateTagCollection bar = new TemplateTagCollection(mockReader.Object);

            var headerTemplate = bar.GetTagForType(TagType.Header1);

            Assert.NotNull(headerTemplate);
        }

        [Fact]
        void TagCollectionshouldProcessWithMultipleResults()
        {
            var mockReader = TemplateReaderMockFactory.Get(MultipleTemplate);

            ITemplateTagCollection bar = new TemplateTagCollection(mockReader.Object);

            var headerTemplate = bar.GetTagForType(TagType.Header1);
            var paragraphTemplate = bar.GetTagForType(TagType.Paragraph);

            Assert.NotNull(headerTemplate);
            Assert.NotNull(paragraphTemplate);
        }
    }
}
