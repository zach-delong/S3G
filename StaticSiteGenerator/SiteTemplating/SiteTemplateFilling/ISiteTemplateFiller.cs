using System.Collections.Generic;
using StaticSiteGenerator.MarkdownHtmlConversion;

namespace StaticSiteGenerator.SiteTemplating.SiteTemplateFilling
{
    public interface ISiteTemplateFiller
    {
        public string FillSiteTemplate(string contents);
        public IEnumerable<IHtmlFile> FillSiteTemplate(IEnumerable<IHtmlFile> contents);
    }
}
