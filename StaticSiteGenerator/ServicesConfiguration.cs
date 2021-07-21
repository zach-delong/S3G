using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder
                    .SetMinimumLevel(LogLevel.Trace)
                    .AddFile($"{Path.GetTempPath()}{Path.DirectorySeparatorChar}s3g-log{Path.DirectorySeparatorChar}log.txt", append: true);
            });
        }
    }
}

