using Moq;
using StaticSiteGenerator.HtmlWriting;
using Xunit;

namespace StaticSiteGenerator.UnitTests.HtmlWriting;

public class MarkdownHtmlFileWriterTests
{
    [Theory]
    [InlineData("testFile", "testFile")]
    [InlineData("TESTFILE", "TESTFILE")]
    [InlineData("TestFile", "TestFile")]
    [InlineData("md", "md")]
    public void StringInterface(string inputFileName, string ExpectedFileName)
    {
        var mock = new Mock<IHtmlFileWriter>();

        IHtmlFileWriter writerUnderTest = new MarkdownHtmlFileWriter(mock.Object);

        writerUnderTest.Write(inputFileName, "");

        // Somehow validate that the input was called with an Html file
        mock.Verify(m => m.Write(ExpectedFileName, It.IsAny<string>()));
    }
}
