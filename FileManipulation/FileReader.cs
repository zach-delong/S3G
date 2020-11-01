using System;
using System.IO;
using System.Text;

namespace StaticSiteGenerator.FileManipulation
{
    public class FileReader
    {
        public string ReadFile(StreamReader reader)
        {
            var fileContents = new StringBuilder();
            do
            {
                fileContents.AppendLine(reader.ReadLine());
            } while (reader.Peek() != -1);

            return fileContents.ToString();
        }

        public string ReadFile(string filePath)
        {
            try
            {
                var stream = new StreamReader(filePath);

                return ReadFile(stream);
            }
            catch(FileNotFoundException ex)
            {
                // TODO: do something useful with this error
                Console.WriteLine("Error, file not found");
                throw(ex);
            }
        }
    }
}
