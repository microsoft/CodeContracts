// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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
