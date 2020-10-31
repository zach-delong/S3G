using System.IO;
using System.Text;

namespace StaticSiteGenerator.Markdown
{
    public class MarkdownFileReader
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
    }
}
