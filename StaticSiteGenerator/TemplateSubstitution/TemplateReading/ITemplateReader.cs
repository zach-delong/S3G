using System.Collections.Generic;
using StaticSiteGenerator.TemplateSubstitution.TemplateTags;

namespace StaticSiteGenerator.TemplateReading;

public interface ITemplateReader
{
    IEnumerable<TemplateTag> ReadTemplate();
}
