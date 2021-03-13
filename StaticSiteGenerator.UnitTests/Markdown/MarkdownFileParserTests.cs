using System.Collections.Generic;
using System.Linq;
using Microsoft.Toolkit.Parsers.Markdown.Blocks;
using Moq;
using StaticSiteGenerator.Markdown;
using StaticSiteGenerator.Markdown.Parser;
using StaticSiteGenerator.Markdown.Parser.BlockParser;
using StaticSiteGenerator.Markdown.YamlMetadata;
using StaticSiteGenerator.UnitTests.Doubles.FileManipulation;
using StaticSiteGenerator.UnitTests.Helpers;
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
            var mockMarkdownParser = new Mock<IMarkdownBlockParser>();
            var mockYamlMetadataProcessor = new Mock<IYamlMetadataProcessor>();

            var fileParser = new MarkdownFileParser(mockReader.Object, mockMarkdownParser.Object, mockYamlMetadataProcessor.Object);

            // Act
            var result = fileParser.ReadFiles(new List<string>());

            //Assert
            mockReader.Verify(m => m.ReadFile(It.IsAny<string>()), Times.Never());
            mockMarkdownParser.Verify(m => m.Parse(It.IsAny<IList<MarkdownBlock>>()), Times.Never());
            mockYamlMetadataProcessor.Verify(m => m.ParseYamlMetadata(It.IsAny<IMarkdownFile>()), Times.Never());
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
            var mockMarkdownParser = new Mock<IMarkdownBlockParser>();
            var mockYamlMetadataProcessor = new Mock<IYamlMetadataProcessor>();

            var fileParser = new MarkdownFileParser(mockReader.Object, mockMarkdownParser.Object, mockYamlMetadataProcessor.Object);

            // Act
            var result = fileParser.ReadFiles(fileNames)
                                   .ToList();

            //Assert
            mockReader.Verify(m => m.ReadFile(It.IsAny<string>()), Times.Exactly(numberOfFiles));
            mockMarkdownParser.Verify(m => m.Parse(It.IsAny<IList<MarkdownBlock>>()), Times.Exactly(numberOfFiles));
            mockYamlMetadataProcessor.Verify(m => m.ParseYamlMetadata(It.IsAny<IMarkdownFile>()), Times.Exactly(numberOfFiles));
        }
    }
}
