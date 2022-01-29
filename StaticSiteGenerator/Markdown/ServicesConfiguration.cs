using Microsoft.Extensions.DependencyInjection;
using StaticSiteGenerator.Markdown.InlineElementConverter;
using StaticSiteGenerator.Markdown.BlockElementConverter;

using StaticSiteGenerator.Markdown.Parser;
using StaticSiteGenerator.Utilities.StrategyPattern;
using Markdig.Syntax.Inlines;
using StaticSiteGenerator.Markdown.InlineElement;
using StaticSiteGenerator.Markdown.BlockElement;
using Markdig.Syntax;
using StaticSiteGenerator.Markdown.Renderers;

namespace StaticSiteGenerator.Markdown;

public static class ServicesConfiguration
{
    public static void AddMarkdownConverters(this IServiceCollection services)
    {
        services.AddTransient<IStrategy<IInlineElement, IInline>, TextElementConverter>();
        services.AddTransient<IStrategy<IInlineElement, IInline>, LinkElementConverter>();

        services.AddTransient<IStrategy<IBlockElement, IBlock>, HeaderConverter>();
        services.AddTransient<IStrategy<IBlockElement, IBlock>, ParagraphConverter>();

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
