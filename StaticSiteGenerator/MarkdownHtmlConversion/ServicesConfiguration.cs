using Microsoft.Extensions.DependencyInjection;
using StaticSiteGenerator.Markdown.BlockElement;
using StaticSiteGenerator.Markdown.InlineElement;
using StaticSiteGenerator.MarkdownHtmlConversion.BlockConverterStrategies;
using StaticSiteGenerator.MarkdownHtmlConversion.InlineConverterStrategies;
using StaticSiteGenerator.MarkdownHtmlConversion.TagModelConverters;
using StaticSiteGenerator.Utilities.StrategyPattern;

namespace StaticSiteGenerator.MarkdownHtmlConversion;

public static class ServicesConfiguration
{
    public static void AddHtmlConverters(this IServiceCollection services)
    {
        services.AddTransient<IStrategy<string, IBlockElement>, HeaderHtmlConverterStrategy>();
        services.AddTransient<IStrategy<string, IBlockElement>, ParagraphHtmlConverterStrategy>();

        services.AddTransient<IStrategy<string, IInlineElement>, TextConverter>();
        services.AddTransient<IStrategy<string, IInlineElement>, LinkConverter>();

        services.AddTransient<IMarkdownConverter, MarkdownConverter>();

        services.AddTransient<LinkInlineModelConverter>();
    }

}
