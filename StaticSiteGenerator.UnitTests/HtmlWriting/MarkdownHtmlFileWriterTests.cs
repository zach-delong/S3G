using System;
using System.Linq;
using Moq;
using StaticSiteGenerator.FileManipulation.FileException;
using StaticSiteGenerator.HtmlWriting;
using StaticSiteGenerator.UnitTests.Doubles.FileManipulation;
using Xunit;

namespace StaticSiteGenerator.UnitTests.HtmlWriting
{
    public class MarkdownHtmlFileWriterTests
    {
        [Theory]
        [InlineData("testFile.md", "testFile")]
        [InlineData("TESTFILE.MD", "TESTFILE")]
        [InlineData("TestFile.Md", "TestFile")]
        [InlineData("testFile", "testFile")]
        [InlineData("md", "md")]
        public void foo(string inputFileName, string ExpectedFileName)
        {
            var mock = new Mock<IHtmlFileWriter>();

            IHtmlFileWriter writerUnderTest = new MarkdownHtmlFileWriter(mock.Object);

            writerUnderTest.Write(inputFileName, "");

            // Somehow validate that the input was called with an Html file
            mock.Verify(m => m.Write(ExpectedFileName, It.IsAny<string>()));
        }
    }
}
