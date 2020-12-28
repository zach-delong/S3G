namespace StaticSiteGenerator.TemplateSubstitution.TemplateTags
{
    public interface ITemplateFiller
    {
        string Fill(TemplateTag tag, string content);
    }
}
