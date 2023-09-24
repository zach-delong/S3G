using System.Collections.Generic;
using AutoFixture;
using NSubstitute;
using NSubstitute.Extensions;
using StaticSiteGenerator.HtmlWriting;
using StaticSiteGenerator.SiteTemplating.SiteTemplateFilling;

namespace StaticSiteGenerator.Tests.UnitTests.Doubles.Markdown;

public static class HtmlFilePropertyFillerFactory
{
    public static HtmlFilePropertyFiller SetupHtmlFilePropertyFiller(this IFixture fixture)
    {
        var templatePropertyFillerMock = Substitute.ForPartsOf<HtmlFilePropertyFiller>((IEnumerable<IHtmlFilePropertyFillerStrategy>)null);
        templatePropertyFillerMock
	    .Configure()
	    .FillTemplateProperties(Arg.Any<IHtmlFile>())
	    .Returns((args) => {
		var file = (IHtmlFile)args[0];
		return file.HtmlContent;
	    });

        return templatePropertyFillerMock;
    }
}
