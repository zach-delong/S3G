using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace StaticSiteGenerator.Tests.Assertions;

public class MockFileSystemAssertions : ReferenceTypeAssertions<MockFileSystem, MockFileSystemAssertions>
{
    protected override string Identifier => "file system";

    public MockFileSystemAssertions(MockFileSystem fileSystem) : base(fileSystem)
    { }

    [CustomAssertion]
    public AndConstraint<MockFileSystemAssertions> Contain(
	string path, string because="", params object[] args)
    {
        Execute.Assertion
            .BecauseOf(because, args)
            .ForCondition(!string.IsNullOrWhiteSpace(path))
	    .FailWith("The input path should not be null or empty")
	    .Then
	    .Given(() => Subject.AllNodes)
	    .ForCondition(paths => paths.Any(p => p.Contains(path)))
	    .FailWith("Expected {context:file system} to contain {0}{path}, but found {1}.", _ => path, paths => paths);


        return new AndConstraint<MockFileSystemAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<MockFileSystemAssertions> ContainDirectory(string path)
    {
	Subject.AllDirectories
	    .Should()
	    .Contain(path, $"the file system should contain {path} but doesn't");

        return new AndConstraint<MockFileSystemAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<MockFileSystemAssertions> NotContainFile(
    string path)
    {
        Subject.FileExists(path).Should().BeFalse($"the file system should not contain {path} but does.");

        return new AndConstraint<MockFileSystemAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<MockFileSystemAssertions> FileHasContents(string path, string expectedContents, string becauseReasons = "", params object[] becauseArgs)
    {
	Subject.GetFile(path)
	    ?.TextContents
	    ?.Should()
	    .NotBeNull()
	    .And
	    .BeEquivalentTo(expectedContents, becauseReasons, becauseArgs);

        return new AndConstraint<MockFileSystemAssertions>(this);
    }

    [CustomAssertion]
    public AndConstraint<MockFileSystemAssertions> FileExistsWithContents(string path, string expectedContents, string becauseReasons = "", params object[] becauseArgs)
    {
        return Contain(path)
	    .And.FileHasContents(path, expectedContents, becauseReasons, becauseArgs);
    }

    [CustomAssertion]
    public AndConstraint<MockFileSystemAssertions> HaveFileCount(int expectedFileCount)
    {
        Subject.AllFiles.Should().HaveCount(expectedFileCount);
        return new AndConstraint<MockFileSystemAssertions>(this);
    }
}
