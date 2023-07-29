using Markdig;

namespace StaticSiteGenerator.Markdown;

public class CustomMarkdownPipelineFactory
{
    private readonly CustomExtension customExtension;

    public CustomMarkdownPipelineFactory(CustomExtension customExtension)
    {
        this.customExtension = customExtension;
    }

    public virtual MarkdownPipeline Get()
    {
        var pipeline = new MarkdownPipelineBuilder();

        // Note: add in their extension first, because if you don't,
        // It won't find the correct renderer to replace! (because I swap them off)
        SetupProvidedExtensions(pipeline);

        AddS3GExtension(pipeline);

        return pipeline.Build();
    }

    private void AddS3GExtension(MarkdownPipelineBuilder pipeline)
    {
        pipeline.Extensions.AddIfNotAlready(customExtension);
    }

    private static void SetupProvidedExtensions(MarkdownPipelineBuilder pipeline)
    {
        pipeline.UseYamlFrontMatter();
    }
}
