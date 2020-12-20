namespace StaticSiteGenerator.TemplateSubstitution
{
    public interface IHtmlConverter<T>
    {
        string Convert(T foo);
    }
}
