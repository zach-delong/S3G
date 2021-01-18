using StaticSiteGenerator.TemplateSubstitution.TemplateTags;

namespace StaticSiteGenerator.TemplateSubstitution.TagCollection
{
    public interface ITemplateTagCollection
    {
        TemplateTag GetTagForType(TagType type);
    }
}
