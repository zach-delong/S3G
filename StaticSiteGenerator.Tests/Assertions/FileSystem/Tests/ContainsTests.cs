using System;
using System.IO.Abstractions.TestingHelpers;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace StaticSiteGenerator.Tests.Assertions.FileSystem.Tests;

public class ContainsTests
{
    [Fact]
    public void file_exists_contains_should_pass()
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
    public void file_does_not_exist_should_fail()
    {
        var mockFileSystem = new MockFileSystem();

        mockFileSystem
	    .AddFile(
		"myFile.txt",
		new MockFileData("this is a test file."));

        mockFileSystem
            .Invoking(fileSystem => fileSystem.Should().Contain("notMyFile.txt"))
	    .Should().Throw<Exception>()
	    .WithMessage("""Expected fileSystem to contain "notMyFile.txt", *""");
    }

    [Fact]
    public void null_path_should_fail()
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
    public void empty_path_should_fail()
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
    public void providing_custom_context_should_appear_in_message()
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
	    .WithMessage("""Expected file system to contain "notMyFile.txt", *""");
    }

    [Fact]
    public void providing_custom_because_and_args_should_appear_in_message()
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
	    .WithMessage("""Expected fileSystem to contain "notMyFile.txt" because I need it to 13 hi, *""");
    }
}
