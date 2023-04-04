using System.IO.Abstractions.TestingHelpers;
using FluentAssertions;

namespace StaticSiteGenerator.IntegrationTests.Utilities.FluentAssertionExtensions;

public class MockFileSystemAssertions
{
    private readonly MockFileSystem FileSystem;

    public MockFileSystemAssertions(MockFileSystem fileSystem)
    {
	FileSystem = fileSystem;
    }

    [CustomAssertion]
    public void ContainFile(
    string path,
    string because = "",
    params object[] becauseArgs)
    {
	FileSystem.FileExists(path).Should().BeTrue(because, becauseArgs);
    }

    [CustomAssertion]
    public void NotContainFile(
    string path,
    string because = "",
    params object[] becauseArgs)
    {
        FileSystem.FileExists(path).Should().BeFalse(because, becauseArgs);
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
           .ContainFile(path);

	FileSystem.Should()
	  .FileHasContents(path,
                    expectedContents,
                    becauseReasons,
                    becauseArgs);
    }
}
