using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;

namespace StaticSiteGenerator.Files.FileListing;

/// <summary>
/// Given a path and pattern, return an iterable containing the contents of that directory.
/// </summary
public class DeferredExecutionDirectoryEnumerator : IDirectoryEnumerator
{
    private readonly IFileSystem FileSystem;

    public DeferredExecutionDirectoryEnumerator(IFileSystem fileSystem)
    {
        FileSystem = fileSystem;
    }

    public IEnumerable<string> GetFiles(string path, string pattern)
    {
        return FileSystem.Directory.EnumerateFiles(path, pattern);
    }

    public IEnumerable<IFileSystemObject> ListAllContents(string path)
    {
        Stack<string> pathsToExplore = new Stack<string>();

        pathsToExplore.Push(path);

        while (pathsToExplore.Any())
        {
            var p = pathsToExplore.Pop();

            foreach (var filePath in FileSystem.Directory.GetFileSystemEntries(p))
            {
                var attrs = FileSystem.File.GetAttributes(filePath);

                var rootPathToInputFile = FileSystem.Path.GetFullPath(filePath);

                if (attrs.HasFlag(FileAttributes.Directory))
                {
                    yield return new FolderFileSystemObject(rootPathToInputFile);
                    pathsToExplore.Push(filePath);
                }
                else if (filePath.ToLower().EndsWith(".md"))
                {
                    yield return new MarkdownFileSystemObject(rootPathToInputFile);
                }
                else
                {
                    yield return new FileFileSystemObject(rootPathToInputFile);
                }
            }
        }
    }
}
