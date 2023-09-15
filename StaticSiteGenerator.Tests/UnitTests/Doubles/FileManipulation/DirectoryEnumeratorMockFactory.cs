using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using NSubstitute.Extensions;
using StaticSiteGenerator.Files.FileListing;

namespace StaticSiteGenerator.Tests.UnitTests.Doubles.FileManipulation;

public class DirectoryEnumeratorMockFactory
{
    public static IDirectoryEnumerator Get(IEnumerable<string> result)
    {
        var fileIteratorMock = Substitute.For<IDirectoryEnumerator>();

        fileIteratorMock
	    .Configure()
            .GetFiles(Arg.Any<string>(), Arg.Any<string>())
            .Returns((input) =>
            {
                var path = (string)input[0];
                var pattern = (string)input[1];
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
