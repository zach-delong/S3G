using System;
using System.IO;

using StaticSiteGenerator.FileManipulation;

namespace StaticSiteGenerator
{
    public class StaticSiteGenerator
    {
        private FileIterator fileIterator;
        private MarkdownFileReader MarkdownFileReader;

        public StaticSiteGenerator(
            FileIterator fileIterator,
            MarkdownFileReader markdownFileParser) {
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
                    var stream = new StreamReader(file);

                    var contents = MarkdownFileReader.ReadFile(stream);

                    Console.WriteLine(contents);
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
