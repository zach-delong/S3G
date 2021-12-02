using Xunit;
using Microsoft.Extensions.DependencyInjection;
using System.IO.Abstractions.TestingHelpers;
using StaticSiteGenerator.IntegrationTests.Utilities;

namespace StaticSiteGenerator.IntegrationTests.Tests
{
    public class ParagraphIntegrationTests: IntegrationTestBase
    {
        [Fact]
        public void ParagraphShouldParseCorrectly()
        {
            FileSystemCache.AddFile("templates/template/tag_templates/p.html", new MockFileData("<p>{{}}</p>"));
            FileSystemCache.AddFile("templates/template/site_template.html", new MockFileData("<html>{{}}</html>"));
            FileSystemCache.AddDirectory("output");
            FileSystemCache.AddFile("input/file1.md", new MockFileData("This is some text!"));

            ServiceProvider.GetService<Generator>().Start();

            const string expectedContent = @"<html><p>This is some text!</p></html>";
            const string expectedName = "/output/file1.html";

            Assert.True(this.FileExists(expectedName));
            Assert.Equal(expectedContent, this.ReadFileContents(expectedName));
        }
    }
}
