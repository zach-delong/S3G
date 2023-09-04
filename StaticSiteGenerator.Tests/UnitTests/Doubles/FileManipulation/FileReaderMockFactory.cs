using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using NSubstitute;
using StaticSiteGenerator.Files;

namespace StaticSiteGenerator.Tests.UnitTests.Doubles.FileManipulation;

public class FileReaderMockFactory
{
    public static FileReader Get(IDictionary<string, string> fileNameToResults)
    {
        var fileSystem = new MockFileSystem(GetFileDataDictionary(fileNameToResults));

        var mock = Substitute.ForPartsOf<FileReader>(fileSystem);

        return mock;
    }

    private static IDictionary<string, MockFileData> GetFileDataDictionary(IDictionary<string, string> fileNameToResults)
    {
        return fileNameToResults.ToDictionary(d => d.Key, d => new MockFileData(d.Value));
    }
}
