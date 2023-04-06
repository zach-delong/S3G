using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using FluentAssertions;
using StaticSiteGenerator.Files;
using StaticSiteGenerator.Files.FileListing;
using StaticSiteGenerator.Tests.Assertions;
using StaticSiteGenerator.UnitTests.Helpers;
using StaticSiteGenerator.UnitTests.Helpers.TemporaryFiles;
using Xunit;

namespace StaticSiteGenerator.UnitTests.Files;

public class DeferredExecutionDirectoryEnumeratorTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(3)]
    public void Test_Files(int numberOfTestFiles)
    {
        var path = "tmp";
	var mockFileSystem = new MockFileSystem();
	mockFileSystem.AddDirectory(path);

	var filePaths = Enumerable
	    .Range(1, numberOfTestFiles)
	    .Select(_ => $"{path}/{Guid.NewGuid()}.txt")
	    .ToArray();

	foreach (var filePath in filePaths)
	    mockFileSystem.AddFile(filePath, new MockFileData(string.Empty));

	var sut = new DeferredExecutionDirectoryEnumerator(mockFileSystem);

        mockFileSystem
	    .Must()
	    .HaveFileCount(numberOfTestFiles);
    }

    [Theory]
    [MemberData(nameof(TestCaseData))]
    public void ListAllContentsTests(
        MockFileSystem fileSystem,
        string pathToExamine,
        int expectedFiles,
        int expectedFolders,
        int expectedMarkdownFiles)
    {
        var sut = new DeferredExecutionDirectoryEnumerator(fileSystem);

        var objects = sut.ListAllContents(pathToExamine);

        var files = objects.Count(o => o.GetType() == typeof(FileFileSystemObject));
        var folders = objects.Count(o => o.GetType() == typeof(FolderFileSystemObject));
        var markdownFiles = objects.Count(o => o.GetType() == typeof(MarkdownFileSystemObject));

        Assert.Equal(expectedFiles, files);
        Assert.Equal(expectedFolders, folders);
        Assert.Equal(expectedMarkdownFiles, markdownFiles);
    }

    public static IEnumerable<object[]> TestCaseData
    {
        get
        {
            var mock = new MockFileSystem();

            mock.AddDirectory("foo");
            mock.AddFile("bar.txt", new MockFileData("bar!"));

            yield return new object[] { mock, "./", 1, 2 /* note, we always have a /temp directory */, 0 };

            var mockWithSubfolders = new MockFileSystem();

            mockWithSubfolders.AddDirectory("foo");
            mockWithSubfolders.AddDirectory("foo/bar");
            mockWithSubfolders.AddFile("foo/bar/file1.txt", new MockFileData("this is only a test"));
            mockWithSubfolders.AddFile("foo/file2.txt", new MockFileData("This is also only a test"));

            yield return new object[] { mockWithSubfolders, "./", 2, 3, 0 };

            var mockWithMarkdownFiles = new MockFileSystem();

            mockWithMarkdownFiles.AddFile("a.md", "#Heading1");
            mockWithMarkdownFiles.AddFile("b.md", "#Heading1");

            yield return new object[] { mockWithMarkdownFiles, "./", 0, 1, 2 };
        }
    }
}
