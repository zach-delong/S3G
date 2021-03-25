using System.Collections.Generic;
using System.Linq;
using StaticSiteGenerator.FileManipulation.FileListing;
using StaticSiteGenerator.UnitTests.Helpers;
using StaticSiteGenerator.UnitTests.Helpers.TemporaryFiles;
using Xunit;

namespace StaticSiteGenerator.UnitTests.FileManipulation
{
    public class DeferredExecutionDirectoryEnumeratorTests
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 0)]
        [InlineData(3, 0)]
        [InlineData(0, 1)]
        [InlineData(0, 3)]
        public void Test_Children(int numberOfTestFiles, int numberOfTestDirectories)
        {
            using (var directory = TempFileHelper.GetTempFolder())
            {
                var expectedResultcount = numberOfTestFiles + numberOfTestDirectories;
                var files = new List<TempFile>();
                var folders = new List<TempDirectory>();

                foreach (var _ in Enumerable.Range(1, numberOfTestFiles))
                {
                    TempFile item = TempFileHelper.GetTempTextFile(directory);
                    files.Add(item: item);
                }

                foreach(var _ in Enumerable.Range(1, numberOfTestDirectories))
                {
                    TempDirectory item = TempFileHelper.GetTempFolder(directory);
                    folders.Add(item);
                }

                var sut = new DeferredExecutionDirectoryEnumerator();

                var result = sut.GetChildren(directory.Path, "*").ToList();

                Assert.Equal(expectedResultcount, result.Count);

                files.ForEach((file) => file.Dispose());
                folders.ForEach((folder) => folder.Dispose());
            }
        }


        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(3)]
        public void Test_Files(int numberOfTestFiles)
        {
            using (var directory = TempFileHelper.GetTempFolder())
            {
                var files = new List<TempFile>();

                foreach (var _ in Enumerable.Range(1, numberOfTestFiles))
                {
                    TempFile item = TempFileHelper.GetTempTextFile(directory);
                    files.Add(item: item);
                }

                var sut = new DeferredExecutionDirectoryEnumerator();

                var result = sut.GetFiles(directory.Path, "*").ToList();

                Assert.Equal(numberOfTestFiles, result.Count);

                files.ForEach((file) => file.Dispose());
            }
        }


        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(3)]
        public void Test_Folders(int numberOfTestFolders)
        {
            using (var directory = TempFileHelper.GetTempFolder())
            {
                var folders = new List<TempDirectory>();

                foreach (var _ in Enumerable.Range(1, numberOfTestFolders))
                {
                    TempDirectory item = TempFileHelper.GetTempFolder(directory);
                    folders.Add(item: item);
                }

                var sut = new DeferredExecutionDirectoryEnumerator();

                var result = sut.GetDirectories(directory.Path, "*").ToList();

                Assert.Equal(numberOfTestFolders, result.Count);

                folders.ForEach((file) => file.Dispose());
            }
        }
    }
}
