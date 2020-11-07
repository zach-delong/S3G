namespace StaticSiteGenerator.TemplateSubstitution
{
    public interface IConverter<T>
    {
        void Convert(T foo);
    }
}
