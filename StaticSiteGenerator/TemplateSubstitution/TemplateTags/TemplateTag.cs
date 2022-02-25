using System;

namespace StaticSiteGenerator.TemplateSubstitution.TemplateTags;

public class TemplateTag
{
    public string Template { get; set; }
    public TagType Type { get; set; }

    internal object Split(string v)
    {
        throw new NotImplementedException();
    }
}
