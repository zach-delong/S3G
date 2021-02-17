using System.Collections.Generic;
using StaticSiteGenerator.MarkdownHtmlConversion;
using StaticSiteGenerator.SiteTemplating.SiteTemplateFilling;
using StaticSiteGenerator.UnitTests.Doubles.SiteTemplating;
using Xunit;

namespace StaticSiteGenerator.UnitTests.SiteTemplating
{
    public class SiteTemplateFillerTests
    {
        private SiteTemplateReaderMockFactory MockFactory => new SiteTemplateReaderMockFactory();
        [Fact]
        public void SiteTemplateFillerShouldFillWithCachedTemplate()
        {
            var mock = MockFactory.Get("<html>{{}}</html>");

            var sut = new SiteTemplateFiller(mock.Object);

            var result = sut.FillSiteTemplate("asdf");

            Assert.Equal("<html>asdf</html>", result);
        }

        [Fact]
        public void SiteTemplateShouldNotFillWithoutMustache()
        {
            var mock = MockFactory.Get("<html></html>");

            var sut = new SiteTemplateFiller(mock.Object);

            var result = sut.FillSiteTemplate("asdf");

            Assert.Equal("<html></html>", result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(3)]
        public void SiteTemplateFillerShouldEnumerateCorrectly(int num)
        {
            var mock = MockFactory.Get("<html></html>");

            var sut = new SiteTemplateFiller(mock.Object);

            var templates = new List<IHtmlFile>();

            for (var i = 0; i < num; i++)
            {
                templates.Add(new HtmlFile());
            }

            var result = sut.FillSiteTemplate(templates);

            Assert.All(result, item => Assert.Equal("<html></html>", item.HtmlContent));
        }

    }
}
