using StaticSiteGenerator.Utilities.StrategyPattern;
using StaticSiteGenerator.Markdown.BlockElement;
using StaticSiteGenerator.TemplateSubstitution.TemplateTags;
using StaticSiteGenerator.TemplateSubstitution.TagCollection;
using StaticSiteGenerator.TemplateSubstitution.TemplateFilling;
using System;
using StaticSiteGenerator.Markdown.InlineElement;
using System.Linq;

namespace StaticSiteGenerator.MarkdownHtmlConversion.BlockConverterStrategies;

[HtmlConverterFor(nameof(Paragraph))]
public class ParagraphHtmlConverterStrategy : IBlockHtmlConverterStrategy
{
    private readonly IStrategyExecutor<string, IInlineElement> InlineConverter;
    private readonly ITemplateTagCollection TemplateTagCollection;
    private readonly ITemplateFiller TemplateFiller;

    public ParagraphHtmlConverterStrategy(IStrategyExecutor<string, IInlineElement> inlineConverter,
                                      ITemplateTagCollection templateCollection,
                                      ITemplateFiller templateFiller)
    {
        InlineConverter = inlineConverter;
        TemplateTagCollection = templateCollection;
        TemplateFiller = templateFiller;
    }

    public string Execute(IBlockElement block)
    {
        var b = (Paragraph)block;
        var inlineText = String.Empty;

        if (b.Inlines != null)
        {
            inlineText = string.Join(Environment.NewLine, InlineConverter.Process(b.Inlines).ToList());
        }

        var template = TemplateTagCollection.GetTagForType(TagType.Paragraph);

        return TemplateFiller.Fill(template, inlineText);
    }
}
