using System.IO.Abstractions;
using StaticSiteGenerator.Utilities.StrategyPattern;

namespace StaticSiteGenerator.Files.FileProcessingStrategies;

[FileProcessorForType(nameof(FolderFileSystemObject))]
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
        var rootPathToInputFile = system.Path.GetFullPath(input.FullPath);
        var rootPathToInputRoot = system.Path.GetFullPath(options.PathToMarkdownFiles);

        var inputPathRelativeToInputRoot = system.Path.GetRelativePath(rootPathToInputRoot, rootPathToInputFile);
        var path = system.Path.Join(options.OutputLocation, inputPathRelativeToInputRoot);
        system.Directory.CreateDirectory(path);

        return null;
    }
}
