using System;
using System.IO;

using Test.Helpers.TemporaryFiles;

namespace Test.Helpers
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
            var tempFilePath = path + "/" + Guid.NewGuid().ToString() + ".txt";

            return new TempFile(tempFilePath);
        }

        public static TempFile GetTempTextFile()
        {
            var tempFilePath = Path.GetTempPath() + Guid.NewGuid().ToString();

            return GetTempTextFile(tempFilePath);
        }
    }
}
