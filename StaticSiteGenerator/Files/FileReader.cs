using System.IO;
using System.IO.Abstractions;
using StaticSiteGenerator.Files.FileException;

namespace StaticSiteGenerator.Files
{
    public class FileReader
    {
        private readonly IFileSystem fileSystem;

        public FileReader(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }
        public virtual string ReadFile(string filePath)
        {
            try
            {
                return fileSystem.File.ReadAllText(filePath);
            }
            catch (FileNotFoundException ex)
            {
                throw new FileManipulationException($"Error, the file {filePath} was not found when attempting to read it", ex);
            }
        }
    }
}
