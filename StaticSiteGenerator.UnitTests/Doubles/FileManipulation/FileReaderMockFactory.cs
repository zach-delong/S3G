using System;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using Moq;
using StaticSiteGenerator.FileManipulation;

namespace StaticSiteGenerator.UnitTests.Doubles.FileManipulation
{
    public class FileReaderMockFactory
    {
        public static Mock<FileReader> Get(IDictionary<string, string> fileNameToResults)
        {
            var fileSystem = new MockFileSystem(GetFileDataDictionary(fileNameToResults));

            var mock = new Mock<FileReader>(fileSystem);

            foreach (var entry in fileNameToResults)
            {
                mock.Setup(m => m.ReadFile(It.IsAny<string>()))
                    .Returns((string filePath) => fileSystem.File.ReadAllText(filePath));
            }

            return mock;
        }

        private static IDictionary<string, MockFileData> GetFileDataDictionary(IDictionary<string, string> fileNameToResults)
        {
            return fileNameToResults.ToDictionary(d => d.Key, d => new MockFileData(d.Value));
        }
    }
}
