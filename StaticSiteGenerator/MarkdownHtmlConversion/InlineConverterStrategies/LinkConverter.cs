using StaticSiteGenerator.Markdown.InlineElement;
using StaticSiteGenerator.MarkdownHtmlConversion.TagModelConverters;
using StaticSiteGenerator.TemplateSubstitution.TagCollection;
using StaticSiteGenerator.TemplateSubstitution.TemplateFilling;
using StaticSiteGenerator.TemplateSubstitution.TemplateTags;

namespace StaticSiteGenerator.MarkdownHtmlConversion.InlineConverterStrategies;

public delegate void BeforeLinkTemplateFilling(LinkElement input);
public delegate void AfterLinkTemplateFilling(LinkElement input, string html);

[HtmlConverterFor(nameof(LinkElement))]
public class LinkConverter : IInlineConverterStrategy
{
    private readonly LinkInlineModelConverter ModelConverter;
    private readonly ITemplateTagCollection TemplateCollection;
    private readonly BeforeLinkTemplateFilling PreLinkTemplateFill;
    private readonly AfterLinkTemplateFilling PostLinkTemplateFill;
    private readonly ITemplateFiller TemplateFiller;

    public LinkConverter(LinkInlineModelConverter modelConverter,
                         ITemplateFiller templateFiller,
                         ITemplateTagCollection templateCollection,
                         BeforeLinkTemplateFilling preLinkTemplateFill = null,
                         AfterLinkTemplateFilling postLinkTemplateFill = null)
    {
        ModelConverter = modelConverter;
        TemplateCollection = templateCollection;
        TemplateFiller = templateFiller;
        PreLinkTemplateFill = preLinkTemplateFill;
        PostLinkTemplateFill = postLinkTemplateFill;
    }

    public string Execute(IInlineElement input)
    {
        return Execute((LinkElement)input);
    }

    public string Execute(LinkElement input)
    {
        PreLinkTemplateFill?.Invoke(input);

        var model = ModelConverter.Convert(input);

        var tag = TemplateCollection.GetTagForType(TagType.Link);

        var result = TemplateFiller.Fill(tag, model);

        PostLinkTemplateFill?.Invoke(input, result);

        return result;
    }
}
