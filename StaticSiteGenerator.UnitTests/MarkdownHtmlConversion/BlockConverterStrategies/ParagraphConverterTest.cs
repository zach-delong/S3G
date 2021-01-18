using System.Collections.Generic;

using Moq;
using Xunit;
using StaticSiteGenerator.Markdown.InlineElement;
using StaticSiteGenerator.Markdown.BlockElement;
using StaticSiteGenerator.TemplateSubstitution;
using StaticSiteGenerator.TemplateSubstitution.TemplateTags;
using Test.Markdown.Parser;
using StaticSiteGenerator.UnitTests.Doubles;
using StaticSiteGenerator.MarkdownHtmlConversion.BlockConverterStrategies;
using Test.TemplateSubstitution.BlockConverterStrategies;

namespace Test.MarkdownHtmlconversion.BlockConverterStrategies
{

    public class ParagraphConverterTest
    {
        private TemplateCollectionMockFactory templateCollectionMockFactory => new TemplateCollectionMockFactory();
        private StrategyCollectionMockFactory strategyCollectionMockFactory => new StrategyCollectionMockFactory();
        private MarkdownInlineHtmlConverterMockFactory inlineConverterMockFactory => new MarkdownInlineHtmlConverterMockFactory();

        [Fact]
        public void Test()
        {
            var inlineConverterMock = inlineConverterMockFactory.Get("TestText");

            Mock<ITemplateTagCollection> templateReader = templateCollectionMockFactory
                .Get(new List<TemplateTag> {
                        new TemplateTag {
                            Template ="<p>{{}}</p>",
                            Type = TagType.Paragraph
                        }
                    });

            var templateFillerMock = TemplateFillerMockFactory.Get();

            var converter = new ParagraphHtmlConverterStrategy(
                inlineConverterMock.Object,
                templateReader.Object,
                templateFillerMock.Object);

            var headerBlock = new Paragraph
            {
                Inlines = new List<IInlineElement>()
            };
            var result = converter.Convert(headerBlock);

            Assert.Equal("<p>TestText</p>", result);
        }
    }
}
