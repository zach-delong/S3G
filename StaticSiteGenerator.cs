using System;
using System.IO;
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
        private MarkdownFileParser MarkdownFileReader;
        private MarkdownConverter MarkdownConverter;

        public StaticSiteGenerator(
            FileIterator fileIterator,
            MarkdownFileParser markdownFileParser,
            MarkdownConverter markdownConverter
        ) {
            this.fileIterator = fileIterator;
            this.MarkdownFileReader = markdownFileParser;
            this.MarkdownConverter = markdownConverter;
        }

        public void Start()
        {
            try
            {
                var files = fileIterator.GetFilesInDirectory("exampleMarkdownDirectory");

                foreach(var file in files)
                {
                    var contents = MarkdownFileReader.ReadFile(file);

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
