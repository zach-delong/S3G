using System.IO.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using StaticSiteGenerator.Files.FileListing;
using StaticSiteGenerator.Files.FileProcessingStrategies;
using StaticSiteGenerator.Files.FileWriting;
using StaticSiteGenerator.Utilities.StrategyPattern;

namespace StaticSiteGenerator.Files;

public static class ServicesConfiguration
{
    public static void AddFileManipulationServices(this IServiceCollection services)
    {
        services.AddTransient<IFileSystem, FileSystem>();
        services.AddTransient<IFileWriter, OverwritingFileWriter>();

        services.AddTransient<IDirectoryEnumerator, DeferredExecutionDirectoryEnumerator>();
        services.AddTransient<FileExistenceChecker>();

        services.AddTransient<FileReader>();

        services.AddTransient<IStrategy<object, IFileSystemObject>, FileProcessingStrategy>();
        services.AddTransient<IStrategy<object, IFileSystemObject>, FolderProcessingStrategy>();
        services.AddTransient<IStrategy<object, IFileSystemObject>, MarkdownFileProcessingStrategy>();
    }
}
