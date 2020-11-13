using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using StaticSiteGenerator.FileManipulation;
using StaticSiteGenerator.FileManipulation.FileException;
using Test.Helpers;

namespace Test.FileManipulation
{
    public class FileIteratorTests
    {
        [Test]
        [Parallelizable(ParallelScope.Self)]
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
        [Parallelizable(ParallelScope.Self)]
        public void FolderContainsNoFiles()
        {
            using(var tempFolderPath = TempFileHelper.GetTempFolder())
            {
                var fileIterator = new FileIterator();

                var result = fileIterator.GetFilesInDirectory(tempFolderPath.Path);

                Assert.That(result.Count, Is.EqualTo(0));
            }
        }

        [Test]
        [Parallelizable(ParallelScope.Self)]
        public void FolderDoesNotExist()
        {
            var tempFolderPath = Path.GetTempPath() + Guid.NewGuid().ToString();

            var tempFileName = tempFolderPath + "/" + Guid.NewGuid().ToString() + ".txt";

            var fileIterator = new FileIterator();

            Assert.Throws<FileManipulationException>( () => { fileIterator.GetFilesInDirectory(tempFolderPath); } );
        }
    }
}
