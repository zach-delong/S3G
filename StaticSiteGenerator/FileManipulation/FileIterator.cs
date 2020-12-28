using System;
using System.Collections.Generic;
using System.IO;

using TanvirArjel.Extensions.Microsoft.DependencyInjection;

using StaticSiteGenerator.FileManipulation.FileException;

namespace StaticSiteGenerator.FileManipulation
{
    [TransientService]
    public class FileIterator
    {
        public virtual IEnumerable<string> GetFilesInDirectory(string directory)
        {
            try
            {
                return Directory.GetFiles(directory);
            }
            catch(DirectoryNotFoundException ex)
            {
                // TODO: do something useful with this exception
                throw new FileManipulationException($"Could not open directory {directory} for reading. Directory may not exist", ex);
            }
        }
    }
}
