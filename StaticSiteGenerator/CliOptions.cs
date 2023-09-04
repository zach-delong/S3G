using CommandLine;
namespace StaticSiteGenerator;

public class CliOptions
{
    [Option('p',
            "pathToMarkdown",
            Required = false,
            Default = "markdownInput",
            HelpText = "The path to the markdown files to be parsed")]
    public virtual string PathToMarkdownFiles { get; set; }

    [Option('t',
            "pathToTemplate",
            Required = false,
            Default = "/input/templates/default",
            HelpText = "The path of the template to be used.")]
    public virtual string TemplatePath { get; set; }

    [Option('o',
            "OutputLocation",
            Required = false,
            Default = "output",
            HelpText = "The full path to the desired output folder")]
    public virtual string OutputLocation { get; set; }


    public override string ToString()
    {
        return $"{nameof(PathToMarkdownFiles)}: {PathToMarkdownFiles}, {nameof(TemplatePath)}: {TemplatePath}, {nameof(OutputLocation)}: {OutputLocation}";
    }
}
