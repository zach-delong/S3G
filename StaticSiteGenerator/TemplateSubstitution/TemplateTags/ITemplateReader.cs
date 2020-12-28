using System.Collections.Generic;

namespace StaticSiteGenerator.TemplateSubstitution.TemplateTags
{
    public interface ITemplateReader
    {
        IEnumerable<TemplateTag> ReadTemplate();
    }
}
