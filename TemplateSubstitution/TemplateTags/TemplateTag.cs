using TanvirArjel.Extensions.Microsoft.DependencyInjection;
using StaticSiteGenerator.TemplateSubstitution.TemplateTags;

namespace StaticSiteGenerator.TemplateSubstitution
{
    public class TemplateTag: IHtmlWriter
    {
        public string Template { get; set; }
        public TagType Type { get; set; }

        public TemplateTag()
        {}

        public string ToHtml(string content)
        {
            return Template.Replace("{{}}", content);
        }
    }
}
