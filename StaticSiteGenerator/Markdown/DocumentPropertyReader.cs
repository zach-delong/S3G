using System.Linq;
using System.Text;
using Markdig.Extensions.Yaml;
using Markdig.Syntax;
using YamlDotNet.Serialization.NamingConventions;

namespace StaticSiteGenerator.Markdown;

public class DocumentPropertyReader
{
    public delegate void OnPropertiesFound(DocumentProperties foundProperties);

    private readonly OnPropertiesFound AfterPropertiesFound;

    public DocumentPropertyReader(OnPropertiesFound afterPropertiesFound = null)
    {
        AfterPropertiesFound = afterPropertiesFound;
    }

    public virtual DocumentProperties GetDocumentProperties(MarkdownDocument document)
    {
        var firstBlock = document.FirstOrDefault();
        DocumentProperties metadata = null;

	if (firstBlock is YamlFrontMatterBlock)
	{
            var bar = (YamlFrontMatterBlock)firstBlock;

	    var lines = bar.Lines;
	    var yamlString = new StringBuilder();

	    foreach (var line in lines)
	    {
		yamlString.AppendLine(line.ToString());
	    }

            var deserializer = new YamlDotNet.Serialization.DeserializerBuilder()
		.WithNamingConvention(CamelCaseNamingConvention.Instance)
		.IgnoreUnmatchedProperties()
		.Build();

	    metadata = deserializer.Deserialize<DocumentProperties>(yamlString.ToString());

            AfterPropertiesFound?.Invoke(metadata);

            return metadata;
	}

        return null;
    }
}
