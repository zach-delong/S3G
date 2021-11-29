using System.IO.Abstractions;

namespace StaticSiteGenerator.UnitTests.Utilities.Extensions
{
    public static class PathExtensions
    {
        public static string GetSystemRoot(this IPath p)
        {
            return p.GetPathRoot(p.GetTempPath());
        }
    }
}
