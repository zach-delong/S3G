using System;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using Moq;
using StaticSiteGenerator.FileManipulation;

namespace StaticSiteGenerator.UnitTests.Doubles.FileManipulation
{
    public class FileReaderMockFactory
    {
        public static Mock<FileReader> Get(IDictionary<string, string> fileNameToResults)
        {
            var cache = GetFileDataDictionary(fileNameToResults);

            var fileSystem = new MockFileSystem(cache);

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
            var cache = new Dictionary<string, MockFileData>();
            foreach (var kvp in fileNameToResults)
            {
                cache.Add(kvp.Key, new MockFileData(kvp.Value));
            }

            return cache;
        }
    }
}
