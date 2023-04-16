using Xunit;
using StaticSiteGenerator.Files;
using StaticSiteGenerator.Tests.UnitTests.Helpers;
using System.IO.Abstractions;
using StaticSiteGenerator.Files.FileException;
using FluentAssertions;

namespace StaticSiteGenerator.Tests.UnitTests.Filese;

public class FileReaderTest
{
    [Fact]
    public void FileDoesNotExist()
    {
        var FileReader = new FileReader(new FileSystem());

        var filePath = "NonExistantFileName.txt";

        Assert.Throws<FileManipulationException>(() => { FileReader.ReadFile(filePath); });

        FileReader
	    .Invoking(r => r.ReadFile(filePath))
	    .Should().Throw<FileManipulationException>();
    }

    [Fact]
    public void FileExistsButIsEmpty()
    {
        using (var file = TempFileHelper.GetTempTextFile())
        {
            var fileReader = new FileReader(new FileSystem());

            var fileContents = fileReader.ReadFile(file.Path);

            fileContents.Should().BeEmpty();
        }
    }

    [Fact]
    public void FileExists()
    {
        using (var file = TempFileHelper.GetTempTextFile())
        {
            var contents = "Test File Contents";
            file.WriteToFile(contents);

            var fileReader = new FileReader(new FileSystem());

            var fileContents = fileReader.ReadFile(file.Path);

            fileContents
		.Trim()
		.Should().BeEquivalentTo(contents);
        }
    }
}
