using System;
using System.IO;

namespace Test.FileManipulation
{
    public class TempDirectory: TempFileObject
    {
        public TempDirectory(string path): base(path)
        {
            Console.WriteLine($"Building a new temp directory with \n\t {path}");
            Directory.CreateDirectory(path);
        }

        public override void Dispose()
        {
            Console.WriteLine($"Garbage collecting directory \n\t {Path}");
            Directory.Delete(Path);
        }
    }
}
