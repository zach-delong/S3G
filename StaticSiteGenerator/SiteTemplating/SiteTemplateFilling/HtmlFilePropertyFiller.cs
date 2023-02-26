using System.Collections.Generic;
using System.Linq;
using StaticSiteGenerator.HtmlWriting;

namespace StaticSiteGenerator.SiteTemplating.SiteTemplateFilling;

public class HtmlFilePropertyFiller
{
    private readonly IHtmlFilePropertyFillerStrategy[] strategies;

    public HtmlFilePropertyFiller(IEnumerable<IHtmlFilePropertyFillerStrategy> strategies)
    {
        this.strategies = strategies?.ToArray();
    }

    public virtual string FillTemplateProperties(IHtmlFile file)
    {
        foreach (var strategy in strategies)
        {
            file.HtmlContent = strategy.Execute(file);
        }

        return file.HtmlContent;
    }
}
