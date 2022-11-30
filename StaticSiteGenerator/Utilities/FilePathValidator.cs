using System;

namespace StaticSiteGenerator.Utilities;

public class FilePathValidator
{
    public bool IsFilePath(string uri)
    {
	if (uri == string.Empty)
            return false;

        Uri parsedUri;

        var isValidUri = Uri.TryCreate(uri, new UriCreationOptions(), out parsedUri);

	if (!isValidUri)
            return true; // Assume it's a relative file path

	if(parsedUri.Scheme == "file")
            return true;

        return false;
    }
}
