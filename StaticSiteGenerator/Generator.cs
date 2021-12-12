using System;
using StaticSiteGenerator.Files.FileListing;
using StaticSiteGenerator.Utilities.StrategyPattern;
using StaticSiteGenerator.Files;
using System.Linq;

namespace StaticSiteGenerator;

public class Generator
{
    private readonly IDirectoryEnumerator directoryLister;
    private readonly CliOptions Options;
    private readonly IStrategyExecutor<object, IFileSystemObject> converter;

    public delegate void OnSiteStart();
    public delegate void OnSiteDone();

    private readonly OnSiteStart BeforeStart;
    private readonly OnSiteDone AfterEnd;

    public Generator(
        IDirectoryEnumerator directoryLister,
        CliOptions options,
        IStrategyExecutor<object, IFileSystemObject> converter,
        OnSiteStart beforeStart,
        OnSiteDone afterEnd
    )
    {
        this.directoryLister = directoryLister;
        this.Options = options;
        this.converter = converter;
        this.BeforeStart = beforeStart;
        this.AfterEnd = afterEnd;
    }

    public void Start()
    {
        try
        {
            BeforeStart?.Invoke();

            var fileNames = directoryLister.ListAllContents(Options.PathToMarkdownFiles);

            converter.Process(fileNames)
                     .ToList();

            AfterEnd?.Invoke();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An exception was thrown");
            Console.Write(ex);
        }
    }

}
