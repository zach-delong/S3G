using System.Collections.Generic;

using StaticSiteGenerator.TemplateSubstitution.TemplateTags;

namespace StaticSiteGenerator.TemplateSubstitution
{
    public interface ITemplateReader
    {
        TemplateTag GetTemplateTagForType(TagType type);
        IList<TemplateTag> ReadTemplate(string templatePath);
        TemplateTag ReadTemplateFile(string filePath);
    }
}
