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
                // var typeName = baseType.Name;
                // var genericParamName = baseType.GenericTypeArguments
                //     .First()
                //     .Name;

                // var replaceWithTypeName = toReplace.Name;
                // var toReplaceGenericParamName = toReplace.GenericTypeArguments
                //     .First()
                //     .Name;

                // System.Console.WriteLine($"Replacing {typeName}<{genericParamName}> with {replaceWithTypeName}<{toReplaceGenericParamName}>");
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
