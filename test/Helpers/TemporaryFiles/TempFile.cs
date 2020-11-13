using System;
using System.IO;

namespace Test.Helpers.TemporaryFiles
{
    public class TempFile: TempFileObject
    {
        public TempFile(string path): base(path)
        {
            File.Create(path);
        }

        public override void Dispose()
        {
            File.Delete(Path);
        }
    }
}
