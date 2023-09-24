using System.IO.Abstractions.TestingHelpers;
using AutoFixture;
using NSubstitute;
using StaticSiteGenerator.HtmlWriting;

namespace StaticSiteGenerator.Tests.UnitTests.Doubles.HtmlWriting;

public static class HtmlFileWriterMockFactory
{
    public static IHtmlFileWriter SetupHtmlfileWriter(this IFixture fixture, MockFileSystem fs)
    {
        var mockHtmlFileWriter = Substitute.For<IHtmlFileWriter>();
        mockHtmlFileWriter.When(x => x.Write(Arg.Any<string>(), Arg.Any<string>()))
        .Do(x => {
            var path = (string)x[0];
            var contents = (string)x[1];

            fs.AddFile(path, contents);
	});

        fixture.Inject<IHtmlFileWriter>(mockHtmlFileWriter);

        return mockHtmlFileWriter;
    }
}
