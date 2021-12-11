using System;

namespace StaticSiteGenerator.Files.FileException;

public class FileManipulationException : Exception
{
    public FileManipulationException(string message, Exception ex) : base(message, ex) { }
}
