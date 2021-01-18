namespace StaticSiteGenerator.MarkdownHtmlConversion
{
    public interface IHtmlConverter<T>
    {
        string Convert(T foo);
    }
}
