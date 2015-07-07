// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace ContractAdornments
{
    using ContractAdornments.Interfaces;

    public static class ContractsPackageAccessor
    {
        public const string InvalidOperationExceptionMessage_TheSnapshotIsOutOfDate = "The snapshot is out of date";
        public const string COMExceptionMessage_BindingFailed = "Binding failed because IntelliSense for source file";

        public static IContractsPackage Current
        {
            get;
            set;
        }
    }
}
