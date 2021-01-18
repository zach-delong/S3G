using System.Collections.Generic;
using StaticSiteGenerator.TemplateSubstitution;

namespace StaticSiteGenerator.TemplateReading
{
    public interface ITemplateReader
    {
        IEnumerable<TemplateTag> ReadTemplate();
    }
}
