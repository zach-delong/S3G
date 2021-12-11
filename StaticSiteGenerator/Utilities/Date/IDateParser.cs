using System;

namespace StaticSiteGenerator.Utilities.Date;

public interface IDateParser
{
    bool TryParse(string value, out DateTime result);
}
