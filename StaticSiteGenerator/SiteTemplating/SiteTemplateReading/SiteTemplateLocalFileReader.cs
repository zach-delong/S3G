using System.IO;
using StaticSiteGenerator.CLI;
using StaticSiteGenerator.Files;

namespace StaticSiteGenerator.SiteTemplating.SiteTemplateReading;

public class SiteTemplateLocalFileReader : ISiteTemplateReader
{
    private readonly string templatePath;
    private readonly FileReader Reader;

    public SiteTemplateLocalFileReader(FileReader fileReader, TemplatePathOption options)
    {
        Reader = fileReader;
        templatePath = options.TemplatePath;
    }

    public string ReadTemplate()
    {
        return Reader.ReadFile(Path.Combine(templatePath, "site_template.html"));
    }
}
