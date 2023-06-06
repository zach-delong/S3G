using System;
using System.IO.Abstractions.TestingHelpers;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace StaticSiteGenerator.Tests.Assertions.FileSystem;

public class MockFileSystemAssertionTests
{
    [Fact]
    public void ContainsShouldPass()
    {
        var mockFileSystem = new MockFileSystem();

        mockFileSystem
	    .AddFile(
		"myFile.txt",
		new MockFileData("this is a test file."));

        mockFileSystem
            .Invoking(s => s.Should().Contain("myFile.txt"))
	    .Should().NotThrow<Exception>();
    }

    [Fact]
    public void ContainsShouldFail()
    {
        var mockFileSystem = new MockFileSystem();

        mockFileSystem
	    .AddFile(
		"myFile.txt",
		new MockFileData("this is a test file."));

        mockFileSystem
            .Invoking(fileSystem => fileSystem.Should().Contain("notMyFile.txt"))
	    .Should().Throw<Exception>()
	    .WithMessage("Expected fileSystem to contain \"notMyFile.txt\", but found {\"/temp\", \"/myFile.txt\"}.");
    }

    [Fact]
    public void ContainsShouldFailOnNull()
    {
        var mockFileSystem = new MockFileSystem();

        mockFileSystem
	    .AddFile(
		"myFile.txt",
		new MockFileData("this is a test file."));

        mockFileSystem
            .Invoking(s => s.Should().Contain(null))
	    .Should().Throw<Exception>()
	    .WithMessage("The input path should not be null or empty");
    }

    [Fact]
    public void ContainsShouldFailOnEmptyString()
    {
        var mockFileSystem = new MockFileSystem();

        mockFileSystem
	    .AddFile(
		"myFile.txt",
		new MockFileData("this is a test file."));

        mockFileSystem
            .Invoking(s => s.Should().Contain(null))
	    .Should().Throw<Exception>()
	    .WithMessage("The input path should not be null or empty");
    }

    [Fact]
    public void PassingParentShouldWork()
    {
        var mockFileSystem = new MockFileSystem();

        mockFileSystem
	    .AddFile(
		"myFile.txt",
		new MockFileData("this is a test file."));

        mockFileSystem
            .Invoking(fileSystem => {
                using (new AssertionScope("file system"))
                {
		    fileSystem.Should().Contain("notMyFile.txt");
                }
            })
	    .Should().Throw<Exception>()
	    .WithMessage("Expected file system to contain \"notMyFile.txt\", but found {\"/temp\", \"/myFile.txt\"}.");
    }

    [Fact]
    public void BecauseShouldWorkWithArgs()
    {
        var mockFileSystem = new MockFileSystem();

        mockFileSystem
	    .AddFile(
		"myFile.txt",
		new MockFileData("this is a test file."));

        mockFileSystem
            .Invoking(fileSystem => {
		fileSystem
		    .Should()
		    .Contain(
			"notMyFile.txt",
			"because I need it to {0} {1}",
			13,
			"hi");
            })
	    .Should().Throw<Exception>()
	    .WithMessage("Expected fileSystem to contain \"notMyFile.txt\" because I need it to 13 hi, but found {\"/temp\", \"/myFile.txt\"}.");
    }
}
