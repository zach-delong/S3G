using System;
using System.IO;

namespace Test.FileManipulation
{
    public abstract class TempFileObject : IDisposable
    {
        public string Path { get; set; }

        public TempFileObject(string? path)
        {
            Path = path;
        }

        public abstract void Dispose();
    }
}
