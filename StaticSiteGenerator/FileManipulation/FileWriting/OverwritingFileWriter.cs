using System.IO;

namespace StaticSiteGenerator.FileManipulation.FileWriting
{
    public class OverwritingFileWriter : IFileWriter
    {
        private readonly IFileWriter Writer;

        public OverwritingFileWriter(IFileWriter writer)
        {
            Writer = writer;
        }

        public void WriteFile(string fileName, string contents)
        {
            if(File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            Writer.WriteFile(fileName, contents);
        }
    }
}
