using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StaticSiteGenerator.Markdown.Parser;
using StaticSiteGenerator.Markdown.Renderers;
using static StaticSiteGenerator.Markdown.Renderers.LinkRenderer;

namespace StaticSiteGenerator.Markdown;

public static class ServicesConfiguration
{
    public static void AddMarkdownConverters(this IServiceCollection services)
    {
        services.AddTransient<ICustomRenderer, LiteralRenderer>();
        services.AddTransient<ICustomRenderer, ParagraphRenderer>();
        services.AddTransient<ICustomRenderer, HeaderRenderer>();
        services.AddTransient<BeforeLinkWrite>((sp) =>
        {
            var logger = sp.GetService<ILogger<LiteralRenderer>>();
            return (bool isUrl, string url) => logger.LogDebug($"Is Url: {isUrl}, Url: {url}");
        });
        services.AddTransient<ICustomRenderer, LinkRenderer>();
        services.AddTransient<ICustomRenderer, ListRenderer>();
        services.AddTransient<ICustomRenderer, EmphasisRenderer>();

        services.AddTransient<CustomExtension>();
        services.AddTransient<MarkdownConverter>();
        services.AddTransient<CustomMarkdownPipelineFactory>();
    }

    public static void AddMarkdownParsers(this IServiceCollection services)
    {
        services.AddTransient<IMarkdownFileParser, MarkdownFileParser>();
    }
}
