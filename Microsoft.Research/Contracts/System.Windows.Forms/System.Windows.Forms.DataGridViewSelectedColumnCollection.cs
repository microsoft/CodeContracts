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

namespace System.Windows.Forms
{
    /// <summary>
    /// Represents a collection of <see cref="T:System.Windows.Forms.DataGridViewColumn"/> objects that are selected in a <see cref="T:System.Windows.Forms.DataGridView"/>.
    /// </summary>
    
    [ListBindable(false)]
    public class DataGridViewSelectedColumnCollection : BaseCollection //, IList, ICollection, IEnumerable
    {
        
        /// <returns>
        /// An <see cref="T:System.Collections.ArrayList"/> containing the elements of the collection. This property returns null unless overridden in a derived class.
        /// </returns>
        // protected override ArrayList List {get;}
        
        /// <summary>
        /// Gets the column at the specified index.
        /// </summary>
        /// 
        /// <returns>
        /// The <see cref="T:System.Windows.Forms.DataGridViewColumn"/> at the specified index.
        /// </returns>
        /// <param name="index">The index of the <see cref="T:System.Windows.Forms.DataGridViewColumn"/> to get from the <see cref="T:System.Windows.Forms.DataGridViewSelectedColumnCollection"/>.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than 0.-or-<paramref name="index"/> is equal to or greater than the number of columns in the collection.</exception>
        public DataGridViewColumn this[int index]
        {
            get
            {
                Contract.Requires(index >= 0 && index < this.Count);
                return default(DataGridViewColumn);
            }
        }
        
        /// <summary>
        /// Clears the collection.
        /// </summary>
        /// <exception cref="T:System.NotSupportedException">Always thrown.</exception>
        //public void Clear()
        
        /// <summary>
        /// Determines whether the specified column is contained in the collection.
        /// </summary>
        /// 
        /// <returns>
        /// true if the <paramref name="dataGridViewColumn"/> parameter is in the collection; otherwise, false.
        /// </returns>
        /// <param name="dataGridViewColumn">A <see cref="T:System.Windows.Forms.DataGridViewColumn"/> to locate in the <see cref="T:System.Windows.Forms.DataGridViewSelectedColumnCollection"/>.</param>
        // public bool Contains(DataGridViewColumn dataGridViewColumn)
        
        /// <summary>
        /// Copies the elements of the collection to the specified array, starting at the specified index.
        /// </summary>
        /// <param name="array">The one-dimensional array that is the destination of the elements copied from the collection. The array must have zero-based indexing.</param><param name="index">The zero-based index in the array at which copying begins.</param><exception cref="T:System.ArgumentNullException"><paramref name="array"/> is null.</exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than zero.</exception><exception cref="T:System.ArgumentException"><paramref name="array"/> is multidimensional.-or-The number of elements in the <see cref="T:System.Windows.Forms.DataGridViewCellCollection"/> is greater than the available space from <paramref name="index"/> to the end of <paramref name="array"/>.</exception><exception cref="T:System.InvalidCastException">The <see cref="T:System.Windows.Forms.DataGridViewColumnCollection"/> cannot be cast automatically to the type of <paramref name="array"/>.</exception>
        // public void CopyTo(DataGridViewColumn[] array, int index)
        
        /// <summary>
        /// Inserts a column into the collection at the specified position.
        /// </summary>
        /// <param name="index">The zero-based index at which the column should be inserted. </param><param name="dataGridViewColumn">The <see cref="T:System.Windows.Forms.DataGridViewColumn"/> to insert into the <see cref="T:System.Windows.Forms.DataGridViewSelectedColumnCollection"/>.</param><exception cref="T:System.NotSupportedException">Always thrown.</exception>
        // public void Insert(int index, DataGridViewColumn dataGridViewColumn)
    }
}
