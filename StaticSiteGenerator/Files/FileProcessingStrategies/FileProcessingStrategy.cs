using System.IO.Abstractions;
using StaticSiteGenerator.Utilities.StrategyPattern;

namespace StaticSiteGenerator.Files.FileProcessingStrategies
{
    [FileProcessorForType(nameof(File))]
    public class FileProcessingStrategy : IStrategy<object, IFileSystemObject>
    {
        private readonly IFileSystem system;
        private readonly CliOptions options;

        public FileProcessingStrategy(IFileSystem system, CliOptions options)
        {
            this.system = system;
            this.options = options;
        }
        public object Execute(IFileSystemObject input)
        {
            var rootPathToInputFile = system.Path.GetFullPath(input.Name);
            var rootPathToInputRoot = system.Path.GetFullPath(options.PathToMarkdownFiles);
            
            var inputPathRelativeToInputRoot = system.Path.GetRelativePath(rootPathToInputRoot, rootPathToInputFile);
            string destinationFilePath = system.Path.Combine(options.OutputLocation, inputPathRelativeToInputRoot);

            system.File.Copy(input.Name, destinationFilePath);
            
            return null;
        }
    }
}