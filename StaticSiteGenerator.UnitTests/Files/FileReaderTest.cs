using Xunit;

using StaticSiteGenerator.Files;
using StaticSiteGenerator.UnitTests.Helpers;
using System.IO.Abstractions;
using StaticSiteGenerator.Files.FileException;

namespace StaticSiteGenerator.UnitTests.Filese
{
    public class FileReaderTest
    {
        [Fact]
        public void FileDoesNotExist()
        {
            var FileReader = new FileReader(new FileSystem());

            var filePath = "NonExistantFileName.txt";

            Assert.Throws<FileManipulationException>(() => { FileReader.ReadFile(filePath); });
        }

        [Fact]
        public void FileExistsButIsEmpty()
        {
            using(var file = TempFileHelper.GetTempTextFile())
            {
                var fileReader = new FileReader(new FileSystem());

                var fileContents = fileReader.ReadFile(file.Path);

                Assert.Equal("", fileContents);
            }
        }

        [Fact]
        public void FileExists()
        {
            using(var file = TempFileHelper.GetTempTextFile())
            {
                var contents = "Test File Contents";
                file.WriteToFile(contents);

                var fileReader = new FileReader(new FileSystem());

                var fileContents = fileReader.ReadFile(file.Path);


                Assert.Contains(contents, fileContents);
            }
        }
    }
}