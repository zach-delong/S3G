using System.Collections.Generic;
using Moq;
using StaticSiteGenerator.FileManipulation;

namespace StaticSiteGenerator.UnitTests.Doubles.FileManipulation
{
    public class FileIteratorMockFactory
    {
        public static Mock<FileIterator> Get(IEnumerable<string> result)
        {
            var fileIteratorMock = new Mock<FileIterator>();

            fileIteratorMock
                .Setup(i => i.GetFilesInDirectory(It.IsAny<string>()))
                .Returns(result);

            return fileIteratorMock;
        }
    }
}
