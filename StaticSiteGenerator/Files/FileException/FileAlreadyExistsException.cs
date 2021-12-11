using System;

namespace StaticSiteGenerator.Files.FileException;

public class FileAlreadyExistsException : Exception
{
    public FileAlreadyExistsException()
    {
    }

    public FileAlreadyExistsException(string message) : base(message)
    {
    }
}
