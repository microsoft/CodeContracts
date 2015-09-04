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
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
    /// <summary>
    /// Represents an individual cell in a <see cref="T:System.Windows.Forms.DataGridView"/> control.
    /// </summary>
    public abstract class DataGridViewCell : DataGridViewElement //, ICloneable, IDisposable
    {
        /// <summary>
        /// Gets the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject"/> assigned to the <see cref="T:System.Windows.Forms.DataGridViewCell"/>.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject"/> assigned to the <see cref="T:System.Windows.Forms.DataGridViewCell"/>.
        /// </returns>
        public AccessibleObject AccessibilityObject
        {
            get
            {
                Contract.Ensures(Contract.Result<AccessibleObject>() != null);
                return default(AccessibleObject);
            }
        }

        /// <summary>
        /// Gets the column index for this cell.
        /// </summary>
        /// <returns>
        /// The index of the column that contains the cell; -1 if the cell is not contained within a column.
        /// </returns>
        public int ColumnIndex
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= -1);
                return default(int);
            }
        }

        // <summary>
        // Gets the bounding rectangle that encloses the cell's content area.
        // </summary>
        // <returns>
        // The <see cref="T:System.Drawing.Rectangle"/> that bounds the cell's contents.
        // </returns>
        // <exception cref="T:System.ArgumentOutOfRangeException">The row containing the cell is a shared row.-or-The cell is a column header cell.</exception><exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.ColumnIndex"/> property is less than 0, indicating that the cell is a row header cell.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public Rectangle ContentBounds {get;}
        
        // <summary>
        // Gets or sets the shortcut menu associated with the cell.
        // </summary>
        // <returns>
        // The <see cref="T:System.Windows.Forms.ContextMenuStrip"/> associated with the cell.
        // </returns>
        // public virtual ContextMenuStrip ContextMenuStrip {get; set;}
        
        // <summary>
        // Gets the default value for a cell in the row for new records.
        // </summary>
        // <returns>
        // An <see cref="T:System.Object"/> representing the default value.
        // </returns>
        // public virtual object DefaultNewRowValue {get;}
        
        // <summary>
        // Gets a value that indicates whether the cell is currently displayed on-screen.
        // </summary>
        // <returns>
        // true if the cell is on-screen or partially on-screen; otherwise, false.
        // </returns>
        // public virtual bool Displayed {get;}
        
        // <summary>
        // Gets the current, formatted value of the cell, regardless of whether the cell is in edit mode and the value has not been committed.
        // </summary>
        // <returns>
        // The current, formatted value of the <see cref="T:System.Windows.Forms.DataGridViewCell"/>.
        // </returns>
        // <exception cref="T:System.ArgumentOutOfRangeException">The row containing the cell is a shared row.-or-The cell is a column header cell.</exception><exception cref="T:System.InvalidOperationException"><see cref="P:System.Windows.Forms.DataGridViewCell.ColumnIndex"/> is less than 0, indicating that the cell is a row header cell. </exception><exception cref="T:System.Exception">Formatting failed and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError"/> event of the <see cref="T:System.Windows.Forms.DataGridView"/> control or the handler set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException"/> property to true. The exception object can typically be cast to type <see cref="T:System.FormatException"/>.</exception>
        // public object EditedFormattedValue {get};
        
        // <summary>
        // Gets the type of the cell's hosted editing control.
        // </summary>
        // <returns>
        // A <see cref="T:System.Type"/> representing the <see cref="T:System.Windows.Forms.DataGridViewTextBoxEditingControl"/> type.
        // </returns>
        // public virtual System.Type EditType { get; }
        

        // <summary>
        // Gets the bounds of the error icon for the cell.
        // </summary>
        // <returns>
        // A <see cref="T:System.Drawing.Rectangle"/> that represents the bounds of the error icon for the current <see cref="T:System.Windows.Forms.DataGridViewCell"/>.
        // </returns>
        // <exception cref="T:System.InvalidOperationException">The cell does not belong to a <see cref="T:System.Windows.Forms.DataGridView"/> control.-or- <see cref="P:System.Windows.Forms.DataGridViewCell.ColumnIndex"/> is less than 0, indicating that the cell is a row header cell.</exception><exception cref="T:System.ArgumentOutOfRangeException">The row containing the cell is a shared row.-or-The cell is a column header cell.</exception>
        // public Rectangle ErrorIconBounds {get;}

        // <summary>
        // Gets or sets the text describing an error condition associated with the cell.
        // </summary>
        // <returns>
        // The text that describes an error condition associated with the cell.
        // </returns>
        // public string ErrorText {get; set;}

        // <summary>
        // Gets the value of the cell as formatted for display.
        // </summary>
        // <returns>
        // The formatted value of the cell or null if the cell does not belong to a <see cref="T:System.Windows.Forms.DataGridView"/> control.
        // </returns>
        // <exception cref="T:System.InvalidOperationException"><see cref="P:System.Windows.Forms.DataGridViewCell.ColumnIndex"/> is less than 0, indicating that the cell is a row header cell.</exception><exception cref="T:System.ArgumentOutOfRangeException">The row containing the cell is a shared row.-or-The cell is a column header cell.</exception><exception cref="T:System.Exception">Formatting failed and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError"/> event of the <see cref="T:System.Windows.Forms.DataGridView"/> control or the handler set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException"/> property to true. The exception object can typically be cast to type <see cref="T:System.FormatException"/>.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public object FormattedValue {get;}

        // <summary>
        // Gets the type of the formatted value associated with the cell.
        // </summary>
        // <returns>
        // A <see cref="T:System.Type"/> representing the type of the cell's formatted value.
        // </returns>
        // public virtual System.Type FormattedValueType {get;}

        // <summary>
        // Gets a value indicating whether the cell is frozen.
        // </summary>
        // <returns>
        // true if the cell is frozen; otherwise, false.
        // </returns>
        // public virtual bool Frozen {get;}

        // <summary>
        // Gets a value indicating whether the <see cref="P:System.Windows.Forms.DataGridViewCell.Style"/> property has been set.
        // </summary>
        // <returns>
        // true if the <see cref="P:System.Windows.Forms.DataGridViewCell.Style"/> property has been set; otherwise, false.
        // </returns>
        // public bool HasStyle {get;}

        // <summary>
        // Gets the current state of the cell as inherited from the state of its row and column.
        // </summary>
        // <returns>
        // A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates"/> values representing the current state of the cell.
        // </returns>
        // <exception cref="T:System.ArgumentException">The cell is not contained within a <see cref="T:System.Windows.Forms.DataGridView"/> control and the value of its <see cref="P:System.Windows.Forms.DataGridViewCell.RowIndex"/> property is not -1.</exception><exception cref="T:System.ArgumentOutOfRangeException">The cell is contained within a <see cref="T:System.Windows.Forms.DataGridView"/> control and the value of its <see cref="P:System.Windows.Forms.DataGridViewCell.RowIndex"/> property is -1.</exception>
        // public DataGridViewElementStates InheritedState { get; }

        // <summary>
        // Gets the style currently applied to the cell.
        // </summary>
        // <returns>
        // The <see cref="T:System.Windows.Forms.DataGridViewCellStyle"/> currently applied to the cell.
        // </returns>
        // <exception cref="T:System.InvalidOperationException">The cell does not belong to a <see cref="T:System.Windows.Forms.DataGridView"/> control.-or- <see cref="P:System.Windows.Forms.DataGridViewCell.ColumnIndex"/> is less than 0, indicating that the cell is a row header cell.</exception><exception cref="T:System.ArgumentOutOfRangeException">The row containing the cell is a shared row.-or-The cell is a column header cell.</exception>
        // public DataGridViewCellStyle InheritedStyle {get;}

        // <summary>
        // Gets a value indicating whether this cell is currently being edited.
        // </summary>
        // <returns>
        // true if the cell is in edit mode; otherwise, false.
        // </returns>
        // <exception cref="T:System.InvalidOperationException">The row containing the cell is a shared row.</exception>
        // public bool IsInEditMode {get;}

        /// <summary>
        /// Gets the column that contains this cell.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Windows.Forms.DataGridViewColumn"/> that contains the cell, or null if the cell is not in a column.
        /// </returns>
        public DataGridViewColumn OwningColumn
        {
            get
            {
                return default(DataGridViewColumn);
            }
        }

        /// <summary>
        /// Gets the row that contains this cell.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Windows.Forms.DataGridViewRow"/> that contains the cell, or null if the cell is not in a row.
        /// </returns>
        public DataGridViewRow OwningRow
        {
            get
            {
                return default(DataGridViewRow);
            }
        }
        
        // <summary>
        // Gets the size, in pixels, of a rectangular area into which the cell can fit.
        // </summary>
        // <returns>
        // A <see cref="T:System.Drawing.Size"/> containing the height and width, in pixels.
        // </returns>
        // <exception cref="T:System.InvalidOperationException"><see cref="P:System.Windows.Forms.DataGridViewCell.ColumnIndex"/> is less than 0, indicating that the cell is a row header cell.</exception><exception cref="T:System.ArgumentOutOfRangeException">The row containing the cell is a shared row.-or-The cell is a column header cell.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // [Browsable(false)]
        // public Size PreferredSize {get;}
        
        // <summary>
        // Gets or sets a value indicating whether the cell's data can be edited.
        // </summary>
        // <returns>
        // true if the cell's data cannot be edited; otherwise, false.
        // </returns>
        // <exception cref="T:System.InvalidOperationException">There is no owning row when setting this property. -or-The owning row is shared when setting this property.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public virtual bool ReadOnly { get; set; }
        
        // <summary>
        // Gets a value indicating whether the cell can be resized.
        // </summary>
        // <returns>
        // true if the cell can be resized; otherwise, false.
        // </returns>
        // public virtual bool Resizable {get;}
        
        /// <summary>
        /// Gets the index of the cell's parent row.
        /// </summary>
        /// <returns>
        /// The index of the row that contains the cell; -1 if there is no owning row.
        /// </returns>
        /// [Browsable(false)]
        public int RowIndex
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= -1);
                return default(int);
            }
        }

        // <summary>
        // Gets or sets a value indicating whether the cell has been selected.
        // </summary>
        // <returns>
        // true if the cell has been selected; otherwise, false.
        // </returns>
        // <exception cref="T:System.InvalidOperationException">There is no associated <see cref="T:System.Windows.Forms.DataGridView"/> when setting this property. -or-The owning row is shared when setting this property.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public virtual bool Selected {get; set;}
       
        // <summary>
        // Gets the size of the cell.
        // </summary>
        // <returns>
        // A <see cref="T:System.Drawing.Size"/> set to the owning row's height and the owning column's width.
        // </returns>
        // <exception cref="T:System.InvalidOperationException">The row containing the cell is a shared row.-or-The cell is a column header cell.</exception>
        // public Size Size {get;}
        
        // <summary>
        // Gets or sets the style for the cell.
        // </summary>
        // <returns>
        // The style associated with the cell.
        // </returns>
        // public DataGridViewCellStyle Style {get; set;}
        
        // <summary>
        // Gets or sets the object that contains supplemental data about the cell.
        // </summary>
        // <returns>
        // An <see cref="T:System.Object"/> that contains data about the cell. The default is null.
        // </returns>
        // public object Tag {get; set}
        
        // <summary>
        // Gets or sets the ToolTip text associated with this cell.
        // </summary>
        // <returns>
        // The ToolTip text associated with the cell. The default is <see cref="F:System.String.Empty"/>.
        // </returns>
        // public string ToolTipText {get; set;}
        
        // <summary>
        // Gets or sets the value associated with this cell.
        // </summary>
        // <returns>
        // Gets or sets the data to be displayed by the cell. The default is null.
        // </returns>
        // <exception cref="T:System.ArgumentOutOfRangeException"><see cref="P:System.Windows.Forms.DataGridViewCell.RowIndex"/> is outside the valid range of 0 to the number of rows in the control minus 1.</exception><exception cref="T:System.InvalidOperationException"><see cref="P:System.Windows.Forms.DataGridViewCell.ColumnIndex"/> is less than 0, indicating that the cell is a row header cell.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public object Value {get; set;}
       
        // <summary>
        // Gets or sets the data type of the values in the cell.
        // </summary>
        // <returns>
        // A <see cref="T:System.Type"/> representing the data type of the value in the cell.
        // </returns>
        // public virtual System.Type ValueType {get; set;}
        
        // <summary>
        // Gets a value indicating whether the cell is in a row or column that has been hidden.
        // </summary>
        // <returns>
        // true if the cell is visible; otherwise, false.
        // </returns>
        // public virtual bool Visible {get;}
        
        // <summary>
        // Modifies the input cell border style according to the specified criteria.
        // </summary>
        // <returns>
        // The modified <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle"/>.
        // </returns>
        // <param name="dataGridViewAdvancedBorderStyleInput">A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle"/> that represents the cell border style to modify.</param><param name="dataGridViewAdvancedBorderStylePlaceholder">A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle"/> that is used to store intermediate changes to the cell border style. </param><param name="singleVerticalBorderAdded">true to add a vertical border to the cell; otherwise, false. </param><param name="singleHorizontalBorderAdded">true to add a horizontal border to the cell; otherwise, false. </param><param name="isFirstDisplayedColumn">true if the hosting cell is in the first visible column; otherwise, false. </param><param name="isFirstDisplayedRow">true if the hosting cell is in the first visible row; otherwise, false. </param>
        // public virtual DataGridViewAdvancedBorderStyle AdjustCellBorderStyle(DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyleInput, DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStylePlaceholder, bool singleVerticalBorderAdded, bool singleHorizontalBorderAdded, bool isFirstDisplayedColumn, bool isFirstDisplayedRow)
        
        // <summary>
        // Returns a <see cref="T:System.Drawing.Rectangle"/> that represents the widths of all the cell margins.
        // </summary>
        // <returns>
        // A <see cref="T:System.Drawing.Rectangle"/> that represents the widths of all the cell margins.
        // </returns>
        // <param name="advancedBorderStyle">A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle"/> that the margins are to be calculated for. </param>
        // protected virtual Rectangle BorderWidths(DataGridViewAdvancedBorderStyle advancedBorderStyle)
        
        // <summary>
        // Indicates whether the cell's row will be unshared when the cell is clicked.
        // </summary>
        // <returns>
        // true if the row will be unshared, otherwise, false. The base <see cref="T:System.Windows.Forms.DataGridViewCell"/> class always returns false.
        // </returns>
        // <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> containing the data passed to the <see cref="M:System.Windows.Forms.DataGridViewCell.OnClick(System.Windows.Forms.DataGridViewCellEventArgs)"/> method.</param>
        // protected virtual bool ClickUnsharesRow(DataGridViewCellEventArgs e);
        
        // <summary>
        // Creates an exact copy of this cell.
        // </summary>
        // <returns>
        // An <see cref="T:System.Object"/> that represents the cloned <see cref="T:System.Windows.Forms.DataGridViewCell"/>.
        // </returns>
        // <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public virtual object Clone()
        
        // <summary>
        // Indicates whether the cell's row will be unshared when the cell's content is clicked.
        // </summary>
        // <returns>
        // true if the row will be unshared, otherwise, false. The base <see cref="T:System.Windows.Forms.DataGridViewCell"/> class always returns false.
        // </returns>
        // <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> containing the data passed to the <see cref="M:System.Windows.Forms.DataGridViewCell.OnContentClick(System.Windows.Forms.DataGridViewCellEventArgs)"/> method.</param>
        // protected virtual bool ContentClickUnsharesRow(DataGridViewCellEventArgs e)
        
        // <summary>
        // Indicates whether the cell's row will be unshared when the cell's content is double-clicked.
        // </summary>
        // <returns>
        // true if the row will be unshared, otherwise, false. The base <see cref="T:System.Windows.Forms.DataGridViewCell"/> class always returns false.
        // </returns>
        // <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> containing the data passed to the <see cref="M:System.Windows.Forms.DataGridViewCell.OnContentDoubleClick(System.Windows.Forms.DataGridViewCellEventArgs)"/> method.</param>
        // protected virtual bool ContentDoubleClickUnsharesRow(DataGridViewCellEventArgs e)
        
        /// <summary>
        /// Creates a new accessible object for the <see cref="T:System.Windows.Forms.DataGridViewCell"/>.
        /// </summary>
        /// <returns>
        /// A new <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject"/> for the <see cref="T:System.Windows.Forms.DataGridViewCell"/>.
        /// </returns>
        protected virtual AccessibleObject CreateAccessibilityInstance()
        {
            Contract.Ensures(Contract.Result<AccessibleObject>() != null);
            return default(AccessibleObject);
        }
        
        // <summary>
        // Removes the cell's editing control from the <see cref="T:System.Windows.Forms.DataGridView"/>.
        // </summary>
        // <exception cref="T:System.InvalidOperationException">This cell is not associated with a <see cref="T:System.Windows.Forms.DataGridView"/>.-or-The <see cref="P:System.Windows.Forms.DataGridView.EditingControl"/> property of the associated <see cref="T:System.Windows.Forms.DataGridView"/> has a value of null. This is the case, for example, when the control is not in edit mode.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public virtual void DetachEditingControl()
        
        // <summary>
        // Releases all resources used by the <see cref="T:System.Windows.Forms.DataGridViewCell"/>.
        // </summary>
        // public void Dispose()
        
        // <summary>
        // Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.DataGridViewCell"/> and optionally releases the managed resources.
        // </summary>
        // <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        // protected virtual void Dispose(bool disposing)
        
        // <summary>
        // Indicates whether the cell's row will be unshared when the cell is double-clicked.
        // </summary>
        // <returns>
        // true if the row will be unshared, otherwise, false. The base <see cref="T:System.Windows.Forms.DataGridViewCell"/> class always returns false.
        // </returns>
        // <param name="e">The <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> containing the data passed to the <see cref="M:System.Windows.Forms.DataGridViewCell.OnDoubleClick(System.Windows.Forms.DataGridViewCellEventArgs)"/> method.</param>
        // protected virtual bool DoubleClickUnsharesRow(DataGridViewCellEventArgs e)
        
        // <summary>
        // Indicates whether the parent row will be unshared when the focus moves to the cell.
        // </summary>
        // <returns>
        // true if the row will be unshared; otherwise, false. The base <see cref="T:System.Windows.Forms.DataGridViewCell"/> class always returns false.
        // </returns>
        // <param name="rowIndex">The index of the cell's parent row.</param><param name="throughMouseClick">true if a user action moved focus to the cell; false if a programmatic operation moved focus to the cell.</param>
        // protected virtual bool EnterUnsharesRow(int rowIndex, bool throughMouseClick)
        
        // <summary>
        // Retrieves the formatted value of the cell to copy to the <see cref="T:System.Windows.Forms.Clipboard"/>.
        // </summary>
        // <returns>
        // An <see cref="T:System.Object"/> that represents the value of the cell to copy to the <see cref="T:System.Windows.Forms.Clipboard"/>.
        // </returns>
        // <param name="rowIndex">The zero-based index of the row containing the cell.</param><param name="firstCell">true to indicate that the cell is in the first column of the region defined by the selected cells; otherwise, false.</param><param name="lastCell">true to indicate that the cell is the last column of the region defined by the selected cells; otherwise, false.</param><param name="inFirstRow">true to indicate that the cell is in the first row of the region defined by the selected cells; otherwise, false.</param><param name="inLastRow">true to indicate that the cell is in the last row of the region defined by the selected cells; otherwise, false.</param><param name="format">The current format string of the cell.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="rowIndex"/> is less than 0 or greater than or equal to the number of rows in the control.</exception><exception cref="T:System.InvalidOperationException">The value of the cell's <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView"/> property is null.-or-<see cref="P:System.Windows.Forms.DataGridViewCell.ColumnIndex"/> is less than 0, indicating that the cell is a row header cell.</exception><exception cref="T:System.Exception">Formatting failed and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError"/> event of the <see cref="T:System.Windows.Forms.DataGridView"/> control or the handler set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException"/> property to true. The exception object can typically be cast to type <see cref="T:System.FormatException"/>.</exception>
        // protected virtual object GetClipboardContent(int rowIndex, bool firstCell, bool lastCell, bool inFirstRow, bool inLastRow, string format)
        
        /// <summary>
        /// Returns the bounding rectangle that encloses the cell's content area using a default <see cref="T:System.Drawing.Graphics"/> and cell style currently in effect for the cell.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Drawing.Rectangle"/> that bounds the cell's contents.
        /// </returns>
        /// <param name="rowIndex">The index of the cell's parent row.</param><exception cref="T:System.ArgumentOutOfRangeException">The specified <paramref name="rowIndex"/> is less than 0 or greater than the number of rows in the control minus 1. </exception><exception cref="T:System.InvalidOperationException"><see cref="P:System.Windows.Forms.DataGridViewCell.ColumnIndex"/> is less than 0, indicating that the cell is a row header cell.</exception>
        public Rectangle GetContentBounds(int rowIndex)
        {
            Contract.Requires(DataGridView == null || (rowIndex >= 0 || rowIndex < DataGridView.Rows.Count));
            return default(Rectangle);
        }

        // <summary>
        // Returns the bounding rectangle that encloses the cell's content area, which is calculated using the specified <see cref="T:System.Drawing.Graphics"/> and cell style.
        // </summary>
        // <returns>
        // The <see cref="T:System.Drawing.Rectangle"/> that bounds the cell's contents.
        // </returns>
        // <param name="graphics">The graphics context for the cell.</param><param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle"/> to be applied to the cell.</param><param name="rowIndex">The index of the cell's parent row.</param>
        // protected virtual Rectangle GetContentBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)
        
        /// <summary>
        /// Returns the current, formatted value of the cell, regardless of whether the cell is in edit mode and the value has not been committed.
        /// </summary>
        /// 
        /// <returns>
        /// The current, formatted value of the <see cref="T:System.Windows.Forms.DataGridViewCell"/>.
        /// </returns>
        /// <param name="rowIndex">The row index of the cell.</param><param name="context">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewDataErrorContexts"/> values that specifies the data error context.</param><exception cref="T:System.ArgumentOutOfRangeException">The specified <paramref name="rowIndex"/> is less than 0 or greater than the number of rows in the control minus 1. </exception><exception cref="T:System.InvalidOperationException"><see cref="P:System.Windows.Forms.DataGridViewCell.ColumnIndex"/> is less than 0, indicating that the cell is a row header cell.</exception><exception cref="T:System.Exception">Formatting failed and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError"/> event of the <see cref="T:System.Windows.Forms.DataGridView"/> control or the handler set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException"/> property to true. The exception object can typically be cast to type <see cref="T:System.FormatException"/>.</exception>
        public object GetEditedFormattedValue(int rowIndex, DataGridViewDataErrorContexts context)
        {
            Contract.Requires(DataGridView == null || (rowIndex >= 0 || rowIndex < DataGridView.Rows.Count));
            return default(object);
        }

        // <summary>
        // Returns the bounding rectangle that encloses the cell's error icon, if one is displayed.
        // </summary>
        // <returns>
        // The <see cref="T:System.Drawing.Rectangle"/> that bounds the cell's error icon, if one is displayed; otherwise, <see cref="F:System.Drawing.Rectangle.Empty"/>.
        // </returns>
        // <param name="graphics">The graphics context for the cell.</param><param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle"/> to be applied to the cell.</param><param name="rowIndex">The index of the cell's parent row.</param>
        // protected virtual Rectangle GetErrorIconBounds(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex)

        // <summary>
        // Returns a string that represents the error for the cell.
        // </summary>
        // <returns>
        // A string that describes the error for the current <see cref="T:System.Windows.Forms.DataGridViewCell"/>.
        // </returns>
        // <param name="rowIndex">The row index of the cell.</param>
        // protected internal virtual string GetErrorText(int rowIndex)

        // <summary>
        // Gets the value of the cell as formatted for display.
        // </summary>
        // <returns>
        // The formatted value of the cell or null if the cell does not belong to a <see cref="T:System.Windows.Forms.DataGridView"/> control.
        // </returns>
        // <param name="value">The value to be formatted. </param><param name="rowIndex">The index of the cell's parent row. </param><param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle"/> in effect for the cell.</param><param name="valueTypeConverter">A <see cref="T:System.ComponentModel.TypeConverter"/> associated with the value type that provides custom conversion to the formatted value type, or null if no such custom conversion is needed.</param><param name="formattedValueTypeConverter">A <see cref="T:System.ComponentModel.TypeConverter"/> associated with the formatted value type that provides custom conversion from the value type, or null if no such custom conversion is needed.</param><param name="context">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewDataErrorContexts"/> values describing the context in which the formatted value is needed.</param><exception cref="T:System.Exception">Formatting failed and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError"/> event of the <see cref="T:System.Windows.Forms.DataGridView"/> control or the handler set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException"/> property to true. The exception object can typically be cast to type <see cref="T:System.FormatException"/>.</exception>
        // protected virtual object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, TypeConverter valueTypeConverter, TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)

        /// <summary>
        /// Gets the inherited shortcut menu for the current cell.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Windows.Forms.ContextMenuStrip"/> if the parent <see cref="T:System.Windows.Forms.DataGridView"/>, <see cref="T:System.Windows.Forms.DataGridViewRow"/>, or <see cref="T:System.Windows.Forms.DataGridViewColumn"/> has a <see cref="T:System.Windows.Forms.ContextMenuStrip"/> assigned; otherwise, null.
        /// </returns>
        /// <param name="rowIndex">The row index of the current cell.</param><exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView"/> property of the cell is not null and the specified <paramref name="rowIndex"/> is less than 0 or greater than the number of rows in the control minus 1. </exception><exception cref="T:System.InvalidOperationException"><see cref="P:System.Windows.Forms.DataGridViewCell.ColumnIndex"/> is less than 0, indicating that the cell is a row header cell.</exception>
        public virtual ContextMenuStrip GetInheritedContextMenuStrip(int rowIndex)
        {
            Contract.Requires(DataGridView == null || (rowIndex >= 0 || rowIndex < DataGridView.Rows.Count));
            Contract.Requires(ColumnIndex < 0);
            return default(ContextMenuStrip);
        }

        /// <summary>
        /// Returns a value indicating the current state of the cell as inherited from the state of its row and column.
        /// </summary>
        /// <returns>
        /// A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates"/> values representing the current state of the cell.
        /// </returns>
        /// <param name="rowIndex">The index of the row containing the cell.</param><exception cref="T:System.ArgumentException">The cell is not contained within a <see cref="T:System.Windows.Forms.DataGridView"/> control and <paramref name="rowIndex"/> is not -1.-or-<paramref name="rowIndex"/> is not the index of the row containing this cell.</exception><exception cref="T:System.ArgumentOutOfRangeException">The cell is contained within a <see cref="T:System.Windows.Forms.DataGridView"/> control and <paramref name="rowIndex"/> is outside the valid range of 0 to the number of rows in the control minus 1.</exception>
        public virtual DataGridViewElementStates GetInheritedState(int rowIndex)
        {
            Contract.Requires(DataGridView == null || (rowIndex >= 0 || rowIndex < DataGridView.Rows.Count));
            return default(DataGridViewElementStates);
        }

        /// <summary>
        /// Gets the style applied to the cell.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Windows.Forms.DataGridViewCellStyle"/> that includes the style settings of the cell inherited from the cell's parent row, column, and <see cref="T:System.Windows.Forms.DataGridView"/>.
        /// </returns>
        /// <param name="inheritedCellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle"/> to be populated with the inherited cell style. </param><param name="rowIndex">The index of the cell's parent row. </param><param name="includeColors">true to include inherited colors in the returned cell style; otherwise, false. </param><exception cref="T:System.InvalidOperationException">The cell has no associated <see cref="T:System.Windows.Forms.DataGridView"/>.-or-<see cref="P:System.Windows.Forms.DataGridViewCell.ColumnIndex"/> is less than 0, indicating that the cell is a row header cell.</exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="rowIndex"/> is less than 0, or greater than or equal to the number of rows in the parent <see cref="T:System.Windows.Forms.DataGridView"/>.</exception>
        public virtual DataGridViewCellStyle GetInheritedStyle(DataGridViewCellStyle inheritedCellStyle, int rowIndex,
            bool includeColors)
        {
            Contract.Requires(DataGridView != null);
            Contract.Requires(rowIndex >= 0 || rowIndex < DataGridView.Rows.Count);
            Contract.Requires(ColumnIndex < 0);
            return default(DataGridViewCellStyle);
        }

        // <summary>
        // Calculates the preferred size, in pixels, of the cell.
        // </summary>
        // <returns>
        // A <see cref="T:System.Drawing.Size"/> that represents the preferred size, in pixels, of the cell.
        // </returns>
        // <param name="graphics">The <see cref="T:System.Drawing.Graphics"/> used to draw the cell.</param><param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle"/> that represents the style of the cell.</param><param name="rowIndex">The zero-based row index of the cell.</param><param name="constraintSize">The cell's maximum allowable size.</param>
        // protected virtual Size GetPreferredSize(Graphics graphics, DataGridViewCellStyle cellStyle, int rowIndex, Size constraintSize)
        
        /// <summary>
        /// Gets the size of the cell.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Drawing.Size"/> representing the cell's dimensions.
        /// </returns>
        /// <param name="rowIndex">The index of the cell's parent row.</param><exception cref="T:System.InvalidOperationException"><paramref name="rowIndex"/> is -1</exception>
        protected virtual Size GetSize(int rowIndex)
        {
            Contract.Requires(rowIndex != -1);
            return default(Size);
        }
        
        /// <summary>
        /// Gets the value of the cell.
        /// </summary>
        /// <returns>
        /// The value contained in the <see cref="T:System.Windows.Forms.DataGridViewCell"/>.
        /// </returns>
        /// <param name="rowIndex">The index of the cell's parent row.</param><exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView"/> property of the cell is not null and <paramref name="rowIndex"/> is less than 0 or greater than or equal to the number of rows in the parent <see cref="T:System.Windows.Forms.DataGridView"/>.</exception><exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView"/> property of the cell is not null and the value of the <see cref="P:System.Windows.Forms.DataGridViewCell.ColumnIndex"/> property is less than 0, indicating that the cell is a row header cell.</exception>
        protected virtual object GetValue(int rowIndex)
        {
            Contract.Requires(DataGridView == null || (rowIndex >= 0 || rowIndex < DataGridView.Rows.Count));
            Contract.Requires(ColumnIndex < 0);
            return default(object);
            
        }
        
        /// <summary>
        /// Initializes the control used to edit the cell.
        /// </summary>
        /// <param name="rowIndex">The zero-based row index of the cell's location.</param><param name="initialFormattedValue">An <see cref="T:System.Object"/> that represents the value displayed by the cell when editing is started.</param><param name="dataGridViewCellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle"/> that represents the style of the cell.</param><exception cref="T:System.InvalidOperationException">There is no associated <see cref="T:System.Windows.Forms.DataGridView"/> or if one is present, it does not have an associated editing control. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public virtual void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            Contract.Requires(DataGridView != null);
            Contract.Requires(DataGridView.EditingControl != null);
        }

        // <summary>
        // Indicates whether the parent row is unshared if the user presses a key while the focus is on the cell.
        // </summary>
        // <returns>
        // true if the row will be unshared, otherwise, false. The base <see cref="T:System.Windows.Forms.DataGridViewCell"/> class always returns false.
        // </returns>
        // <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs"/> that contains the event data. </param><param name="rowIndex">The index of the cell's parent row. </param>
        // protected virtual bool KeyDownUnsharesRow(KeyEventArgs e, int rowIndex)
        
        // <summary>
        // Determines if edit mode should be started based on the given key.
        // </summary>
        // <returns>
        // true if edit mode should be started; otherwise, false. The default is false.
        // </returns>
        // <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs"/> that represents the key that was pressed.</param>
        // public virtual bool KeyEntersEditMode(KeyEventArgs e)
        
        // <summary>
        // Indicates whether a row will be unshared if a key is pressed while a cell in the row has focus.
        // </summary>
        // <returns>
        // true if the row will be unshared, otherwise, false. The base <see cref="T:System.Windows.Forms.DataGridViewCell"/> class always returns false.
        // </returns>
        // <param name="e">A <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> that contains the event data. </param><param name="rowIndex">The index of the cell's parent row. </param>
        // protected virtual bool KeyPressUnsharesRow(KeyPressEventArgs e, int rowIndex)
        
        // <summary>
        // Indicates whether the parent row is unshared when the user releases a key while the focus is on the cell.
        // </summary>
        // <returns>
        // true if the row will be unshared, otherwise, false. The base <see cref="T:System.Windows.Forms.DataGridViewCell"/> class always returns false.
        // </returns>
        // <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs"/> that contains the event data. </param><param name="rowIndex">The index of the cell's parent row. </param>
        // protected virtual bool KeyUpUnsharesRow(KeyEventArgs e, int rowIndex)
        
        // <summary>
        // Indicates whether a row will be unshared when the focus leaves a cell in the row.
        // </summary>
        // <returns>
        // true if the row will be unshared, otherwise, false. The base <see cref="T:System.Windows.Forms.DataGridViewCell"/> class always returns false.
        // </returns>
        // <param name="rowIndex">The index of the cell's parent row.</param><param name="throughMouseClick">true if a user action moved focus to the cell; false if a programmatic operation moved focus to the cell.</param>
        // protected virtual bool LeaveUnsharesRow(int rowIndex, bool throughMouseClick)
        
        /// <summary>
        /// Gets the height, in pixels, of the specified text, given the specified characteristics.
        /// </summary>
        /// <returns>
        /// The height, in pixels, of the text.
        /// </returns>
        /// <param name="graphics">The <see cref="T:System.Drawing.Graphics"/> used to render the text.</param><param name="text">The text to measure.</param><param name="font">The <see cref="T:System.Drawing.Font"/> applied to the text.</param><param name="maxWidth">The maximum width of the text.</param><param name="flags">A bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags"/>  values to apply to the text.</param><exception cref="T:System.ArgumentNullException"><paramref name="graphics"/> is null.-or-<paramref name="font"/> is null.</exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="maxWidth"/> is less than 1.</exception><exception cref="T:System.ComponentModel.InvalidEnumArgumentException"><paramref name="flags"/> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags"/>  values.</exception>
        public static int MeasureTextHeight(Graphics graphics, string text, Font font, int maxWidth, TextFormatFlags flags)
        {
            Contract.Requires(graphics != null);
            Contract.Requires(font != null);
            Contract.Requires(maxWidth > 0);
            // Contract.Requires(DataGridViewUtilities.ValidTextFormatFlags(flags));
            return default(int);
        }

        /// <summary>
        /// Gets the height, in pixels, of the specified text, given the specified characteristics. Also indicates whether the required width is greater than the specified maximum width.
        /// </summary>
        /// <returns>
        /// The height, in pixels, of the text.
        /// </returns>
        /// <param name="graphics">The <see cref="T:System.Drawing.Graphics"/> used to render the text.</param><param name="text">The text to measure.</param><param name="font">The <see cref="T:System.Drawing.Font"/> applied to the text.</param><param name="maxWidth">The maximum width of the text.</param><param name="flags">A bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags"/>  values to apply to the text.</param><param name="widthTruncated">Set to true if the required width of the text is greater than <paramref name="maxWidth"/>.</param><exception cref="T:System.ArgumentNullException"><paramref name="graphics"/> is null.-or-<paramref name="font"/> is null.</exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="maxWidth"/> is less than 1.</exception><exception cref="T:System.ComponentModel.InvalidEnumArgumentException"><paramref name="flags"/> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags"/>  values.</exception>
        public static int MeasureTextHeight(Graphics graphics, string text, Font font, int maxWidth, TextFormatFlags flags, out bool widthTruncated)
        {
            Contract.Requires(graphics != null);
            Contract.Requires(font != null);
            Contract.Requires(maxWidth > 0);
            Contract.Requires(DataGridViewUtilities.ValidTextFormatFlags(flags));
            widthTruncated = default(bool);
            return default(int);
        }

        /// <summary>
        /// Gets the ideal height and width of the specified text given the specified characteristics.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Drawing.Size"/> representing the preferred height and width of the text.
        /// </returns>
        /// <param name="graphics">The <see cref="T:System.Drawing.Graphics"/> used to render the text.</param><param name="text">The text to measure.</param><param name="font">The <see cref="T:System.Drawing.Font"/> applied to the text.</param><param name="maxRatio">The maximum width-to-height ratio of the block of text.</param><param name="flags">A bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags"/>  values to apply to the text.</param><exception cref="T:System.ArgumentNullException"><paramref name="graphics"/> is null.-or-<paramref name="font"/> is null.</exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="maxRatio"/> is less than or equal to 0.</exception><exception cref="T:System.ComponentModel.InvalidEnumArgumentException"><paramref name="flags"/> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags"/>  values.</exception>
        public static Size MeasureTextPreferredSize(Graphics graphics, string text, Font font, float maxRatio, TextFormatFlags flags)
        {
            Contract.Requires(graphics != null);
            Contract.Requires(font != null);
            Contract.Requires(maxRatio > 0.0);
            Contract.Requires(DataGridViewUtilities.ValidTextFormatFlags(flags));
            return default(Size);
        }

        /// <summary>
        /// Gets the height and width of the specified text given the specified characteristics.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Drawing.Size"/> representing the height and width of the text.
        /// </returns>
        /// <param name="graphics">The <see cref="T:System.Drawing.Graphics"/> used to render the text.</param><param name="text">The text to measure.</param><param name="font">The <see cref="T:System.Drawing.Font"/> applied to the text.</param><param name="flags">A bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags"/>  values to apply to the text.</param><exception cref="T:System.ArgumentNullException"><paramref name="graphics"/> is null.-or-<paramref name="font"/> is null.</exception><exception cref="T:System.ComponentModel.InvalidEnumArgumentException"><paramref name="flags"/> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags"/>  values.</exception>
        public static Size MeasureTextSize(Graphics graphics, string text, Font font, TextFormatFlags flags)
        {
            Contract.Requires(graphics != null);
            Contract.Requires(font != null);
            Contract.Requires(DataGridViewUtilities.ValidTextFormatFlags(flags));
            return default(Size);
        }

        /// <summary>
        /// Gets the width, in pixels, of the specified text given the specified characteristics.
        /// </summary>
        /// <returns>
        /// The width, in pixels, of the text.
        /// </returns>
        /// <param name="graphics">The <see cref="T:System.Drawing.Graphics"/> used to render the text.</param><param name="text">The text to measure.</param><param name="font">The <see cref="T:System.Drawing.Font"/> applied to the text.</param><param name="maxHeight">The maximum height of the text.</param><param name="flags">A bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags"/>  values to apply to the text.</param><exception cref="T:System.ArgumentNullException"><paramref name="graphics"/> is null.-or-<paramref name="font"/> is null.</exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="maxHeight"/> is less than 1.</exception><exception cref="T:System.ComponentModel.InvalidEnumArgumentException"><paramref name="flags"/> is not a valid bitwise combination of <see cref="T:System.Windows.Forms.TextFormatFlags"/>  values.</exception>
        
        public static int MeasureTextWidth(Graphics graphics, string text, Font font, int maxHeight, TextFormatFlags flags)
        {
            Contract.Requires(graphics != null);
            Contract.Requires(font != null);
            Contract.Requires(maxHeight > 0);
            return default(int);
        }

        // <summary>
        // Indicates whether a row will be unshared if the user clicks a mouse button while the pointer is on a cell in the row.
        // </summary>
        // <returns>
        // true if the row will be unshared, otherwise, false. The base <see cref="T:System.Windows.Forms.DataGridViewCell"/> class always returns false.
        // </returns>
        // <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> that contains the event data. </param>
        // protected virtual bool MouseClickUnsharesRow(DataGridViewCellMouseEventArgs e)

        // <summary>
        // Indicates whether a row will be unshared if the user double-clicks a cell in the row.
        // </summary>
        // <returns>
        // true if the row will be unshared, otherwise, false. The base <see cref="T:System.Windows.Forms.DataGridViewCell"/> class always returns false.
        // </returns>
        // <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> that contains the event data.</param>
        // protected virtual bool MouseDoubleClickUnsharesRow(DataGridViewCellMouseEventArgs e)

        // <summary>
        // Indicates whether a row will be unshared when the user holds down a mouse button while the pointer is on a cell in the row.
        // </summary>
        // <returns>
        // true if the row will be unshared, otherwise, false. The base <see cref="T:System.Windows.Forms.DataGridViewCell"/> class always returns false.
        // </returns>
        // <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> that contains the event data. </param>
        // protected virtual bool MouseDownUnsharesRow(DataGridViewCellMouseEventArgs e)

        // <summary>
        // Indicates whether a row will be unshared when the mouse pointer moves over a cell in the row.
        // </summary>
        // <returns>
        // true if the row will be unshared, otherwise, false. The base <see cref="T:System.Windows.Forms.DataGridViewCell"/> class always returns false.
        // </returns>
        // <param name="rowIndex">The index of the cell's parent row. </param>
        // protected virtual bool MouseEnterUnsharesRow(int rowIndex)

        // <summary>
        // Indicates whether a row will be unshared when the mouse pointer leaves the row.
        // </summary>
        // <returns>
        // true if the row will be unshared, otherwise, false. The base <see cref="T:System.Windows.Forms.DataGridViewCell"/> class always returns false.
        // </returns>
        // <param name="rowIndex">The index of the cell's parent row. </param>
        // protected virtual bool MouseLeaveUnsharesRow(int rowIndex)

        // <summary>
        // Indicates whether a row will be unshared when the mouse pointer moves over a cell in the row.
        // </summary>
        // <returns>
        // true if the row will be unshared, otherwise, false. The base <see cref="T:System.Windows.Forms.DataGridViewCell"/> class always returns false.
        // </returns>
        // <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> that contains the event data. </param>
        // protected virtual bool MouseMoveUnsharesRow(DataGridViewCellMouseEventArgs e)

        // <summary>
        // Indicates whether a row will be unshared when the user releases a mouse button while the pointer is on a cell in the row.
        // </summary>
        // <returns>
        // true if the row will be unshared, otherwise, false. The base <see cref="T:System.Windows.Forms.DataGridViewCell"/> class always returns false.
        // </returns>
        // <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> that contains the event data. </param>
        // protected virtual bool MouseUpUnsharesRow(DataGridViewCellMouseEventArgs e)

        // <summary>
        // Called when the cell's contents are clicked.
        // </summary>
        // <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> that contains the event data. </param>
        // protected virtual void OnContentClick(DataGridViewCellEventArgs e)

        // <summary>
        // Called when the cell's contents are double-clicked.
        // </summary>
        // <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> that contains the event data. </param>
        // protected virtual void OnContentDoubleClick(DataGridViewCellEventArgs e)

        // <summary>
        // Called when the cell is double-clicked.
        // </summary>
        // <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> that contains the event data. </param>
        // protected virtual void OnDoubleClick(DataGridViewCellEventArgs e)

        // <summary>
        // Called when the focus moves to a cell.
        // </summary>
        // <param name="rowIndex">The index of the cell's parent row. </param><param name="throughMouseClick">true if a user action moved focus to the cell; false if a programmatic operation moved focus to the cell.</param>
        // protected virtual void OnEnter(int rowIndex, bool throughMouseClick)

        // <summary>
        // Called when a character key is pressed while the focus is on a cell.
        // </summary>
        // <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs"/> that contains the event data. </param><param name="rowIndex">The index of the cell's parent row. </param>
        // protected virtual void OnKeyDown(KeyEventArgs e, int rowIndex)

        // <summary>
        // Called when a key is pressed while the focus is on a cell.
        // </summary>
        // <param name="e">A <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> that contains the event data. </param><param name="rowIndex">The index of the cell's parent row. </param>
        // protected virtual void OnKeyPress(KeyPressEventArgs e, int rowIndex)

        // <summary>
        // Called when a character key is released while the focus is on a cell.
        // </summary>
        // <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs"/> that contains the event data. </param><param name="rowIndex">The index of the cell's parent row. </param>
        // protected virtual void OnKeyUp(KeyEventArgs e, int rowIndex)

        // <summary>
        // Called when the focus moves from a cell.
        // </summary>
        // <param name="rowIndex">The index of the cell's parent row. </param><param name="throughMouseClick">true if a user action moved focus from the cell; false if a programmatic operation moved focus from the cell.</param>
        // protected virtual void OnLeave(int rowIndex, bool throughMouseClick)

        // <summary>
        // Called when the user clicks a mouse button while the pointer is on a cell.
        // </summary>
        // <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> that contains the event data. </param>
        // protected virtual void OnMouseClick(DataGridViewCellMouseEventArgs e)

        // <summary>
        // Called when the user double-clicks a mouse button while the pointer is on a cell.
        // </summary>
        // <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> that contains the event data. </param>
        // protected virtual void OnMouseDoubleClick(DataGridViewCellMouseEventArgs e)

        // <summary>
        // Called when the user holds down a mouse button while the pointer is on a cell.
        // </summary>
        // <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> that contains the event data. </param>
        // protected virtual void OnMouseDown(DataGridViewCellMouseEventArgs e)

        // <summary>
        // Called when the mouse pointer moves over a cell.
        // </summary>
        // <param name="rowIndex">The index of the cell's parent row. </param>
        // protected virtual void OnMouseEnter(int rowIndex)

        // <summary>
        // Called when the mouse pointer leaves the cell.
        // </summary>
        // <param name="rowIndex">The index of the cell's parent row. </param>
        // protected virtual void OnMouseLeave(int rowIndex)

        // <summary>
        // Called when the mouse pointer moves within a cell.
        // </summary>
        // <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> that contains the event data. </param>
        // protected virtual void OnMouseMove(DataGridViewCellMouseEventArgs e)

        // <summary>
        // Called when the user releases a mouse button while the pointer is on a cell.
        // </summary>
        // <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> that contains the event data. </param>
        // protected virtual void OnMouseUp(DataGridViewCellMouseEventArgs e)

        // <summary>
        // Called when the <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView"/> property of the cell changes.
        // </summary>
        // protected override void OnDataGridViewChanged()

        /// <summary>
        /// Paints the current <see cref="T:System.Windows.Forms.DataGridViewCell"/>.
        /// </summary>
        /// <param name="graphics">The <see cref="T:System.Drawing.Graphics"/> used to paint the <see cref="T:System.Windows.Forms.DataGridViewCell"/>.</param><param name="clipBounds">A <see cref="T:System.Drawing.Rectangle"/> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView"/> that needs to be repainted.</param><param name="cellBounds">A <see cref="T:System.Drawing.Rectangle"/> that contains the bounds of the <see cref="T:System.Windows.Forms.DataGridViewCell"/> that is being painted.</param><param name="rowIndex">The row index of the cell that is being painted.</param><param name="cellState">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates"/> values that specifies the state of the cell.</param><param name="value">The data of the <see cref="T:System.Windows.Forms.DataGridViewCell"/> that is being painted.</param><param name="formattedValue">The formatted data of the <see cref="T:System.Windows.Forms.DataGridViewCell"/> that is being painted.</param><param name="errorText">An error message that is associated with the cell.</param><param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle"/> that contains formatting and style information about the cell.</param><param name="advancedBorderStyle">A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle"/> that contains border styles for the cell that is being painted.</param><param name="paintParts">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewPaintParts"/> values that specifies which parts of the cell need to be painted.</param>
        protected virtual void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex,
            DataGridViewElementStates cellState, object value, object formattedValue, string errorText,
            DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle,
            DataGridViewPaintParts paintParts)
        {
            
        }

        /// <summary>
        /// Paints the border of the current <see cref="T:System.Windows.Forms.DataGridViewCell"/>.
        /// </summary>
        /// <param name="graphics">The <see cref="T:System.Drawing.Graphics"/> used to paint the border.</param><param name="clipBounds">A <see cref="T:System.Drawing.Rectangle"/> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView"/> that needs to be repainted.</param><param name="bounds">A <see cref="T:System.Drawing.Rectangle"/> that contains the area of the border that is being painted.</param><param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle"/> that contains formatting and style information about the current cell.</param><param name="advancedBorderStyle">A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle"/> that contains border styles of the border that is being painted.</param>
        protected virtual void PaintBorder(Graphics graphics, Rectangle clipBounds, Rectangle bounds,
            DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle)
        {
            Contract.Requires(graphics != null);
            Contract.Requires(cellStyle != null);
        }

        /// <summary>
        /// Paints the error icon of the current <see cref="T:System.Windows.Forms.DataGridViewCell"/>.
        /// </summary>
        /// <param name="graphics">The <see cref="T:System.Drawing.Graphics"/> used to paint the border.</param><param name="clipBounds">A <see cref="T:System.Drawing.Rectangle"/> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView"/> that needs to be repainted.</param><param name="cellValueBounds">The bounding <see cref="T:System.Drawing.Rectangle"/> that encloses the cell's content area.</param><param name="errorText">An error message that is associated with the cell.</param>
        protected virtual void PaintErrorIcon(Graphics graphics, Rectangle clipBounds, Rectangle cellValueBounds, string errorText)
        {
            Contract.Requires(graphics != null);
        }
        
        /// <summary>
        /// Converts a value formatted for display to an actual cell value.
        /// </summary>
        /// 
        /// <returns>
        /// The cell value.
        /// </returns>
        /// <param name="formattedValue">The display value of the cell.</param><param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle"/> in effect for the cell.</param><param name="formattedValueTypeConverter">A <see cref="T:System.ComponentModel.TypeConverter"/> for the display value type, or null to use the default converter.</param><param name="valueTypeConverter">A <see cref="T:System.ComponentModel.TypeConverter"/> for the cell value type, or null to use the default converter.</param><exception cref="T:System.ArgumentNullException"><paramref name="cellStyle"/> is null.</exception><exception cref="T:System.FormatException">The <see cref="P:System.Windows.Forms.DataGridViewCell.FormattedValueType"/> property value is null.-or-The <see cref="P:System.Windows.Forms.DataGridViewCell.ValueType"/> property value is null.-or-<paramref name="formattedValue"/> cannot be converted.</exception><exception cref="T:System.ArgumentException"><paramref name="formattedValue"/> is null.-or-The type of <paramref name="formattedValue"/> does not match the type indicated by the <see cref="P:System.Windows.Forms.DataGridViewCell.FormattedValueType"/> property. </exception>
        public virtual object ParseFormattedValue(object formattedValue, DataGridViewCellStyle cellStyle, TypeConverter formattedValueTypeConverter, TypeConverter valueTypeConverter)
        {
            Contract.Requires(cellStyle != null);
            return default(object);
        }
        
        /// <summary>
        /// Sets the location and size of the editing control hosted by a cell in the <see cref="T:System.Windows.Forms.DataGridView"/> control.
        /// </summary>
        /// <param name="setLocation">true to have the control placed as specified by the other arguments; false to allow the control to place itself.</param><param name="setSize">true to specify the size; false to allow the control to size itself. </param><param name="cellBounds">A <see cref="T:System.Drawing.Rectangle"/> that defines the cell bounds. </param><param name="cellClip">The area that will be used to paint the editing control.</param><param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle"/> that represents the style of the cell being edited.</param><param name="singleVerticalBorderAdded">true to add a vertical border to the cell; otherwise, false.</param><param name="singleHorizontalBorderAdded">true to add a horizontal border to the cell; otherwise, false.</param><param name="isFirstDisplayedColumn">true if the hosting cell is in the first visible column; otherwise, false.</param><param name="isFirstDisplayedRow">true if the hosting cell is in the first visible row; otherwise, false.</param><exception cref="T:System.InvalidOperationException">The cell is not contained within a <see cref="T:System.Windows.Forms.DataGridView"/> control.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public virtual void PositionEditingControl(bool setLocation, bool setSize, Rectangle cellBounds, Rectangle cellClip, DataGridViewCellStyle cellStyle, bool singleVerticalBorderAdded, bool singleHorizontalBorderAdded, bool isFirstDisplayedColumn, bool isFirstDisplayedRow)
        {
            Contract.Requires(DataGridView != null);
        }

        /// <summary>
        /// Sets the location and size of the editing panel hosted by the cell, and returns the normal bounds of the editing control within the editing panel.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Drawing.Rectangle"/> that represents the normal bounds of the editing control within the editing panel.
        /// </returns>
        /// <param name="cellBounds">A <see cref="T:System.Drawing.Rectangle"/> that defines the cell bounds. </param><param name="cellClip">The area that will be used to paint the editing panel.</param><param name="cellStyle">A <see cref="T:System.Windows.Forms.DataGridViewCellStyle"/> that represents the style of the cell being edited.</param><param name="singleVerticalBorderAdded">true to add a vertical border to the cell; otherwise, false.</param><param name="singleHorizontalBorderAdded">true to add a horizontal border to the cell; otherwise, false.</param><param name="isFirstDisplayedColumn">true if the cell is in the first column currently displayed in the control; otherwise, false.</param><param name="isFirstDisplayedRow">true if the cell is in the first row currently displayed in the control; otherwise, false.</param><exception cref="T:System.InvalidOperationException">The cell has not been added to a <see cref="T:System.Windows.Forms.DataGridView"/> control.</exception>
        public virtual Rectangle PositionEditingPanel(Rectangle cellBounds, Rectangle cellClip,
            DataGridViewCellStyle cellStyle, bool singleVerticalBorderAdded, bool singleHorizontalBorderAdded,
            bool isFirstDisplayedColumn, bool isFirstDisplayedRow)
        {
            Contract.Requires(DataGridView != null);
            return default(Rectangle);
        }

        // <summary>
        // Sets the value of the cell.
        // </summary>
        // <returns>
        // true if the value has been set; otherwise, false.
        // </returns>
        // <param name="rowIndex">The index of the cell's parent row. </param><param name="value">The cell value to set. </param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="rowIndex"/> is less than 0 or greater than or equal to the number of rows in the parent <see cref="T:System.Windows.Forms.DataGridView"/>.</exception><exception cref="T:System.InvalidOperationException"><see cref="P:System.Windows.Forms.DataGridViewCell.ColumnIndex"/> is less than 0.</exception>
        // protected virtual bool SetValue(int rowIndex, object value)
        
        // <summary>
        // Returns a string that describes the current object.
        // </summary>
        // <returns>
        // A string that represents the current object.
        // </returns>
        // public override string ToString()
        
        /// <summary>
        /// Provides information about a <see cref="T:System.Windows.Forms.DataGridViewCell"/> to accessibility client applications.
        /// </summary>
        protected class DataGridViewCellAccessibleObject : AccessibleObject
        {
            // <summary>
            // Gets the location and size of the accessible object.
            // </summary>
            // <returns>
            // A <see cref="T:System.Drawing.Rectangle"/> that represents the bounds of the accessible object.
            // </returns>
            // <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner"/> property is null.</exception>
            // public override Rectangle Bounds {get;}
            
            /// <summary>
            /// Gets a string that describes the default action of the <see cref="T:System.Windows.Forms.DataGridViewCell"/>.
            /// </summary>
            /// <returns>
            /// The string "Edit".
            /// </returns>
            /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner"/> property is null.</exception>
            public override string DefaultAction
            {
                get
                {
                    Contract.Requires(Owner != null);
                    Contract.Ensures(Contract.Result<string>() != null);
                    return default(string);
                }
            }

            /// <summary>
            /// Gets the names of the owning cell's type and base type.
            /// </summary>
            /// <returns>
            /// The names of the owning cell's type and base type.
            /// </returns>
            public override string Help
            {
                get
                {
                    Contract.Ensures(Contract.Result<string>() != null);
                    return default(string);
                }
            }

            /// <summary>
            /// Gets the name of the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject"/>.
            /// </summary>
            /// <returns>
            /// The name of the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject"/>.
            /// </returns>
            /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner"/> property is null.</exception>
            public override string Name
            {
                get
                {
                    Contract.Requires(Owner != null);
                    Contract.Ensures(Contract.Result<string>() != null);
                    return default(string);
                }
            }

            /// <summary>
            /// Gets or sets the cell that owns the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject"/>.
            /// </summary>
            /// <returns>
            /// The <see cref="T:System.Windows.Forms.DataGridViewCell"/> that owns the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject"/>.
            /// </returns>
            /// <exception cref="T:System.InvalidOperationException">When setting this property, the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner"/> property has already been set.</exception>
            public DataGridViewCell Owner
            {
                get { return default(DataGridViewCell); }
                set { Contract.Requires(Owner == null); } 
            }

            /// <summary>
            /// Gets the parent of the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject"/>.
            /// </summary>
            /// <returns>
            /// The parent of the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject"/>.
            /// </returns>
            /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner"/> property is null.</exception>
            public override AccessibleObject Parent
            {
                get
                {
                    Contract.Requires(Owner != null);
                    return default(AccessibleObject);
                }
            }
            
            // <summary>
            // Gets the role of the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject"/>.
            // </summary>
            // <returns>
            // The <see cref="F:System.Windows.Forms.AccessibleRole.Cell"/> value.
            // </returns>
            // public override AccessibleRole Role;
            
            /// <summary>
            /// Gets the state of the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject"/>.
            /// </summary>
            /// <returns>
            /// A bitwise combination of <see cref="T:System.Windows.Forms.AccessibleStates"/> values.
            /// </returns>
            /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner"/> property is null.</exception>
            public override AccessibleStates State
            {
                get
                {
                    Contract.Requires(Owner != null);
                    return default(AccessibleStates);
                }
            }

            /// <summary>
            /// Gets or sets a string representing the formatted value of the owning cell.
            /// </summary>
            /// <returns>
            /// A <see cref="T:System.String"/> representation of the cell value.
            /// </returns>
            /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner"/> property is null.</exception>
            public override string Value
            {
                get
                {
                    Contract.Requires(Owner != null);
                    return default(string);
                }
                set
                {
                    
                }
            }

            // <summary>
            // Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject"/> class without initializing the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner"/> property.
            // </summary>
            // public DataGridViewCellAccessibleObject()

            // <summary>
            // Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject"/> class, setting the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner"/> property to the specified <see cref="T:System.Windows.Forms.DataGridViewCell"/>.
            // </summary>
            // <param name="owner">The <see cref="T:System.Windows.Forms.DataGridViewCell"/> that owns the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject"/>.</param>
            // public DataGridViewCellAccessibleObject(DataGridViewCell owner)

            /// <summary>
            /// Performs the default action associated with the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject"/>.
            /// </summary>
            /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner"/> property is null.-or-The value of the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject"/> property is not null and the <see cref="P:System.Windows.Forms.DataGridViewCell.RowIndex"/> property of the <see cref="T:System.Windows.Forms.DataGridViewCell"/> returned by the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner"/> property is equal to -1.</exception>
            public override void DoDefaultAction()
            {
                Contract.Requires(Owner != null);
            }
            
            /// <summary>
            /// Returns the accessible object corresponding to the specified index.
            /// </summary>
            /// <returns>
            /// An <see cref="T:System.Windows.Forms.AccessibleObject"/> that represents the accessible child corresponding to the specified index.
            /// </returns>
            /// <param name="index">The zero-based index of the child accessible object.</param><exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner"/> property is null.</exception>
            public override AccessibleObject GetChild(int index)
            {
                Contract.Requires(Owner != null);
                return default(AccessibleObject);
            }

            /// <summary>
            /// Returns the number of children that belong to the <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject"/>.
            /// </summary>
            /// <returns>
            /// The value 1 if the <see cref="T:System.Windows.Forms.DataGridViewCell"/> that owns <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject"/> is being edited; otherwise, –1.
            /// </returns>
            /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner"/> property is null.</exception>
            public override int GetChildCount()
            {
                Contract.Requires(Owner != null);
                return default(int);
            }

            // <summary>
            // Returns the child accessible object that has keyboard focus.
            // </summary>
            // <returns>
            // null in all cases.
            // </returns>
            // public override AccessibleObject GetFocused()
            
            // <summary>
            // Returns the child accessible object that is currently selected.
            // </summary>
            // <returns>
            // null in all cases.
            // </returns>
            // public override AccessibleObject GetSelected()
            
            /// <summary>
            /// Navigates to another accessible object.
            /// </summary>
            /// <returns>
            /// A <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject"/> that represents the <see cref="T:System.Windows.Forms.DataGridViewCell"/> at the specified <see cref="T:System.Windows.Forms.AccessibleNavigation"/> value.
            /// </returns>
            /// <param name="navigationDirection">One of the <see cref="T:System.Windows.Forms.AccessibleNavigation"/> values.</param><exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner"/> property is null.</exception>
            public override AccessibleObject Navigate(AccessibleNavigation navigationDirection)
            {
                Contract.Requires(Owner != null);
                return default(AccessibleObject);
            }
            
            /// <summary>
            /// Modifies the selection or moves the keyboard focus of the accessible object.
            /// </summary>
            /// <param name="flags">One of the <see cref="T:System.Windows.Forms.AccessibleSelection"/> values.</param><exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject.Owner"/> property is null.</exception>
            public override void Select(AccessibleSelection flags)
            {
                Contract.Requires(Owner != null);
            }
        }
    }
}
