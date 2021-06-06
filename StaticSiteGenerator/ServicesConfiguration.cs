using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using StaticSiteGenerator.FileManipulation;
using StaticSiteGenerator.HtmlWriting;
using StaticSiteGenerator.Markdown;
using StaticSiteGenerator.MarkdownHtmlConversion;
using StaticSiteGenerator.SiteTemplating;
using StaticSiteGenerator.TemplateSubstitution;
using StaticSiteGenerator.Utilities;
using StaticSiteGenerator.Utilities.Date;

namespace StaticSiteGenerator
{
    public static class ServicesConfiguration
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddFileManipulationServices();
            services.AddYamalConverters();
            services.AddHtmlConverters();
            services.AddMarkdownConverters();
            services.AddMarkdownParsers();
            services.AddTemplateManagement();
            services.AddHtmlWriting();
            services.AddSiteTemplateServices();

            services.AddUtilities();
            services.AddTransient<IDateParser, DateParser>();
            services.AddTransient<StaticSiteGenerator>();

            services.AddLogging();
        }

        public static void AddLogging(this IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                // TODO: This appears to be the only way to specify the minimum level 
                // It should probably respect the MS format (below)
                .MinimumLevel.Verbose() 
                .Enrich.FromLogContext()
                .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddSerilog()
                    .SetMinimumLevel(LogLevel.Trace); // TODO Serilog appears to ignore this.
            });
        }
    }
}

