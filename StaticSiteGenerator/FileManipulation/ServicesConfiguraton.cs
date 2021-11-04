using System.IO.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using StaticSiteGenerator.FileManipulation.FileListing;
using StaticSiteGenerator.FileManipulation.FileWriting;

namespace StaticSiteGenerator.FileManipulation
{
    public static class ServicesConfiguration
    {
        public static void AddFileManipulationServices(this IServiceCollection services)
        {
            services.AddTransient<IFileSystem, FileSystem>();
            services.AddTransient<IFileWriter, OverwritingFileWriter>();

            services.AddTransient<IDirectoryEnumerator, DeferredExecutionDirectoryEnumerator>();

            services.AddTransient<FileReader>();
        }
    }
}
