using Microsoft.Extensions.DependencyInjection;

using StaticSiteGenerator.Markdown.Parser;
using StaticSiteGenerator.Markdown.Renderers;

namespace StaticSiteGenerator.Markdown;

public static class ServicesConfiguration
{
    public static void AddMarkdownConverters(this IServiceCollection services)
    {
        services.AddTransient<ICustomRenderer, LiteralRenderer>();
        services.AddTransient<ICustomRenderer, ParagraphRenderer>();
        services.AddTransient<ICustomRenderer, HeaderRenderer>();
        services.AddTransient<ICustomRenderer, LinkRenderer>();

        services.AddTransient<CustomExtension>();
        services.AddTransient<MarkdownConverter>();
        services.AddTransient<CustomMarkdownPipelineFactory>();
    }

    public static void AddMarkdownParsers(this IServiceCollection services)
    {
        services.AddTransient<IMarkdownFileParser, MarkdownFileParser>();
    }
}
