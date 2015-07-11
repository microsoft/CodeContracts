// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Research.DataStructures
{
    /// <summary>
    /// Equality comparer using the Equals and GetHashCode methods
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ObjectEqualityComparer<T> : IEqualityComparer<T>
    {
        /// <summary>
        /// Instance
        /// </summary>
        public static readonly ObjectEqualityComparer<T> Instance = new ObjectEqualityComparer<T>(); // Thread-safe

        private ObjectEqualityComparer() { }

        #region IEqualityComparer<T> Members

        /// <summary>
        /// Determines whether the specified objects are equal.
        /// </summary>
        /// <param name="x">The first object of type <typeparamref name="T"/> to compare.</param>
        /// <param name="y">The second object of type <typeparamref name="T"/> to compare.</param>
        /// <returns>
        /// true if the specified objects are equal; otherwise, false.
        /// </returns>
        public bool Equals(T x, T y)
        {
            if (x == null)
                return y == null;
            else
                return x.Equals(y);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">
        /// The type of <paramref name="obj"/> is a reference type and <paramref name="obj"/> is null.
        /// </exception>
        public int GetHashCode(T obj)
        {
            return obj.GetHashCode();
        }

        #endregion
    }
}
