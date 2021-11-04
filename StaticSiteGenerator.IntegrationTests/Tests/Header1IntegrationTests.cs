using Xunit;
using Microsoft.Extensions.DependencyInjection;
using System.IO.Abstractions.TestingHelpers;
using System;
using System.IO.Abstractions;
using StaticSiteGenerator.IntegrationTests.Utilities;

namespace StaticSiteGenerator.IntegrationTests.Tests
{
    public class Header1IntegrationTests : IntegrationTestBase
    {
        [Fact]
        public void Header1ShoulsParseCorrectly()
        {
            FileSystemCache.Add("templates/template/tag_templates/h1.html", new MockFileData("<h1>{{}}</h1>"));
            FileSystemCache.Add("templates/template/tag_templates/p.html", new MockFileData("<p>{{}}</p>"));
            FileSystemCache.Add("templates/template/site_template.html", new MockFileData("<html>{{}}</html>"));
            FileSystemCache.Add("output/.tmp", new MockFileData(""));
            FileSystemCache.Add("input/file1.md", new MockFileData("# This is some text!"));

            ServiceProvider.GetService<StaticSiteGenerator>().Start();

            const string expectedFileContent = @"<html><h1>This is some text!</h1></html>";
            const string expectedFileName = "output/file1.html";

            Assert.True(this.FileExists(expectedFileName));
            Assert.Equal(expectedFileContent, this.ReadFileContents(expectedFileName));
        }
    }
}
