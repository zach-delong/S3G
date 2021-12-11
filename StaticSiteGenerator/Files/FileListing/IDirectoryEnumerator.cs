using System.Collections.Generic;

namespace StaticSiteGenerator.Files.FileListing;

/// <summary>
/// Given a path to a directory, give a list of items in that directory
/// </summary
public interface IDirectoryEnumerator
{
    /// <summary>
    /// Given a path and a pattern, get all files (not directories) in the
    /// directory that match the given pattern.
    /// </summary
    public IEnumerable<string> GetFiles(string path, string pattern);

    /// <summary>
    /// Given a path, returns all folders and files in the path, "recursively"
    /// Will always give you back folders before the files inside them
    /// </summary
    public IEnumerable<IFileSystemObject> ListAllContents(string path);
}
