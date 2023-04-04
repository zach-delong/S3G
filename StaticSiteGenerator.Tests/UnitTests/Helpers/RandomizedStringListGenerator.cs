using System;
using System.Collections.Generic;

namespace StaticSiteGenerator.UnitTests.Helpers;

public class RandomizedStringListGenerator
{
    private char[] CharacterWhiteList = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };

    public IEnumerable<string> GetStrings(int numberOfFiles)
    {
        var length = 10;

        var random = new Random();
        for (var i = 0; i < numberOfFiles; i++)
        {
            var str = "";
            for (var j = 0; j < length; j++)
            {
                var number = random.Next(0, CharacterWhiteList.Length);
                str += CharacterWhiteList[number];
            }

            yield return str;
        }
    }
}
