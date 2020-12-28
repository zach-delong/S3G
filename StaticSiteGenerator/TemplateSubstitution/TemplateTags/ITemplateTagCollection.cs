namespace StaticSiteGenerator.TemplateSubstitution.TemplateTags
{
    public interface ITemplateTagCollection
    {
        TemplateTag GetTagForType(TagType type);
    }
}
