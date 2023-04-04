using System.Collections.Generic;
using System.Linq;
using Moq;
using StaticSiteGenerator.Files.FileListing;

namespace StaticSiteGenerator.UnitTests.Doubles.FileManipulation;

public class DirectoryEnumeratorMockFactory
{
    public static Mock<IDirectoryEnumerator> Get(IEnumerable<string> result)
    {
        var fileIteratorMock = new Mock<IDirectoryEnumerator>();

        fileIteratorMock
            .Setup(i => i.GetFiles(It.IsAny<string>(), It.IsAny<string>()))
            .Returns<string, string>((path, pattern) =>
            {
                    // System.Console.WriteLine($"Looking for file path '{path}' and pattern '{pattern}'");

                    // Note: this isn't a perfect analogue for the pattern
                    // parameter, but it should work for the tests that I need
                    // to write
                    var whitelistedPattern = pattern
                    .Replace("*", "")
                    .Replace("?", "");

                    // System.Console.WriteLine($"The cleaned pattern is '{whitelistedPattern}'");

                    var filteredResult = result
                    .Where(s => s.StartsWith(path))
                    .Where(s => s.IndexOf(whitelistedPattern) > 0);

                return filteredResult;
            });

        return fileIteratorMock;
    }
}
