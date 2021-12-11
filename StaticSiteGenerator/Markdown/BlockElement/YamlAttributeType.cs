using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace StaticSiteGenerator.Markdown.BlockElement;

public class YamlAttributeType
{
    public static YamlAttributeType PublishDate = new YamlAttributeType("publish_date");

    public string Name { get; set; }
    private static IList<YamlAttributeType> values;
    public static IList<YamlAttributeType> Values
    {
        get
        {
            values ??= List();
            return values;
        }
    }

    private YamlAttributeType(string name)
    {
        Name = name;
    }

    public static bool IsValidValue(string input)
    {
        return Values.Select(v => v.Name).Contains(input);
    }

    public static YamlAttributeType GetFromStringOrDefault(string input)
    {
        return Values.SingleOrDefault(v => v.Name == input);
    }

    private static IList<YamlAttributeType> List()
    {
        return typeof(YamlAttributeType).GetFields(BindingFlags.Public | BindingFlags.Static)
                                        .Where(p => p.FieldType == typeof(YamlAttributeType))
                                        .Select(p => (YamlAttributeType)p.GetValue(null))
                                        .OrderBy(p => p.Name)
                                        .ToList();
    }
}
