using System.Collections.Generic;
using Moq;
using StaticSiteGenerator.HtmlWriting;
using StaticSiteGenerator.MarkdownHtmlConversion;
using Xunit;

namespace StaticSiteGenerator.UnitTests.HtmlWriting
{
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

        [Theory]
        [MemberData(nameof(Data))]
        public void HtmlFileInterface(IEnumerable<IHtmlFile> files)
        {
            var mock = new Mock<IHtmlFileWriter>();

            var sut = new MarkdownHtmlFileWriter(mock.Object);

            sut.Write(files);

            foreach (var file in files)
            {
                mock.Verify(m => m.Write($"{ file.Name }.{ file.FileExtension }", file.HtmlContent));
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
