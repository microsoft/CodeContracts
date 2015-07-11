// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Research.DataStructures
{
    /// <summary>
    /// Equality comparer using the IEquatable interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EquatableEqualityComparer<T> : IEqualityComparer<T>
        where T : IEquatable<T>
    {
        /// <summary>
        /// Instance
        /// </summary>
        public static readonly IEqualityComparer<T> Instance = new EquatableEqualityComparer<T>(); // Thread-safe

        private EquatableEqualityComparer() { }


        #region IEqualityComparer<T> Members

        bool IEqualityComparer<T>.Equals(T x, T y)
        {
            if (x == null)
                return y == null;
            else
                return x.Equals(y);
        }

        int IEqualityComparer<T>.GetHashCode(T obj)
        {
            return obj.GetHashCode();
        }

        #endregion
    }
}
