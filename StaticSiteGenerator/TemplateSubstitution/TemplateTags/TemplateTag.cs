using TanvirArjel.Extensions.Microsoft.DependencyInjection;
using StaticSiteGenerator.TemplateSubstitution.TemplateTags;

namespace StaticSiteGenerator.TemplateSubstitution
{
    public class TemplateTag
    {
        public string Template { get; set; }
        public TagType Type { get; set; }
    }
}
