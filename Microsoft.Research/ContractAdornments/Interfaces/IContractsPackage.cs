// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace ContractAdornments.Interfaces
{
    using System;
    using Microsoft.VisualStudio.Text;
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

        void AskForNewVSModel(ITextBuffer textBuffer);
    }
}
