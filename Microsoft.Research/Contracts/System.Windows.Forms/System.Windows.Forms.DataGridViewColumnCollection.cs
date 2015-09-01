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
using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Diagnostics.Contracts;

namespace System.Windows.Forms
{
    /// <summary>
    /// Represents a collection of <see cref="T:System.Windows.Forms.DataGridViewColumn"/> objects in a <see cref="T:System.Windows.Forms.DataGridView"/> control.
    /// </summary>
    public class DataGridViewColumnCollection : BaseCollection // IList, 
    {
        // <summary>
        // Gets the <see cref="T:System.Windows.Forms.DataGridView"/> upon which the collection performs column-related operations.
        // </summary>
        // 
        // <returns>
        // <see cref="T:System.Windows.Forms.DataGridView"/>.
        // </returns>
        // protected DataGridView DataGridView { get; }
        
        /// <summary>
        /// Gets or sets the column at the given index in the collection.
        /// </summary>
        /// 
        /// <returns>
        /// The <see cref="T:System.Windows.Forms.DataGridViewColumn"/> at the given index.
        /// </returns>
        /// <param name="index">The zero-based index of the column to get or set.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than zero or greater than the number of columns in the collection minus one.</exception>
        public DataGridViewColumn this[int index]
        {
            get
            {
                Contract.Requires(index >= 0);
                Contract.Requires(index < this.Count);
                return default(DataGridViewColumn);
            }
        }

        /// <summary>
        /// Gets or sets the column of the given name in the collection.
        /// </summary>
        /// 
        /// <returns>
        /// The <see cref="T:System.Windows.Forms.DataGridViewColumn"/> identified by the <paramref name="columnName"/> parameter.
        /// </returns>
        /// <param name="columnName">The name of the column to get or set.</param><exception cref="T:System.ArgumentNullException"><paramref name="columnName"/> is null.</exception>
        public DataGridViewColumn this[string columnName]
        {
            get
            {
                Contract.Requires(columnName != null);
                return default(DataGridViewColumn);
            }
        }
        
        // <summary>
        // Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewColumnCollection"/> class for the given <see cref="T:System.Windows.Forms.DataGridView"/>.
        // </summary>
        // <param name="dataGridView">The <see cref="T:System.Windows.Forms.DataGridView"/> that created this collection.</param>
        // public DataGridViewColumnCollection(DataGridView dataGridView);
        
        // <summary>
        // Adds a <see cref="T:System.Windows.Forms.DataGridViewTextBoxColumn"/> with the given column name and column header text to the collection.
        // </summary>
        // 
        // <returns>
        // The index of the column.
        // </returns>
        // <param name="columnName">The name by which the column will be referred.</param><param name="headerText">The text for the column's header.</param><exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView"/> control is performing one of the following actions that temporarily prevents new columns from being added:Selecting all cells in the control.Clearing the selection.Updating column <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex"/> property values. -or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView"/> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter"/><see cref="E:System.Windows.Forms.DataGridView.CellLeave"/><see cref="E:System.Windows.Forms.DataGridView.CellValidating"/><see cref="E:System.Windows.Forms.DataGridView.CellValidated"/><see cref="E:System.Windows.Forms.DataGridView.RowEnter"/><see cref="E:System.Windows.Forms.DataGridView.RowLeave"/><see cref="E:System.Windows.Forms.DataGridView.RowValidated"/><see cref="E:System.Windows.Forms.DataGridView.RowValidating"/>-or-The <see cref="P:System.Windows.Forms.DataGridView.SelectionMode"/> property value is <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.FullColumnSelect"/> or <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.ColumnHeaderSelect"/>, which conflicts with the default column <see cref="P:System.Windows.Forms.DataGridViewColumn.SortMode"/> property value of <see cref="F:System.Windows.Forms.DataGridViewColumnSortMode.Automatic"/>.-or-The default column <see cref="P:System.Windows.Forms.DataGridViewColumn.FillWeight"/> property value of 100 would cause the combined <see cref="P:System.Windows.Forms.DataGridViewColumn.FillWeight"/> values of all columns in the control to exceed 65535.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public virtual int Add(string columnName, string headerText);;
        
        /// <summary>
        /// Adds the given column to the collection.
        /// </summary>
        /// 
        /// <returns>
        /// The index of the column.
        /// </returns>
        /// <param name="dataGridViewColumn">The <see cref="T:System.Windows.Forms.DataGridViewColumn"/> to add.</param><exception cref="T:System.ArgumentNullException"><paramref name="dataGridViewColumn"/> is null.</exception><exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView"/> control is performing one of the following actions that temporarily prevents new columns from being added:Selecting all cells in the control.Clearing the selection.Updating column <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex"/> property values. -or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView"/> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter"/><see cref="E:System.Windows.Forms.DataGridView.CellLeave"/><see cref="E:System.Windows.Forms.DataGridView.CellValidating"/><see cref="E:System.Windows.Forms.DataGridView.CellValidated"/><see cref="E:System.Windows.Forms.DataGridView.RowEnter"/><see cref="E:System.Windows.Forms.DataGridView.RowLeave"/><see cref="E:System.Windows.Forms.DataGridView.RowValidated"/><see cref="E:System.Windows.Forms.DataGridView.RowValidating"/>-or-<paramref name="dataGridViewColumn"/> already belongs to a <see cref="T:System.Windows.Forms.DataGridView"/> control.-or-The <paramref name="dataGridViewColumn"/><see cref="P:System.Windows.Forms.DataGridViewColumn.SortMode"/> property value is <see cref="F:System.Windows.Forms.DataGridViewColumnSortMode.Automatic"/> and the <see cref="P:System.Windows.Forms.DataGridView.SelectionMode"/> property value is <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.FullColumnSelect"/> or <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.ColumnHeaderSelect"/>. Use the control <see cref="M:System.Windows.Forms.DataGridView.System#ComponentModel#ISupportInitialize#BeginInit"/> and <see cref="M:System.Windows.Forms.DataGridView.System#ComponentModel#ISupportInitialize#EndInit"/> methods to temporarily set conflicting property values. -or-The <paramref name="dataGridViewColumn"/><see cref="P:System.Windows.Forms.DataGridViewColumn.InheritedAutoSizeMode"/> property value is <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader"/> and the <see cref="P:System.Windows.Forms.DataGridView.ColumnHeadersVisible"/> property value is false.-or-<paramref name="dataGridViewColumn"/> has an <see cref="P:System.Windows.Forms.DataGridViewColumn.InheritedAutoSizeMode"/> property value of <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill"/> and a <see cref="P:System.Windows.Forms.DataGridViewColumn.Frozen"/> property value of true.-or-<paramref name="dataGridViewColumn"/> has a <see cref="P:System.Windows.Forms.DataGridViewColumn.FillWeight"/> property value that would cause the combined <see cref="P:System.Windows.Forms.DataGridViewColumn.FillWeight"/> values of all columns in the control to exceed 65535.-or-<paramref name="dataGridViewColumn"/> has <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex"/> and <see cref="P:System.Windows.Forms.DataGridViewColumn.Frozen"/> property values that would display it among a set of adjacent columns with the opposite <see cref="P:System.Windows.Forms.DataGridViewColumn.Frozen"/> property value.-or-The <see cref="T:System.Windows.Forms.DataGridView"/> control contains at least one row and <paramref name="dataGridViewColumn"/> has a <see cref="P:System.Windows.Forms.DataGridViewColumn.CellType"/> property value of null.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public virtual int Add(DataGridViewColumn dataGridViewColumn)
        {
            Contract.Requires(dataGridViewColumn != null);
            return default(int);
        }

        /// <summary>
        /// Adds a range of columns to the collection.
        /// </summary>
        /// <param name="dataGridViewColumns">An array of <see cref="T:System.Windows.Forms.DataGridViewColumn"/> objects to add.</param><exception cref="T:System.ArgumentNullException"><paramref name="dataGridViewColumns"/> is null.</exception><exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView"/> control is performing one of the following actions that temporarily prevents new columns from being added:Selecting all cells in the control.Clearing the selection.Updating column <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex"/> property values. -or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView"/> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter"/><see cref="E:System.Windows.Forms.DataGridView.CellLeave"/><see cref="E:System.Windows.Forms.DataGridView.CellValidating"/><see cref="E:System.Windows.Forms.DataGridView.CellValidated"/><see cref="E:System.Windows.Forms.DataGridView.RowEnter"/><see cref="E:System.Windows.Forms.DataGridView.RowLeave"/><see cref="E:System.Windows.Forms.DataGridView.RowValidated"/><see cref="E:System.Windows.Forms.DataGridView.RowValidating"/>-or-At least one of the values in <paramref name="dataGridViewColumns"/> is null.-or-At least one of the columns in <paramref name="dataGridViewColumns"/> already belongs to a <see cref="T:System.Windows.Forms.DataGridView"/> control.-or-At least one of the columns in <paramref name="dataGridViewColumns"/> has a <see cref="P:System.Windows.Forms.DataGridViewColumn.CellType"/> property value of null and the <see cref="T:System.Windows.Forms.DataGridView"/> control contains at least one row.-or-At least one of the columns in <paramref name="dataGridViewColumns"/> has a <see cref="P:System.Windows.Forms.DataGridViewColumn.SortMode"/> property value of <see cref="F:System.Windows.Forms.DataGridViewColumnSortMode.Automatic"/> and the <see cref="P:System.Windows.Forms.DataGridView.SelectionMode"/> property value is <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.FullColumnSelect"/> or <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.ColumnHeaderSelect"/>. Use the control <see cref="M:System.Windows.Forms.DataGridView.System#ComponentModel#ISupportInitialize#BeginInit"/> and <see cref="M:System.Windows.Forms.DataGridView.System#ComponentModel#ISupportInitialize#EndInit"/> methods to temporarily set conflicting property values. -or-At least one of the columns in <paramref name="dataGridViewColumns"/> has an <see cref="P:System.Windows.Forms.DataGridViewColumn.InheritedAutoSizeMode"/> property value of <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader"/> and the <see cref="P:System.Windows.Forms.DataGridView.ColumnHeadersVisible"/> property value is false.-or-At least one of the columns in <paramref name="dataGridViewColumns"/> has an <see cref="P:System.Windows.Forms.DataGridViewColumn.InheritedAutoSizeMode"/> property value of <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill"/> and a <see cref="P:System.Windows.Forms.DataGridViewColumn.Frozen"/> property value of true.-or-The columns in <paramref name="dataGridViewColumns"/> have <see cref="P:System.Windows.Forms.DataGridViewColumn.FillWeight"/> property values that would cause the combined <see cref="P:System.Windows.Forms.DataGridViewColumn.FillWeight"/> values of all columns in the control to exceed 65535.-or-At least two of the values in <paramref name="dataGridViewColumns"/> are references to the same <see cref="T:System.Windows.Forms.DataGridViewColumn"/>.-or-At least one of the columns in <paramref name="dataGridViewColumns"/> has <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex"/> and <see cref="P:System.Windows.Forms.DataGridViewColumn.Frozen"/> property values that would display it among a set of adjacent columns with the opposite <see cref="P:System.Windows.Forms.DataGridViewColumn.Frozen"/> property value.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public virtual void AddRange(params DataGridViewColumn[] dataGridViewColumns)
        {
            Contract.Requires(dataGridViewColumns != null);
        }

        // <summary>
        // Clears the collection.
        // </summary>
        // <exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView"/> control is performing one of the following actions that temporarily prevents new columns from being added:Selecting all cells in the control.Clearing the selection.Updating column <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex"/> property values. -or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView"/> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter"/><see cref="E:System.Windows.Forms.DataGridView.CellLeave"/><see cref="E:System.Windows.Forms.DataGridView.CellValidating"/><see cref="E:System.Windows.Forms.DataGridView.CellValidated"/><see cref="E:System.Windows.Forms.DataGridView.RowEnter"/><see cref="E:System.Windows.Forms.DataGridView.RowLeave"/><see cref="E:System.Windows.Forms.DataGridView.RowValidated"/><see cref="E:System.Windows.Forms.DataGridView.RowValidating"/></exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public virtual void Clear()
        
        // <summary>
        // Determines whether the collection contains the given column.
        // </summary>
        // 
        // <returns>
        // true if the given column is in the collection; otherwise, false.
        // </returns>
        // <param name="dataGridViewColumn">The <see cref="T:System.Windows.Forms.DataGridViewColumn"/> to look for.</param>
        // public virtual bool Contains(DataGridViewColumn dataGridViewColumn);
        
        /// <summary>
        /// Determines whether the collection contains the column referred to by the given name.
        /// </summary>
        /// 
        /// <returns>
        /// true if the column is contained in the collection; otherwise, false.
        /// </returns>
        /// <param name="columnName">The name of the column to look for.</param><exception cref="T:System.ArgumentNullException"><paramref name="columnName"/> is null.</exception>
        public virtual bool Contains(string columnName)
        {
            Contract.Requires(columnName != null);
            return default(bool);
        }

        /// <summary>
        /// Copies the items from the collection to the given array.
        /// </summary>
        /// <param name="array">The destination <see cref="T:System.Windows.Forms.DataGridViewColumn"/> array.</param><param name="index">The index of the destination array at which to start copying.</param>
        public void CopyTo(DataGridViewColumn[] array, int index)
        {
            Contract.Requires(index >= 0);
            Contract.Requires(index < this.Count);
            Contract.Requires(array != null);
        }

        /// <summary>
        /// Returns the number of columns that meet the given filter requirements.
        /// </summary>
        /// 
        /// <returns>
        /// The number of columns that meet the filter requirements.
        /// </returns>
        /// <param name="includeFilter">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewElementStates"/> values that represent the filter for inclusion.</param><exception cref="T:System.ArgumentException"><paramref name="includeFilter"/> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates"/> values.</exception>
        public int GetColumnCount(DataGridViewElementStates includeFilter)
        {
            Contract.Ensures(Contract.Result<int>() >= 0);
            return default(int);
        }
        /// <summary>
        /// Returns the width, in pixels, required to display all of the columns that meet the given filter requirements.
        /// </summary>
        /// 
        /// <returns>
        /// The width, in pixels, that is necessary to display all of the columns that meet the filter requirements.
        /// </returns>
        /// <param name="includeFilter">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewElementStates"/> values that represent the filter for inclusion.</param><exception cref="T:System.ArgumentException"><paramref name="includeFilter"/> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates"/> values.</exception>
        public int GetColumnsWidth(DataGridViewElementStates includeFilter)
        {
            Contract.Ensures(Contract.Result<int>() >= 0);
            return default(int);
        }

        // <summary>
        // Returns the first column in display order that meets the given inclusion-filter requirements.
        // </summary>
        // 
        // <returns>
        // The first column in display order that meets the given filter requirements, or null if no column is found.
        // </returns>
        // <param name="includeFilter">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewElementStates"/> values that represents the filter for inclusion.</param><exception cref="T:System.ArgumentException"><paramref name="includeFilter"/> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates"/> values.</exception>
        //  public DataGridViewColumn GetFirstColumn(DataGridViewElementStates includeFilter);
        
        // <summary>
        // Returns the first column in display order that meets the given inclusion-filter and exclusion-filter requirements.
        // </summary>
        // 
        // <returns>
        // The first column in display order that meets the given filter requirements, or null if no column is found.
        // </returns>
        // <param name="includeFilter">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewElementStates"/> values that represent the filter to apply for inclusion.</param><param name="excludeFilter">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewElementStates"/> values that represent the filter to apply for exclusion.</param><exception cref="T:System.ArgumentException">At least one of the filter values is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates"/> values.</exception>
        // public DataGridViewColumn GetFirstColumn(DataGridViewElementStates includeFilter, DataGridViewElementStates excludeFilter);
        
        // <summary>
        // Returns the last column in display order that meets the given filter requirements.
        // </summary>
        // 
        // <returns>
        // The last displayed column in display order that meets the given filter requirements, or null if no column is found.
        // </returns>
        // <param name="includeFilter">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewElementStates"/> values that represent the filter to apply for inclusion.</param><param name="excludeFilter">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewElementStates"/> values that represent the filter to apply for exclusion.</param><exception cref="T:System.ArgumentException">At least one of the filter values is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates"/> values.</exception>
        // public DataGridViewColumn GetLastColumn(DataGridViewElementStates includeFilter, DataGridViewElementStates excludeFilter);
        
        /// <summary>
        /// Gets the first column after the given column in display order that meets the given filter requirements.
        /// </summary>
        /// 
        /// <returns>
        /// The next column that meets the given filter requirements, or null if no column is found.
        /// </returns>
        /// <param name="dataGridViewColumnStart">The column from which to start searching for the next column.</param><param name="includeFilter">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewElementStates"/> values that represent the filter to apply for inclusion.</param><param name="excludeFilter">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewElementStates"/> values that represent the filter to apply for exclusion.</param><exception cref="T:System.ArgumentNullException"><paramref name="dataGridViewColumnStart"/> is null.</exception><exception cref="T:System.ArgumentException">At least one of the filter values is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates"/> values.</exception>
        public DataGridViewColumn GetNextColumn(DataGridViewColumn dataGridViewColumnStart, DataGridViewElementStates includeFilter, DataGridViewElementStates excludeFilter)
        {
            Contract.Requires(dataGridViewColumnStart != null);
            return default(DataGridViewColumn);
        }

        /// <summary>
        /// Gets the last column prior to the given column in display order that meets the given filter requirements.
        /// </summary>
        /// 
        /// <returns>
        /// The previous column that meets the given filter requirements, or null if no column is found.
        /// </returns>
        /// <param name="dataGridViewColumnStart">The column from which to start searching for the previous column.</param><param name="includeFilter">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewElementStates"/> values that represent the filter to apply for inclusion.</param><param name="excludeFilter">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewElementStates"/> values that represent the filter to apply for exclusion.</param><exception cref="T:System.ArgumentNullException"><paramref name="dataGridViewColumnStart"/> is null.</exception><exception cref="T:System.ArgumentException">At least one of the filter values is not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates"/> values.</exception>
        public DataGridViewColumn GetPreviousColumn(DataGridViewColumn dataGridViewColumnStart, DataGridViewElementStates includeFilter, DataGridViewElementStates excludeFilter)
        {
            Contract.Requires(dataGridViewColumnStart != null);
            return default(DataGridViewColumn);
        }

        // <summary>
        // Gets the index of the given <see cref="T:System.Windows.Forms.DataGridViewColumn"/> in the collection.
        // </summary>
        // 
        // <returns>
        // The index of the given <see cref="T:System.Windows.Forms.DataGridViewColumn"/>.
        // </returns>
        // <param name="dataGridViewColumn">The <see cref="T:System.Windows.Forms.DataGridViewColumn"/> to return the index of.</param>
        // public int IndexOf(DataGridViewColumn dataGridViewColumn);

       
        /// <summary>
        /// Inserts a column at the given index in the collection.
        /// </summary>
        /// <param name="columnIndex">The zero-based index at which to insert the given column.</param><param name="dataGridViewColumn">The <see cref="T:System.Windows.Forms.DataGridViewColumn"/> to insert.</param><exception cref="T:System.ArgumentNullException"><paramref name="dataGridViewColumn"/> is null.</exception><exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView"/> control is performing one of the following actions that temporarily prevents new columns from being added:Selecting all cells in the control.Clearing the selection.Updating column <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex"/> property values. -or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView"/> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter"/><see cref="E:System.Windows.Forms.DataGridView.CellLeave"/><see cref="E:System.Windows.Forms.DataGridView.CellValidating"/><see cref="E:System.Windows.Forms.DataGridView.CellValidated"/><see cref="E:System.Windows.Forms.DataGridView.RowEnter"/><see cref="E:System.Windows.Forms.DataGridView.RowLeave"/><see cref="E:System.Windows.Forms.DataGridView.RowValidated"/><see cref="E:System.Windows.Forms.DataGridView.RowValidating"/>-or-<paramref name="dataGridViewColumn"/> already belongs to a <see cref="T:System.Windows.Forms.DataGridView"/> control.-or-The <paramref name="dataGridViewColumn"/><see cref="P:System.Windows.Forms.DataGridViewColumn.SortMode"/> property value is <see cref="F:System.Windows.Forms.DataGridViewColumnSortMode.Automatic"/> and the <see cref="P:System.Windows.Forms.DataGridView.SelectionMode"/> property value is <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.FullColumnSelect"/> or <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.ColumnHeaderSelect"/>. Use the control <see cref="M:System.Windows.Forms.DataGridView.System#ComponentModel#ISupportInitialize#BeginInit"/> and <see cref="M:System.Windows.Forms.DataGridView.System#ComponentModel#ISupportInitialize#EndInit"/> methods to temporarily set conflicting property values. -or-The <paramref name="dataGridViewColumn"/><see cref="P:System.Windows.Forms.DataGridViewColumn.InheritedAutoSizeMode"/> property value is <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader"/> and the <see cref="P:System.Windows.Forms.DataGridView.ColumnHeadersVisible"/> property value is false.-or-<paramref name="dataGridViewColumn"/> has an <see cref="P:System.Windows.Forms.DataGridViewColumn.InheritedAutoSizeMode"/> property value of <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill"/> and a <see cref="P:System.Windows.Forms.DataGridViewColumn.Frozen"/> property value of true.-or-<paramref name="dataGridViewColumn"/> has <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex"/> and <see cref="P:System.Windows.Forms.DataGridViewColumn.Frozen"/> property values that would display it among a set of adjacent columns with the opposite <see cref="P:System.Windows.Forms.DataGridViewColumn.Frozen"/> property value.-or-The <see cref="T:System.Windows.Forms.DataGridView"/> control contains at least one row and <paramref name="dataGridViewColumn"/> has a <see cref="P:System.Windows.Forms.DataGridViewColumn.CellType"/> property value of null.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public virtual void Insert(int columnIndex, DataGridViewColumn dataGridViewColumn)
        {
            Contract.Requires(columnIndex >= 0);
            Contract.Requires(dataGridViewColumn != null);
        }
        
        /// <summary>
        /// Removes the specified column from the collection.
        /// </summary>
        /// <param name="dataGridViewColumn">The column to delete.</param><exception cref="T:System.ArgumentException"><paramref name="dataGridViewColumn"/> is not in the collection.</exception><exception cref="T:System.ArgumentNullException"><paramref name="dataGridViewColumn"/> is null.</exception><exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView"/> control is performing one of the following actions that temporarily prevents new columns from being added:Selecting all cells in the control.Clearing the selection.Updating column <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex"/> property values. -or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView"/> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter"/><see cref="E:System.Windows.Forms.DataGridView.CellLeave"/><see cref="E:System.Windows.Forms.DataGridView.CellValidating"/><see cref="E:System.Windows.Forms.DataGridView.CellValidated"/><see cref="E:System.Windows.Forms.DataGridView.RowEnter"/><see cref="E:System.Windows.Forms.DataGridView.RowLeave"/><see cref="E:System.Windows.Forms.DataGridView.RowValidated"/><see cref="E:System.Windows.Forms.DataGridView.RowValidating"/></exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public virtual void Remove(DataGridViewColumn dataGridViewColumn)
        {
            Contract.Requires(dataGridViewColumn != null);
        }

        /// <summary>
        /// Removes the column with the specified name from the collection.
        /// </summary>
        /// <param name="columnName">The name of the column to delete.</param><exception cref="T:System.ArgumentException"><paramref name="columnName"/> does not match the name of any column in the collection.</exception><exception cref="T:System.ArgumentNullException"><paramref name="columnName"/> is null.</exception><exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView"/> control is performing one of the following actions that temporarily prevents new columns from being added:Selecting all cells in the control.Clearing the selection.Updating column <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex"/> property values. -or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView"/> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter"/><see cref="E:System.Windows.Forms.DataGridView.CellLeave"/><see cref="E:System.Windows.Forms.DataGridView.CellValidating"/><see cref="E:System.Windows.Forms.DataGridView.CellValidated"/><see cref="E:System.Windows.Forms.DataGridView.RowEnter"/><see cref="E:System.Windows.Forms.DataGridView.RowLeave"/><see cref="E:System.Windows.Forms.DataGridView.RowValidated"/><see cref="E:System.Windows.Forms.DataGridView.RowValidating"/></exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public virtual void Remove(string columnName)
        {
            Contract.Requires(columnName != null);
        }

        /// <summary>
        /// Removes the column at the given index in the collection.
        /// </summary>
        /// <param name="index">The index of the column to delete.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index"/> is less than zero or greater than the number of columns in the control minus one. </exception><exception cref="T:System.InvalidOperationException">The associated <see cref="T:System.Windows.Forms.DataGridView"/> control is performing one of the following actions that temporarily prevents new columns from being added:Selecting all cells in the control.Clearing the selection.Updating column <see cref="P:System.Windows.Forms.DataGridViewColumn.DisplayIndex"/> property values. -or-This method is being called from a handler for one of the following <see cref="T:System.Windows.Forms.DataGridView"/> events:<see cref="E:System.Windows.Forms.DataGridView.CellEnter"/><see cref="E:System.Windows.Forms.DataGridView.CellLeave"/><see cref="E:System.Windows.Forms.DataGridView.CellValidating"/><see cref="E:System.Windows.Forms.DataGridView.CellValidated"/><see cref="E:System.Windows.Forms.DataGridView.RowEnter"/><see cref="E:System.Windows.Forms.DataGridView.RowLeave"/><see cref="E:System.Windows.Forms.DataGridView.RowValidated"/><see cref="E:System.Windows.Forms.DataGridView.RowValidating"/></exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public virtual void RemoveAt(int index)
        {
            Contract.Requires(index >= 0);
            Contract.Requires(index < this.Count);
        }
    }
}
