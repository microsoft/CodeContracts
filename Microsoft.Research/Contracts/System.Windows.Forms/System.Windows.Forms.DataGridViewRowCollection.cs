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
using System.Collections;
using System.Diagnostics.Contracts;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Globalization;

namespace System.Windows.Forms
{
    /// <summary>
    /// A collection of <see cref="T:System.Windows.Forms.DataGridViewRow"/> objects.
    /// </summary>
    
    public class DataGridViewRowCollection // : IList, ICollection, IEnumerable
    {
        
        /// <summary>
        /// Gets the number of rows in the collection.
        /// </summary>
        /// 
        /// <returns>
        /// The number of rows in the <see cref="T:System.Windows.Forms.DataGridViewRowCollection"/>.
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
        /// Gets an array of <see cref="T:System.Windows.Forms.DataGridViewRow"/> objects.
        /// </summary>
        /// 
        /// <returns>
        /// An array of <see cref="T:System.Windows.Forms.DataGridViewRow"/> objects.
        /// </returns>
        // protected ArrayList List {get;}

        internal ArrayList SharedList { get; }
        
        /// <summary>
        /// Gets the <see cref="T:System.Windows.Forms.DataGridView"/> that owns the collection.
        /// </summary>
        /// 
        /// <returns>
        /// The <see cref="T:System.Windows.Forms.DataGridView"/> that owns the <see cref="T:System.Windows.Forms.DataGridViewRowCollection"/>.
        /// </returns>
        protected DataGridView DataGridView {get;}

        /// <summary>
        /// Gets the <see cref="T:System.Windows.Forms.DataGridViewRow"/> at the specified index.
        /// </summary>
        /// 
        /// <returns>
        /// The <see cref="T:System.Windows.Forms.DataGridViewRow"/> at the specified index. Accessing a <see cref="T:System.Windows.Forms.DataGridViewRow"/> with this indexer causes the row to become unshared. To keep the row shared, use the <see cref="M:System.Windows.Forms.DataGridViewRowCollection.SharedRow(System.Int32)"/> method. For more information, see Best Practices for Scaling the Windows Forms DataGridView Control.
        /// </returns>
        /// <param name="index">The zero-based index of the <see cref="T:System.Windows.Forms.DataGridViewRow"/> to get.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than 0.- or -<paramref name="index"/> is equal to or greater than <see cref="P:System.Windows.Forms.DataGridViewRowCollection.Count"/>.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public DataGridViewRow this[int index]
        {
            get
            {
                Contract.Requires(index >= 0 && index < this.Count);
                return default(DataGridViewRow);
            }
        }

        /// <summary>
        /// Occurs when the contents of the collection change.
        /// </summary>
        /// 
        // public event CollectionChangeEventHandler CollectionChanged
        
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewRowCollection"/> class.
        /// </summary>
        /// <param name="dataGridView">The <see cref="T:System.Windows.Forms.DataGridView"/> that owns the <see cref="T:System.Windows.Forms.DataGridViewRowCollection"/>.</param>
        // public DataGridViewRowCollection(DataGridView dataGridView)
        
        /// <summary>
        /// Returns the <see cref="T:System.Windows.Forms.DataGridViewRow"/> at the specified index.
        /// </summary>
        /// 
        /// <returns>
        /// The <see cref="T:System.Windows.Forms.DataGridViewRow"/> positioned at the specified index.
        /// </returns>
        /// <param name="rowIndex">The index of the <see cref="T:System.Windows.Forms.DataGridViewRow"/> to get.</param>
        public DataGridViewRow SharedRow(int rowIndex)
        {
            Contract.Requires(rowIndex >= 0 && rowIndex < SharedList.Count);
            return default(DataGridViewRow);
        }

        /// <summary>
        /// Adds a new row to the collection.
        /// </summary>
        /// 
        /// <returns>
        /// The index of the new row.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView"/> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView"/> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter"/><see cref="E:System.Windows.Forms.DataGridView.CellLeave"/><see cref="E:System.Windows.Forms.DataGridView.CellValidating"/><see cref="E:System.Windows.Forms.DataGridView.CellValidated"/><see cref="E:System.Windows.Forms.DataGridView.RowEnter"/><see cref="E:System.Windows.Forms.DataGridView.RowLeave"/><see cref="E:System.Windows.Forms.DataGridView.RowValidated"/><see cref="E:System.Windows.Forms.DataGridView.RowValidating"/>-or-The <see cref="P:System.Windows.Forms.DataGridView.DataSource"/> property of the <see cref="T:System.Windows.Forms.DataGridView"/> is not null.-or-The <see cref="T:System.Windows.Forms.DataGridView"/> has no columns.-or-This operation would add a frozen row after unfrozen rows.</exception><exception cref="T:System.ArgumentException">The row returned by the <see cref="P:System.Windows.Forms.DataGridView.RowTemplate"/> property has more cells than there are columns in the control.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int Add()
        {
            Contract.Requires(this.DataGridView.DataSource == null);
            Contract.Requires(this.DataGridView.NoDimensionChangeAllowed == false);
            Contract.Ensures(Contract.Result<int>() >= 0);
            Contract.Ensures(this.Count == Contract.OldValue(this.Count) + 1);
            return default(int);
        }
        
        /// <summary>
        /// Adds a new row to the collection, and populates the cells with the specified objects.
        /// </summary>
        /// 
        /// <returns>
        /// The index of the new row.
        /// </returns>
        /// <param name="values">A variable number of objects that populate the cells of the new <see cref="T:System.Windows.Forms.DataGridViewRow"/>.</param><exception cref="T:System.ArgumentNullException"><paramref name="values"/> is null.</exception><exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView"/> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView"/> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter"/><see cref="E:System.Windows.Forms.DataGridView.CellLeave"/><see cref="E:System.Windows.Forms.DataGridView.CellValidating"/><see cref="E:System.Windows.Forms.DataGridView.CellValidated"/><see cref="E:System.Windows.Forms.DataGridView.RowEnter"/><see cref="E:System.Windows.Forms.DataGridView.RowLeave"/><see cref="E:System.Windows.Forms.DataGridView.RowValidated"/><see cref="E:System.Windows.Forms.DataGridView.RowValidating"/>-or-The <see cref="P:System.Windows.Forms.DataGridView.VirtualMode"/> property of the <see cref="T:System.Windows.Forms.DataGridView"/> is set to true.- or -The <see cref="P:System.Windows.Forms.DataGridView.DataSource"/> property of the <see cref="T:System.Windows.Forms.DataGridView"/> is not null.-or-The <see cref="T:System.Windows.Forms.DataGridView"/> has no columns. -or-The row returned by the <see cref="P:System.Windows.Forms.DataGridView.RowTemplate"/> property has more cells than there are columns in the control.-or-This operation would add a frozen row after unfrozen rows.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int Add(params object[] values)
        {
            Contract.Requires(values != null);
            Contract.Requires(this.DataGridView.VirtualMode == false);
            Contract.Requires(this.DataGridView.DataSource == null);
            Contract.Requires(this.DataGridView.NoDimensionChangeAllowed == false);
            Contract.Ensures(Contract.Result<int>() >= 0);
            Contract.Ensures(this.Count == Contract.OldValue(this.Count) + 1);
            return default(int);
        }

        /// <summary>
        /// Adds the specified <see cref="T:System.Windows.Forms.DataGridViewRow"/> to the collection.
        /// </summary>
        /// 
        /// <returns>
        /// The index of the new <see cref="T:System.Windows.Forms.DataGridViewRow"/>.
        /// </returns>
        /// <param name="dataGridViewRow">The <see cref="T:System.Windows.Forms.DataGridViewRow"/> to add to the <see cref="T:System.Windows.Forms.DataGridViewRowCollection"/>.</param><exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView"/> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView"/> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter"/><see cref="E:System.Windows.Forms.DataGridView.CellLeave"/><see cref="E:System.Windows.Forms.DataGridView.CellValidating"/><see cref="E:System.Windows.Forms.DataGridView.CellValidated"/><see cref="E:System.Windows.Forms.DataGridView.RowEnter"/><see cref="E:System.Windows.Forms.DataGridView.RowLeave"/><see cref="E:System.Windows.Forms.DataGridView.RowValidated"/><see cref="E:System.Windows.Forms.DataGridView.RowValidating"/>-or-The <see cref="P:System.Windows.Forms.DataGridView.DataSource"/> property of the <see cref="T:System.Windows.Forms.DataGridView"/> is not null.-or-The <see cref="T:System.Windows.Forms.DataGridView"/> has no columns.-or-The <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView"/> property of the <paramref name="dataGridViewRow"/> is not null.-or-<paramref name="dataGridViewRow"/> has a <see cref="P:System.Windows.Forms.DataGridViewRow.Selected"/> property value of true. -or-This operation would add a frozen row after unfrozen rows.</exception><exception cref="T:System.ArgumentNullException"><paramref name="dataGridViewRow"/> is null.</exception><exception cref="T:System.ArgumentException"><paramref name="dataGridViewRow"/> has more cells than there are columns in the control.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public virtual int Add(DataGridViewRow dataGridViewRow)
        {
            Contract.Requires(this.DataGridView.Columns.Count > 0);
            Contract.Requires(this.DataGridView.DataSource == null);
            Contract.Requires(this.DataGridView.NoDimensionChangeAllowed == false);
            Contract.Ensures(Contract.Result<int>() >= 0);
            Contract.Ensures(this.Count == Contract.OldValue(this.Count) + 1);
            return default(int);
        }

        /// <summary>
        /// Adds the specified number of new rows to the collection.
        /// </summary>
        /// 
        /// <returns>
        /// The index of the last row that was added.
        /// </returns>
        /// <param name="count">The number of rows to add to the <see cref="T:System.Windows.Forms.DataGridViewRowCollection"/>.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="count"/> is less than 1.</exception><exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView"/> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView"/> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter"/><see cref="E:System.Windows.Forms.DataGridView.CellLeave"/><see cref="E:System.Windows.Forms.DataGridView.CellValidating"/><see cref="E:System.Windows.Forms.DataGridView.CellValidated"/><see cref="E:System.Windows.Forms.DataGridView.RowEnter"/><see cref="E:System.Windows.Forms.DataGridView.RowLeave"/><see cref="E:System.Windows.Forms.DataGridView.RowValidated"/><see cref="E:System.Windows.Forms.DataGridView.RowValidating"/>-or-The <see cref="P:System.Windows.Forms.DataGridView.DataSource"/> property of the <see cref="T:System.Windows.Forms.DataGridView"/> is not null.-or-The <see cref="T:System.Windows.Forms.DataGridView"/> has no columns.-or-The row returned by the <see cref="P:System.Windows.Forms.DataGridView.RowTemplate"/> property has more cells than there are columns in the control. -or-This operation would add frozen rows after unfrozen rows.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual int Add(int count)
        {
            Contract.Requires(count >= 1);
            Contract.Requires(this.DataGridView.Columns.Count > 0);
            Contract.Requires(this.DataGridView.DataSource == null);
            Contract.Requires(this.DataGridView.NoDimensionChangeAllowed == false);
            Contract.Requires(this.DataGridView.RowTemplate.Cells.Count <= this.DataGridView.Columns.Count);
            Contract.Ensures(Contract.Result<int>() >= 0);
            Contract.Ensures(this.Count == Contract.OldValue(this.Count) + 1);
            return default(int);
        }
        
        /// <summary>
        /// Adds a new row based on the row at the specified index.
        /// </summary>
        /// 
        /// <returns>
        /// The index of the new row.
        /// </returns>
        /// <param name="indexSource">The index of the row on which to base the new row.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="indexSource"/> is less than zero or greater than or equal to the number of rows in the collection.</exception><exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView"/> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView"/> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter"/><see cref="E:System.Windows.Forms.DataGridView.CellLeave"/><see cref="E:System.Windows.Forms.DataGridView.CellValidating"/><see cref="E:System.Windows.Forms.DataGridView.CellValidated"/><see cref="E:System.Windows.Forms.DataGridView.RowEnter"/><see cref="E:System.Windows.Forms.DataGridView.RowLeave"/><see cref="E:System.Windows.Forms.DataGridView.RowValidated"/><see cref="E:System.Windows.Forms.DataGridView.RowValidating"/>-or-The <see cref="P:System.Windows.Forms.DataGridView.DataSource"/> property of the <see cref="T:System.Windows.Forms.DataGridView"/> is not null.-or-This operation would add a frozen row after unfrozen rows.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public virtual int AddCopy(int indexSource)
        {   
            Contract.Requires(indexSource >= 0 && indexSource < this.Count);
            Contract.Requires(this.DataGridView.DataSource == null);
            Contract.Requires(this.DataGridView.NoDimensionChangeAllowed == false);
            Contract.Ensures(Contract.Result<int>() >= 0);
            Contract.Ensures(this.Count == Contract.OldValue(this.Count) + 1);
            return default(int);
        }
        
        /// <summary>
        /// Adds the specified number of rows to the collection based on the row at the specified index.
        /// </summary>
        /// 
        /// <returns>
        /// The index of the last row that was added.
        /// </returns>
        /// <param name="indexSource">The index of the row on which to base the new rows.</param><param name="count">The number of rows to add to the <see cref="T:System.Windows.Forms.DataGridViewRowCollection"/>.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="indexSource"/> is less than zero or greater than or equal to the number of rows in the control.-or-<paramref name="count"/> is less than zero.</exception><exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView"/> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView"/> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter"/><see cref="E:System.Windows.Forms.DataGridView.CellLeave"/><see cref="E:System.Windows.Forms.DataGridView.CellValidating"/><see cref="E:System.Windows.Forms.DataGridView.CellValidated"/><see cref="E:System.Windows.Forms.DataGridView.RowEnter"/><see cref="E:System.Windows.Forms.DataGridView.RowLeave"/><see cref="E:System.Windows.Forms.DataGridView.RowValidated"/><see cref="E:System.Windows.Forms.DataGridView.RowValidating"/>-or-The <see cref="P:System.Windows.Forms.DataGridView.DataSource"/> property of the <see cref="T:System.Windows.Forms.DataGridView"/> is not null.-or-This operation would add a frozen row after unfrozen rows.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public virtual int AddCopies(int indexSource, int count)
        {
            Contract.Requires(indexSource >= 0 && indexSource < this.Count);
            Contract.Requires(this.DataGridView.DataSource == null);
            Contract.Requires(this.DataGridView.NoDimensionChangeAllowed == false);
            Contract.Ensures(Contract.Result<int>() >= 0);
            Contract.Ensures(this.Count == Contract.OldValue(this.Count) + count);
            return default(int);
        }
        
        /// <summary>
        /// Adds the specified <see cref="T:System.Windows.Forms.DataGridViewRow"/> objects to the collection.
        /// </summary>
        /// <param name="dataGridViewRows">An array of <see cref="T:System.Windows.Forms.DataGridViewRow"/> objects to be added to the <see cref="T:System.Windows.Forms.DataGridViewRowCollection"/>.</param><exception cref="T:System.ArgumentNullException"><paramref name="dataGridViewRows"/> is null.</exception><exception cref="T:System.ArgumentException"><paramref name="dataGridViewRows"/> contains only one row, and the row it contains has more cells than there are columns in the control.</exception><exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView"/> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView"/> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter"/><see cref="E:System.Windows.Forms.DataGridView.CellLeave"/><see cref="E:System.Windows.Forms.DataGridView.CellValidating"/><see cref="E:System.Windows.Forms.DataGridView.CellValidated"/><see cref="E:System.Windows.Forms.DataGridView.RowEnter"/><see cref="E:System.Windows.Forms.DataGridView.RowLeave"/><see cref="E:System.Windows.Forms.DataGridView.RowValidated"/><see cref="E:System.Windows.Forms.DataGridView.RowValidating"/>-or-The <see cref="P:System.Windows.Forms.DataGridView.DataSource"/> property of the <see cref="T:System.Windows.Forms.DataGridView"/> is not null.-or-At least one entry in the <paramref name="dataGridViewRows"/> array is null.-or-The <see cref="T:System.Windows.Forms.DataGridView"/> has no columns.-or-At least one row in the <paramref name="dataGridViewRows"/> array has a <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView"/> property value that is not null.-or-At least one row in the <paramref name="dataGridViewRows"/> array has a <see cref="P:System.Windows.Forms.DataGridViewRow.Selected"/> property value of true.-or-Two or more rows in the <paramref name="dataGridViewRows"/> array are identical.-or-At least one row in the <paramref name="dataGridViewRows"/> array contains one or more cells of a type that is incompatible with the type of the corresponding column in the control.-or-At least one row in the <paramref name="dataGridViewRows"/> array contains more cells than there are columns in the control.-or-This operation would add frozen rows after unfrozen rows.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual void AddRange(params DataGridViewRow[] dataGridViewRows)
        {
            Contract.Requires(dataGridViewRows != null);
            Contract.Requires(this.DataGridView.Columns.Count > 0);
            Contract.Requires(this.DataGridView.NoDimensionChangeAllowed == false);
            Contract.Ensures(Contract.Result<int>() >= 0);
            Contract.Ensures(this.Count == Contract.OldValue(this.Count) + dataGridViewRows.Length);
        }

        /// <summary>
        /// Clears the collection.
        /// </summary>
        /// <exception cref="T:System.InvalidOperationException">The collection is data bound and the underlying data source does not support clearing the row data.-or-The associated <see cref="T:System.Windows.Forms.DataGridView"/> control is performing one of the following actions that temporarily prevents the row collection from being modified:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView"/> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter"/><see cref="E:System.Windows.Forms.DataGridView.CellLeave"/><see cref="E:System.Windows.Forms.DataGridView.CellValidating"/><see cref="E:System.Windows.Forms.DataGridView.CellValidated"/><see cref="E:System.Windows.Forms.DataGridView.RowEnter"/><see cref="E:System.Windows.Forms.DataGridView.RowLeave"/><see cref="E:System.Windows.Forms.DataGridView.RowValidated"/><see cref="E:System.Windows.Forms.DataGridView.RowValidating"/></exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public virtual void Clear()
        {
            Contract.Requires(this.DataGridView.NoDimensionChangeAllowed == false);
            Contract.Ensures(this.Count == 0);
        }
        
        /// <summary>
        /// Determines whether the specified <see cref="T:System.Windows.Forms.DataGridViewRow"/> is in the collection.
        /// </summary>
        /// 
        /// <returns>
        /// true if the <see cref="T:System.Windows.Forms.DataGridViewRow"/> is in the <see cref="T:System.Windows.Forms.DataGridViewRowCollection"/>; otherwise, false.
        /// </returns>
        /// <param name="dataGridViewRow">The <see cref="T:System.Windows.Forms.DataGridViewRow"/> to locate in the <see cref="T:System.Windows.Forms.DataGridViewRowCollection"/>.</param>
        // public virtual bool Contains(DataGridViewRow dataGridViewRow)
        
        /// <summary>
        /// Copies the items from the collection into the specified <see cref="T:System.Windows.Forms.DataGridViewRow"/> array, starting at the specified index.
        /// </summary>
        /// <param name="array">A <see cref="T:System.Windows.Forms.DataGridViewRow"/> array that is the destination of the items copied from the <see cref="T:System.Windows.Forms.DataGridViewRowCollection"/>.</param><param name="index">The zero-based index in <paramref name="array"/> at which copying begins.</param><exception cref="T:System.ArgumentNullException"><paramref name="array"/> is null. </exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than zero. </exception><exception cref="T:System.ArgumentException"><paramref name="array"/> is multidimensional.-or- The number of elements in the <see cref="T:System.Windows.Forms.DataGridViewRowCollection"/> is greater than the available space from <paramref name="index"/> to the end of <paramref name="array"/>. </exception>
        public void CopyTo(DataGridViewRow[] array, int index)
        {
            Contract.Requires(array != null);
            Contract.Requires(index >= 0 && index <= this.Count);
        }
        
        /// <summary>
        /// Returns the index of the first <see cref="T:System.Windows.Forms.DataGridViewRow"/> that meets the specified criteria.
        /// </summary>
        /// 
        /// <returns>
        /// The index of the first <see cref="T:System.Windows.Forms.DataGridViewRow"/> that has the attributes specified by <paramref name="includeFilter"/>; -1 if no row is found.
        /// </returns>
        /// <param name="includeFilter">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates"/> values.</param><exception cref="T:System.ArgumentException"><paramref name="includeFilter"/> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates"/> values.</exception>
        public int GetFirstRow(DataGridViewElementStates includeFilter)
        {
            Contract.Requires(
                !((includeFilter &
                  ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen |
                    DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable |
                    DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) !=
                DataGridViewElementStates.None));
            Contract.Ensures(Contract.Result<int>() >= -1);
            return default(int);
        }

        /// <summary>
        /// Returns the index of the first <see cref="T:System.Windows.Forms.DataGridViewRow"/> that meets the specified inclusion and exclusion criteria.
        /// </summary>
        /// 
        /// <returns>
        /// The index of the first <see cref="T:System.Windows.Forms.DataGridViewRow"/> that has the attributes specified by <paramref name="includeFilter"/>, and does not have the attributes specified by <paramref name="excludeFilter"/>; -1 if no row is found.
        /// </returns>
        /// <param name="includeFilter">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates"/> values.</param><param name="excludeFilter">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates"/> values.</param><exception cref="T:System.ArgumentException">One or both of the specified filter values is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates"/> values.</exception>
        public int GetFirstRow(DataGridViewElementStates includeFilter, DataGridViewElementStates excludeFilter)
        {
            Contract.Requires(
                !((includeFilter &
                  ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen |
                    DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable |
                    DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) !=
                DataGridViewElementStates.None));
            Contract.Requires(excludeFilter == DataGridViewElementStates.None ||
                !((excludeFilter &
                    ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen |
                    DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable |
                    DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) !=
                DataGridViewElementStates.None));
            Contract.Ensures(Contract.Result<int>() >= -1);
            return default(int);
        }

        /// <summary>
        /// Returns the index of the last <see cref="T:System.Windows.Forms.DataGridViewRow"/> that meets the specified criteria.
        /// </summary>
        /// 
        /// <returns>
        /// The index of the last <see cref="T:System.Windows.Forms.DataGridViewRow"/> that has the attributes specified by <paramref name="includeFilter"/>; -1 if no row is found.
        /// </returns>
        /// <param name="includeFilter">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates"/> values.</param><exception cref="T:System.ArgumentException"><paramref name="includeFilter"/> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates"/> values.</exception>
        public int GetLastRow(DataGridViewElementStates includeFilter)
        {
            Contract.Requires(
               !((includeFilter &
                 ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen |
                   DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable |
                   DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) !=
               DataGridViewElementStates.None));
            Contract.Ensures(Contract.Result<int>() >= -1);
            return default(int);
        }
        
        /// <summary>
        /// Returns the index of the next <see cref="T:System.Windows.Forms.DataGridViewRow"/> that meets the specified criteria.
        /// </summary>
        /// 
        /// <returns>
        /// The index of the first <see cref="T:System.Windows.Forms.DataGridViewRow"/> after <paramref name="indexStart"/> that has the attributes specified by <paramref name="includeFilter"/>, or -1 if no row is found.
        /// </returns>
        /// <param name="indexStart">The index of the row where the method should begin to look for the next <see cref="T:System.Windows.Forms.DataGridViewRow"/>.</param><param name="includeFilter">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates"/> values.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="indexStart"/> is less than -1.</exception><exception cref="T:System.ArgumentException"><paramref name="includeFilter"/> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates"/> values.</exception>
        public int GetNextRow(int indexStart, DataGridViewElementStates includeFilter)
        {
            Contract.Requires(indexStart < -1);
            Contract.Requires(
                !((includeFilter &
                  ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen |
                    DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable |
                    DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) !=
                DataGridViewElementStates.None));
            Contract.Ensures(Contract.Result<int>() >= -1);
            return default(int);
        }

        /// <summary>
        /// Returns the index of the next <see cref="T:System.Windows.Forms.DataGridViewRow"/> that meets the specified inclusion and exclusion criteria.
        /// </summary>
        /// 
        /// <returns>
        /// The index of the next <see cref="T:System.Windows.Forms.DataGridViewRow"/> that has the attributes specified by <paramref name="includeFilter"/>, and does not have the attributes specified by <paramref name="excludeFilter"/>; -1 if no row is found.
        /// </returns>
        /// <param name="indexStart">The index of the row where the method should begin to look for the next <see cref="T:System.Windows.Forms.DataGridViewRow"/>.</param><param name="includeFilter">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates"/> values.</param><param name="excludeFilter">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates"/> values.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="indexStart"/> is less than -1.</exception><exception cref="T:System.ArgumentException">One or both of the specified filter values is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates"/> values.</exception>
        public int GetNextRow(int indexStart, DataGridViewElementStates includeFilter, DataGridViewElementStates excludeFilter)
        {
            Contract.Requires(indexStart < -1);
            Contract.Requires(
                !((includeFilter &
                  ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen |
                    DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable |
                    DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) !=
                DataGridViewElementStates.None));
            Contract.Requires(excludeFilter == DataGridViewElementStates.None ||
                !((excludeFilter &
                    ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen |
                    DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable |
                    DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) !=
                DataGridViewElementStates.None));
            Contract.Ensures(Contract.Result<int>() >= -1);
            return default(int);
        }

        /// <summary>
        /// Returns the index of the previous <see cref="T:System.Windows.Forms.DataGridViewRow"/> that meets the specified criteria.
        /// </summary>
        /// 
        /// <returns>
        /// The index of the previous <see cref="T:System.Windows.Forms.DataGridViewRow"/> that has the attributes specified by <paramref name="includeFilter"/>; -1 if no row is found.
        /// </returns>
        /// <param name="indexStart">The index of the row where the method should begin to look for the previous <see cref="T:System.Windows.Forms.DataGridViewRow"/>.</param><param name="includeFilter">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates"/> values.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="indexStart"/> is greater than the number of rows in the collection.</exception><exception cref="T:System.ArgumentException"><paramref name="includeFilter"/> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates"/> values.</exception>
        public int GetPreviousRow(int indexStart, DataGridViewElementStates includeFilter)
        {
            Contract.Requires(indexStart <= this.Count);
            Contract.Requires(
                !((includeFilter &
                  ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen |
                    DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable |
                    DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) !=
                DataGridViewElementStates.None));
            Contract.Ensures(Contract.Result<int>() >= -1);
            return default(int);
        }

        /// <summary>
        /// Returns the index of the previous <see cref="T:System.Windows.Forms.DataGridViewRow"/> that meets the specified inclusion and exclusion criteria.
        /// </summary>
        /// 
        /// <returns>
        /// The index of the previous <see cref="T:System.Windows.Forms.DataGridViewRow"/> that has the attributes specified by <paramref name="includeFilter"/>, and does not have the attributes specified by <paramref name="excludeFilter"/>; -1 if no row is found.
        /// </returns>
        /// <param name="indexStart">The index of the row where the method should begin to look for the previous <see cref="T:System.Windows.Forms.DataGridViewRow"/>.</param><param name="includeFilter">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates"/> values.</param><param name="excludeFilter">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates"/> values.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="indexStart"/> is greater than the number of rows in the collection.</exception><exception cref="T:System.ArgumentException">One or both of the specified filter values is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates"/> values.</exception>
        public int GetPreviousRow(int indexStart, DataGridViewElementStates includeFilter, DataGridViewElementStates excludeFilter)
        {
            Contract.Requires(indexStart <= this.Count);
            Contract.Requires(
               !((includeFilter &
                 ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen |
                   DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable |
                   DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) !=
               DataGridViewElementStates.None));
            Contract.Requires(excludeFilter == DataGridViewElementStates.None ||
                !((excludeFilter &
                    ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen |
                    DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable |
                    DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) !=
                DataGridViewElementStates.None));
            Contract.Ensures(Contract.Result<int>() >= -1);
            return default(int);
        }

        /// <summary>
        /// Returns the number of <see cref="T:System.Windows.Forms.DataGridViewRow"/> objects in the collection that meet the specified criteria.
        /// </summary>
        /// 
        /// <returns>
        /// The number of <see cref="T:System.Windows.Forms.DataGridViewRow"/> objects in the <see cref="T:System.Windows.Forms.DataGridViewRowCollection"/> that have the attributes specified by <paramref name="includeFilter"/>.
        /// </returns>
        /// <param name="includeFilter">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates"/> values.</param><exception cref="T:System.ArgumentException"><paramref name="includeFilter"/> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates"/> values.</exception>
        public int GetRowCount(DataGridViewElementStates includeFilter)
        {
            Contract.Requires(
                !((includeFilter &
                  ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen |
                    DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable |
                    DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) !=
                DataGridViewElementStates.None));
            Contract.Ensures(Contract.Result<int>() >= 0);
            return default(int);
        }

        /// <summary>
        /// Returns the cumulative height of the <see cref="T:System.Windows.Forms.DataGridViewRow"/> objects that meet the specified criteria.
        /// </summary>
        /// 
        /// <returns>
        /// The cumulative height of <see cref="T:System.Windows.Forms.DataGridViewRow"/> objects in the <see cref="T:System.Windows.Forms.DataGridViewRowCollection"/> that have the attributes specified by <paramref name="includeFilter"/>.
        /// </returns>
        /// <param name="includeFilter">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates"/> values.</param><exception cref="T:System.ArgumentException"><paramref name="includeFilter"/> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates"/> values.</exception>
        public int GetRowsHeight(DataGridViewElementStates includeFilter)
        {
            Contract.Requires(
               !((includeFilter &
                 ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen |
                   DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable |
                   DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) !=
               DataGridViewElementStates.None));
            Contract.Ensures(Contract.Result<int>() >= 0);
            return default(int);
        }
        
        /// <summary>
        /// Gets the state of the row with the specified index.
        /// </summary>
        /// 
        /// <returns>
        /// A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates"/> values indicating the state of the specified row.
        /// </returns>
        /// <param name="rowIndex">The index of the row.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="rowIndex"/> is less than zero and greater than the number of rows in the collection minus one.</exception>
        public virtual DataGridViewElementStates GetRowState(int rowIndex)
        {
            Contract.Requires(rowIndex >= 0 && rowIndex < this.Count);
            return default(DataGridViewElementStates);
        }

        /// <summary>
        /// Returns the index of a specified item in the collection.
        /// </summary>
        /// 
        /// <returns>
        /// The index of <paramref name="value"/> if it is a <see cref="T:System.Windows.Forms.DataGridViewRow"/> found in the <see cref="T:System.Windows.Forms.DataGridViewRowCollection"/>; otherwise, -1.
        /// </returns>
        /// <param name="dataGridViewRow">The <see cref="T:System.Windows.Forms.DataGridViewRow"/> to locate in the <see cref="T:System.Windows.Forms.DataGridViewRowCollection"/>.</param>
        public int IndexOf(DataGridViewRow dataGridViewRow)
        {
            Contract.Ensures(Contract.Result<int>() >= -1);
            return default(int);
        }

        /// <summary>
        /// Inserts a row into the collection at the specified position, and populates the cells with the specified objects.
        /// </summary>
        /// <param name="rowIndex">The position at which to insert the row.</param><param name="values">A variable number of objects that populate the cells of the new row.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="rowIndex"/> is less than zero or greater than the number of rows in the collection. </exception><exception cref="T:System.ArgumentNullException"><paramref name="values"/> is null.</exception><exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView"/> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView"/> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter"/><see cref="E:System.Windows.Forms.DataGridView.CellLeave"/><see cref="E:System.Windows.Forms.DataGridView.CellValidating"/><see cref="E:System.Windows.Forms.DataGridView.CellValidated"/><see cref="E:System.Windows.Forms.DataGridView.RowEnter"/><see cref="E:System.Windows.Forms.DataGridView.RowLeave"/><see cref="E:System.Windows.Forms.DataGridView.RowValidated"/><see cref="E:System.Windows.Forms.DataGridView.RowValidating"/>-or-The <see cref="P:System.Windows.Forms.DataGridView.VirtualMode"/> property of the <see cref="T:System.Windows.Forms.DataGridView"/> is set to true.-or-The <see cref="P:System.Windows.Forms.DataGridView.DataSource"/> property of the <see cref="T:System.Windows.Forms.DataGridView"/> is not null.-or-The <see cref="T:System.Windows.Forms.DataGridView"/> has no columns.-or-<paramref name="rowIndex"/> is equal to the number of rows in the collection and the <see cref="P:System.Windows.Forms.DataGridView.AllowUserToAddRows"/> property of the <see cref="T:System.Windows.Forms.DataGridView"/> is set to true.-or-The <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView"/> property of the row returned by the control's <see cref="P:System.Windows.Forms.DataGridView.RowTemplate"/> property is not null. -or-This operation would insert a frozen row after unfrozen rows or an unfrozen row before frozen rows.</exception><exception cref="T:System.ArgumentException">The row returned by the control's <see cref="P:System.Windows.Forms.DataGridView.RowTemplate"/> property has more cells than there are columns in the control.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public virtual void Insert(int rowIndex, params object[] values)
        {
            Contract.Requires(values != null);
            Contract.Requires(rowIndex >= 0 && rowIndex < this.Count);
            Contract.Requires(!(this.DataGridView.NewRowIndex != -1 && rowIndex == this.Count));
            Contract.Requires(this.DataGridView.Columns.Count > 0);
            Contract.Requires(this.DataGridView.NoDimensionChangeAllowed == false);
            Contract.Requires(this.DataGridView.VirtualMode == false);
            Contract.Requires(this.DataGridView.DataSource == null);
            Contract.Requires(values.Length <= this.DataGridView.Columns.Count);
        }

        /// <summary>
        /// Inserts the specified <see cref="T:System.Windows.Forms.DataGridViewRow"/> into the collection.
        /// </summary>
        /// <param name="rowIndex">The position at which to insert the row.</param><param name="dataGridViewRow">The <see cref="T:System.Windows.Forms.DataGridViewRow"/> to insert into the <see cref="T:System.Windows.Forms.DataGridViewRowCollection"/>.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="rowIndex"/> is less than zero or greater than the number of rows in the collection. </exception><exception cref="T:System.ArgumentNullException"><paramref name="dataGridViewRow"/> is null.</exception><exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView"/> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView"/> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter"/><see cref="E:System.Windows.Forms.DataGridView.CellLeave"/><see cref="E:System.Windows.Forms.DataGridView.CellValidating"/><see cref="E:System.Windows.Forms.DataGridView.CellValidated"/><see cref="E:System.Windows.Forms.DataGridView.RowEnter"/><see cref="E:System.Windows.Forms.DataGridView.RowLeave"/><see cref="E:System.Windows.Forms.DataGridView.RowValidated"/><see cref="E:System.Windows.Forms.DataGridView.RowValidating"/>-or-The <see cref="P:System.Windows.Forms.DataGridView.DataSource"/> property of the <see cref="T:System.Windows.Forms.DataGridView"/> is not null.-or-<paramref name="rowIndex"/> is equal to the number of rows in the collection and the <see cref="P:System.Windows.Forms.DataGridView.AllowUserToAddRows"/> property of the <see cref="T:System.Windows.Forms.DataGridView"/> is set to true.-or-The <see cref="T:System.Windows.Forms.DataGridView"/> has no columns.-or-The <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView"/> property of <paramref name="dataGridViewRow"/> is not null.-or-<paramref name="dataGridViewRow"/> has a <see cref="P:System.Windows.Forms.DataGridViewRow.Selected"/> property value of true. -or-This operation would insert a frozen row after unfrozen rows or an unfrozen row before frozen rows.</exception><exception cref="T:System.ArgumentException"><paramref name="dataGridViewRow"/> has more cells than there are columns in the control.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public virtual void Insert(int rowIndex, DataGridViewRow dataGridViewRow)
        {
            Contract.Requires(dataGridViewRow != null);
            Contract.Requires(rowIndex >= 0 && rowIndex < this.Count);
            Contract.Requires(!(this.DataGridView.NewRowIndex != -1 && rowIndex == this.Count));
            Contract.Requires(this.DataGridView.Columns.Count > 0);
            Contract.Requires(this.DataGridView.NoDimensionChangeAllowed == false);
            Contract.Requires(this.DataGridView.VirtualMode == false);
            Contract.Requires(this.DataGridView.DataSource == null);
            Contract.Requires(dataGridViewRow.Cells.Count <= this.DataGridView.Columns.Count);
        }

        /// <summary>
        /// Inserts the specified number of rows into the collection at the specified location.
        /// </summary>
        /// <param name="rowIndex">The position at which to insert the rows.</param><param name="count">The number of rows to insert into the <see cref="T:System.Windows.Forms.DataGridViewRowCollection"/>.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="rowIndex"/> is less than zero or greater than the number of rows in the collection. -or-<paramref name="count"/> is less than 1.</exception><exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView"/> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView"/> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter"/><see cref="E:System.Windows.Forms.DataGridView.CellLeave"/><see cref="E:System.Windows.Forms.DataGridView.CellValidating"/><see cref="E:System.Windows.Forms.DataGridView.CellValidated"/><see cref="E:System.Windows.Forms.DataGridView.RowEnter"/><see cref="E:System.Windows.Forms.DataGridView.RowLeave"/><see cref="E:System.Windows.Forms.DataGridView.RowValidated"/><see cref="E:System.Windows.Forms.DataGridView.RowValidating"/>-or-The <see cref="P:System.Windows.Forms.DataGridView.DataSource"/> property of the <see cref="T:System.Windows.Forms.DataGridView"/> is not null.-or-The <see cref="T:System.Windows.Forms.DataGridView"/> has no columns.-or-<paramref name="rowIndex"/> is equal to the number of rows in the collection and the <see cref="P:System.Windows.Forms.DataGridView.AllowUserToAddRows"/> property of the <see cref="T:System.Windows.Forms.DataGridView"/> is set to true.-or-The row returned by the <see cref="P:System.Windows.Forms.DataGridView.RowTemplate"/> property has more cells than there are columns in the control. -or-This operation would insert a frozen row after unfrozen rows or an unfrozen row before frozen rows.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public virtual void Insert(int rowIndex, int count)
        {
            Contract.Requires(rowIndex >= 0 && rowIndex < this.Count);
            Contract.Requires(count > 0);
            Contract.Requires(!(this.DataGridView.NewRowIndex != -1 && rowIndex == this.Count));
            Contract.Requires(this.DataGridView.Columns.Count > 0);
            Contract.Requires(this.DataGridView.NoDimensionChangeAllowed == false);
            Contract.Requires(this.DataGridView.VirtualMode == false);
            Contract.Requires(this.DataGridView.DataSource == null);
            Contract.Requires(this.DataGridView.RowTemplate.Cells.Count <= this.DataGridView.Columns.Count);
        }
        
        /// <summary>
        /// Inserts a row into the collection at the specified position, based on the row at specified position.
        /// </summary>
        /// <param name="indexSource">The index of the row on which to base the new row.</param><param name="indexDestination">The position at which to insert the row.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="indexSource"/> is less than zero or greater than the number of rows in the collection minus one.-or-<paramref name="indexDestination"/> is less than zero or greater than the number of rows in the collection.</exception><exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView"/> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView"/> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter"/><see cref="E:System.Windows.Forms.DataGridView.CellLeave"/><see cref="E:System.Windows.Forms.DataGridView.CellValidating"/><see cref="E:System.Windows.Forms.DataGridView.CellValidated"/><see cref="E:System.Windows.Forms.DataGridView.RowEnter"/><see cref="E:System.Windows.Forms.DataGridView.RowLeave"/><see cref="E:System.Windows.Forms.DataGridView.RowValidated"/><see cref="E:System.Windows.Forms.DataGridView.RowValidating"/>-or-<paramref name="indexDestination"/> is equal to the number of rows in the collection and <see cref="P:System.Windows.Forms.DataGridView.AllowUserToAddRows"/> is true. -or-This operation would insert a frozen row after unfrozen rows or an unfrozen row before frozen rows.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public virtual void InsertCopy(int indexSource, int indexDestination)
        {
            this.InsertCopies(indexSource, indexDestination, 1);
        }

        /// <summary>
        /// Inserts rows into the collection at the specified position.
        /// </summary>
        /// <param name="indexSource">The index of the <see cref="T:System.Windows.Forms.DataGridViewRow"/> on which to base the new rows.</param><param name="indexDestination">The position at which to insert the rows.</param><param name="count">The number of <see cref="T:System.Windows.Forms.DataGridViewRow"/> objects to add to the <see cref="T:System.Windows.Forms.DataGridViewRowCollection"/>.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="indexSource"/> is less than zero or greater than the number of rows in the collection minus one.-or-<paramref name="indexDestination"/> is less than zero or greater than the number of rows in the collection.-or-<paramref name="count"/> is less than 1.</exception><exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView"/> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView"/> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter"/><see cref="E:System.Windows.Forms.DataGridView.CellLeave"/><see cref="E:System.Windows.Forms.DataGridView.CellValidating"/><see cref="E:System.Windows.Forms.DataGridView.CellValidated"/><see cref="E:System.Windows.Forms.DataGridView.RowEnter"/><see cref="E:System.Windows.Forms.DataGridView.RowLeave"/><see cref="E:System.Windows.Forms.DataGridView.RowValidated"/><see cref="E:System.Windows.Forms.DataGridView.RowValidating"/>-or-<paramref name="indexDestination"/> is equal to the number of rows in the collection and <see cref="P:System.Windows.Forms.DataGridView.AllowUserToAddRows"/> is true.-or-This operation would insert frozen rows after unfrozen rows or unfrozen rows before frozen rows.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public virtual void InsertCopies(int indexSource, int indexDestination, int count)
        {
            Contract.Requires(this.DataGridView.DataSource == null);
            Contract.Requires(this.DataGridView.NoDimensionChangeAllowed == false);
            Contract.Requires(count > 0);
        }
        
        /// <summary>
        /// Inserts the <see cref="T:System.Windows.Forms.DataGridViewRow"/> objects into the collection at the specified position.
        /// </summary>
        /// <param name="rowIndex">The position at which to insert the rows.</param><param name="dataGridViewRows">An array of <see cref="T:System.Windows.Forms.DataGridViewRow"/> objects to add to the <see cref="T:System.Windows.Forms.DataGridViewRowCollection"/>.</param><exception cref="T:System.ArgumentNullException"><paramref name="dataGridViewRows"/> is null.</exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="rowIndex"/> is less than zero or greater than the number of rows in the collection.</exception><exception cref="T:System.ArgumentException"><paramref name="dataGridViewRows"/> contains only one row, and the row it contains has more cells than there are columns in the control.</exception><exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView"/> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView"/> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter"/><see cref="E:System.Windows.Forms.DataGridView.CellLeave"/><see cref="E:System.Windows.Forms.DataGridView.CellValidating"/><see cref="E:System.Windows.Forms.DataGridView.CellValidated"/><see cref="E:System.Windows.Forms.DataGridView.RowEnter"/><see cref="E:System.Windows.Forms.DataGridView.RowLeave"/><see cref="E:System.Windows.Forms.DataGridView.RowValidated"/><see cref="E:System.Windows.Forms.DataGridView.RowValidating"/>-or-<paramref name="rowIndex"/> is equal to the number of rows in the collection and <see cref="P:System.Windows.Forms.DataGridView.AllowUserToAddRows"/> is true.-or-The <see cref="P:System.Windows.Forms.DataGridView.DataSource"/> property of the <see cref="T:System.Windows.Forms.DataGridView"/> is not null.-or-At least one entry in the <paramref name="dataGridViewRows"/> array is null.-or-The <see cref="T:System.Windows.Forms.DataGridView"/> has no columns.-or-At least one row in the <paramref name="dataGridViewRows"/> array has a <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView"/> property value that is not null.-or-At least one row in the <paramref name="dataGridViewRows"/> array has a <see cref="P:System.Windows.Forms.DataGridViewRow.Selected"/> property value of true.-or-Two or more rows in the <paramref name="dataGridViewRows"/> array are identical.-or-At least one row in the <paramref name="dataGridViewRows"/> array contains one or more cells of a type that is incompatible with the type of the corresponding column in the control.-or-At least one row in the <paramref name="dataGridViewRows"/> array contains more cells than there are columns in the control. -or-This operation would insert frozen rows after unfrozen rows or unfrozen rows before frozen rows.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public virtual void InsertRange(int rowIndex, params DataGridViewRow[] dataGridViewRows)
        {
            Contract.Requires(dataGridViewRows != null);
            Contract.Requires(rowIndex >= 0 && rowIndex < this.Count);
            Contract.Requires(!(this.DataGridView.NewRowIndex != -1 && rowIndex == this.Count));
            Contract.Requires(this.DataGridView.Columns.Count > 0);
            Contract.Requires(this.DataGridView.NoDimensionChangeAllowed == false);
            Contract.Requires(this.DataGridView.VirtualMode == false);
            Contract.Requires(this.DataGridView.DataSource == null);
            Contract.Requires(this.DataGridView.RowTemplate.Cells.Count <= this.DataGridView.Columns.Count);
        }
        
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridViewRowCollection.CollectionChanged"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.ComponentModel.CollectionChangeEventArgs"/> that contains the event data. </param>
        // protected virtual void OnCollectionChanged(CollectionChangeEventArgs e)
       
        /// <summary>
        /// Removes the row from the collection.
        /// </summary>
        /// <param name="dataGridViewRow">The row to remove from the <see cref="T:System.Windows.Forms.DataGridViewRowCollection"/>.</param><exception cref="T:System.ArgumentNullException"><paramref name="dataGridViewRow"/> is null.</exception><exception cref="T:System.ArgumentException"><paramref name="dataGridViewRow"/> is not contained in this collection.-or-<paramref name="dataGridViewRow"/> is a shared row.</exception><exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView"/> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView"/> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter"/><see cref="E:System.Windows.Forms.DataGridView.CellLeave"/><see cref="E:System.Windows.Forms.DataGridView.CellValidating"/><see cref="E:System.Windows.Forms.DataGridView.CellValidated"/><see cref="E:System.Windows.Forms.DataGridView.RowEnter"/><see cref="E:System.Windows.Forms.DataGridView.RowLeave"/><see cref="E:System.Windows.Forms.DataGridView.RowValidated"/><see cref="E:System.Windows.Forms.DataGridView.RowValidating"/>-or-<paramref name="dataGridViewRow"/> is the row for new records.-or-The associated <see cref="T:System.Windows.Forms.DataGridView"/> control is bound to an <see cref="T:System.ComponentModel.IBindingList"/> implementation with <see cref="P:System.ComponentModel.IBindingList.AllowRemove"/> and <see cref="P:System.ComponentModel.IBindingList.SupportsChangeNotification"/> property values that are not both true. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public virtual void Remove(DataGridViewRow dataGridViewRow)
        {
            Contract.Requires(dataGridViewRow != null);
            Contract.Requires(dataGridViewRow.DataGridView == this.DataGridView);
            Contract.Requires(dataGridViewRow.Index >= 0 && dataGridViewRow.Index < this.Count);
            Contract.Requires(this.DataGridView.NewRowIndex != dataGridViewRow.Index);
            Contract.Requires(this.DataGridView.NoDimensionChangeAllowed == false);
            Contract.Requires(this.DataGridView.DataSource == null);
        }

        /// <summary>
        /// Removes the row at the specified position from the collection.
        /// </summary>
        /// <param name="index">The position of the row to remove.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than zero and greater than the number of rows in the collection minus one. </exception><exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView"/> control is performing one of the following actions that temporarily prevents new rows from being added:Selecting all cells in the control.Clearing the selection.-or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView"/> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter"/><see cref="E:System.Windows.Forms.DataGridView.CellLeave"/><see cref="E:System.Windows.Forms.DataGridView.CellValidating"/><see cref="E:System.Windows.Forms.DataGridView.CellValidated"/><see cref="E:System.Windows.Forms.DataGridView.RowEnter"/><see cref="E:System.Windows.Forms.DataGridView.RowLeave"/><see cref="E:System.Windows.Forms.DataGridView.RowValidated"/><see cref="E:System.Windows.Forms.DataGridView.RowValidating"/>-or-<paramref name="index"/> is equal to the number of rows in the collection and the <see cref="P:System.Windows.Forms.DataGridView.AllowUserToAddRows"/> property of the <see cref="T:System.Windows.Forms.DataGridView"/> is set to true.-or-The associated <see cref="T:System.Windows.Forms.DataGridView"/> control is bound to an <see cref="T:System.ComponentModel.IBindingList"/> implementation with <see cref="P:System.ComponentModel.IBindingList.AllowRemove"/> and <see cref="P:System.ComponentModel.IBindingList.SupportsChangeNotification"/> property values that are not both true.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public virtual void RemoveAt(int index)
        {
            Contract.Requires(index >= 0 && index < this.Count);
            Contract.Requires(this.DataGridView.NewRowIndex != index);
            Contract.Requires(this.DataGridView.NoDimensionChangeAllowed == false);
            Contract.Requires(this.DataGridView.DataSource == null);
        }
    }
}
