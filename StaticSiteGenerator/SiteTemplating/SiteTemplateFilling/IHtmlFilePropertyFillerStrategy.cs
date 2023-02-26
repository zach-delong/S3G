using StaticSiteGenerator.HtmlWriting;

namespace StaticSiteGenerator.SiteTemplating.SiteTemplateFilling;

public interface IHtmlFilePropertyFillerStrategy
{
    public string Execute(IHtmlFile file);
}
