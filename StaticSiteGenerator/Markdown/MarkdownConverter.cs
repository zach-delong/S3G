using Markdig;

namespace StaticSiteGenerator.Markdown;

public class MarkdownConverter
{
    private readonly CustomMarkdownPipelineFactory pipelineFactory;
    private MarkdownPipeline pipeline;

    public MarkdownConverter(CustomMarkdownPipelineFactory pipelineFactory)
    {
        this.pipelineFactory = pipelineFactory;
    }

    public string ConvertToHtml(string markdownString)
    {
        var document = Markdig.Markdown.Parse(markdownString, Pipeline);

        return document.ToHtml(Pipeline);
    }

    private MarkdownPipeline Pipeline => pipeline ??= pipelineFactory.Get();

}
