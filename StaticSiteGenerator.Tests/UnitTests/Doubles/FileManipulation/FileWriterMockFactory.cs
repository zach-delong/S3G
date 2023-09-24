using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using NSubstitute;
using NSubstitute.Extensions;
using StaticSiteGenerator.Files.FileException;
using StaticSiteGenerator.Files.FileWriting;

namespace StaticSiteGenerator.Tests.UnitTests.Doubles.FileManipulation;

public static class FileWriterMockFactory
{
    public static IFileWriter SetupFileWriter(this IFixture fixture)
    {
        return SetupFileWriter(fixture, new List<string>());
    }

    public static IFileWriter SetupFileWriter(this IFixture fixture, IEnumerable<string> existingFileNames)
    {
        var mock = Substitute.For<IFileWriter>();

        mock.Configure()
            .When(c => c.WriteFile(Arg.Is<string>(s => existingFileNames.Contains(s)), Arg.Any<string>()))
	    .Do((_) => throw new FileAlreadyExistsException());

        fixture.Inject<IFileWriter>(mock);
        return mock;
    }
}
