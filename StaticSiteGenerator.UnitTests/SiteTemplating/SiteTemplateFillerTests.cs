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
    }
}
