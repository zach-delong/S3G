using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using Moq.AutoMock;

namespace StaticSiteGenerator.Tests.UnitTests.Doubles.FileManipulation;

public static class FilesystemAutoMockerExtensions
{
    public static void MockFileSystem(this AutoMocker mocker, string[] filePaths)
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

        mocker.Use<IFileSystem>(mockFilesystem);
    }
}
