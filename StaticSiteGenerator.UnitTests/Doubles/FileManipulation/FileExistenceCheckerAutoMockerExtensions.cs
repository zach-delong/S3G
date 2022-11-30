using System.IO.Abstractions.TestingHelpers;
using Moq;
using Moq.AutoMock;
using StaticSiteGenerator.Files.FileListing;

namespace StaticSiteGenerator.UnitTests.Doubles.FileManipulation;

public static class FileExistenceCheckerAutoMockerExtensions
{
    public static void MockFileExistenceChecker(this AutoMocker mocker, string[] filePaths)
    {
        var mockFilesystem = new MockFileSystem();

        if (filePaths == null || filePaths.Length > 0)
        {
            mocker.GetMock<FileExistenceChecker>()
                  .Setup(s => s.FileExists(It.IsAny<string>()))
                  .Returns(false);
        }

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

        mocker.Use(new FileExistenceChecker(mockFilesystem));
    }
}
