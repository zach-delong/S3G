using System;
using System.IO.Abstractions.TestingHelpers;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace StaticSiteGenerator.Tests.Assertions.FileSystem.Tests;

public class ContainDirectoryTests 
{
    [Fact]
    public void folder_exists_contains_should_pass()
    {
        var mockFileSystem = new MockFileSystem();

        mockFileSystem
	    .AddDirectory("example_directory");

        mockFileSystem
            .Invoking(s => s.Should().ContainDirectory("example_directory"))
	    .Should().NotThrow<Exception>();
    }

    [Fact]
    public void sub_folder_does_not_exist_contains_should_fail()
    {
        var mockFileSystem = new MockFileSystem();

        mockFileSystem
	    .AddDirectory("example_directory");


        var targetFolderAbsolutePath = mockFileSystem.Path.GetFullPath(
	    mockFileSystem.Path.Combine("example_directory", "target_directory"));

        mockFileSystem
            .Invoking(s => s.Should().ContainDirectory(targetFolderAbsolutePath))
	    .Should().Throw<Exception>();
    }

    [Fact]
    public void folder_does_not_exist_should_fail()
    {
        var mockFileSystem = new MockFileSystem();

        mockFileSystem
	    .AddDirectory("example_directory");

        var targetFolderAbsolutePath = mockFileSystem.Path.GetFullPath("target");

        mockFileSystem
            .Invoking(fileSystem => fileSystem.Should().ContainDirectory(targetFolderAbsolutePath))
	    .Should().Throw<Exception>()
	    .WithMessage($"""Expected fileSystem to contain "{targetFolderAbsolutePath}", *""");
    }

    [Fact]
    public void null_path_should_fail()
    {
        var mockFileSystem = new MockFileSystem();

        mockFileSystem.AddDirectory("example_directory");

        mockFileSystem
            .Invoking(s => s.Should().ContainDirectory(null))
	    .Should().Throw<Exception>()
	    .WithMessage("The input path should not be null or empty");
    }

    [Fact]
    public void empty_path_should_fail()
    {
        var mockFileSystem = new MockFileSystem();

        mockFileSystem
	    .AddDirectory("example_directory");

        mockFileSystem
            .Invoking(s => s.Should().ContainDirectory(""))
	    .Should().Throw<Exception>()
	    .WithMessage("The input path should not be null or empty");
    }

    [Fact]
    public void providing_custom_context_should_appear_in_message()
    {
        var mockFileSystem = new MockFileSystem();

        mockFileSystem
	    .AddDirectory("example_directory");

        var targetFolderAbsolutePath = mockFileSystem.Path.GetFullPath("target");

        mockFileSystem
            .Invoking(fileSystem => {
                using (new AssertionScope("file system"))
                {
		    fileSystem.Should().ContainDirectory(targetFolderAbsolutePath);
                }
            })
	    .Should().Throw<Exception>()
	    .WithMessage($"""Expected file system to contain "{targetFolderAbsolutePath}", *""");
    }

    [Fact]
    public void providing_custom_because_and_args_should_appear_in_message()
    {
        var mockFileSystem = new MockFileSystem();

        mockFileSystem
	    .AddDirectory("example_directory");

        mockFileSystem
            .Invoking(fileSystem => {
		fileSystem
		    .Should()
		    .ContainDirectory(
			mockFileSystem.Path.GetFullPath("target"),
			"because I need it to {0} {1}",
			13,
			"hi");
            })
	    .Should().Throw<Exception>()
	    .WithMessage($"""Expected fileSystem to contain "{mockFileSystem.Path.GetFullPath("target")}" because I need it to 13 hi, *""");
    }
}
