using TanvirArjel.Extensions.Microsoft.DependencyInjection;
using StaticSiteGenerator.TemplateSubstitution.TemplateTags;

namespace StaticSiteGenerator.TemplateSubstitution
{
    public class TagWriter: IHtmlWriter
    {
        public string Template { get; set; }
        public TagType Type { get; set; }

        public TagWriter()
        {}

        public string ToHtml(string content)
        {
            return Template.Replace("{{}}", content);
        }
    }
}
