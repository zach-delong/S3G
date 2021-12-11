using Microsoft.Extensions.DependencyInjection;
using StaticSiteGenerator.Markdown.BlockElement;
using StaticSiteGenerator.Markdown.InlineElement;
using StaticSiteGenerator.MarkdownHtmlConversion.BlockConverterStrategies;
using StaticSiteGenerator.MarkdownHtmlConversion.InlineConverterStrategies;
using StaticSiteGenerator.Utilities.StrategyPattern;

namespace StaticSiteGenerator.MarkdownHtmlConversion;

public static class ServicesConfiguration
{
    public static void AddHtmlConverters(this IServiceCollection services)
    {
        services.AddTransient<IStrategy<string, IBlockElement>, HeaderHtmlConverterStrategy>();
        services.AddTransient<IStrategy<string, IBlockElement>, ParagraphHtmlConverterStrategy>();

        services.AddTransient<IStrategy<string, IInlineElement>, TextConverter>();

        services.AddTransient<IMarkdownConverter, MarkdownConverter>();
    }

}
