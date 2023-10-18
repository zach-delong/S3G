using System.IO.Abstractions;
using StaticSiteGenerator.CLI;
using StaticSiteGenerator.Files;
using StaticSiteGenerator.Utilities.StrategyPattern;

namespace StaticSiteGenerator.FileProcessingStrategies;

[FileProcessorForType(nameof(FileFileSystemObject))]
public class FileProcessingStrategy : IStrategy<object, IFileSystemObject>
{
    private readonly IFileSystem system;
    private readonly OutputLocationOption outputLocationOption;
    private readonly MarkdownFilePathOption markdownFilePathOption;

    public FileProcessingStrategy(IFileSystem system, OutputLocationOption options, MarkdownFilePathOption markdownFilePathOption)
    {
        this.system = system;
        this.outputLocationOption = options;
        this.markdownFilePathOption = markdownFilePathOption;
    }
    public object Execute(IFileSystemObject input)
    {
        var rootPathToInputFile = system.Path.GetFullPath(input.FullPath);
        var rootPathToInputRoot = system.Path.GetFullPath(markdownFilePathOption.PathToMarkdownFiles);

        var inputPathRelativeToInputRoot = system.Path.GetRelativePath(rootPathToInputRoot, rootPathToInputFile);
        string destinationFilePath = system.Path.Combine(outputLocationOption.OutputLocation, inputPathRelativeToInputRoot);

        system.File.Copy(input.FullPath, destinationFilePath);

        return null;
    }
}
