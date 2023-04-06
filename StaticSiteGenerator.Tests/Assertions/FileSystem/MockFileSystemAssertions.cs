using System.IO.Abstractions.TestingHelpers;
using FluentAssertions;

namespace StaticSiteGenerator.Tests.Assertions.FileSystem;

public class MockFileSystemAssertions
{
    private readonly MockFileSystem FileSystem;

    public MockFileSystemAssertions(MockFileSystem fileSystem)
    {
	FileSystem = fileSystem;
    }

    [CustomAssertion]
    public void Contain(
    string path)
    {
	FileSystem.FileExists(path).Should().BeTrue($"the file system should contain {path} but doesn't");
    }

    [CustomAssertion]
    public void NotContainFile(
    string path)
    {
        FileSystem.FileExists(path).Should().BeFalse($"the file system should not contain {path} but does.");
    }

    [CustomAssertion]
    public void FileHasContents(string path, string expectedContents, string becauseReasons = "", params object[] becauseArgs)
    {
	FileSystem.GetFile(path)
	    .TextContents
	    .Should()
	    .BeEquivalentTo(expectedContents, becauseReasons, becauseArgs);
    }

    [CustomAssertion]
    public void FileExistsWithContents(string path, string expectedContents, string becauseReasons = "", params object[] becauseArgs)
    {
	FileSystem.Should()
           .Contain(path);

	FileSystem.Should()
	  .FileHasContents(path,
                    expectedContents,
                    becauseReasons,
                    becauseArgs);
    }

    [CustomAssertion]
    public void HaveFileCount(int expectedFileCount)
    {
        FileSystem.AllFiles.Should().HaveCount(expectedFileCount);
    }
}
