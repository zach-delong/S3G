using StaticSiteGenerator.FileManipulation;
using Microsoft.Extensions.DependencyInjection;
using StaticSiteGenerator;
using StaticSiteGenerator.Markdown;

public static class ServicesConfiguration
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddTransient<StaticSiteGenerator.StaticSiteGenerator, StaticSiteGenerator.StaticSiteGenerator>();
    }

    public static void AddFileManipulationServices(this IServiceCollection services)
    {
        services.AddSingleton<FileIterator, FileIterator>();
        services.AddTransient<FileReader, FileReader>();
    }

    public static void AddMarkdownServices(this IServiceCollection services)
    {
        services.AddTransient<MarkdownFileParser, MarkdownFileParser>();
        services.AddTransient<MarkdownParser, MarkdownParser>();
    }
}
