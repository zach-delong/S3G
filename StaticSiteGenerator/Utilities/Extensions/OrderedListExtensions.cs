using System;
using Markdig.Helpers;
using Markdig.Renderers;

namespace StaticSiteGenerator.Utilities.Extensions;

public static class OrderedListExtensions
{
    public static void Replace(this OrderedList<IMarkdownObjectRenderer> t,
                               Type toReplace,
                               IMarkdownObjectRenderer replaceWith)
    {
        var found = false;
        for (var i = 0; i < t.Count && !found; i++)
        {
            if (t[i].GetType().Name == toReplace.Name)
            {
                t.RemoveAt(i);
                t.Insert(i, replaceWith);
                found = true;
            }
        }
    }
}
