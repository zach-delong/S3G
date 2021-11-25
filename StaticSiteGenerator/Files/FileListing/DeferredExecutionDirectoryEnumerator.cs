using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;

namespace StaticSiteGenerator.Files.FileListing
{
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

        public IEnumerable<string> GetChildren(string path, string pattern)
        {
            foreach (var result in GetDirectories(path, pattern))
            {
                yield return result;
            }

            foreach (var result in GetFiles(path, pattern))
            {
                yield return result;
            }
        }

        public IEnumerable<string> GetDirectories(string path, string pattern)
        {
            return FileSystem.Directory.EnumerateDirectories(path, pattern);
        }

        public IEnumerable<string> GetFiles(string path, string pattern)
        {
            return FileSystem.Directory.EnumerateFiles(path, pattern);
        }

        public IEnumerable<IFileSystemObject> ListAllContents(string path)
        {
            Stack<string> pathsToExplore = new Stack<string>();

            pathsToExplore.Push(path);
            
            while(pathsToExplore.Any())
            {
                var p = pathsToExplore.Pop();

                foreach(var fileSystemObject in FileSystem.Directory.GetFileSystemEntries(p))
                {
                    var attrs = FileSystem.File.GetAttributes(fileSystemObject);

                    if(attrs.HasFlag(FileAttributes.Directory))
                    {
                        pathsToExplore.Push(fileSystemObject);
                        yield return new Folder(fileSystemObject);
                    }
                    else 
                    {
                        yield return new File(fileSystemObject);
                    }
                }
            }
        }
    }
}
