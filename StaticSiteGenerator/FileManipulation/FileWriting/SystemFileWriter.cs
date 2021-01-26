using System;
using System.IO;
using StaticSiteGenerator.FileManipulation.FileException;

namespace StaticSiteGenerator.FileManipulation.FileWriting
{
    public class SystemFileWriter: IFileWriter
    {
        public void WriteFile(string fileName, string contents)
        {
            if(File.Exists(fileName))
            {
                throw new FileAlreadyExistsException($"The file '{fileName}' already exists");
            }

            using (var writer = File.CreateText(fileName))
            {
                writer.Write(contents);
            }
        }
    }
}
