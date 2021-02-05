using Microsoft.Extensions.DependencyInjection;
using StaticSiteGenerator.FileManipulation;
using StaticSiteGenerator.HtmlWriting;
using StaticSiteGenerator.Markdown;
using StaticSiteGenerator.MarkdownHtmlConversion;
using StaticSiteGenerator.SiteTemplating;
using StaticSiteGenerator.TemplateSubstitution;

using TanvirArjel.Extensions.Microsoft.DependencyInjection;

public static class ServicesConfiguration
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddServicesWithAttributeOfType<TransientServiceAttribute>();
        services.AddServicesWithAttributeOfType<SingletonServiceAttribute>();

        services.AddFileManipulationServices();
        services.AddHtmlConverters();
        services.AddMarkdownConverters();
        services.AddMarkdownParsers();
        services.AddTemplateManagement();
        services.AddHtmlWriting();
        services.AddSiteTemplateServices();
    }
}
