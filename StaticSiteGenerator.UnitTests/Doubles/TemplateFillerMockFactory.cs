using Moq;
using StaticSiteGenerator.TemplateSubstitution.TemplateFilling;
using StaticSiteGenerator.TemplateSubstitution.TemplateTags;

namespace StaticSiteGenerator.UnitTests.Doubles
{
    public class TemplateFillerMockFactory
    {
        public static Mock<ITemplateFiller> Get()
        {
            var mock = new Mock<ITemplateFiller>();

            mock.Setup(m => m.Fill(It.IsAny<TemplateTag>(),
                                   It.IsAny<string>()))
                .Returns((TemplateTag tag, string str) => tag.Template.Replace("{{}}", str));

            return mock;
        }
    }
}
