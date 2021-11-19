using System.Collections.Generic;
using System.Linq;
using Markdig.Syntax;
using Microsoft.Extensions.Logging;
using Moq;
using StaticSiteGenerator.Markdown;
using StaticSiteGenerator.Markdown.BlockElement;
using StaticSiteGenerator.Markdown.Parser;
using StaticSiteGenerator.UnitTests.Doubles.FileManipulation;
using StaticSiteGenerator.UnitTests.Helpers;
using StaticSiteGenerator.Utilities.StrategyPattern;
using Xunit;

namespace StaticSiteGenerator.UnitTests.Markdown
{
    public class MarkdownFileParserTests
    {
        private FileReaderMockFactory FileReaderFactory => new FileReaderMockFactory();
        private RandomizedStringListGenerator FileNameGenerator => new RandomizedStringListGenerator();
        private BlankMarkdownFileDictionaryGenerator BlankMarkdownFileDictionaryGenerator = new BlankMarkdownFileDictionaryGenerator();

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(3)]
        public void TestShouldNeverCallMembersWithoutEnumeration(int numberOfFiles)
        {
            var fileNames = FileNameGenerator.GetStrings(numberOfFiles).ToList();

            // Arrange
            var mockReader = FileReaderMockFactory.Get(fileNames.ToDictionary(name => name, name => ""));
            var mockMarkdownParser = new Mock<IStrategyExecutor<IBlockElement, IBlock>>();
            var mockedLogger = new Mock<ILogger<MarkdownFileParser>>();

            var fileParser = new MarkdownFileParser(mockReader.Object,
                                                    mockMarkdownParser.Object,
                                                    mockedLogger.Object);

            // Act
            var result = fileParser.ReadFiles(new List<string>());

            //Assert
            mockReader.Verify(m => m.ReadFile(It.IsAny<string>()), Times.Never());
            mockMarkdownParser.Verify(m => m.Process(It.IsAny<MarkdownDocument>()), Times.Never());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(3)]
        public void TestShouldCallMembersOnEnumeration(int numberOfFiles)
        {
            var fileNames = FileNameGenerator.GetStrings(numberOfFiles)
                                             .ToList();

            // Arrange
            var mockReader = FileReaderMockFactory.Get(fileNames.ToDictionary(name => name, name => ""));
            var mockMarkdownParser = new Mock<IStrategyExecutor<IBlockElement, IBlock>>();
            var mockLogger = new Mock<ILogger<MarkdownFileParser>>();

            var fileParser = new MarkdownFileParser(mockReader.Object,
                                                    mockMarkdownParser.Object,
                                                    mockLogger.Object);

            // Act
            var result = fileParser.ReadFiles(fileNames)
                                   .ToList();

            //Assert
            mockReader.Verify(m => m.ReadFile(It.IsAny<string>()), Times.Exactly(numberOfFiles));
            mockMarkdownParser.Verify(m => m.Process(It.IsAny<MarkdownDocument>()), Times.Exactly(numberOfFiles));
        }
    }
}
