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
                {"input/file1.md", "# This is some text!" },
            };

            var services = new ServiceCollection();
            services.AddCustomServices();
            services.OverrideFileReadingLayerWithDictionary(fileDictionary);
            var mockedFileWriter = services.MockFileWriter();
            services.MockCliOptions("template", "input", "output");


            var sp = services.BuildServiceProvider();

            sp.GetService<StaticSiteGenerator>().Start();

            mockedFileWriter
                .Verify(m => m.WriteFile("output/file1.html", "<h1>This is some text!</h1>"));
        }

        [Fact]
        public void ParagraphShoulsParseCorrectly()
        {
            // A dictionary mapping file paths to contents
            var fileDictionary = new Dictionary<string, string>()
            {
                {"templates/template/tag_templates/p.html", "<p>{{}}</p>"},
                {"input/file1.md", "This is some text!" },
            };

            var services = new ServiceCollection();
            services.AddCustomServices();
            services.OverrideFileReadingLayerWithDictionary(fileDictionary);
            var mockedFileWriter = services.MockFileWriter();
            services.MockCliOptions("template", "input", "output");


            var sp = services.BuildServiceProvider();

            sp.GetService<StaticSiteGenerator>().Start();

            mockedFileWriter
                .Verify(m => m.WriteFile("output/file1.html", "<p>This is some text!</p>"));
        }
    }
}
