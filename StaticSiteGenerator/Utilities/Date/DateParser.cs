using System;
using System.Globalization;

namespace StaticSiteGenerator.Utilities.Date
{
    public class DateParser: IDateParser
    {
        public bool TryParse(string value, out DateTime result)
        {
            return  DateTime.TryParse(value, new CultureInfo("en-US"), DateTimeStyles.None, out result);
        }
    }
}
