using System.Collections.Generic;

using Moq;
using Xunit;

using StaticSiteGenerator.FileManipulation;
using StaticSiteGenerator.Markdown.InlineElement;
using StaticSiteGenerator.Markdown.BlockElement;
using StaticSiteGenerator.TemplateSubstitution;
using StaticSiteGenerator.TemplateSubstitution.TemplateTags;
using StaticSiteGenerator.TemplateSubstitution.BlockConverterStrategies;
using StaticSiteGenerator.TemplateSubstitution.MarkdownHtmlConverters;
using StaticSiteGenerator.TemplateSubstitution.InlineConverterStrategies;
using StaticSiteGenerator.Utilities.StrategyPattern;
using Test.Markdown.Parser;

namespace Test.TemplateSubstitution.BlockConverterStrategies
{
    public class TestHeaderBlockConverterStrategy
    {
        private StrategyCollectionMockFactory mockFactory => new StrategyCollectionMockFactory();

        [Fact]
        public void Test()
        {
            var inlineConverterMock = GetInlineConverterMock("TestText");

            Mock<ITemplateReader> templateReader = getTemplateReaderMock("<h1>{{}}</h1>", TagType.Header1);

            var converter = new HeaderConverterStrategy(
                inlineConverterMock.Object,
                templateReader.Object);

            var headerBlock = new Header
            {
                Inlines = new List<IInlineElement>()
            };
            var result = converter.Convert(headerBlock);

            Assert.Equal("<h1>TestText</h1>", result);
        }

        private static Mock<ITemplateReader> getTemplateReaderMock(string template, TagType type)
        {
            var templateReader = new Mock<ITemplateReader>();

            templateReader
                .Setup(r => r.GetTemplateTagForType(It.IsAny<TagType>()))
                .Returns(new TemplateTag
                {
                    Template = template,
                    Type = type
                });
            return templateReader;
        }

        private static Mock<IMarkdownInlineConverter> GetInlineConverterMock(string resultText)
        {
            var inlineConverterMock = new Mock<IMarkdownInlineConverter>();

            inlineConverterMock
                .Setup(c => c.Convert(It.IsAny<IList<IInlineElement>>()))
                .Returns(resultText);
            return inlineConverterMock;
        }
    }
}
