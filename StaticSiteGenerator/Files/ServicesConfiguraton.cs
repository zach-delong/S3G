using System.IO.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using StaticSiteGenerator.Files.FileListing;
using StaticSiteGenerator.Files.FileWriting;

namespace StaticSiteGenerator.Files;

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
