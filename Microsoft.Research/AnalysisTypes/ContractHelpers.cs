// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Diagnostics.Contracts
{
    public static class ContractHelpers
    {
        [Pure]
        [ContractVerification(false)]
        static public void AssumeInvariant<T>(this T obj)
        {
            // does nothing
        }

        [Pure]
        [ContractVerification(false)]
        static public T AssumeNotNull<T>(this T obj)
          where T : class
        {
            Contract.Ensures(Contract.Result<T>() != null);
            Contract.Ensures(Contract.Result<T>() == obj);

            return obj;
        }

        [Pure]
        [ContractVerification(false)]
        static public string AssumeNotNullOrEmpty(this string str)
        {
            Contract.Ensures(Contract.Result<string>() != null);
            Contract.Ensures(Contract.Result<string>().Length > 0);
            Contract.Ensures(Contract.Result<string>() == str);

            return str;
        }
    }
}
