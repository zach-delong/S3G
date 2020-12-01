using CommandLine;
namespace StaticSiteGenerator
{
    public class CliOptions
    {
        [Option('p',
                "pathToMarkdown",
                Required = false,
                Default = "exampleMarkdownDirectory",
                HelpText = "The path to the markdown files to be parsed")]
        public string PathToMarkdownFiles { get; set; }

        [Option('t',
                "TemplateName",
                Required = false,
                Default="default",
                HelpText="The name of the template to be used. Should exist inside the 'templates' directory")]
        public string TemplateName { get; set; }

        public override string ToString()
        {
            return $"{nameof(PathToMarkdownFiles)}: {PathToMarkdownFiles}, {nameof(TemplateName)}: {TemplateName}"; 
        }
    }
}
