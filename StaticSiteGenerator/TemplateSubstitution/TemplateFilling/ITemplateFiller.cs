using System.Collections.Generic;
using StaticSiteGenerator.TemplateSubstitution.TemplateTags;

namespace StaticSiteGenerator.TemplateSubstitution.TemplateFilling;

public interface ITemplateFiller
{
    string Fill(TemplateTag tag, string content);
    string Fill(TemplateTag tag, IDictionary<string, string> content);
}
