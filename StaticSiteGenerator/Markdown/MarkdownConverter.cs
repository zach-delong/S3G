using Markdig;

namespace StaticSiteGenerator.Markdown;

// TODO: add logging here.
public class MarkdownConverter
{
    private readonly CustomMarkdownPipelineFactory pipelineFactory;
    private readonly DocumentPropertyReader propertyReader;
    private MarkdownPipeline pipeline;

    public MarkdownConverter(
	CustomMarkdownPipelineFactory pipelineFactory,
	DocumentPropertyReader propertyReader)
    {
        this.pipelineFactory = pipelineFactory;
        this.propertyReader = propertyReader;
    }

    public HtmlDocument ConvertToHtml(string markdownString)
    {
        var document = Markdig.Markdown.Parse(markdownString, Pipeline);
        var metadata = propertyReader.GetDocumentProperties(document);

        return new HtmlDocument
        {
            Contents = document.ToHtml(Pipeline),
	    Properties = metadata
	};
    }

    private MarkdownPipeline Pipeline => pipeline ??= pipelineFactory.Get();

}
