using System;
using System.IO;

namespace Test.FileManipulation
{
    public class TempFile: TempFileObject
    {
        public TempFile(string path): base(path)
        {
            Console.WriteLine($"Building a new temp file with \n\t {path}");
            File.Create(path);
        }

        public override void Dispose()
        {
            Console.WriteLine($"Garbage collecting file \n\t {Path}");
            File.Delete(Path);
        }
    }
}
