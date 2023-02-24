using Markdig;

namespace StaticSiteGenerator.Markdown;

public class CustomMarkdownPipelineFactory
{
    private readonly CustomExtension customExtension;

    public CustomMarkdownPipelineFactory(CustomExtension customExtension)
    {
        this.customExtension = customExtension;
    }

    public MarkdownPipeline Get()
    {
        var pipeline = new MarkdownPipelineBuilder();

        pipeline.Extensions.Add(customExtension);
        pipeline.UseYamlFrontMatter();

        return pipeline.Build();
    }
}
