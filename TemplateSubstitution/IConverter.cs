namespace StaticSiteGenerator.TemplateSubstitution
{
    public interface IConverter<T>
    {
        string Convert(T foo);
    }
}
