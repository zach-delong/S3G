using Microsoft.Extensions.DependencyInjection;
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
        }
    }
}

