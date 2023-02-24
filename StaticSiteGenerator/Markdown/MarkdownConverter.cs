using System.ComponentModel;
using System.Text;
using Markdig;
using Markdig.Extensions.Yaml;
using YamlDotNet.Serialization.NamingConventions;

namespace StaticSiteGenerator.Markdown;

public class DocumentProperties
{
    public string Title { get; set; } = string.Empty;
    public bool Published { get; set; } = true;

    public override string ToString()
    {
        var builder = new StringBuilder();

        builder.AppendLine($"Document Properties:");
        builder.AppendLine($"{nameof(Title)}:\t{Title}");
        builder.AppendLine($"{nameof(Published)}:\t{Published}");

        return builder.ToString();
    }
}

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
	var foo = document[0];

	if (foo is YamlFrontMatterBlock)
	{
	    System.Console.WriteLine("TESTING: FOUND FRONT MATTER");
	    var bar = (YamlFrontMatterBlock)foo;

	    var lines = bar.Lines;
	    var yamlString = new StringBuilder();

            foreach (var line in lines)
	    {
                yamlString.AppendLine(line.ToString());
            }

            System.Console.WriteLine(yamlString.ToString());

            var deserializer = new YamlDotNet.Serialization.DeserializerBuilder()
		.WithNamingConvention(CamelCaseNamingConvention.Instance)
		.IgnoreUnmatchedProperties()
		.Build();

            var metadata = deserializer.Deserialize<DocumentProperties>(yamlString.ToString());

            System.Console.WriteLine(metadata);
        }

        return document.ToHtml(Pipeline);
    }

    private MarkdownPipeline Pipeline => pipeline ??= pipelineFactory.Get();

}
