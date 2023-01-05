using Microsoft.Extensions.DependencyInjection;
using StaticSiteGenerator.Files;
using StaticSiteGenerator.Utilities.StrategyPattern;

namespace StaticSiteGenerator.FileProcessingStrategies;

public static class ServicesConfiguration
{
    public static void AddFileProcessingStrategies(this IServiceCollection services)
    {
        services.AddTransient<IStrategy<object, IFileSystemObject>, FileProcessingStrategy>();
        services.AddTransient<IStrategy<object, IFileSystemObject>, FolderProcessingStrategy>();
        services.AddTransient<IStrategy<object, IFileSystemObject>, MarkdownFileProcessingStrategy>();
    }
}
