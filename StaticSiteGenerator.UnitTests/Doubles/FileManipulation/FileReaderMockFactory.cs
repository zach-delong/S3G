using System.Collections.Generic;
using Moq;
using StaticSiteGenerator.FileManipulation;

namespace StaticSiteGenerator.UnitTests.Doubles.FileManipulation
{
    public class FileReaderMockFactory
    {
        public static Mock<FileReader> Get(IDictionary<string, string> fileNameToResults)
        {
            var mock = new Mock<FileReader>();

            foreach (var entry in fileNameToResults)
            {
                mock.Setup(m => m.ReadFile(entry.Key))
                    .Returns(entry.Value);
            }

            return mock;
        }
    }
}
