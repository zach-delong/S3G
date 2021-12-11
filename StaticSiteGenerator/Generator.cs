using System;
using Microsoft.Extensions.Logging;
using StaticSiteGenerator.Files.FileListing;
using StaticSiteGenerator.Utilities.StrategyPattern;
using StaticSiteGenerator.Files;
using System.Linq;

namespace StaticSiteGenerator;

public class Generator
{
    private readonly IDirectoryEnumerator directoryLister;
    private readonly CliOptions Options;
    private readonly ILogger<Generator> logger;
    private readonly IStrategyExecutor<object, IFileSystemObject> converter;

    public Generator(
        IDirectoryEnumerator directoryLister,
        CliOptions options,
        ILogger<Generator> logger,
        IStrategyExecutor<object, IFileSystemObject> converter
    )
    {
        this.directoryLister = directoryLister;
        this.Options = options;
        this.logger = logger;
        this.converter = converter;
    }

    public void Start()
    {
        try
        {
            logger.LogTrace("Starting conversion of static site.");

            var fileNames = directoryLister.ListAllContents(Options.PathToMarkdownFiles);

            converter.Process(fileNames)
                     .ToList();

            logger.LogTrace("Finished conversion of static site.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An exception was thrown");
            Console.Write(ex);
        }
    }

}
