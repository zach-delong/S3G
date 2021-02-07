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

        public IEnumerable<IHtmlFile> FillSiteTemplate(IEnumerable<IHtmlFile> contents)
        {
            // Console.WriteLine("Filling in site template");
            foreach(var file in contents)
            {
                // Console.WriteLine($"(2) Filling in site template for {file.Name}");
                file.HtmlContent = FillSiteTemplate(file.HtmlContent);
                yield return file;

                // Console.WriteLine($"(2) Done filling in site template for {file.Name}");
            }

            // Console.WriteLine("Done filling in site template");
        }
    }
}
