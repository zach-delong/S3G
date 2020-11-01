using System;
using System.IO;
using StaticSiteGenerator.Markdown;
using StaticSiteGenerator.FileManipulation;

namespace StaticSiteGenerator
{
    public class StaticSiteGenerator
    {
        private FileIterator fileIterator;
        private MarkdownFileParser MarkdownFileReader;

        public StaticSiteGenerator(
            FileIterator fileIterator,
            MarkdownFileParser markdownFileParser
        ) {
            this.fileIterator = fileIterator;
            this.MarkdownFileReader = markdownFileParser;
        }

        public void Start()
        {
            try
            {
                var files = fileIterator.GetFilesInDirectory("exampleMarkdownDirectory");

                foreach(var file in files)
                {
                    var contents = MarkdownFileReader.ReadFile(file);

                    Console.WriteLine(contents);
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
