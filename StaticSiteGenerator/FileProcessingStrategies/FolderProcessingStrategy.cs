using System.IO.Abstractions;
using StaticSiteGenerator.CLI;
using StaticSiteGenerator.Files;
using StaticSiteGenerator.Utilities.StrategyPattern;

namespace StaticSiteGenerator.FileProcessingStrategies;

[FileProcessorForType(nameof(FolderFileSystemObject))]
public class FolderProcessingStrategy : IStrategy<object, IFileSystemObject>
{
    private readonly IFileSystem system;

    private readonly MarkdownFilePathOption markdownFilePathOption;
    private readonly OutputLocationOption outputLocationOption;

    public FolderProcessingStrategy(IFileSystem system, OutputLocationOption outputLocationOption, MarkdownFilePathOption markdownFilePathOption)
    {
        this.system = system;
        this.outputLocationOption = outputLocationOption;
        this.markdownFilePathOption = markdownFilePathOption;
    }

    public object Execute(IFileSystemObject input)
    {
        var rootPathToInputFile = system.Path.GetFullPath(input.FullPath);
        var rootPathToInputRoot = system.Path.GetFullPath(markdownFilePathOption.PathToMarkdownFiles);

        var inputPathRelativeToInputRoot = system.Path.GetRelativePath(rootPathToInputRoot, rootPathToInputFile);
        var path = system.Path.Join(outputLocationOption.OutputLocation, inputPathRelativeToInputRoot);
        system.Directory.CreateDirectory(path);

        return null;
    }
}
