using StaticSiteGenerator.Markdown.BlockElement;
using Xunit;

namespace StaticSiteGenerator.UnitTests.Markdown.BlockConverter
{
    public class YamlAttributeTypeTests
    {
        [Theory]
        [InlineData("publish_date", true)]
        [InlineData("invalid_value", false)]
        [InlineData("", false)]
        [InlineData(null, false)]
        public void YamlAttributeType_IsValidValue(string inputValue, bool expectedResult)
        {
            var result = YamlAttributeType.IsValidValue(inputValue);

            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("publish_date", true)]
        [InlineData("invalid_value", false)]
        [InlineData(null, false)]
        [InlineData("", false)]
        public void YamlAttributeType_GetFromStringOrDefault(string inputValue, bool success)
        {
            var result = YamlAttributeType.GetFromStringOrDefault(inputValue);


            if(success)
                Assert.NotNull(result);
            else
                Assert.Null(result);
        }
    }
}
