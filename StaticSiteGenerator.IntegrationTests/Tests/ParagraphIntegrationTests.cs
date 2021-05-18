using Xunit;
using Microsoft.Extensions.DependencyInjection;

namespace StaticSiteGenerator.IntegrationTests.Tests
{
    public class ParagraphIntegrationTests: IntegrationTestBase
    {
        [Fact]
        public void ParagraphShouldParseCorrectly()
        {
            FileSystemCache.Add("templates/template/tag_templates/p.html", "<p>{{}}</p>");
            FileSystemCache.Add("templates/template/site_template.html", "<html>{{}}</html>");
            FileSystemCache.Add("input/file1.md", "This is some text!");

            ServiceProvider.GetService<StaticSiteGenerator>().Start();

            const string expectedContent = @"<html><p>This is some text!</p></html>";
            const string expectedName = "output/file1.html";

            Assert.True(FileSystemCache.ContainsKey(expectedName));
            Assert.Equal(expectedContent, FileSystemCache[expectedName]);
        }
    }
}
