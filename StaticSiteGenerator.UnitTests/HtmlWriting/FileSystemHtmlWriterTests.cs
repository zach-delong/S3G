using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using StaticSiteGenerator.Files.FileException;
using StaticSiteGenerator.HtmlWriting;
using StaticSiteGenerator.MarkdownHtmlConversion;
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

        [Theory]
        [MemberData(nameof(Data))]
        public void HtmlFileInterface(IEnumerable<IHtmlFile> files)
        {
            var mock = FileWriterMockFactory.Get();

            var sut = new FileSystemHtmlWriter(mock.Object);

            sut.Write(files);

            foreach (var file in files)
            {
                mock.Verify(m => m.WriteFile($"{ file.Name }.{ file.FileExtension }", file.HtmlContent));
            }
        }

        public static TheoryData<IEnumerable<IHtmlFile>> Data
        {
            get
            {
                var data = new TheoryData<IEnumerable<IHtmlFile>>();

                data.Add(new List<IHtmlFile>());

                data.Add(new List<IHtmlFile>
                {
                    new HtmlFile
                    {
                        Name = "TestFile_1",
                        HtmlContent = "<h1>Hello, world!</h1>"
                    }
                });

                data.Add(new List<IHtmlFile>
                {
                    new HtmlFile
                    {
                        Name = "TestFile_1",
                        HtmlContent = "<h1>Hello, world!</h1>"
                    },
                    new HtmlFile
                    {
                        Name = "TestFile_2",
                        HtmlContent = "<h1>Hello, world!</h1>"
                    },
                    new HtmlFile
                    {
                        Name = "TestFile_3",
                        HtmlContent = "<h1>Hello, world!</h1>"
                    }
                });

                return data;
            }
        }
    }
}
