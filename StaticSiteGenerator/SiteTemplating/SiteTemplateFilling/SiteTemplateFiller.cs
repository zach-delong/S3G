using System;
using System.Collections.Generic;
using StaticSiteGenerator.MarkdownHtmlConversion;
using StaticSiteGenerator.SiteTemplating.SiteTemplateReading;

namespace StaticSiteGenerator.SiteTemplating.SiteTemplateFilling
{
    public class SiteTemplateFiller : ISiteTemplateFiller
    {
        public SiteTemplateFiller(ISiteTemplateReader templateReader)
        {
            Reader = templateReader;
        }

        public ISiteTemplateReader Reader { get; }

        public string FillSiteTemplate(string contents)
        {
            var template = Reader.ReadTemplate();

            return template.Replace("{{}}", contents);
        }
    }
}
