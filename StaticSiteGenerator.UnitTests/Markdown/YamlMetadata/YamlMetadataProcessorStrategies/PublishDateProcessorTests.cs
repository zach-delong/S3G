using System;
using System.Collections.Generic;
using Moq;
using StaticSiteGenerator.Markdown;
using StaticSiteGenerator.Markdown.BlockElement;
using StaticSiteGenerator.Markdown.YamlMetadata.YamlMetadataProcessorStrategies;
using StaticSiteGenerator.Utilities.Date;
using Xunit;

namespace StaticSiteGenerator.UnitTests.Markdown.YamlMetadata.YamlMetadataProcessorStrategies
{
    public class PublishDateProcessorTests
    {
        delegate bool TestCallback(string input, out DateTime date);

        [Theory]
        [MemberData(nameof(TestData))]
        public void ShouldWork(IList<YamlHeader> headers, bool expectedBool, DateTime expectedDate)
        {
            var file = new MarkdownFile();
            var mock = new Mock<IDateParser>();
            DateTime parsedTime;

            mock.Setup(m => m.TryParse(It.IsAny<string>(), out parsedTime))
                .Returns(new TestCallback((string input, out DateTime output) => { output = expectedDate; return expectedBool; }));

            var parser = new PublishDateProcessor(mock.Object);

            var result = parser.Process(file, headers);

            Assert.Equal(expectedDate, result.PublishedDate);
        }

        private static readonly List<YamlHeader> yamlHeadersContainingDateWIthDashes = new List<YamlHeader>
        {
            new YamlHeader
            {
                Attributes = new Dictionary<YamlAttributeType, string>
                {
                    { YamlAttributeType.PublishDate, "12-31-2021" }
                }
            }
        };

        public static IEnumerable<object[]> TestData = new List<object[]>
        {
            new object [] { yamlHeadersContainingDateWIthDashes, true, new DateTime(2021, 12, 31, 0, 0, 0) },
            new object [] { new List<YamlHeader>(), false, null },
        };
    }
}
