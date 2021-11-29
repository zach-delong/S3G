using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using StaticSiteGenerator.Files;
using StaticSiteGenerator.Files.FileProcessingStrategies;
using StaticSiteGenerator.UnitTests.Utilities.Extensions;
using Xunit;

namespace StaticSiteGenerator.UnitTests.Files.FileProcessingStrategies
{
    public class FileProcessingStrategyTests
    {
        [Theory]
        [MemberData(nameof(TestCaseData))]
        public void Test(
            MockFileSystem fs,
            string outputLocation,
            string inputLocation,
            string[] targetFileNames,
            string pathToCheck)
        {
            var options = new CliOptions 
            { 
                OutputLocation = outputLocation,
                PathToMarkdownFiles = inputLocation
            };
            var sut = new FileProcessingStrategy(fs, options);

            foreach(var file in targetFileNames)
            {
                sut.Execute(new StaticSiteGenerator.Files.File(file));
            }

            var success = fs.AllFiles.Any(d => d == pathToCheck);

            Assert.True(success);
        }

        public static IEnumerable<object[]> TestCaseData
        {
            get
            {
                var system1 = new MockFileSystem();
                system1.AddDirectory("outputLocation");
                system1.AddDirectory("inputLocation");
                system1.AddFile(system1.Path.Combine("inputLocation", "newFile.jpg"), new MockFileData("foo"));
                system1.Directory.SetCurrentDirectory(system1.Path.GetSystemRoot());

                // Relative paths all the way down
                yield return new object[]
                {
                    system1,
                    "/outputLocation",
                    "/inputLocation",
                    new string[] { "inputLocation/newFile.jpg" },
                    $"{system1.Path.GetSystemRoot()}{system1.Path.Join("outputLocation", "newFile.jpg")}"
                };

                var system2 = new MockFileSystem();

                system2.AddDirectory("workspace");
                system2.AddDirectory(system2.Path.Combine("workspace", "input"));
                system2.AddFile(system2.Path.Combine("workspace", "input", "target.jpg"), new MockFileData("foo"));
                system2.AddDirectory(system2.Path.Combine("workspace", "output"));
                system2.Directory.SetCurrentDirectory(system2.Path.Combine(system2.Path.GetSystemRoot(), "workspace"));

                // Relative path in a subfolder
                yield return new object[]
                {
                    system2,
                    "output",
                    "input",
                    new string[] { "input/target.jpg" },
                    system2.Path.Combine(system2.Path.GetSystemRoot(), "workspace", "output", "target.jpg")
                };

                var system3 = new MockFileSystem();
                system3.AddDirectory("workspace");
                system3.AddDirectory(system3.Path.Combine("workspace", "input"));
                system3.AddFile(system3.Path.Combine("workspace", "input", "target.jpg"), new MockFileData("foo"));
                system3.AddDirectory(system3.Path.Combine("workspace", "output"));
                system3.Directory.SetCurrentDirectory(system3.Path.Combine(system3.Path.GetSystemRoot(), "workspace"));

                // Note the absolute path for the target directory!
                yield return new object[]
                {
                    system3,
                    "/workspace/output",
                    "/workspace/input",
                    new string[] { "input/target.jpg" },
                    system3.Path.Combine(system3.Path.GetSystemRoot(), "workspace", "output", "target.jpg")
                };
            }
        }
    }
}
