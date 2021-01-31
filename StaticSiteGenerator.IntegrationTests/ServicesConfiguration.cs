using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using StaticSiteGenerator.FileManipulation;
using StaticSiteGenerator.FileManipulation.FileWriting;

namespace StaticSiteGenerator.IntegrationTests
{
    public static class ServicesConfiguration
    {
        public static void OverrideFileReadingLayerWithDictionary(this IServiceCollection services,
                                                                  IDictionary<string, string> fileDictionary)
        {

            var fileIteratorMock = new Mock<FileIterator>();

            fileIteratorMock
                .Setup(m => m.GetFilesInDirectory(It.IsAny<string>()))
                .Returns<string>(s => fileDictionary.Keys.Where(k => k.Contains(s)));

            var fileReaderMock = new Mock<FileReader>();

            fileReaderMock
                .Setup(m => m.ReadFile(It.IsAny<string>()))
                .Returns<string>(s => fileDictionary[s]);

            services.Remove(services.First(desc => desc.ServiceType == typeof(FileIterator)));
            services.Remove(services.First(desc => desc.ServiceType == typeof(FileReader)));

            services.AddSingleton<FileIterator>(fileIteratorMock.Object);
            services.AddSingleton<FileReader>(fileReaderMock.Object);
        }

        public static Mock<IFileWriter> MockFileWriter(this IServiceCollection services)
        {
            var fileWriterMock = new Mock<IFileWriter>();
            services.Remove(services.First(desc => desc.ServiceType == typeof(IFileWriter)));
            services.AddSingleton<IFileWriter>(fileWriterMock.Object);

            return fileWriterMock;
        }

        public static void MockCliOptions(this IServiceCollection services,
                                          string templateName,
                                          string markdownFileDirectoryName,
                                          string pathToOutput)
        {

            var cliOptions = new CliOptions()
            {
                TemplateName = templateName,
                PathToMarkdownFiles = markdownFileDirectoryName,
                OutputLocation = pathToOutput
            };

            services.AddSingleton(cliOptions);
        }
    }
}
