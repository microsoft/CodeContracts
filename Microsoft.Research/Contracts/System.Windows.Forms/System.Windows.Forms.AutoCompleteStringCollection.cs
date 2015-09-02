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
using System.Diagnostics.Contracts;
using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Windows.Forms
{
    /// <summary>
    /// Contains a collection of strings to use for the auto-complete feature on certain Windows Forms controls.
    /// </summary>
    
    public class AutoCompleteStringCollection // : IList, ICollection, IEnumerable
    {
    
        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// 
        /// <returns>
        /// The <see cref="T:System.String"/> at the specified position.
        /// </returns>
        /// <param name="index">The index at which to get or set the <see cref="T:System.String"/>.</param>
        public string this[int index]
        {
            get
            {
                Contract.Requires(index >= 0 && index < Count);
                return default(string);
            }
            set
            {
                Contract.Requires(index >= 0 && index < Count);
            }
        }

        /// <summary>
        /// Gets the number of items in the <see cref="T:System.Windows.Forms.AutoCompleteStringCollection"/> .
        /// </summary>
        /// 
        /// <returns>
        /// The number of items in the <see cref="T:System.Windows.Forms.AutoCompleteStringCollection"/>.
        /// </returns>
        /// 
        public int Count
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= 0);
                return default(int);
            }
        }
        
        /// <summary>
        /// Gets a value indicating whether the contents of the collection are read-only.
        /// </summary>
        /// 
        /// <returns>
        /// false in all cases.
        /// </returns>
        /// 
        public bool IsReadOnly
        {
            get
            {
                Contract.Ensures(Contract.Result<bool>() == false);
                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether access to the <see cref="T:System.Windows.Forms.AutoCompleteStringCollection"/> is synchronized (thread safe).
        /// </summary>
        /// 
        /// <returns>
        /// false in all cases.
        /// </returns>
        
        public bool IsSynchronized
        {
            get
            {
                Contract.Ensures(Contract.Result<bool>() == false);
                return false;
            }
        }

        /// <summary>
        /// Gets an object that can be used to synchronize access to the <see cref="T:System.Windows.Forms.AutoCompleteStringCollection"/>.
        /// </summary>
        /// 
        /// <returns>
        /// Returns this <see cref="T:System.Windows.Forms.AutoCompleteStringCollection"/>.
        /// </returns>
        /// 
        public object SyncRoot
        {
            // [HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
            get
            {
                Contract.Ensures(Contract.Result<object>() != null);
                return (object)this;
            }
        }
        
        // <summary>
        // Occurs when the collection changes.
        // </summary>
        // public event CollectionChangeEventHandler CollectionChanged {get; set;}
        
        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.AutoCompleteStringCollection.CollectionChanged"/> event.
        // </summary>
        // <param name="e">A <see cref="T:System.ComponentModel.CollectionChangeEventArgs"/> that contains the event data.</param>
        // protected void OnCollectionChanged(CollectionChangeEventArgs e)
        
        /// <summary>
        /// Inserts a new <see cref="T:System.String"/> into the collection.
        /// </summary>
        /// 
        /// <returns>
        /// The position in the collection where the <see cref="T:System.String"/> was added.
        /// </returns>
        /// <param name="value">The <see cref="T:System.String"/> to add to the collection.</param>
        public int Add(string value)
        {
            Contract.Ensures(Count == Contract.OldValue(Count)+1);
            Contract.Ensures(Contract.Result<int>() >= 0);
            return default(int);
        }

        /// <summary>
        /// Adds the elements of a <see cref="T:System.String"/> collection to the end.
        /// </summary>
        /// <param name="value">The strings to add to the collection.</param>
        public void AddRange(string[] value)
        {
            Contract.Requires(value != null);
            Contract.Ensures(Count == Contract.OldValue(Count) + value.Length);
        }

        /// <summary>
        /// Removes all strings from the collection.
        /// </summary>
        public void Clear()
        {
            Contract.Ensures(Count == 0);
        }

        // <summary>
        // Indicates whether the <see cref="T:System.String"/> exists within the collection.
        // </summary>
        // 
        // <returns>
        // true if the <see cref="T:System.String"/> exists within the collection; otherwise, false.
        // </returns>
        // <param name="value">The <see cref="T:System.String"/> for which to search.</param>
        // public bool Contains(string value)
       
        /// <summary>
        /// Copies an array of <see cref="T:System.String"/> objects into the collection, starting at the specified position.
        /// </summary>
        /// <param name="array">The <see cref="T:System.String"/> objects to add to the collection.</param><param name="index">The position within the collection at which to start the insertion. </param>
        public void CopyTo(string[] array, int index)
        {
            Contract.Requires(array != null);
            Contract.Requires(index >= 0 && index <= Count);
        }

        /// <summary>
        /// Obtains the position of the specified string within the collection.
        /// </summary>
        /// 
        /// <returns>
        /// The index for the specified item.
        /// </returns>
        /// <param name="value">The <see cref="T:System.String"/> for which to search.</param>
        public int IndexOf(string value)
        {
            Contract.Ensures(Contract.Result<int>() >= -1);
            return default(int);
        }

        /// <summary>
        /// Inserts the string into a specific index in the collection.
        /// </summary>
        /// <param name="index">The position at which to insert the string.</param><param name="value">The string to insert.</param>
        public void Insert(int index, string value)
        {
            Contract.Requires(index >= 0 && index <= Count);
        }

        // <summary>
        // Removes a string from the collection.
        // </summary>
        // <param name="value">The <see cref="T:System.String"/> to remove.</param>
        // public void Remove(string value)
        
        // <summary>
        // Removes the string at the specified index.
        // </summary>
        // <param name="index">The zero-based index of the string to remove.</param>
        public void RemoveAt(int index)
        {
            Contract.Requires(index >= 0 && index < Count);
            Contract.Ensures(Count == Contract.OldValue(Count)-1);
        }
        
        // <summary>
        // Returns an enumerator that iterates through the <see cref="T:System.Windows.Forms.AutoCompleteStringCollection"/>.
        // </summary>
        // 
        // <returns>
        // An enumerator that iterates through the collection.
        // </returns>
        public IEnumerator GetEnumerator()
        {
            Contract.Ensures(Contract.Result<IEnumerator>() != null);
            return default(IEnumerator);
        }
    }
}
