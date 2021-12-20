using System.Collections.Generic;
using System.Linq;
using StaticSiteGenerator.TemplateSubstitution.TemplateTags;
using StaticSiteGenerator.Utilities.Extensions;

namespace StaticSiteGenerator.TemplateSubstitution.TemplateFilling;

public class TemplateFiller : ITemplateFiller
{
    public string Fill(TemplateTag tag, string content)
    {
        return tag.Template.Replace("{{}}", content);
    }

    public string Fill(TemplateTag tag, IDictionary<string, string> content)
    {
        var result = tag.Template;

        var replacements = content
            .ToDictionary(kvp => $"{{{{{kvp.Key}}}}}", kvp => kvp.Value);

        return result.Replace(replacements);
    }
}
