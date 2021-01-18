namespace StaticSiteGenerator.TemplateSubstitution.TemplateFilling
{
    public interface ITemplateFiller
    {
        string Fill(TemplateTag tag, string content);
    }
}
