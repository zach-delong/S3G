using System;
using System.Linq;
using Markdig.Helpers;
using Markdig.Renderers;

namespace StaticSiteGenerator.Utilities.Extensions;

public static class OrderedListExtensions
{
    public static void ReplaceOrAdd(this OrderedList<IMarkdownObjectRenderer> t,
                               Type toReplace,
                               IMarkdownObjectRenderer replaceWith)
    {
        var found = false;
        for (var i = 0; i < t.Count && !found; i++)
        {
            var baseType = t[i].GetType().BaseType;
            if (baseType.Name == toReplace.Name
		&& baseType.GenericTypeArguments.Count() == 1
		&& baseType.GenericTypeArguments.First().Name == toReplace.GenericTypeArguments.First().Name)
            {
                t.RemoveAt(i);
                t.Insert(i, replaceWith);
                found = true;
            }
        }

        if(!found)
        {
            t.Add(replaceWith);
        }
    }
}
