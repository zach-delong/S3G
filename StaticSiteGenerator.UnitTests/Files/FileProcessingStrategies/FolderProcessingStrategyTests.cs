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
    public class FolderProcessingStrategyTests
    {
        [Theory]
        [MemberData(nameof(TestCaseData))]
        public void Test(
            MockFileSystem fs,
            string outputLocation,
            string[] targetFolderNames,
            string pathToCheck)
        {
            var options = new CliOptions { OutputLocation = outputLocation };
            var sut = new FolderProcessingStrategy(fs, options);

            foreach(var folder in targetFolderNames)
            {
                sut.Execute(new Folder(folder));
            }

            var success = fs.AllDirectories.Any(d => d == pathToCheck);

            Assert.True(success);
        }

        public static IEnumerable<object[]> TestCaseData
        {
            get
            {
                var system1 = new MockFileSystem();

                system1.Directory.SetCurrentDirectory(system1.Path.GetSystemRoot());

                // Relative paths all the way down
                yield return new object[]
                {
                    system1,
                    "outputLocation",
                    new string[] { "newFolder" },
                    $"{system1.Path.GetSystemRoot()}{system1.Path.Join("outputLocation", "newFolder")}"
                };

                var system2 = new MockFileSystem();

                system2.Directory.CreateDirectory("workspace");
                system2.Directory.CreateDirectory(system2.Path.Combine("workspace", "output"));
                system2.Directory.SetCurrentDirectory(system2.Path.Combine(system2.Path.GetSystemRoot(), "workspace"));

                // Relative path in a subfolder
                yield return new object[]
                {
                    system2,
                    "output",
                    new string[] { "target" },
                    system2.Path.Combine(system2.Path.GetSystemRoot(), "workspace", "output", "target")
                };

                var system3 = new MockFileSystem();
                system3.Directory.CreateDirectory("workspace");
                system3.Directory.CreateDirectory(system3.Path.Combine("workspace", "output"));
                system3.Directory.SetCurrentDirectory(system3.Path.Combine(system3.Path.GetSystemRoot(), "workspace"));

                // Note the absolute path for the target directory!
                yield return new object[]
                {
                    system3,
                    "/workspace/output",
                    new string[] { "target" },
                    system3.Path.Combine(system3.Path.GetSystemRoot(), "workspace", "output", "target")
                };
            }
        }
    }
}
