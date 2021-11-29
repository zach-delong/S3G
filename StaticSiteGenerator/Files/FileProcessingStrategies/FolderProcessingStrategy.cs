using System.IO.Abstractions;
using StaticSiteGenerator.Utilities.StrategyPattern;

namespace StaticSiteGenerator.Files.FileProcessingStrategies
{
    [FileProcessorForType(nameof(Folder))]
    public class FolderProcessingStrategy : IStrategy<object, IFileSystemObject>
    {
        private readonly IFileSystem system;
        private readonly CliOptions options;

        public FolderProcessingStrategy(IFileSystem system, CliOptions options)
        {
            this.system = system;
            this.options = options;
        }

        public object Execute(IFileSystemObject input)
        {
            var path = system.Path.Join(options.OutputLocation, input.Name);
            system.Directory.CreateDirectory(path);

            return null;
        }
    }
}