// CodeContracts
// 
// Copyright (c) Microsoft Corporation
// 
// All rights reserved. 
// 
// MIT License
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED *AS IS*, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

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
