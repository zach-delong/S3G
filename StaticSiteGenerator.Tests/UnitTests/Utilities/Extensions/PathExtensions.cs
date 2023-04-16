using System.IO.Abstractions;

namespace StaticSiteGenerator.Tests.UnitTests.Utilities.Extensions;

public static class PathExtensions
{
    public static string GetSystemRoot(this IPath p)
    {
        return p.GetPathRoot(p.GetTempPath());
    }
}
