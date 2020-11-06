using System;
using System.IO;
using System.Text;

using TanvirArjel.Extensions.Microsoft.DependencyInjection;

namespace StaticSiteGenerator.FileManipulation
{
    [TransientService]
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

        public StreamReader ReadFile(string filePath)
        {
            try
            {
                var stream = new StreamReader(filePath);

                return stream;
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
