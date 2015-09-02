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

using System.Collections;
using System.Drawing;
using System.Diagnostics.Contracts;
using System.Windows.Forms.Layout;
using System.ComponentModel;

namespace System.Windows.Forms
{
    /// <summary>
    /// Provides the base functionality for creating data-related collections in the <see cref="N:System.Windows.Forms"/> namespace.
    /// </summary>
    public class BaseCollection : MarshalByRefObject // ICollection, IEnumerable
    {
        /// <summary>
        /// Gets the total number of elements in the collection.
        /// </summary>
        /// 
        /// <returns>
        /// The total number of elements in the collection.
        /// </returns>
        
        public virtual int Count
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= 0);
                return default(int);
            }
        }

        // <summary>
        // Gets a value indicating whether the collection is read-only.
        // </summary>
        // 
        // <returns>
        // This property is always false.
        // </returns>
        // <filterpriority>1</filterpriority>
        // public bool IsReadOnly { get; }
        
        /// <summary>
        /// Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection"/> is synchronized.
        /// </summary>
        /// 
        /// <returns>
        /// This property always returns false.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public bool IsSynchronized
        {
            get
            {
                Contract.Ensures(Contract.Result<bool>() == false);
                return false;
            }
        }

        // <summary>
        // Gets an object that can be used to synchronize access to the <see cref="T:System.Windows.Forms.BaseCollection"/>.
        // </summary>
        // 
        // <returns>
        // An object that can be used to synchronize the <see cref="T:System.Windows.Forms.BaseCollection"/>.
        // </returns>
        // public object SyncRoot { get; }
        
        /// <summary>
        /// Copies all the elements of the current one-dimensional <see cref="T:System.Array"/> to the specified one-dimensional <see cref="T:System.Array"/> starting at the specified destination <see cref="T:System.Array"/> index.
        /// </summary>
        /// <param name="ar">The one-dimensional <see cref="T:System.Array"/> that is the destination of the elements copied from the current Array. </param><param name="index">The zero-based relative index in <paramref name="ar"/> at which copying begins. </param><filterpriority>1</filterpriority>
        public void CopyTo(Array ar, int index)
        {
            Contract.Requires(ar != null);
            Contract.Requires(index >= 0);
            Contract.Requires(index < this.Count);
        }

        /// <summary>
        /// Gets the object that enables iterating through the members of the collection.
        /// </summary>
        /// 
        /// <returns>
        /// An object that implements the <see cref="T:System.Collections.IEnumerator"/> interface.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public IEnumerator GetEnumerator()
        {
            Contract.Ensures(Contract.Result<IEnumerator>() != null);
            return default(IEnumerator);
        }
    }   
}
