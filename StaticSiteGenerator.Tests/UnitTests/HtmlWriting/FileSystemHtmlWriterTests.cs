using System;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using NSubstitute;
using StaticSiteGenerator.Files.FileException;
using StaticSiteGenerator.HtmlWriting;
using StaticSiteGenerator.Tests.UnitTests.Doubles.FileManipulation;
using Xunit;

namespace StaticSiteGenerator.Tests.UnitTests.HtmlWriting;

public class FileSystemHtmlWriterTests: AutoFixture.MockingTestBase
{
    [Theory]
    [InlineData("testFile", "testFile")]
    [InlineData("testFile.html", "testFile.html")]
    [InlineData("", "")]
    [InlineData("a", "a")]
    public void InputFileShouldBeWrittenAsHtml(string inputFileName, string ExpectedFileName)
    {
        var mock = Mocker.SetupFileWriter();

        IHtmlFileWriter writerUnderTest = Mocker.Create<FileSystemHtmlWriter>();

        writerUnderTest.Write(inputFileName, "");

        // Somehow validate that the input was called with an Html file
        mock.Received()
	    .WriteFile(ExpectedFileName, Arg.Any<string>());
    }

    [Theory]
    [InlineData("testFile.html")]
    [InlineData("asdf.html")]
    public void WriterShouldThrowErrorWhenFileAlreadyExists(string fileName)
    {
        var fileNames = new string[] { "testfile.html" };

        var mock = Mocker.SetupFileWriter();

        IHtmlFileWriter writerUnderTest = Mocker.Create<FileSystemHtmlWriter>();


        if (fileNames.Contains(fileName))
        {
	    writerUnderTest
		.Invoking(w => w.Write(fileName, ""))
		.Should().Throw<FileAlreadyExistsException>();
        }

        writerUnderTest.Write(fileName, "");

        mock
	    .Received()
	    .WriteFile(fileName, Arg.Any<string>());
    }
}
