using System;

namespace StaticSiteGenerator.FileManipulation.FileException
{
    public class FileManipulationException: Exception
    {
        public FileManipulationException(string message, Exception ex): base(message, ex) { }
        public FileManipulationException(string message): base(message) { }
    }
}
