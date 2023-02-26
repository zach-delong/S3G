using Markdig;

namespace StaticSiteGenerator.Markdown;

public class MarkdownConverter
{
    private readonly CustomMarkdownPipelineFactory pipelineFactory;
    private readonly DocumentPropertyReader propertyReader;
    private MarkdownPipeline pipeline;

    public delegate void OnConversionStart(string markdownString);
    public delegate void OnConversionEnd(string htmlString);

    private OnConversionStart BeforeConversion;
    private OnConversionEnd AfterConversion;

    public MarkdownConverter(
	CustomMarkdownPipelineFactory pipelineFactory,
	DocumentPropertyReader propertyReader,
        OnConversionStart conversionStart = null,
	OnConversionEnd conversionEnd = null)
    {
        this.pipelineFactory = pipelineFactory;
        this.propertyReader = propertyReader;
        this.BeforeConversion = conversionStart;
        this.AfterConversion = conversionEnd;
    }

    public HtmlDocument ConvertToHtml(string markdownString)
    {
        BeforeConversion?.Invoke(markdownString);

        var document = Markdig.Markdown.Parse(markdownString, Pipeline);
        var htmlContent = document.ToHtml(Pipeline);
        var metadata = propertyReader.GetDocumentProperties(document);

        AfterConversion?.Invoke(htmlContent);

        return new HtmlDocument
        {
            Contents = htmlContent,
	    Properties = metadata
	};
    }

    private MarkdownPipeline Pipeline => pipeline ??= pipelineFactory.Get();
}
