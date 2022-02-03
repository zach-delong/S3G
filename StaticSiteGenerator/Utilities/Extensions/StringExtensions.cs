using System.Collections.Generic;

namespace StaticSiteGenerator.Utilities.Extensions;

public static class StringExtensions
{
    public static string Replace(this string target, IDictionary<string, string> replacements)
    {
        var result = target;
        foreach(var pair in replacements)
        {
            result = result.Replace(pair.Key, pair.Value);
        }

        return result;
    }
}
