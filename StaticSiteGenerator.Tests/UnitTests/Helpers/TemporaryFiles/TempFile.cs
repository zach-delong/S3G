using System.IO;

namespace StaticSiteGenerator.Tests.UnitTests.Helpers.TemporaryFiles;

public class TempFile : TempFileObject
{
    public TempFile(string path) : base(path)
    {
        var file = File.Create(path);
        file.Dispose();
    }

    public void WriteToFile(string contents)
    {
        using (var file = File.AppendText(Path))
        {
            file.WriteLine(contents);
        }
    }

    public override void Dispose()
    {
        File.Delete(Path);
    }
}
