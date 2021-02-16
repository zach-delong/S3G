using System.Collections.Generic;
using Moq;
using StaticSiteGenerator.Markdown.BlockElement;
using StaticSiteGenerator.MarkdownHtmlConversion.MarkdownHtmlConverters;

namespace StaticSiteGenerator.UnitTests.Doubles.Markdown
{
    public class MarkdownBlockConverterMockFactory
    {
        public Mock<IMarkdownBlockConverter> Get()
        {
            var mock = new Mock<IMarkdownBlockConverter>();

            return mock;
        }
    }
}
