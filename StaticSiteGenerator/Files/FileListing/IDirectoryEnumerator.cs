using System.Collections.Generic;

namespace StaticSiteGenerator.Files.FileListing
{
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
        /// Given a path and a pattern, get all directories (not files) in the
        /// directory that match the given pattern
        /// </summary
        public IEnumerable<string> GetDirectories(string path, string pattern);

        /// <summary>
        /// Given a path and a pattern, get all directories or files in the
        /// given directory that match the given pattern
        /// </summary
        public IEnumerable<string> GetChildren(string path, string pattern);

        public IEnumerable<IFileSystemObject> ListAllContents(string path);
    }
}
