using StaticSiteGenerator.TemplateSubstitution.TemplateTags;

namespace StaticSiteGenerator.Utilities;

public class HeaderLevelHelper
{
    public TagType GetHeaderTagTypeFor(int level)
    {
        TagType result;

        switch(level)
        {
            case 1:
                result = TagType.Header1;
                break;
            case 2:
                result = TagType.Header2;
                break;
            case 3:
                result = TagType.Header3;
                break;
            case 4:
                result = TagType.Header4;
                break;
            case 5:
                result = TagType.Header5;
                break;
            case 6:
            default:
                result = TagType.Header6;
                break;
        }

        return result;
    }
}
