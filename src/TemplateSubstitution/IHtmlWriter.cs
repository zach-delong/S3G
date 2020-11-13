namespace StaticSiteGenerator.TemplateSubstitution
{
    public interface IHtmlWriter
    {
        string ToHtml(string content);
    }
}
