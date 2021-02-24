using System;
using System.Linq;
using Moq;
using StaticSiteGenerator.FileManipulation.FileException;
using StaticSiteGenerator.HtmlWriting;
using StaticSiteGenerator.UnitTests.Doubles.FileManipulation;
using Xunit;

namespace StaticSiteGenerator.UnitTests.HtmlWriting
{
    public class FileSystemHtmlWriterTests
    {
        private FileWriterMockFactory FileWriterMockFactory => new FileWriterMockFactory();

        [Theory]
        [InlineData("testFile", "testFile")]
        [InlineData("testFile.html", "testFile.html")]
        [InlineData("", "")]
        [InlineData("a", "a")]
        public void InputFileShouldBeWrittenAsHtml(string inputFileName, string ExpectedFileName)
        {
            var mock = FileWriterMockFactory.Get();

            IHtmlFileWriter writerUnderTest = new FileSystemHtmlWriter(mock.Object);

            writerUnderTest.Write(inputFileName, "");

            // Somehow validate that the input was called with an Html file
            mock.Verify(m => m.WriteFile(ExpectedFileName, It.IsAny<string>()));
        }

        [Theory]
        [InlineData("testFile.html")]
        [InlineData("asdf.html")]
        public void WriterShouldThrowErrorWhenFileAlreadyExists(string fileName)
        {
            var fileNames = new string[] { "testfile.html" };

            var mock = FileWriterMockFactory.Get();

            IHtmlFileWriter writerUnderTest = new FileSystemHtmlWriter(mock.Object);


            if(fileNames.Contains(fileName))
            {
                Assert.Throws<FileAlreadyExistsException>(() => writerUnderTest.Write(fileName, "")) ;
            }

            writerUnderTest.Write(fileName, "");

            mock.Verify(m => m.WriteFile(fileName, It.IsAny<string>()));
        }
    }
}
