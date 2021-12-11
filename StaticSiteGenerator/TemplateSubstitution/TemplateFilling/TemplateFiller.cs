using StaticSiteGenerator.TemplateSubstitution.TemplateTags;

namespace StaticSiteGenerator.TemplateSubstitution.TemplateFilling;

public class TemplateFiller : ITemplateFiller
{
    public string Fill(TemplateTag tag, string content)
    {
        return tag.Template.Replace("{{}}", content);
    }
}
