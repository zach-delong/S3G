namespace StaticSiteGenerator.Files.FileWriting;

public interface IFileWriter
{
    public void WriteFile(string fileName, string contents);
}
