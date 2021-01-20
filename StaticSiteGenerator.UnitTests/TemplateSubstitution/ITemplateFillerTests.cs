using StaticSiteGenerator.TemplateSubstitution.TemplateFilling;
using StaticSiteGenerator.TemplateSubstitution.TemplateTags;
using Xunit;

namespace StaticSiteGenerator.UnitTests.TemplateSubstitution
{
    public class ITemplateFillerTests
    {
        [Theory]
        [InlineData("{{}}", "content", "content")]
        [InlineData("", "stuff", "")]
        [InlineData("{{as}}", "foo", "{{as}}")]
        [InlineData("", "", "")]
        public void TestFill(string templateString, string content, string expected)
        {
            var tag = new TemplateTag
            {
                Template = templateString
            };

            var filler = new TemplateFiller();

            var result = filler.Fill(tag, content);

            Assert.Equal(expected, result);
        }
    }
}
