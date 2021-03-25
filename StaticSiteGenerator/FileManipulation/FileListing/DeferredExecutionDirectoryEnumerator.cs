using System.Collections.Generic;
using System.IO;

namespace StaticSiteGenerator.FileManipulation.FileListing
{
    /// <summary>
    /// Given a path and pattern, return an iterable containing the contents of that directory.
    /// </summary
    public class DeferredExecutionDirectoryEnumerator: IDirectoryEnumerator
    {
        public IEnumerable<string> GetChildren(string path, string pattern)
        {
            foreach(var result in GetDirectories(path, pattern))
            {
                yield return result;
            }

            foreach(var result in GetFiles(path, pattern))
            {
                yield return result;
            }
        }

        public IEnumerable<string> GetDirectories(string path, string pattern)
        {
            return Directory.EnumerateDirectories(path, pattern);
        }

        public IEnumerable<string> GetFiles(string path, string pattern)
        {
            return Directory.EnumerateFiles(path, pattern);
        }
    }
}
