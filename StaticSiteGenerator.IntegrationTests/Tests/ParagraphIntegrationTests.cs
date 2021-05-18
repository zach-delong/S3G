using Xunit;
using Microsoft.Extensions.DependencyInjection;

namespace StaticSiteGenerator.IntegrationTests.Tests
{
    public class ParagraphIntegrationTests: IntegrationTestBase
    {
        [Fact]
        public void ParagraphShouldParseCorrectly()
        {
            InputFileSystem.Add("templates/template/tag_templates/p.html", "<p>{{}}</p>");
            InputFileSystem.Add("templates/template/site_template.html", "<html>{{}}</html>");
            InputFileSystem.Add("input/file1.md", "This is some text!");

            ServiceProvider.GetService<StaticSiteGenerator>().Start();

            const string expectedContent = @"<html><p>This is some text!</p></html>";
            const string expectedName = "output/file1.html";

            Assert.True(FileSystemWritingCache.ContainsKey(expectedName));
            Assert.Equal(expectedContent, FileSystemWritingCache[expectedName]);
        }
    }
}
