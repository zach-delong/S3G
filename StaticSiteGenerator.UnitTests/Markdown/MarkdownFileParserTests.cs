using System.Collections.Generic;
using Microsoft.Toolkit.Parsers.Markdown.Blocks;
using Moq;
using StaticSiteGenerator.Markdown;
using StaticSiteGenerator.Markdown.Parser.BlockParser;
using StaticSiteGenerator.UnitTests.Doubles.FileManipulation;
using Xunit;

namespace StaticSiteGenerator.UnitTests.Markdown
{
    public class MarkdownFileParserTests
    {
        public FileReaderMockFactory FileReaderFactory => new FileReaderMockFactory();

        [Fact]
        public void TestShouldNeverCallMembersOnEmptyInput()
        {
            // Arrange
            var mockReader = FileReaderMockFactory.Get(new Dictionary<string, string>());
            var mockMarkdownParser = new Mock<IMarkdownBlockParser>();

            var fileParser = new MarkdownFileParser(mockReader.Object, mockMarkdownParser.Object);

            // Act
            var result = fileParser.ReadFiles(new List<string>());

            //Assert
            mockReader.Verify(m => m.ReadFile(It.IsAny<string>()), Times.Never());
            mockMarkdownParser.Verify(m => m.Parse(It.IsAny<IList<MarkdownBlock>>()), Times.Never());

        }
    }
}
