using System;
using System.Text;
using System.IO;

namespace Test.Helpers.TemporaryFiles
{
    public class TempFile: TempFileObject
    {
        public TempFile(string path): base(path)
        {
            Console.WriteLine($"Created Temp File {Path}");
            var file = File.Create(path);
            file.Dispose();
        }

        public void WriteToFile(string contents)
        {
            Console.WriteLine($"Writing to file {Path} {contents}");
            using (var file = File.AppendText(Path))
            {
                file.WriteLine(contents);
            }
        }

        public override void Dispose()
        {
            Console.WriteLine($"Disposing of {Path}");
            File.Delete(Path);
        }
    }
}
