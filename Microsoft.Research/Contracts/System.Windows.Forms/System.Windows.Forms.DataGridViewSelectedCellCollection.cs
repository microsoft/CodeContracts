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
    /// Represents a collection of cells that are selected in a <see cref="T:System.Windows.Forms.DataGridView"/>.
    /// </summary>
    public class DataGridViewSelectedCellCollection : BaseCollection //,IList, ICollection, IEnumerable
    {
        
        // <summary>
        // Gets a list of elements in the collection.
        // </summary>
        // 
        // <returns>
        // An <see cref="T:System.Collections.ArrayList"/> containing the elements of the collection.
        // </returns>
        // protected override ArrayList List
        
        /// <summary>
        /// Gets the cell at the specified index.
        /// </summary>
        /// 
        /// <returns>
        /// The <see cref="T:System.Windows.Forms.DataGridViewCell"/> at the specified index.
        /// </returns>
        /// <param name="index">The index of the <see cref="T:System.Windows.Forms.DataGridViewCell"/> to get from the <see cref="T:System.Windows.Forms.DataGridViewSelectedCellCollection"/>.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than 0.-or-<paramref name="index"/> is equal to or greater than the number of cells in the collection.</exception>
        public DataGridViewCell this[int index]
        {
            get
            {
                Contract.Requires(index >= 0 && index < Count);
                return default(DataGridViewCell);
            }
        }
        
        // <summary>
        // Clears the collection.
        // </summary>
        // <exception cref="T:System.NotSupportedException">Always thrown.</exception>
        // public void Clear()
        
        // <summary>
        // Determines whether the specified cell is contained in the collection.
        // </summary>
        // 
        // <returns>
        // true if <paramref name="dataGridViewCell"/> is in the <see cref="T:System.Windows.Forms.DataGridViewSelectedCellCollection"/>; otherwise, false.
        // </returns>
        // <param name="dataGridViewCell">The <see cref="T:System.Windows.Forms.DataGridViewCell"/> to locate in the <see cref="T:System.Windows.Forms.DataGridViewSelectedCellCollection"/>.</param>
        // public bool Contains(DataGridViewCell dataGridViewCell)
        
        // <summary>
        // Copies the elements of the collection to the specified <see cref="T:System.Windows.Forms.DataGridViewCell"/> array, starting at the specified index.
        // </summary>
        // <param name="array">The one-dimensional array of type <see cref="T:System.Windows.Forms.DataGridViewCell"/> that is the destination of the elements copied from the collection. The array must have zero-based indexing.</param><param name="index">The zero-based index in <paramref name="array"/> at which copying begins.</param><exception cref="T:System.ArgumentNullException"><paramref name="array"/> is null.</exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than zero.</exception><exception cref="T:System.ArgumentException"><paramref name="array"/> is multidimensional.-or-The number of elements in the <see cref="T:System.Windows.Forms.DataGridViewCellCollection"/> is greater than the available space from <paramref name="index"/> to the end of <paramref name="array"/>.</exception><exception cref="T:System.InvalidCastException">The <see cref="T:System.Windows.Forms.DataGridViewCellCollection"/> cannot be cast automatically to the type of <paramref name="array"/>.</exception>
        // public void CopyTo(DataGridViewCell[] array, int index)
        
        // <summary>
        // Inserts a cell into the collection.
        // </summary>
        // <param name="index">The index at which <paramref name="dataGridViewCell"/> should be inserted.</param><param name="dataGridViewCell">The object to be added to the <see cref="T:System.Windows.Forms.DataGridViewSelectedCellCollection"/>.</param><exception cref="T:System.NotSupportedException">Always thrown.</exception>
        // public void Insert(int index, DataGridViewCell dataGridViewCell)
        
    }
}
