// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace ContractAdornments.Interfaces
{
    using System;
    using Microsoft.Cci;
    using VSLangProj;

    public interface IProjectTracker
    {
        event Action BuildBegin;
        event Action BuildDone;

        string ProjectName
        {
            get;
        }

        AssemblyIdentity AssemblyIdentity
        {
            get;
        }

        INonlockingHost Host
        {
            get;
        }

        References References
        {
            get;
        }

        IContractsProvider ContractsProvider
        {
            get;
        }
    }
}
