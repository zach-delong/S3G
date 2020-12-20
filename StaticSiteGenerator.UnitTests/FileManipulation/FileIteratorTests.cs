using System;
using System.IO;
using System.Linq;
using Xunit;

using StaticSiteGenerator.UnitTests.Helpers;

using StaticSiteGenerator.FileManipulation;
using StaticSiteGenerator.FileManipulation.FileException;

namespace StaticsiteGenerator.UnitTests.FileManipulation
{
    public class FileIteratorTests
    {
        [Fact]
        public void Test1()
        {
            using(var tempFolderPath = TempFileHelper.GetTempFolder())
            using(var tempFileName = TempFileHelper.GetTempTextFile(tempFolderPath.Path + "/"))
            {
                var fileIterator = new FileIterator();

                var result = fileIterator.GetFilesInDirectory(tempFolderPath.Path);

                Assert.Equal(result.Count(), 1);
            }
        }

        [Fact]
        public void FolderContainsNoFiles()
        {
            using(var tempFolderPath = TempFileHelper.GetTempFolder())
            {
                var fileIterator = new FileIterator();

                var result = fileIterator.GetFilesInDirectory(tempFolderPath.Path);

                Assert.Equal(result.Count(), 0);
            }
        }

        [Fact]
        public void FolderDoesNotExist()
        {
            var tempFolderPath = Path.GetTempPath() + Guid.NewGuid().ToString();

            var tempFileName = tempFolderPath + "/" + Guid.NewGuid().ToString() + ".txt";

            var fileIterator = new FileIterator();

            Assert.Throws<FileManipulationException>( () => { fileIterator.GetFilesInDirectory(tempFolderPath); } );
        }
    }
}
