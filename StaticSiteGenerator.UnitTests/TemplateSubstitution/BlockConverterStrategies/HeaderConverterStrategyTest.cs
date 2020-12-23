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
            var fileIteratorMock = new Mock<FileIterator>();
            var fileReadermock = new Mock<FileReader>();

            var strategyMock = mockFactory.Get(new Dictionary<string, IInlineConverterStrategy>());

            var inlineConverterMock = new Mock<MarkdownInlineConverter>(strategyMock.Object);

            var templateReader = new Mock<TemplateReader>(
                fileIteratorMock.Object,
                fileReadermock.Object
            );

            inlineConverterMock
                .Setup(c => c.Convert(It.IsAny<IList<IInlineElement>>()))
                .Returns("TestText");

            templateReader
                .Setup(r => r.GetTemplateTagForType(It.IsAny<TagType>()))
                .Returns(new TemplateTag {
                    Template = "<h1>{{}}</h1>",
                    Type = TagType.Header1
                });

            var converter = new HeaderConverterStrategy(
                inlineConverterMock.Object,
                templateReader.Object);

            var headerBlock = new Header {
                Inlines = new List<IInlineElement>()
            };
            var result = converter.Convert(headerBlock);

            Assert.Equal("<h1>TestText</h1>", result);
        }
    }
}
