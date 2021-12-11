using System.Collections.Generic;
using StaticSiteGenerator.Markdown.BlockElement;

namespace StaticSiteGenerator.UnitTests.Helpers;

public class BlankMarkdownFileDictionaryGenerator
{
    public IDictionary<string, IList<IBlockElement>> GetBlankBlockListForFilesWithNames(IEnumerable<string> result)
    {
        Dictionary<string, IList<IBlockElement>> input = new Dictionary<string, IList<IBlockElement>>();

        foreach (var name in result)
        {
            input.Add(name, new List<IBlockElement>());
        }

        return input;
    }
}
