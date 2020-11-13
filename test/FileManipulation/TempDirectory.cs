using System;
using System.IO;

namespace Test.FileManipulation
{
    public class TempDirectory: TempFileObject
    {
        public TempDirectory(string path): base(path)
        {
            Directory.CreateDirectory(path);
        }

        public override void Dispose()
        {
            Directory.Delete(Path);
        }
    }
}
