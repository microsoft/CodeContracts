namespace ContractAdornments.Interfaces
{
    public interface IContractOptionsPage
    {
        bool SmartFormatting
        {
            get;
        }

        bool SyntaxColoring
        {
            get;
        }

        bool CollapseMetadataContracts
        {
            get;
        }

        bool Caching
        {
            get;
        }

#if false
        bool InheritanceOnMethods
        {
            get;
        }

        bool InheritanceOnProperties
        {
            get;
        }
#endif
    }
}
