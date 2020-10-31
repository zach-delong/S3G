using System;
using System.IO;
using StaticSiteGenerator.Markdown;
using StaticSiteGenerator.FileManipulation;

namespace StaticSiteGenerator
{
    public class StaticSiteGenerator
    {
        private FileIterator fileIterator;
        private MarkdownFileReader MarkdownFileReader;
        private MarkdownParser MarkdownParser;

        public StaticSiteGenerator(
            FileIterator fileIterator,
            MarkdownFileReader markdownFileParser,
            MarkdownParser markdownParser
        ) {
            this.fileIterator = fileIterator;
            this.MarkdownFileReader = markdownFileParser;
            this.MarkdownParser = markdownParser;
        }

        public void Start()
        {
            try
            {
                var files = fileIterator.GetFilesInDirectory("exampleMarkdownDirectory");

                foreach(var file in files)
                {
                    var stream = new StreamReader(file);

                    var contents = MarkdownFileReader.ReadFile(stream);

                    var parsedContents = MarkdownParser.ParseMarkdownString(contents);

                    Console.WriteLine(parsedContents);
                }

            }
            catch(FileNotFoundException ex)
            {
                Console.WriteLine("Error, file not found");
                Console.Write(ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An exception was thrown");
                Console.Write(ex);
            }
        }

    }
}
