using StaticSiteGenerator.TemplateSubstitution.TemplateTags;
using StaticSiteGenerator.Markdown.BlockElement;
using StaticSiteGenerator.TemplateSubstitution.TagCollection;
using StaticSiteGenerator.TemplateSubstitution.TemplateFilling;
using StaticSiteGenerator.Utilities.StrategyPattern;
using StaticSiteGenerator.Markdown.InlineElement;
using System.Linq;
using System;

namespace StaticSiteGenerator.MarkdownHtmlConversion.BlockConverterStrategies;

[HtmlConverterFor(nameof(Header))]
public class HeaderHtmlConverterStrategy : IBlockHtmlConverterStrategy
{
    private readonly IStrategyExecutor<string, IInlineElement> InlineConverter;
    private readonly ITemplateTagCollection TemplateTagCollection;

    public readonly ITemplateFiller TemplateFiller;

    public HeaderHtmlConverterStrategy(IStrategyExecutor<string, IInlineElement> inlineConverter,
                                   ITemplateTagCollection templateCollection,
                                   ITemplateFiller templateFiller)
    {
        InlineConverter = inlineConverter;
        TemplateTagCollection = templateCollection;
        TemplateFiller = templateFiller;
    }

    public string Execute(IBlockElement block)
    {
        var template = TemplateTagCollection.GetTagForType(TagType.Header1);
        var inlineText = InlineConverter.Process(((Header)block).Inlines).ToList();

        return TemplateFiller.Fill(template, String.Join(Environment.NewLine, inlineText));
    }
}
