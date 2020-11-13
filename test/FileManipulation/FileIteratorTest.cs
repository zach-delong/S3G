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
            var tempFolderPath = Path.GetTempPath() + Guid.NewGuid().ToString();

            var tempFileName = tempFolderPath + "/" + Guid.NewGuid().ToString() + ".txt";

            Directory.CreateDirectory(tempFolderPath);
            var fileIterator = new FileIterator();

            using (System.IO.FileStream fs = System.IO.File.Create(tempFileName))
            {
                for (byte i = 0; i < 100; i++)
                {
                    fs.WriteByte(i);
                }
            }

            var result = fileIterator.GetFilesInDirectory(tempFolderPath);

            File.Delete(tempFileName);
            Directory.Delete(tempFolderPath);

            Assert.That(result.Count, Is.EqualTo(1));
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
