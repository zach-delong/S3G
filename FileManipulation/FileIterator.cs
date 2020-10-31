using System;
using System.Collections.Generic;
using System.IO;

namespace StaticSiteGenerator.FileManipulation
{
    public class FileIterator
    {
        public IEnumerable<string> GetFilesInDirectory(string directory)
        {
            try
            {
                return Directory.GetFiles(directory);
            }
            catch(DirectoryNotFoundException ex){
                // TODO: do something useful with this exception
                Console.WriteLine("Could not access that directory");
                throw ex;
            }
        }
    }
}
