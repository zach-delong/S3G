using StaticSiteGenerator.HtmlWriting;

namespace StaticSiteGenerator.SiteTemplating.SiteTemplateFilling;

public interface ISiteTemplateFiller
{
    public string FillSiteTemplate(IHtmlFile file);
}
