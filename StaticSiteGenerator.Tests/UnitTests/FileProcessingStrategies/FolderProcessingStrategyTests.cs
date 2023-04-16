using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using StaticSiteGenerator.FileProcessingStrategies;
using StaticSiteGenerator.Files;
using StaticSiteGenerator.Tests.Assertions.FileSystem;
using StaticSiteGenerator.Tests.UnitTests.Utilities.Extensions;
using Xunit;

namespace StaticSiteGenerator.Tests.UnitTests.FileProcessingStrategies;

public class FolderProcessingStrategyTests
{
    [Theory]
    [MemberData(nameof(TestCaseData))]
    public void Test(
        MockFileSystem fs,
        string outputLocation,
        string inputLocation,
        string[] targetFolderNames,
        string pathToCheck)
    {
        var options = new CliOptions
        {
            OutputLocation = outputLocation,
            PathToMarkdownFiles = inputLocation
        };

        var sut = new FolderProcessingStrategy(fs, options);

        foreach (var folder in targetFolderNames)
        {
            sut.Execute(new FolderFileSystemObject(folder));
        }

        fs.Should().Contain(pathToCheck);
    }

    public static IEnumerable<object[]> TestCaseData
    {
        get
        {
            var system1 = new MockFileSystem();

            system1.Directory.SetCurrentDirectory(system1.Path.GetSystemRoot());
            system1.AddDirectory("outputLocation");
            system1.AddDirectory(system1.Path.Combine("inputLocation", "newFolder"));

            // Relative paths all the way down
            yield return new object[]
            {
                    system1,
                    "outputLocation",
                    "inputLocation",
                    new string[] { "inputLocation/newFolder" },
                    $"{system1.Path.GetSystemRoot()}{system1.Path.Join("outputLocation", "newFolder")}"
            };

            var system2 = new MockFileSystem();

            system2.AddDirectory("workspace");
            system2.AddDirectory(system2.Path.Combine("workspace", "input"));
            system2.AddDirectory(system2.Path.Combine("workspace", "input", "target"));
            system2.AddDirectory(system2.Path.Combine("workspace", "output"));
            system2.Directory.SetCurrentDirectory(system2.Path.Combine(system2.Path.GetSystemRoot(), "workspace"));

            // Relative path in a subfolder
            yield return new object[]
            {
                    system2,
                    "output",
                    "input",
                    new string[] { "input/target" },
                    system2.Path.Combine(system2.Path.GetSystemRoot(), "workspace", "output", "target")
            };

            var system3 = new MockFileSystem();
            system3.AddDirectory("workspace");
            system3.AddDirectory(system3.Path.Combine("workspace", "input"));
            system3.AddDirectory(system3.Path.Combine("workspace", "output"));
            system3.Directory.SetCurrentDirectory(system3.Path.Combine(system3.Path.GetSystemRoot(), "workspace"));

            // Note the absolute path for the target directory!
            yield return new object[]
            {
                    system3,
                    "/workspace/output",
                    "/workspace/input",
                    new string[] { "input/target" },
                    system3.Path.Combine(system3.Path.GetSystemRoot(), "workspace", "output", "target")
            };
        }
    }
}
