using Xunit;
using Microsoft.Extensions.DependencyInjection;

namespace StaticSiteGenerator.IntegrationTests.Tests
{
    public class Header1IntegrationTests : IntegrationTestBase
    {
        [Fact]
        public void Header1ShoulsParseCorrectly()
        {
            FileSystemCache.Add("templates/template/tag_templates/h1.html", "<h1>{{}}</h1>");
            FileSystemCache.Add("templates/template/tag_templates/p.html", "<p>{{}}</p>");
            FileSystemCache.Add("templates/template/site_template.html", "<html>{{}}</html>");
            FileSystemCache.Add("input/file1.md", "# This is some text!");

            ServiceProvider.GetService<StaticSiteGenerator>().Start();

            const string expectedFileContent = @"<html><h1>This is some text!</h1></html>";
            const string expectedFileName = "output/file1.html";

            Assert.True(FileSystemCache.ContainsKey(expectedFileName));
            Assert.Equal(expectedFileContent, FileSystemCache[expectedFileName]);
        }
    }
}
