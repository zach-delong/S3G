using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Toolkit.Extensions;
using StaticSiteGenerator.Markdown.Parser;
using StaticSiteGenerator.Markdown.Renderers;
using static StaticSiteGenerator.Markdown.DocumentPropertyReader;
using static StaticSiteGenerator.Markdown.MarkdownConverter;
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
            return (string url) => logger.LogDebug($"Writing url to link: {url}");
        });
        services.AddTransient<ICustomRenderer, LinkRenderer>();
        services.AddTransient<ICustomRenderer, ListRenderer>();
        services.AddTransient<ICustomRenderer, EmphasisRenderer>();
        services.AddTransient<ICustomRenderer, CustomCodeBlockRenderer>();

        services.AddTransient<CustomExtension>();
        services.AddTransient<MarkdownConverter>();
        services.AddTransient<OnConversionStart>((sp) =>
        {
            var logger = sp.GetService<ILogger<MarkdownConverter>>();
            return (string markdownString) => logger.LogDebug($"Input markdown: {markdownString.Truncate(100)}...");
        });
        services.AddTransient<OnConversionEnd>((sp) =>
        {
            var logger = sp.GetService<ILogger<MarkdownConverter>>();
            return (string htmlContent) => logger.LogDebug($"Output Html: {htmlContent.Truncate(100)}...");
        });
        services.AddTransient<CustomMarkdownPipelineFactory>();

        services.AddTransient<DocumentPropertyReader>();
        services.AddTransient<OnPropertiesFound>((sp) =>
        {
            var logger = sp.GetService<ILogger<DocumentPropertyReader>>();
            return (DocumentProperties properties) => logger.LogDebug($"Found properties: {properties.ToString()}");
        });
    }

    public static void AddMarkdownParsers(this IServiceCollection services)
    {
        services.AddTransient<IMarkdownFileParser, MarkdownFileParser>();
    }
}
