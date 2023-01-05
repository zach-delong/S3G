using StaticSiteGenerator.Utilities.StrategyPattern;

namespace StaticSiteGenerator.FileProcessingStrategies;

public class FileProcessorForType : StrategyForTypeAttribute
{
    public FileProcessorForType(string typeName) : base(typeName)
    {
    }
}
