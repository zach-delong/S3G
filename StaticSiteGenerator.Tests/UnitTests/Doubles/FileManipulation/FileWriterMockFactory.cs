using System.Collections.Generic;
using StaticSiteGenerator.Files.FileException;
using StaticSiteGenerator.Files.FileWriting;

namespace StaticSiteGenerator.Tests.UnitTests.Doubles.FileManipulation;

public class FileWriterMockFactory
{
    public Mock<IFileWriter> Get()
    {
        return Get(new List<string>());
    }

    public Mock<IFileWriter> Get(IEnumerable<string> existingFileNames)
    {
        var mock = new Mock<IFileWriter>();

        mock.Setup(m => m.WriteFile(It.IsIn(existingFileNames), It.IsAny<string>()))
            .Throws(new FileAlreadyExistsException());

        return mock;
    }
}
