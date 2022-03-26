using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StaticSiteGenerator.Files;
using StaticSiteGenerator.HtmlWriting;
using StaticSiteGenerator.Markdown;
using StaticSiteGenerator.SiteTemplating;
using StaticSiteGenerator.TemplateSubstitution;
using StaticSiteGenerator.Utilities;
using StaticSiteGenerator.Utilities.Date;
using static StaticSiteGenerator.Generator;

namespace StaticSiteGenerator;

public static class ServicesConfiguration
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddFileManipulationServices();
        services.AddMarkdownConverters();
        services.AddMarkdownParsers();
        services.AddTemplateManagement();
        services.AddHtmlWriting();
        services.AddSiteTemplateServices();

        services.AddUtilities();
        services.AddTransient<IDateParser, DateParser>();

        services.AddLogging();
        services.AddGenerator();
    }

    public static void AddLogging(this IServiceCollection services)
    {
        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder
                .SetMinimumLevel(LogLevel.Debug)
                .AddFile($"{Path.GetTempPath()}{Path.DirectorySeparatorChar}s3g-log{Path.DirectorySeparatorChar}log.txt", append: true)
                .SetMinimumLevel(LogLevel.Information)
                .AddConsole();
        });
    }

    public static void AddGenerator(this IServiceCollection services)
    {
        services.AddTransient<Generator>();

        services.AddTransient<OnSiteStart>((sp) =>
        {
            var logger = sp.GetService<ILogger<Generator>>();
            return () => logger.LogInformation("Starting site conversion");
        });

        services.AddTransient<OnSiteDone>((sp) => {
            var logger = sp.GetService<ILogger<Generator>>();
            return () => logger.LogInformation("Done converting site");
        });
    }
}

