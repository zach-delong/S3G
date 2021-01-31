using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using StaticSiteGenerator.FileManipulation;
using StaticSiteGenerator.FileManipulation.FileWriting;
using Xunit;

namespace StaticSiteGenerator.UnitTests
{
    public class IntegrationTest
    {
        [Fact]
        public void foo() {
            // A dictionary mapping file paths to contents
            var fileDictionary = new Dictionary<string, string>()
            {
                {"templates/template/h1.html", "<h1>{{}}</h1>"},
                {"templates/template/p.html", "<p>{{}}</p>"},
                {"input/file1.md", "# This is some text!" },
            };

            var fileIteratorMock = new Mock<FileIterator>();

            fileIteratorMock
                .Setup(m => m.GetFilesInDirectory(It.IsAny<string>()))
                .Returns<string>(s => fileDictionary.Keys.Where(k => k.Contains(s)));

            var fileReaderMock = new Mock<FileReader>();

            fileReaderMock
                .Setup(m => m.ReadFile(It.IsAny<String>()))
                .Returns<string>(s => fileDictionary[s]);

            var fileWriterMock = new Mock<IFileWriter>();

            var services = new ServiceCollection();
            services.AddCustomServices();

            var cliOptions = new CliOptions()
            {
                TemplateName = "template",
                PathToMarkdownFiles = "input",
                OutputLocation = "output"
            };

            services.AddSingleton(cliOptions);


            services.Remove(services.First(desc => desc.ServiceType == typeof(FileIterator)));
            services.Remove(services.First(desc => desc.ServiceType == typeof(FileReader)));
            services.Remove(services.First(desc => desc.ServiceType == typeof(IFileWriter)));

            services.AddSingleton<FileIterator>(fileIteratorMock.Object);
            services.AddSingleton<FileReader>(fileReaderMock.Object);
            services.AddSingleton<IFileWriter>(fileWriterMock.Object);

            var sp = services.BuildServiceProvider();

            sp.GetService<StaticSiteGenerator>().Start();

            fileWriterMock
                .Verify(m => m.WriteFile("output/file1.html", "<h1> This is some text!</h1>"));
        }
    }
}
