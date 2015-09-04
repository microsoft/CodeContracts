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
    /// Represents a collection of cells in a <see cref="T:System.Windows.Forms.DataGridViewRow"/>.
    /// </summary>
    
    public class DataGridViewCellCollection : BaseCollection // IList, ICollection, IEnumerable
    {
        // <summary>
        // Gets an <see cref="T:System.Collections.ArrayList"/> containing <see cref="T:System.Windows.Forms.DataGridViewCellCollection"/> objects.
        // </summary>
        // <returns>
        // <see cref="T:System.Collections.ArrayList"/>.
        // </returns>
        // protected override ArrayList List
        
        /// <summary>
        /// Gets or sets the cell at the provided index location. In C#, this property is the indexer for the <see cref="T:System.Windows.Forms.DataGridViewCellCollection"/> class.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Windows.Forms.DataGridViewCell"/> stored at the given index.
        /// </returns>
        /// <param name="index">The zero-based index of the cell to get or set.</param><exception cref="T:System.ArgumentNullException">The specified value when setting this property is null.</exception><exception cref="T:System.InvalidOperationException">The specified cell when setting this property already belongs to a <see cref="T:System.Windows.Forms.DataGridView"/> control.-or-The specified cell when setting this property already belongs to a <see cref="T:System.Windows.Forms.DataGridViewRow"/>.</exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than 0.-or-<paramref name="index"/> is equal to or greater than the number of cells in the collection.</exception>
        public DataGridViewCell this[int index]
        {
            get
            {
                Contract.Requires(index >= 0 && index <= Count);
                return default(DataGridViewCell);
            }
            set
            {
                Contract.Requires(index >= 0 && index <= Count);
                Contract.Requires(value != null && value.DataGridView != null && value.OwningRow != null);
            }
        }

        // <summary>
        // Gets or sets the cell in the column with the provided name. In C#, this property is the indexer for the <see cref="T:System.Windows.Forms.DataGridViewCellCollection"/> class.
        // </summary>
        // <returns>
        // The <see cref="T:System.Windows.Forms.DataGridViewCell"/> stored in the column with the given name.
        // </returns>
        // <param name="columnName">The name of the column in which to get or set the cell.</param><exception cref="T:System.ArgumentException"><paramref name="columnName"/> does not match the name of any columns in the control.</exception><exception cref="T:System.ArgumentNullException">The specified value when setting this property is null.</exception><exception cref="T:System.InvalidOperationException">The specified cell when setting this property already belongs to a <see cref="T:System.Windows.Forms.DataGridView"/> control.-or-The specified cell when setting this property already belongs to a <see cref="T:System.Windows.Forms.DataGridViewRow"/>.</exception>
        // public DataGridViewCell this[string columnName]
        
        // <summary>
        // Occurs when the collection is changed.
        // </summary>
        // public event CollectionChangeEventHandler CollectionChanged
        
        // <summary>
        // Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCellCollection"/> class.
        // </summary>
        // <param name="dataGridViewRow">The <see cref="T:System.Windows.Forms.DataGridViewRow"/> that owns the collection.</param>
        // public DataGridViewCellCollection(DataGridViewRow dataGridViewRow)
     
        // <summary>
        // Adds a cell to the collection.
        // </summary>
        // <returns>
        // The position in which to insert the new element.
        // </returns>
        // <param name="dataGridViewCell">A <see cref="T:System.Windows.Forms.DataGridViewCell"/> to add to the collection.</param><exception cref="T:System.InvalidOperationException">The row that owns this <see cref="T:System.Windows.Forms.DataGridViewCellCollection"/> already belongs to a <see cref="T:System.Windows.Forms.DataGridView"/> control.-or-<paramref name="dataGridViewCell"/> already belongs to a <see cref="T:System.Windows.Forms.DataGridViewRow"/>.</exception>
        // public virtual int Add(DataGridViewCell dataGridViewCell)
        
        /// <summary>
        /// Adds an array of cells to the collection.
        /// </summary>
        /// <param name="dataGridViewCells">The array of <see cref="T:System.Windows.Forms.DataGridViewCell"/> objects to add to the collection.</param><exception cref="T:System.ArgumentNullException"><paramref name="dataGridViewCells"/> is null.</exception><exception cref="T:System.InvalidOperationException">The row that owns this <see cref="T:System.Windows.Forms.DataGridViewCellCollection"/> already belongs to a <see cref="T:System.Windows.Forms.DataGridView"/> control.-or-At least one value in <paramref name="dataGridViewCells"/> is null.-or-At least one cell in <paramref name="dataGridViewCells"/> already belongs to a <see cref="T:System.Windows.Forms.DataGridViewRow"/>.-or-At least two values in <paramref name="dataGridViewCells"/> are references to the same <see cref="T:System.Windows.Forms.DataGridViewCell"/>.</exception>
        public virtual void AddRange(params DataGridViewCell[] dataGridViewCells)
        {
            Contract.Requires(dataGridViewCells != null);
        }

        // <summary>
        // Clears all cells from the collection.
        // </summary>
        // <exception cref="T:System.InvalidOperationException">The row that owns this <see cref="T:System.Windows.Forms.DataGridViewCellCollection"/> already belongs to a <see cref="T:System.Windows.Forms.DataGridView"/> control.</exception>
        // public virtual void Clear()
        
        // <summary>
        // Copies the entire collection of cells into an array at a specified location within the array.
        // </summary>
        // <param name="array">The destination array to which the contents will be copied.</param><param name="index">The index of the element in <paramref name="array"/> at which to start copying.</param>
        // public void CopyTo(DataGridViewCell[] array, int index)
        
        // <summary>
        // Determines whether the specified cell is contained in the collection.
        // </summary>
        // <returns>
        // true if <paramref name="dataGridViewCell"/> is in the collection; otherwise, false.
        // </returns>
        // <param name="dataGridViewCell">A <see cref="T:System.Windows.Forms.DataGridViewCell"/> to locate in the collection.</param>
        // public virtual bool Contains(DataGridViewCell dataGridViewCell)
        
        // <summary>
        // Returns the index of the specified cell.
        // </summary>
        // <returns>
        // The zero-based index of the value of <paramref name="dataGridViewCell"/> parameter, if it is found in the collection; otherwise, -1.
        // </returns>
        // <param name="dataGridViewCell">The cell to locate in the collection.</param>
        // public int IndexOf(DataGridViewCell dataGridViewCell)
        
        /// <summary>
        /// Inserts a cell into the collection at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which to place <paramref name="dataGridViewCell"/>.</param><param name="dataGridViewCell">The <see cref="T:System.Windows.Forms.DataGridViewCell"/> to insert.</param><exception cref="T:System.InvalidOperationException">The row that owns this <see cref="T:System.Windows.Forms.DataGridViewCellCollection"/> already belongs to a <see cref="T:System.Windows.Forms.DataGridView"/> control.-or-<paramref name="dataGridViewCell"/> already belongs to a <see cref="T:System.Windows.Forms.DataGridViewRow"/>.</exception>
        public virtual void Insert(int index, DataGridViewCell dataGridViewCell)
        {
            Contract.Requires(index >= 0 && index <= Count);
        }
        
        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridViewCellCollection.CollectionChanged"/> event.
        // </summary>
        // <param name="e">A <see cref="T:System.ComponentModel.CollectionChangeEventArgs"/> that contains the event data. </param>
        // protected void OnCollectionChanged(CollectionChangeEventArgs e)
        
        // <summary>
        // Removes the specified cell from the collection.
        // </summary>
        // <param name="cell">The <see cref="T:System.Windows.Forms.DataGridViewCell"/> to remove from the collection.</param><exception cref="T:System.InvalidOperationException">The row that owns this <see cref="T:System.Windows.Forms.DataGridViewCellCollection"/> already belongs to a <see cref="T:System.Windows.Forms.DataGridView"/> control.</exception><exception cref="T:System.ArgumentException"><paramref name="cell"/> could not be found in the collection.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public virtual void Remove(DataGridViewCell cell)
        
        /// <summary>
        /// Removes the cell at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the <see cref="T:System.Windows.Forms.DataGridViewCell"/> to be removed.</param><exception cref="T:System.InvalidOperationException">The row that owns this <see cref="T:System.Windows.Forms.DataGridViewCellCollection"/> already belongs to a <see cref="T:System.Windows.Forms.DataGridView"/> control.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public virtual void RemoveAt(int index)
        {
            Contract.Requires(index >= 0 && index <= Count);
        }
    }
}
