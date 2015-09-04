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
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;

namespace System.Windows.Forms
{
    /// <summary>
    /// Represents a row in a <see cref="T:System.Windows.Forms.DataGridView"/> control.
    /// </summary>
    public class DataGridViewRow : DataGridViewBand
    {
        /// <summary>
        /// Gets the <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject"/> assigned to the <see cref="T:System.Windows.Forms.DataGridViewRow"/>.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject"/> assigned to the <see cref="T:System.Windows.Forms.DataGridViewRow"/>.
        /// </returns>
        /// 
        public AccessibleObject AccessibilityObject
        {
            get
            {
                Contract.Ensures(Contract.Result<AccessibleObject>() != null);
                return default(AccessibleObject);
            }
        }

        /// <summary>
        /// Gets the collection of cells that populate the row.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Windows.Forms.DataGridViewCellCollection"/> that contains all of the cells in the row.
        /// </returns>
        public DataGridViewCellCollection Cells
        {
            get
            {
                Contract.Ensures(Contract.Result<DataGridViewCellCollection>() != null);
                return default(DataGridViewCellCollection);
            }
        }

        // <summary>
        // Gets or sets the shortcut menu for the row.
        // </summary>
        // <returns>
        // The <see cref="T:System.Windows.Forms.ContextMenuStrip"/> associated with the current <see cref="T:System.Windows.Forms.DataGridViewRow"/>. The default is null.
        // </returns>
        // <exception cref="T:System.InvalidOperationException">When getting the value of this property, the row is in a <see cref="T:System.Windows.Forms.DataGridView"/> control and is a shared row.</exception>
        // public override ContextMenuStrip ContextMenuStrip {get; set;}
        
        // <summary>
        // Gets the data-bound object that populated the row.
        // </summary>
        // <returns>
        // The data-bound <see cref="T:System.Object"/>.
        // </returns>
        // public object DataBoundItem { get;  }
        
        /// <summary>
        /// Gets or sets the default styles for the row, which are used to render cells in the row unless the styles are overridden.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Windows.Forms.DataGridViewCellStyle"/> to be applied as the default style.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">When setting this property, the row is in a <see cref="T:System.Windows.Forms.DataGridView"/> control and is a shared row.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public override DataGridViewCellStyle DefaultCellStyle
        {
            get { return default(DataGridViewCellStyle); }
            set
            {
                Contract.Requires(!(DataGridView != null && Index == -1));
            }
        }

        /// <summary>
        /// Gets a value indicating whether this row is displayed on the screen.
        /// </summary>
        /// <returns>
        /// true if the row is currently displayed on the screen; otherwise, false.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">The row is in a <see cref="T:System.Windows.Forms.DataGridView"/> control and is a shared row.</exception>
        public override bool Displayed 
        {
            get
            {
                Contract.Requires(!(DataGridView != null && Index == -1));
                return default(bool);
            }
        }

        /// <summary>
        /// Gets or sets the height, in pixels, of the row divider.
        /// </summary>
        /// <returns>
        /// The height, in pixels, of the divider (the row's bottom margin).
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">When setting this property, the row is in a <see cref="T:System.Windows.Forms.DataGridView"/> control and is a shared row.</exception>
        public int DividerHeight
        {
            get { return default(int); }
            set
            {
                Contract.Requires(!(DataGridView != null && Index == -1));
            }
        }

        // <summary>
        // Gets or sets the error message text for row-level errors.
        // </summary>
        // <returns>
        // A <see cref="T:System.String"/> containing the error message.
        // </returns>
        // <exception cref="T:System.InvalidOperationException">When getting the value of this property, the row is a shared row in a <see cref="T:System.Windows.Forms.DataGridView"/> control.</exception>
        // public string ErrorText {get; set;}
       
        /// <summary>
        /// Gets or sets a value indicating whether the row is frozen.
        /// </summary>
        /// <returns>
        /// true if the row is frozen; otherwise, false.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">The row is in a <see cref="T:System.Windows.Forms.DataGridView"/> control and is a shared row.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public override bool Frozen
        {
            get
            {
                Contract.Requires(!(DataGridView != null && Index == -1));
                return default(bool);
            }
            set
            {
                Contract.Requires(!(DataGridView != null && Index == -1));
            }
        }
        
        // <summary>
        // Gets or sets the row's header cell.
        // </summary>
        // <returns>
        // A <see cref="T:System.Windows.Forms.DataGridViewRowHeaderCell"/> that represents the header cell of row.
        // </returns>
        // <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public DataGridViewRowHeaderCell HeaderCell {get; set;}
        
        /// <summary>
        /// Gets or sets the current height of the row.
        /// </summary>
        /// <returns>
        /// The height, in pixels, of the row. The default is the height of the default font plus 9 pixels.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">When setting this property, the row is in a <see cref="T:System.Windows.Forms.DataGridView"/> control and is a shared row.</exception><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        /// [DefaultValue(22)]
        public int Height
        {
            get { return default(int); }
            set { Contract.Requires(!(DataGridView != null && Index == -1)); }
        }

        /// <summary>
        /// Gets the cell style in effect for the row.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Windows.Forms.DataGridViewCellStyle"/> that specifies the formatting and style information for the cells in the row.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">The row is in a <see cref="T:System.Windows.Forms.DataGridView"/> control and is a shared row.</exception>
        public override DataGridViewCellStyle InheritedStyle
        {
            get
            {
                Contract.Requires(Index >= 0);
                return default(DataGridViewCellStyle);
            }
        }

        // <summary>
        // Gets a value indicating whether the row is the row for new records.
        // </summary>
        // <returns>
        // true if the row is the last row in the <see cref="T:System.Windows.Forms.DataGridView"/>, which is used for the entry of a new row of data; otherwise, false.
        // </returns>
        // public bool IsNewRow {get;}
        
        /// <summary>
        /// Gets or sets the minimum height of the row.
        /// </summary>
        /// <returns>
        /// The minimum row height in pixels, ranging from 2 to <see cref="F:System.Int32.MaxValue"/>. The default is 3.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">When setting this property, the row is in a <see cref="T:System.Windows.Forms.DataGridView"/> control and is a shared row.</exception><exception cref="T:System.ArgumentOutOfRangeException">The specified value when setting this property is less than 2.</exception><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public int MinimumHeight
        {
            get { return default(int); }
            set { Contract.Requires(!(DataGridView != null && Index == -1)); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the row is read-only.
        /// </summary>
        /// <returns>
        /// true if the row is read-only; otherwise, false.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">The row is in a <see cref="T:System.Windows.Forms.DataGridView"/> control and is a shared row.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        /// [DefaultValue(false)]
        public override bool ReadOnly
        {
            get
            {
                Contract.Requires(!(DataGridView != null && Index == -1));
                return default(bool);
            }
            set
            {
                
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether users can resize the row or indicating that the behavior is inherited from the <see cref="P:System.Windows.Forms.DataGridView.AllowUserToResizeRows"/> property.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Windows.Forms.DataGridViewTriState"/> value that indicates whether the row can be resized or whether it can be resized only when the <see cref="P:System.Windows.Forms.DataGridView.AllowUserToResizeRows"/> property is set to true.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">The row is in a <see cref="T:System.Windows.Forms.DataGridView"/> control and is a shared row.</exception>
        public override DataGridViewTriState Resizable
        {
            get
            {
                Contract.Requires(!(DataGridView != null && Index == -1));
                return default(DataGridViewTriState);
            }
            set
            {
                
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the row is selected.
        /// </summary>
        /// <returns>
        /// true if the row is selected; otherwise, false.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">The row is in a <see cref="T:System.Windows.Forms.DataGridView"/> control and is a shared row.</exception>
        public override bool Selected
        {
            get
            {
                Contract.Requires(!(DataGridView != null && Index == -1));
                return default(bool);
            }
            set
            {
                
            }
        }

        /// <summary>
        /// Gets the current state of the row.
        /// </summary>
        /// <returns>
        /// A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates"/> values indicating the row state.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">The row is in a <see cref="T:System.Windows.Forms.DataGridView"/> control and is a shared row.</exception>
        public override DataGridViewElementStates State
        {
            get
            {
                Contract.Requires(!(DataGridView != null && Index == -1));
                return default(DataGridViewElementStates);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the row is visible.
        /// </summary>
        /// <returns>
        /// true if the row is visible; otherwise, false.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">The row is in a <see cref="T:System.Windows.Forms.DataGridView"/> control and is a shared row.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        [Browsable(false)]
        public override bool Visible
        {
            get
            {
                Contract.Requires(!(DataGridView != null && Index == -1));
                return default(bool);
            }
            set
            {
                Contract.Requires(!(DataGridView != null && Index == -1));
            }
        }

        // <summary>
        // Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewRow"/> class without using a template.
        // </summary>
        // public DataGridViewRow()
        
        // <summary>
        // Modifies an input row header border style according to the specified criteria.
        // </summary>
        // <returns>
        // A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle"/> that represents the new border style used.
        // </returns>
        // <param name="dataGridViewAdvancedBorderStyleInput">A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle"/> that represents the row header border style to modify. </param><param name="dataGridViewAdvancedBorderStylePlaceholder">A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle"/> that is used to store intermediate changes to the row header border style.</param><param name="singleVerticalBorderAdded">true to add a single vertical border to the result; otherwise, false. </param><param name="singleHorizontalBorderAdded">true to add a single horizontal border to the result; otherwise, false. </param><param name="isFirstDisplayedRow">true if the row is the first row displayed in the <see cref="T:System.Windows.Forms.DataGridView"/>; otherwise, false. </param><param name="isLastVisibleRow">true if the row is the last row in the <see cref="T:System.Windows.Forms.DataGridView"/> that has its <see cref="P:System.Windows.Forms.DataGridViewRow.Visible"/> property set to true; otherwise, false. </param><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public virtual DataGridViewAdvancedBorderStyle AdjustRowHeaderBorderStyle(DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyleInput, DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStylePlaceholder, bool singleVerticalBorderAdded, bool singleHorizontalBorderAdded, bool isFirstDisplayedRow, bool isLastVisibleRow)
        
        // <summary>
        // Creates an exact copy of this row.
        // </summary>
        // <returns>
        // An <see cref="T:System.Object"/> that represents the cloned <see cref="T:System.Windows.Forms.DataGridViewRow"/>.
        // </returns>
        // <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public override object Clone()
        
        /// <summary>
        /// Creates a new accessible object for the <see cref="T:System.Windows.Forms.DataGridViewRow"/>.
        /// </summary>
        /// <returns>
        /// A new <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject"/> for the <see cref="T:System.Windows.Forms.DataGridViewRow"/>.
        /// </returns>
        protected virtual AccessibleObject CreateAccessibilityInstance()
        {
            Contract.Ensures(Contract.Result<AccessibleObject>() != null);
            return default(AccessibleObject);
        }

        /// <summary>
        /// Clears the existing cells and sets their template according to the supplied <see cref="T:System.Windows.Forms.DataGridView"/> template.
        /// </summary>
        /// <param name="dataGridView">A <see cref="T:System.Windows.Forms.DataGridView"/> that acts as a template for cell styles. </param><exception cref="T:System.ArgumentNullException"><paramref name="dataGridView"/> is null. </exception><exception cref="T:System.InvalidOperationException">A row that already belongs to the <see cref="T:System.Windows.Forms.DataGridView"/> was added. -or-A column that has no cell template was added.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public void CreateCells(DataGridView dataGridView)
        {
            Contract.Requires(dataGridView != null);
        }

        /// <summary>
        /// Clears the existing cells and sets their template and values.
        /// </summary>
        /// <param name="dataGridView">A <see cref="T:System.Windows.Forms.DataGridView"/> that acts as a template for cell styles. </param><param name="values">An array of objects that initialize the reset cells. </param><exception cref="T:System.ArgumentNullException">Either of the parameters is null. </exception><exception cref="T:System.InvalidOperationException">A row that already belongs to the <see cref="T:System.Windows.Forms.DataGridView"/> was added. -or-A column that has no cell template was added.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public void CreateCells(DataGridView dataGridView, params object[] values)
        {
            Contract.Requires(dataGridView != null);
            Contract.Requires(values != null);
        }

        // <summary>
        // Constructs a new collection of cells based on this row.
        // </summary>
        // <returns>
        // The newly created <see cref="T:System.Windows.Forms.DataGridViewCellCollection"/>.
        // </returns>
        // protected virtual DataGridViewCellCollection CreateCellsInstance()
        
        /// <summary>
        /// Draws a focus rectangle around the specified bounds.
        /// </summary>
        /// <param name="graphics">The <see cref="T:System.Drawing.Graphics"/> used to paint the <see cref="T:System.Windows.Forms.DataGridViewRow"/>.</param><param name="clipBounds">A <see cref="T:System.Drawing.Rectangle"/> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView"/> that needs to be painted.</param><param name="bounds">A <see cref="T:System.Drawing.Rectangle"/> that contains the bounds of the <see cref="T:System.Windows.Forms.DataGridViewRow"/> that is being painted.</param><param name="rowIndex">The row index of the cell that is being painted.</param><param name="rowState">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates"/> values that specifies the state of the row.</param><param name="cellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle"/> used to paint the focus rectangle.</param><param name="cellsPaintSelectionBackground">true to use the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.SelectionBackColor"/> property of <paramref name="cellStyle"/> as the color of the focus rectangle; false to use the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.BackColor"/> property of <paramref name="cellStyle"/> as the color of the focus rectangle.</param><exception cref="T:System.InvalidOperationException">The row has not been added to a <see cref="T:System.Windows.Forms.DataGridView"/> control.</exception><exception cref="T:System.ArgumentNullException"><paramref name="graphics"/> is null.-or-<paramref name="cellStyle"/> is null.</exception>
        protected internal virtual void DrawFocus(Graphics graphics, Rectangle clipBounds, Rectangle bounds, int rowIndex, DataGridViewElementStates rowState, DataGridViewCellStyle cellStyle, bool cellsPaintSelectionBackground)
        {
            Contract.Requires(DataGridView != null);
            Contract.Requires(graphics != null);
            Contract.Requires(cellStyle != null);
        }

        /// <summary>
        /// Gets the shortcut menu for the row.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Windows.Forms.ContextMenuStrip"/> that belongs to the <see cref="T:System.Windows.Forms.DataGridViewRow"/> at the specified index.
        /// </returns>
        /// <param name="rowIndex">The index of the current row.</param><exception cref="T:System.InvalidOperationException"><paramref name="rowIndex"/> is -1.</exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="rowIndex"/> is less than zero or greater than or equal to the number of rows in the control minus one.</exception>
        public ContextMenuStrip GetContextMenuStrip(int rowIndex)
        {
            Contract.Requires(DataGridView == null || (rowIndex >= 0 && rowIndex < DataGridView.Rows.Count));
            return default(ContextMenuStrip);
        }

        /// <summary>
        /// Gets the error text for the row at the specified index.
        /// </summary>
        /// <returns>
        /// A string that describes the error of the row at the specified index.
        /// </returns>
        /// <param name="rowIndex">The index of the row that contains the error.</param><exception cref="T:System.InvalidOperationException">The row belongs to a <see cref="T:System.Windows.Forms.DataGridView"/> control and is a shared row.</exception><exception cref="T:System.ArgumentOutOfRangeException">The row belongs to a <see cref="T:System.Windows.Forms.DataGridView"/> control and <paramref name="rowIndex"/> is less than zero or greater than the number of rows in the control minus one. </exception>
        public string GetErrorText(int rowIndex)
        {
            Contract.Requires(DataGridView == null || (rowIndex >= 0 && rowIndex < DataGridView.Rows.Count));
            return default(string);
        }
        
        /// <summary>
        /// Calculates the ideal height of the specified row based on the specified criteria.
        /// </summary>
        /// <returns>
        /// The ideal height of the row, in pixels.
        /// </returns>
        /// <param name="rowIndex">The index of the row whose preferred height is calculated.</param><param name="autoSizeRowMode">A <see cref="T:System.Windows.Forms.DataGridViewAutoSizeRowMode"/> that specifies an automatic sizing mode.</param><param name="fixedWidth">true to calculate the preferred height for a fixed cell width; otherwise, false.</param><exception cref="T:System.ComponentModel.InvalidEnumArgumentException"><paramref name="autoSizeRowMode"/> is not a valid <see cref="T:System.Windows.Forms.DataGridViewAutoSizeRowMode"/> value. </exception><exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="rowIndex"/> is not in the valid range of 0 to the number of rows in the control minus 1. </exception>
        public virtual int GetPreferredHeight(int rowIndex, DataGridViewAutoSizeRowMode autoSizeRowMode, bool fixedWidth)
        {
            // Contract.Requires(!((autoSizeRowMode & (DataGridViewAutoSizeRowMode) -4) != (DataGridViewAutoSizeRowMode)0));
            Contract.Requires(!(DataGridView != null && (rowIndex < 0 || rowIndex >= DataGridView.Rows.Count)));
            return default(int);
        }
        
        /// <summary>
        /// Returns a value indicating the current state of the row.
        /// </summary>
        /// <returns>
        /// A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates"/> values indicating the row state.
        /// </returns>
        /// <param name="rowIndex">The index of the row.</param><exception cref="T:System.ArgumentOutOfRangeException">The row has been added to a <see cref="T:System.Windows.Forms.DataGridView"/> control, but the <paramref name="rowIndex"/> value is not in the valid range of 0 to the number of rows in the control minus 1.</exception><exception cref="T:System.ArgumentException">The row is not a shared row, but the <paramref name="rowIndex"/> value does not match the row's <see cref="P:System.Windows.Forms.DataGridViewBand.Index"/> property value.-or-The row has not been added to a <see cref="T:System.Windows.Forms.DataGridView"/> control, but the <paramref name="rowIndex"/> value does not match the row's <see cref="P:System.Windows.Forms.DataGridViewBand.Index"/> property value.</exception>
        /// [EditorBrowsable(EditorBrowsableState.Advanced)]
        public virtual DataGridViewElementStates GetState(int rowIndex)
        {
            Contract.Requires(!(DataGridView != null && (rowIndex < 0 || rowIndex >= DataGridView.Rows.Count)));
            return default(DataGridViewElementStates);
        }
        
        // <summary>
        // Paints the current row.
        // </summary>
        // <param name="graphics">The <see cref="T:System.Drawing.Graphics"/> used to paint the <see cref="T:System.Windows.Forms.DataGridViewRow"/>.</param><param name="clipBounds">A <see cref="T:System.Drawing.Rectangle"/> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView"/> that needs to be painted.</param><param name="rowBounds">A <see cref="T:System.Drawing.Rectangle"/> that contains the bounds of the <see cref="T:System.Windows.Forms.DataGridViewRow"/> that is being painted.</param><param name="rowIndex">The row index of the cell that is being painted.</param><param name="rowState">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates"/> values that specifies the state of the row.</param><param name="isFirstDisplayedRow">true to indicate whether the current row is the first row displayed in the <see cref="T:System.Windows.Forms.DataGridView"/>; otherwise, false.</param><param name="isLastVisibleRow">true to indicate whether the current row is the last row in the <see cref="T:System.Windows.Forms.DataGridView"/> that has the <see cref="P:System.Windows.Forms.DataGridViewRow.Visible"/> property set to true; otherwise, false.</param><exception cref="T:System.InvalidOperationException">The row has not been added to a <see cref="T:System.Windows.Forms.DataGridView"/> control.-or-The row is in a <see cref="T:System.Windows.Forms.DataGridView"/> control and is a shared row.</exception><exception cref="T:System.ArgumentOutOfRangeException">The row is in a <see cref="T:System.Windows.Forms.DataGridView"/> control and <paramref name="rowIndex"/> is less than zero or greater than the number of rows in the control minus one.</exception>
        // protected internal virtual void Paint(Graphics graphics, Rectangle clipBounds, Rectangle rowBounds, int rowIndex, DataGridViewElementStates rowState, bool isFirstDisplayedRow, bool isLastVisibleRow)
       
        // <summary>
        // Paints the cells in the current row.
        // </summary>
        // <param name="graphics">The <see cref="T:System.Drawing.Graphics"/> used to paint the <see cref="T:System.Windows.Forms.DataGridViewRow"/>.</param><param name="clipBounds">A <see cref="T:System.Drawing.Rectangle"/> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView"/> that needs to be painted.</param><param name="rowBounds">A <see cref="T:System.Drawing.Rectangle"/> that contains the bounds of the <see cref="T:System.Windows.Forms.DataGridViewRow"/> that is being painted.</param><param name="rowIndex">The row index of the cell that is being painted.</param><param name="rowState">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates"/> values that specifies the state of the row.</param><param name="isFirstDisplayedRow">true to indicate whether the current row is the first row displayed in the <see cref="T:System.Windows.Forms.DataGridView"/>; otherwise, false.</param><param name="isLastVisibleRow">true to indicate whether the current row is the last row in the <see cref="T:System.Windows.Forms.DataGridView"/> that has the <see cref="P:System.Windows.Forms.DataGridViewRow.Visible"/> property set to true; otherwise, false.</param><param name="paintParts">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewPaintParts"/> values indicating the parts of the cells to paint.</param><exception cref="T:System.InvalidOperationException">The row has not been added to a <see cref="T:System.Windows.Forms.DataGridView"/> control.</exception><exception cref="T:System.ArgumentException"><paramref name="paintParts"/> in not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewPaintParts"/> values.</exception>
        // protected internal virtual void PaintCells(Graphics graphics, Rectangle clipBounds, Rectangle rowBounds, int rowIndex, DataGridViewElementStates rowState, bool isFirstDisplayedRow, bool isLastVisibleRow, DataGridViewPaintParts paintParts)
        
        // <summary>
        // Paints the header cell of the current row.
        // </summary>
        // <param name="graphics">The <see cref="T:System.Drawing.Graphics"/> used to paint the <see cref="T:System.Windows.Forms.DataGridViewRow"/>.</param><param name="clipBounds">A <see cref="T:System.Drawing.Rectangle"/> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView"/> that needs to be painted.</param><param name="rowBounds">A <see cref="T:System.Drawing.Rectangle"/> that contains the bounds of the <see cref="T:System.Windows.Forms.DataGridViewRow"/> that is being painted.</param><param name="rowIndex">The row index of the cell that is being painted.</param><param name="rowState">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewElementStates"/> values that specifies the state of the row.</param><param name="isFirstDisplayedRow">true to indicate that the current row is the first row displayed in the <see cref="T:System.Windows.Forms.DataGridView"/>; otherwise, false.</param><param name="isLastVisibleRow">true to indicate that the current row is the last row in the <see cref="T:System.Windows.Forms.DataGridView"/> that has the <see cref="P:System.Windows.Forms.DataGridViewRow.Visible"/> property set to true; otherwise, false.</param><param name="paintParts">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewPaintParts"/> values indicating the parts of the cells to paint.</param><exception cref="T:System.InvalidOperationException">The row has not been added to a <see cref="T:System.Windows.Forms.DataGridView"/> control.</exception><exception cref="T:System.ComponentModel.InvalidEnumArgumentException"><paramref name="paintParts"/> in not a valid bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewPaintParts"/> values.</exception>
        // protected internal virtual void PaintHeader(Graphics graphics, Rectangle clipBounds, Rectangle rowBounds, int rowIndex, DataGridViewElementStates rowState, bool isFirstDisplayedRow, bool isLastVisibleRow, DataGridViewPaintParts paintParts)
        
        /// <summary>
        /// Sets the values of the row's cells.
        /// </summary>
        /// <returns>
        /// true if all values have been set; otherwise, false.
        /// </returns>
        /// <param name="values">One or more objects that represent the cell values in the row.-or-An <see cref="T:System.Array"/> of <see cref="T:System.Object"/> values. </param><exception cref="T:System.ArgumentNullException"><paramref name="values"/> is null. </exception><exception cref="T:System.InvalidOperationException">This method is called when the associated <see cref="T:System.Windows.Forms.DataGridView"/> is operating in virtual mode. -or-This row is a shared row.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public bool SetValues(params object[] values)
        {
            Contract.Requires(values != null);
            Contract.Requires(DataGridView == null || DataGridView.VirtualMode);
            Contract.Requires(DataGridView == null || Index >= 0);
            return default(bool);
        }
        
        // <summary>
        // Gets a human-readable string that describes the row.
        // </summary>
        // <returns>
        // A <see cref="T:System.String"/> that describes this row.
        // </returns>
        // public override string ToString()
      
        /// <summary>
        /// Provides information about a <see cref="T:System.Windows.Forms.DataGridViewRow"/> to accessibility client applications.
        /// </summary>
        /// [ComVisible(true)]
        protected class DataGridViewRowAccessibleObject : AccessibleObject
        {
            /// <summary>
            /// Gets the location and size of the accessible object.
            /// </summary>
            /// <returns>
            /// A <see cref="T:System.Drawing.Rectangle"/> that represents the bounds of the accessible object.
            /// </returns>
            /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.Owner"/> property is null.</exception>
            public override Rectangle Bounds
            {
                get
                {
                    Contract.Requires(Owner != null);
                    return default(Rectangle);
                }
            }

            /// <summary>
            /// Gets the name of the <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject"/>.
            /// </summary>
            /// <returns>
            /// The name of the <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject"/>.
            /// </returns>
            /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.Owner"/> property is null.</exception>
            public override string Name
            {
                get
                {
                    Contract.Requires(Owner != null);
                    return default(string);
                }
            }

            /// <summary>
            /// Gets or sets the <see cref="T:System.Windows.Forms.DataGridViewRow"/> to which this <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject"/> applies.
            /// </summary>
            /// <returns>
            /// The <see cref="T:System.Windows.Forms.DataGridViewRow"/> that owns this <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject"/>.
            /// </returns>
            /// <exception cref="T:System.InvalidOperationException">When setting this property, the <see cref="P:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.Owner"/> property has already been set.</exception>
            public DataGridViewRow Owner
            {
                get { return default(DataGridViewRow); }
                set { Contract.Requires(Owner == null); }
            }

            // <summary>
            // Gets the parent of the <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject"/>.
            // </summary>
            // <returns>
            // The <see cref="T:System.Windows.Forms.DataGridView.DataGridViewAccessibleObject"/> that belongs to the <see cref="T:System.Windows.Forms.DataGridView"/>.
            // </returns>
            // <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.Owner"/> property is null.</exception>
            // public override AccessibleObject Parent {get;}
           
            // <summary>
            // Gets the role of the <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject"/>.
            // </summary>
            // <returns>
            // The <see cref="F:System.Windows.Forms.AccessibleRole.Row"/> value.
            // </returns>
            // public override AccessibleRole Role {get;}
            
            /// <summary>
            /// Gets the state of the <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject"/>.
            /// </summary>
            /// <returns>
            /// A bitwise combination of <see cref="T:System.Windows.Forms.AccessibleStates"/> values. The default is the bitwise combination of the <see cref="F:System.Windows.Forms.AccessibleStates.Selectable"/> and <see cref="F:System.Windows.Forms.AccessibleStates.Focusable"/> values.
            /// </returns>
            /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.Owner"/> property is null.</exception>
            public override AccessibleStates State
            {
                get
                {
                    Contract.Requires(Owner != null);
                    return default(AccessibleStates);
                }
            }

            /// <summary>
            /// Gets the value of the <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject"/>.
            /// </summary>
            /// <returns>
            /// The value of the <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject"/>.
            /// </returns>
            /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.Owner"/> property is null.</exception>
            public override string Value
            {
                get
                {
                    Contract.Requires(Owner != null);
                    return default(string);
                }
            }

            // <summary>
            // Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject"/> class without setting the <see cref="P:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.Owner"/> property.
            // </summary>
            // public DataGridViewRowAccessibleObject()
            
            // <summary>
            // Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject"/> class, setting the <see cref="P:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.Owner"/> property to the specified <see cref="T:System.Windows.Forms.DataGridViewRow"/>.
            // </summary>
            // <param name="owner">The <see cref="T:System.Windows.Forms.DataGridViewRow"/> that owns the <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject"/></param>
            // public DataGridViewRowAccessibleObject(DataGridViewRow owner)
            
            /// <summary>
            /// Returns the accessible child corresponding to the specified index.
            /// </summary>
            /// <returns>
            /// A <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject"/> that represents the <see cref="T:System.Windows.Forms.DataGridViewCell"/> corresponding to the specified index.
            /// </returns>
            /// <param name="index">The zero-based index of the accessible child.</param><exception cref="T:System.InvalidOperationException"><paramref name="index"/> is less than 0.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.Owner"/> property is null.</exception>
            public override AccessibleObject GetChild(int index)
            {
                Contract.Requires(index >= 0);
                Contract.Requires(Owner != null);
                return default(AccessibleObject);
            }

            /// <summary>
            /// Returns the number of children belonging to the accessible object.
            /// </summary>
            /// <returns>
            /// The number of child accessible objects that belong to the <see cref="T:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject"/> corresponds to the number of visible columns in the <see cref="T:System.Windows.Forms.DataGridView"/>. If the <see cref="P:System.Windows.Forms.DataGridView.RowHeadersVisible"/> property is true, the <see cref="M:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.GetChildCount"/> method includes the <see cref="T:System.Windows.Forms.DataGridViewRowHeaderCell"/> in the count of child accessible objects.
            /// </returns>
            /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.Owner"/> property is null.</exception>
            public override int GetChildCount()
            {
                Contract.Requires(Owner != null);
                return default(int);
            }

            // <summary>
            // Gets an accessible object that represents the currently selected <see cref="T:System.Windows.Forms.DataGridViewCell"/> objects.
            // </summary>
            // <returns>
            // An accessible object that represents the currently selected <see cref="T:System.Windows.Forms.DataGridViewCell"/> objects in the <see cref="T:System.Windows.Forms.DataGridViewRow"/>.
            // </returns>
            // <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.Owner"/> property is null.</exception>
            // public override AccessibleObject GetSelected()
            
            /// <summary>
            /// Returns the accessible object that has keyboard focus.
            /// </summary>
            /// <returns>
            /// A <see cref="T:System.Windows.Forms.DataGridViewCell.DataGridViewCellAccessibleObject"/> if the cell indicated by the <see cref="P:System.Windows.Forms.DataGridView.CurrentCell"/> property has keyboard focus and is in the current <see cref="T:System.Windows.Forms.DataGridViewRow"/>; otherwise, null.
            /// </returns>
            /// <exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.Owner"/> property is null.</exception>
            public override AccessibleObject GetFocused()
            {
                Contract.Requires(Owner != null);
                return default(AccessibleObject);
            }

            /// <summary>
            /// Navigates to another accessible object.
            /// </summary>
            /// <returns>
            /// An <see cref="T:System.Windows.Forms.AccessibleObject"/> that represents an object in the specified direction.
            /// </returns>
            /// <param name="navigationDirection">One of the <see cref="T:System.Windows.Forms.AccessibleNavigation"/> values.</param><exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.Owner"/> property is null.</exception>
            /// [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
            public override AccessibleObject Navigate(AccessibleNavigation navigationDirection)
            {
                Contract.Requires(Owner != null);
                return default(AccessibleObject);
            }

            /// <summary>
            /// Modifies the selection or moves the keyboard focus of the accessible object.
            /// </summary>
            /// <param name="flags">One of the <see cref="T:System.Windows.Forms.AccessibleSelection"/> values.</param><exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewRow.DataGridViewRowAccessibleObject.Owner"/> property is null.</exception>
            public override void Select(AccessibleSelection flags)
            {
                Contract.Requires(Owner != null);
            }
        }
    }
}
