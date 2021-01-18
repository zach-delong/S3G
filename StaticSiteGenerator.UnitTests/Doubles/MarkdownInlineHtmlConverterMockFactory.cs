using System.Collections.Generic;

using Moq;
using StaticSiteGenerator.Markdown.InlineElement;
using StaticSiteGenerator.MarkdownHtmlConversion.MarkdownHtmlConverters;

namespace Test.TemplateSubstitution.BlockConverterStrategies
{
    public class MarkdownInlineHtmlConverterMockFactory
    {
        public Mock<IMarkdownInlineConverter> Get(string resultText)
        {
            var inlineConverterMock = new Mock<IMarkdownInlineConverter>();

            inlineConverterMock
                .Setup(c => c.Convert(It.IsAny<IList<IInlineElement>>()))
                .Returns(resultText);
            return inlineConverterMock;
        }
    }
}
