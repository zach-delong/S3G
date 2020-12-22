namespace StaticSiteGenerator.Utilities.StrategyPattern
{
    public abstract class StrategyForTypeAttribute : System.Attribute
    {
        public StrategyForTypeAttribute(string typeName)
        {
            TypeName = typeName;
        }

        public string TypeName { get; set; }
    }
}
