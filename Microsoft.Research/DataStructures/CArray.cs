// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.DataStructures
{
    /// <summary>
    /// Helper methods for arrays
    /// </summary>
    public static class CArray
    {
        private static class Helper<T>
        {
            public static readonly T[] Empty = new T[0];
        }

        /// <summary>
        /// Gets an empty array instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] Empty<T>()
        {
            Contract.Ensures(Contract.Result<T[]>() != null);
            Contract.Ensures(Contract.Result<T[]>().Length == 0);

            return Helper<T>.Empty;
        }
    }
}
