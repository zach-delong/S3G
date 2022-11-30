using System.IO.Abstractions.TestingHelpers;
using StaticSiteGenerator.Files.FileListing;
using Xunit;

namespace StaticSiteGenerator.UnitTests.Files;

public class FileExistenceCheckerTests
{
    [Fact]
    public void Test()
    {
        var fileSystem = new MockFileSystem();

        fileSystem.AddFile("testPath/file.md", new MockFileData("hello, world"));
        var sut = new FileExistenceChecker(fileSystem);

	var result = sut.FileExists("testPath/file.md");

        Assert.True(result);
    }
}
