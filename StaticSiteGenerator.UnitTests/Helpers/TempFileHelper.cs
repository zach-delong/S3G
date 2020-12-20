using System;
using System.IO;

using StaticSiteGenerator.UnitTests.Helpers.TemporaryFiles;

namespace StaticSiteGenerator.UnitTests.Helpers
{
    public static class TempFileHelper
    {
        public static TempDirectory GetTempFolder()
        {
            var tempFolderPath = Path.GetTempPath() + Guid.NewGuid().ToString();

            return new TempDirectory(tempFolderPath);
        }

        public static TempFile GetTempTextFile(string path)
        {
            Console.WriteLine("Creating temp file");
            var tempFilePath = path + Guid.NewGuid().ToString() + ".txt";

            return new TempFile(tempFilePath);
        }

        public static TempFile GetTempTextFile()
        {
            var tempFilePath = Path.GetTempPath();

            return GetTempTextFile(tempFilePath);
        }
    }
}
