// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Compiler;
using System.Diagnostics.Contracts;

namespace Microsoft.Contracts.Foxtrot
{
    /// <summary>
    /// Represents a warning based on CCI source context information.
    /// </summary>
    [ContractVerification(true)]
    public class Warning : Error
    {
        public Warning(int errorCode, string error, SourceContext context)
            : base(errorCode, error, context)
        {
            // F:
            Contract.Requires(error != null);

            this.IsWarning = true;
        }
    }
}
