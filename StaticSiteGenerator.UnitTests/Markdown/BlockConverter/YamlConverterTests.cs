using Xunit;
using Microsoft.Toolkit.Parsers.Markdown.Blocks;
using StaticSiteGenerator.Markdown.BlockElementConverter;
using System.Collections.Generic;
using StaticSiteGenerator.Markdown.BlockElement;

namespace StaticSiteGenerator.UnitTests.Markdown.BlockConverter
{
    public class YamlConverterTests
    {
        [Theory]
        [MemberData(nameof(Data))]
        public void YamlConverter_Convert(Dictionary<string, string> attributes, int countOfAttributesFound)
        {
            var yamlHeaderBlock = new YamlHeaderBlock{
                Children = attributes
            };

            var converter = new YamlConverter();

            var result = (YamlHeader)converter.Convert(yamlHeaderBlock);

            Assert.Equal(countOfAttributesFound, result?.Attributes?.Count);
        }

        public static IEnumerable<object[]> Data => new List<object[]>
        {
            new object[] {new Dictionary<string, string> { { "publish_date", "10.10.10" } }, 1},
            new object[] { new Dictionary<string, string> { { "fake_option", "fake value"} }, 0},
            new object[] { new Dictionary<string, string> { { "", "fake value"} }, 0},

            new object[] {new Dictionary<string, string> { { "publish_date", "10.10.10" }, {"thing", "stuff"} }, 1},
            new object[] { new Dictionary<string, string>(), 0 },
        };
    }
}
