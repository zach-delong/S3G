using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using AutoFixture;

namespace StaticSiteGenerator.Tests.UnitTests.Doubles.FileManipulation;

public static class FilesystemAutoMockerExtensions
{
    public static MockFileSystem MockFileSystem(this IFixture mocker, string[] filePaths)
    {
        var mockFilesystem = new MockFileSystem();

        foreach (var path in filePaths)
        {
	    // If the path ends in a separator, we should assume that
	    // it's a directory
            if (path.EndsWith(mockFilesystem.Path.PathSeparator))
            {
                mockFilesystem.AddDirectory(path);
            }
            else
            {
		// Note: we are mocking a file existence checker: the
		// file contents are entirely unimportant
                mockFilesystem.AddFile(path, new MockFileData(string.Empty));
            }
        }

        mocker.Inject<IFileSystem>(mockFilesystem);

        return mockFilesystem;
    }
}
