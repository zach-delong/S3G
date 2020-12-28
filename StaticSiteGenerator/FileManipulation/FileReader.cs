using System;
using System.IO;
using System.Text;

using TanvirArjel.Extensions.Microsoft.DependencyInjection;

using StaticSiteGenerator.FileManipulation.FileException;

namespace StaticSiteGenerator.FileManipulation
{
    [TransientService]
    public class FileReader
    {
        public virtual string ReadFile(string filePath)
        {
            try
            {
                var stream = new StreamReader(filePath);

                return stream.ReadToEnd();
            }
            catch(FileNotFoundException ex)
            {
                throw new FileManipulationException($"Error, the file {filePath} was not found when attempting to read it", ex);
            }
            catch(IOException ex)
            {
                throw new FileManipulationException($"Error, the file {filePath} could not be opened for Reading. Perhaps there is resource contention?", ex);
            }
        }
    }
}
