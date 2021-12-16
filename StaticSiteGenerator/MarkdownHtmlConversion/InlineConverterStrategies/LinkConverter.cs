using Markdig.Syntax.Inlines;
using StaticSiteGenerator.Markdown.InlineElement;
using StaticSiteGenerator.MarkdownHtmlConversion.TagModelConverters;

namespace StaticSiteGenerator.MarkdownHtmlConversion.InlineConverterStrategies;

public delegate void BeforeLinkTemplateFilling(LinkElement input);
public delegate void AfterLinkTemplateFilling(LinkElement input, string html);

public class LinkConverter : IInlineConverterStrategy
{
    private readonly LinkInlineModelConverter ModelConverter;
    private readonly BeforeLinkTemplateFilling PreLinkTemplateFill;
    private readonly AfterLinkTemplateFilling PostLinkTemplateFill;

    public LinkConverter(LinkInlineModelConverter modelConverter,
                         BeforeLinkTemplateFilling preLinkTemplateFill = null,
                         AfterLinkTemplateFilling postLinkTemplateFill = null)
    {
        ModelConverter = modelConverter;
        PreLinkTemplateFill = preLinkTemplateFill;
        PostLinkTemplateFill = postLinkTemplateFill;
    }

    public string Execute(IInlineElement input)
    {
        return Execute((LinkElement)input);
    }

    public string Execute(LinkElement input)
    {
        PreLinkTemplateFill.Invoke(input);

        var model = ModelConverter.Convert(input);

        var result = 

        PostLinkTemplateFill.Invoke(input, null);
    }
}
