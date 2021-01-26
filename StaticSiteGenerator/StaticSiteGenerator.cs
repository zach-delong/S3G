using System;
using StaticSiteGenerator.Markdown;
using StaticSiteGenerator.MarkdownHtmlConversion;
using StaticSiteGenerator.FileManipulation;

using TanvirArjel.Extensions.Microsoft.DependencyInjection;
using StaticSiteGenerator.HtmlWriting;
using System.IO;

namespace StaticSiteGenerator
{
    [TransientService]
    public class StaticSiteGenerator
    {
        private FileIterator fileIterator;
        private IMarkdownFileParser MarkdownFileParser;
        private IMarkdownConverter MarkdownConverter;

        private CliOptions Options;

        public readonly IHtmlFileWriter HtmlFileWriter;

        public StaticSiteGenerator(
            FileIterator fileIterator,
            IMarkdownFileParser markdownFileParser,
            IMarkdownConverter markdownConverter,
            CliOptions options,
            IHtmlFileWriter fileWriter
        ) {
            this.fileIterator = fileIterator;
            this.MarkdownFileParser = markdownFileParser;
            this.MarkdownConverter = markdownConverter;
            this.Options = options;
            this.HtmlFileWriter = fileWriter;
        }

        public void Start()
        {
            try
            {
                var files = fileIterator.GetFilesInDirectory(Options.PathToMarkdownFiles);


                foreach(var file in files)
                {
                    var contents = MarkdownFileParser.ReadFile(file);

                    var convertedFile = MarkdownConverter.Convert(contents);

                    HtmlFileWriter.Write(Path.Combine(Options.OutputLocation,
                                                      Path.GetFileName(file)),
                                         convertedFile);
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
