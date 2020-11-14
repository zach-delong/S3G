using System;
using NUnit.Framework;

using StaticSiteGenerator.FileManipulation;
using StaticSiteGenerator.FileManipulation.FileException;

using Test.Helpers;

namespace Test.FileManipulation
{
    public class FileReaderTest
    {
        [Test]
        [Parallelizable(ParallelScope.Self)]
        public void FileDoesNotExist()
        {
            var FileReader = new FileReader();

            var filePath = "NonExistantFileName.txt";

            Assert.Throws<FileManipulationException>(() => { FileReader.ReadFile(filePath); });
        }

        [Test]
        [Parallelizable(ParallelScope.Self)]
        public void FileExistsButIsEmpty()
        {
            using(var file = TempFileHelper.GetTempTextFile())
            {
                var fileReader = new FileReader();

                var fileContents = fileReader.ReadFile(file.Path).ReadToEnd();

                Assert.That(fileContents, Is.Empty);
            }
        }
        [Test]
        [Parallelizable(ParallelScope.Self)]
        public void FileExists()
        {
            using(var file = TempFileHelper.GetTempTextFile())
            {
                Console.WriteLine("Beginning Test, writing file contents");
                var contents = "Test File Contents";
                file.WriteToFile(contents);

                Console.WriteLine("Attempting to read file");
                var fileReader = new FileReader();

                var fileContents = fileReader.ReadFile(file.Path).ReadToEnd();


                Assert.That(fileContents, Does.Contain(contents));
            }
        }
    }
}
