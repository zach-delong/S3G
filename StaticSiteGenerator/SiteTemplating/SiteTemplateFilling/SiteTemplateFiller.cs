using StaticSiteGenerator.HtmlWriting;
using StaticSiteGenerator.SiteTemplating.SiteTemplateReading;

namespace StaticSiteGenerator.SiteTemplating.SiteTemplateFilling;

public class SiteTemplateFiller : ISiteTemplateFiller
{
    private readonly ISiteTemplateReader templateReader;
    private readonly HtmlFilePropertyFiller propertyFiller;

    public SiteTemplateFiller(
	ISiteTemplateReader templateReader,
	HtmlFilePropertyFiller propertyFiller)
    {
        this.templateReader = templateReader;
        this.propertyFiller = propertyFiller;
    }


    public string FillSiteTemplate(string contents)
    {
        var template = templateReader.ReadTemplate();

        return template.Replace("{{}}", contents);
    }

    public string FillSiteTemplate(IHtmlFile htmlFile)
    {
        htmlFile.HtmlContent = FillSiteTemplate(htmlFile.HtmlContent);

        htmlFile.HtmlContent = propertyFiller.FillTemplateProperties(htmlFile);

        return htmlFile.HtmlContent;
    }
}
