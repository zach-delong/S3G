using System;
using System.IO;

namespace StaticSiteGenerator.UnitTests.Helpers.TemporaryFiles
{
    public abstract class TempFileObject : IDisposable
    {
        public string Path { get; set; }

        public TempFileObject(string path)
        {
            Path = path;
        }

        public TempFileObject() {}

        public abstract void Dispose();
    }
}
