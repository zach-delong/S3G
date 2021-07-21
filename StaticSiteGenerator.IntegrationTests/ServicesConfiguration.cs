using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using StaticSiteGenerator.FileManipulation;
using StaticSiteGenerator.FileManipulation.FileListing;
using StaticSiteGenerator.FileManipulation.FileWriting;

namespace StaticSiteGenerator.IntegrationTests
{
    public static class ServicesConfiguration
    {
        public static void OverrideFileReadingLayerWithDictionary(this IServiceCollection services,
                                                                  IDictionary<string, string> fileDictionary)
        {

            var fileIteratorMock = new Mock<IDirectoryEnumerator>();

            fileIteratorMock
                .Setup(m => m.GetFiles(It.IsAny<string>(), It.IsAny<string>()))
                .Returns<string, string>(( path, _ ) => fileDictionary.Keys.Where(k => k.StartsWith(path)));

            var fileReaderMock = new Mock<FileReader>();

            fileReaderMock
                .Setup(m => m.ReadFile(It.IsAny<string>()))
                .Returns<string>(s => fileDictionary[s]);

            services.Remove(services.First(desc => desc.ServiceType == typeof(IDirectoryEnumerator)));
            services.Remove(services.First(desc => desc.ServiceType == typeof(FileReader)));

            services.AddSingleton<IDirectoryEnumerator>(fileIteratorMock.Object);
            services.AddSingleton<FileReader>(fileReaderMock.Object);

            services.AddLogging(builder => builder.ClearProviders());
        }

        public static Mock<IFileWriter> MockFileWriter(this IServiceCollection services, IDictionary<string, string> fileCache)
        {
            var fileWriterMock = new Mock<IFileWriter>();

            fileWriterMock.Setup(fw => fw.WriteFile(It.IsAny<string>(), It.IsAny<string>()))
                .Callback((string name, string content) => fileCache[name] = content);

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
