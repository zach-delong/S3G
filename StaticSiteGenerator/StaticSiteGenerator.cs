using System;
using StaticSiteGenerator.Markdown;
using StaticSiteGenerator.FileManipulation;
using StaticSiteGenerator.TemplateSubstitution;

using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace StaticSiteGenerator
{
    [TransientService]
    public class StaticSiteGenerator
    {
        private FileIterator fileIterator;
        private MarkdownFileParser MarkdownFileParser;
        private MarkdownConverter MarkdownConverter;
        private ITemplateReader TemplateReader;

        private CliOptions Options;

        public StaticSiteGenerator(
            FileIterator fileIterator,
            MarkdownFileParser markdownFileParser,
            MarkdownConverter markdownConverter,
            ITemplateReader templateReader,
            CliOptions options
        ) {
            this.fileIterator = fileIterator;
            this.MarkdownFileParser = markdownFileParser;
            this.MarkdownConverter = markdownConverter;
            this.TemplateReader = templateReader;
            this.Options = options;
        }

        public void Start()
        {
            try
            {
                var files = fileIterator.GetFilesInDirectory(Options.PathToMarkdownFiles);

                TemplateReader.ReadTemplate($"templates/{Options.TemplateName}");

                foreach(var file in files)
                {
                    var contents = MarkdownFileParser.ReadFile(file);

                    var convertedFile = MarkdownConverter.Convert(contents);

                    Console.WriteLine(convertedFile);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception was thrown");
                Console.Write(ex);
            }
        }

    }
}
