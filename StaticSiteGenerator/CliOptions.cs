using System;
using System.IO;
using CommandLine;
namespace StaticSiteGenerator;

public class CliOptions
{
    [Option('p',
            "pathToMarkdown",
            Required = false,
            Default = "markdownInput",
            HelpText = "The path to the markdown files to be parsed")]
    public string PathToMarkdownFiles { get; set; }

    [Option('t',
            "TemplateName",
            Required = false,
            Default = "default",
            HelpText = "The name of the template to be used. Should exist inside the 'templates' directory")]
    public string TemplateName { get; set; }

    [Option('o',
            "OutputLocation",
            Required = false,
            Default = "output",
            HelpText = "The full path to the desired output folder")]
    public string OutputLocation { get; set; }

    private string TemplateLocation => "templates";

    public string TemplatePath => Path.Combine(TemplateLocation, TemplateName ?? String.Empty);

    public override string ToString()
    {
        return $"{nameof(PathToMarkdownFiles)}: {PathToMarkdownFiles}, {nameof(TemplateName)}: {TemplateName}, {nameof(OutputLocation)}: {OutputLocation}";
    }
}
