using System.Collections.Generic;
using Moq;
using StaticSiteGenerator.Markdown;
using StaticSiteGenerator.Markdown.BlockElement;
using StaticSiteGenerator.Markdown.YamlMetadata;
using StaticSiteGenerator.Markdown.YamlMetadata.YamlMetadataProcessorStrategies;
using Xunit;

namespace StaticSiteGenerator.UnitTests.Markdown.YamlMetadata
{
    public class YamlMetadataProcessorTests
    {
        [Fact]
        public void YamlMetadataParser_ShouldWork() {
            var mockedStrategy = new Mock<IYamlMetadataProcessorStrategy>();

            mockedStrategy.Setup(m => m.Process(It.IsAny<IMarkdownFile>(), It.IsAny<IList<YamlHeader>>()))
                .Returns<IMarkdownFile, IList<YamlHeader>>((file, yamlHeaders) => file);

            var strategies = new List<IYamlMetadataProcessorStrategy>{ mockedStrategy.Object };

            var sut = new YamlMetadataProcessor(strategies);

            var yamlHeader = new YamlHeader();

            var file = new MarkdownFile {
                Elements = new List<IBlockElement> {
                    yamlHeader
                }
            };

            sut.ParseYamlMetadata(file);

            mockedStrategy.Verify(m => m.Process(It.IsAny<IMarkdownFile>(), It.IsAny<IList<YamlHeader>>()), Times.Once());
        }
    }
}
