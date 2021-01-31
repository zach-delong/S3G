using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using StaticSiteGenerator.FileManipulation;

namespace StaticSiteGenerator.IntegrationTests
{
    public static class ServicesConfiguration
    {
        public static void OverrideFileReadingLayerWithDictionary(
            this IServiceCollection services,
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
    }
}
