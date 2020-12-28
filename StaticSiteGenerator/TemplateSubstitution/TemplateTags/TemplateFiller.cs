namespace StaticSiteGenerator.TemplateSubstitution.TemplateTags
{
    public class TemplateFiller : ITemplateFiller
    {
        public string Fill(TemplateTag tag, string content)
        {
            return tag.Template.Replace("{{}}", content);
        }
    }
}
