namespace StaticSiteGenerator.Markdown.YamlMetadata
{
    public interface IYamlMetadataProcessor
    {
        IMarkdownFile ParseYamlMetadata(IMarkdownFile file);
    }
}
