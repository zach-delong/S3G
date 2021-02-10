using System;
using Moq;
using StaticSiteGenerator.MarkdownHtmlConversion;

namespace StaticSiteGenerator.UnitTests.Doubles.Markdown
{
    public static class MarkdownConverterMockFactory
    {
        public static Mock<IMarkdownConverter> Get()
        {
            var mock = new Mock<IMarkdownConverter>();

            return mock;
        }
    }
}
