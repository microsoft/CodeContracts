namespace ContractAdornments.Interfaces
{
    using Microsoft.Cci.Contracts;

    public interface INonlockingHost : IContractAwareHost
    {
        void AddLibPath(string path);
    }
}
