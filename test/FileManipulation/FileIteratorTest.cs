using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using StaticSiteGenerator.FileManipulation;

namespace Test.FileManipulation
{
    public class FileIteratorTests
    {
        [Test]
        public void FolderContainsOneFile()
        {
            using(var tempFolderPath = TempFileHelper.GetTempFolder())
            using(var tempFileName = TempFileHelper.GetTempTextFile(tempFolderPath.Path))
            {
                var fileIterator = new FileIterator();

                var result = fileIterator.GetFilesInDirectory(tempFolderPath.Path);

                Assert.That(result.Count, Is.EqualTo(1));
            }
        }

        [Test]
        public void FolderDoesNotExist()
        {
            var tempFolderPath = Path.GetTempPath() + Guid.NewGuid().ToString();

            var tempFileName = tempFolderPath + "/" + Guid.NewGuid().ToString() + ".txt";

            var fileIterator = new FileIterator();

            Assert.Throws<DirectoryNotFoundException>( () => { fileIterator.GetFilesInDirectory(tempFolderPath); } );
        }
    }
}
