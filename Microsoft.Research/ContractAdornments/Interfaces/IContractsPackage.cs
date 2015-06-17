namespace ContractAdornments.Interfaces
{
    using System;
    using UtilitiesNamespace;

    public interface IContractsPackage
    {
        event Action<object> NewCompilation;
        event Action ExtensionFailed;

        Logger Logger
        {
            get;
        }

        IContractOptionsPage VSOptionsPage
        {
            get;
        }

        IVersionedServicesFactory GetVersionedServicesFactory();

        void QueueWorkItem(Action action, Func<bool> condition, bool requeueIfConditionIsntMet = true);

        void QueueWorkItem(Action action);

        void AskForNewVSModel();
    }
}
