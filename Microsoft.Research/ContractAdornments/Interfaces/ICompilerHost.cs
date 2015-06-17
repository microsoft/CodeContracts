namespace ContractAdornments.Interfaces
{
    using System.Collections.Generic;

    public interface ICompilerHost
    {
        IEnumerable<ICompiler> Compilers
        {
            get;
        }
    }
}
