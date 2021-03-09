using Microsoft.Extensions.DependencyInjection;
using StaticSiteGenerator.FileManipulation;
using StaticSiteGenerator.HtmlWriting;
using StaticSiteGenerator.Markdown;
using StaticSiteGenerator.MarkdownHtmlConversion;
using StaticSiteGenerator.SiteTemplating;
using StaticSiteGenerator.TemplateSubstitution;
using StaticSiteGenerator.Utilities.Date;
using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace StaticSiteGenerator
{
    public static class ServicesConfiguration
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddServicesWithAttributeOfType<TransientServiceAttribute>();
            services.AddServicesWithAttributeOfType<SingletonServiceAttribute>();

            services.AddFileManipulationServices();
            services.AddYamalConverters();
            services.AddHtmlConverters();
            services.AddMarkdownConverters();
            services.AddMarkdownParsers();
            services.AddTemplateManagement();
            services.AddHtmlWriting();
            services.AddSiteTemplateServices();

            services.AddTransient<IDateParser, DateParser>();
        }
    }
}

