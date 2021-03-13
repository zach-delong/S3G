using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using StaticSiteGenerator.FileManipulation.FileWriting;
using Xunit;

namespace StaticSiteGenerator.IntegrationTests
{
    public class IntegrationTest
    {
        [Fact]
        public void Header1ShoulsParseCorrectly() {
            // A dictionary mapping file paths to contents
            var fileDictionary = new Dictionary<string, string>()
            {
                {"templates/template/tag_templates/h1.html", "<h1>{{}}</h1>"},
                {"templates/template/tag_templates/p.html", "<p>{{}}</p>"},
                {"templates/template/site_template.html", "<html>{{}}</html>"},
                {"input/file1.md", "# This is some text!" },
            };

            var services = new ServiceCollection();
            services.AddCustomServices();
            services.OverrideFileReadingLayerWithDictionary(fileDictionary);
            var mockedFileWriter = services.MockFileWriter();
            services.MockCliOptions("template", "input", "output");


            var sp = services.BuildServiceProvider();

            sp.GetService<StaticSiteGenerator>().Start();

            var expectedContent = @"<html><h1>This is some text!</h1></html>";

            mockedFileWriter
                .Verify(m => m.WriteFile("output/file1.html", expectedContent));
        }

        [Fact]
        public void ParagraphShoulsParseCorrectly()
        {
            // A dictionary mapping file paths to contents
            var fileDictionary = new Dictionary<string, string>()
            {
                {"templates/template/tag_templates/p.html", "<p>{{}}</p>"},
                {"templates/template/site_template.html", "<html>{{}}</html>"},
                {"input/file1.md", "This is some text!" },
            };

            var services = new ServiceCollection();
            services.AddCustomServices();
            services.OverrideFileReadingLayerWithDictionary(fileDictionary);
            var mockedFileWriter = services.MockFileWriter();
            services.MockCliOptions("template", "input", "output");


            var sp = services.BuildServiceProvider();

            sp.GetService<StaticSiteGenerator>().Start();

            var expectedContent = @"<html><p>This is some text!</p></html>";
            mockedFileWriter
                .Verify(m => m.WriteFile("output/file1.html", expectedContent));
        }

        [Fact]
        public void YamlMetadataShouldParseCorrectly()
        {
            // Notice that the --- is the first thing on the line. It must be so
            // for the library to work.
            var yamlFile = @"---
publish_date: ""12/31/2020""
---

This is some text!";
            // A dictionary mapping file paths to contents
            var fileDictionary = new Dictionary<string, string>()
            {
                {"templates/template/tag_templates/p.html", "<p>{{}}</p>"},
                {"templates/template/site_template.html", "<html>{{}}</html>"},
                {"input/file1.md", yamlFile },
            };

            var services = new ServiceCollection();
            services.AddCustomServices();
            services.OverrideFileReadingLayerWithDictionary(fileDictionary);
            var mockedFileWriter = services.MockFileWriter();
            services.MockCliOptions("template", "input", "output");


            var sp = services.BuildServiceProvider();

            sp.GetService<StaticSiteGenerator>().Start();

            var expectedContent = @"<html><p>This is some text!</p></html>";
            mockedFileWriter
                .Verify(m => m.WriteFile("output/file1.html", expectedContent));
        }
    }
}
