using StaticSiteGenerator.Utilities.StrategyPattern;

namespace StaticSiteGenerator.Files.FileProcessingStrategies
{
    public class FileProcessorForType : StrategyForTypeAttribute
    {
        public FileProcessorForType(string typeName) : base(typeName)
        {
        }
    }
}