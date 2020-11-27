using Microsoft.Extensions.DependencyInjection;
using StaticSiteGenerator.Markdown;
using StaticSiteGenerator.TemplateSubstitution;

using TanvirArjel.Extensions.Microsoft.DependencyInjection;

public static class ServicesConfiguration
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddServicesWithAttributeOfType<TransientServiceAttribute>();
        services.AddServicesWithAttributeOfType<SingletonServiceAttribute>();

        services.AddMarkdownConverters();
        services.AddMarkdownParsers();
        services.AddHtmlConverters();
    }
}
