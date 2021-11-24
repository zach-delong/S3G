using System.IO;
using StaticSiteGenerator.Files;

namespace StaticSiteGenerator.SiteTemplating.SiteTemplateReading
{
    public class SiteTemplateLocalFileReader : ISiteTemplateReader
    {
        public SiteTemplateLocalFileReader(FileReader fileReader, CliOptions options)
        {
            Reader = fileReader;

            TemplatePath = options.TemplatePath;
        }

        public FileReader Reader { get; }

        private readonly string TemplatePath;

        public string ReadTemplate()
        {
            return Reader.ReadFile(Path.Combine(TemplatePath, "site_template.html"));
        }
    }
}
