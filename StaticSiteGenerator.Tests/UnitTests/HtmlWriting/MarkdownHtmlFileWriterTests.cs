using AutoFixture;
using NSubstitute;
using StaticSiteGenerator.HtmlWriting;
using StaticSiteGenerator.Tests.AutoFixture;
using Xunit;

namespace StaticSiteGenerator.Tests.UnitTests.HtmlWriting;

public class MarkdownHtmlFileWriterTests: MockingTestBase
{
    [Theory]
    [InlineData("testFile", "testFile")]
    [InlineData("TESTFILE", "TESTFILE")]
    [InlineData("TestFile", "TestFile")]
    [InlineData("md", "md")]
    public void StringInterface(string inputFileName, string ExpectedFileName)
    {
        var htmlFileWriter = Mocker.Freeze<IHtmlFileWriter>();
        var writerUnderTest = Mocker.Create<MarkdownHtmlFileWriter>();

        writerUnderTest.Write(inputFileName, "");

        // Somehow validate that the input was called with an Html file
        htmlFileWriter
	    .Received()
	    .Write(ExpectedFileName, Arg.Any<string>());
    }
}
