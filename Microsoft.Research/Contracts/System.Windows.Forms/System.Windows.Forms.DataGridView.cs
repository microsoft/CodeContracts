// CodeContracts
// AllowUserToAddRowsInternal
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
using System.Diagnostics.Contracts;
using System.Drawing;

namespace System.Windows.Forms
{
    /// <summary>
    /// Displays data in a customizable grid.
    /// </summary>

    public class DataGridView : Control // ,ISupportInitialize
    {
        /// <summary>
        /// Gets the border style for the upper-left cell in the <see cref="T:System.Windows.Forms.DataGridView"/>.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle"/> that represents the style of the border of the upper-left cell in the <see cref="T:System.Windows.Forms.DataGridView"/>.
        /// </returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public virtual DataGridViewAdvancedBorderStyle AdjustedTopLeftHeaderBorderStyle
        {
            get
            {
                Contract.Ensures(Contract.Result<DataGridViewAdvancedBorderStyle>() != null);
                return default(DataGridViewAdvancedBorderStyle);
            }
        }

        /// <summary>
        /// Gets the border style of the cells in the <see cref="T:System.Windows.Forms.DataGridView"/>.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle"/> that represents the border style of the cells in the <see cref="T:System.Windows.Forms.DataGridView"/>.
        /// </returns>
        public DataGridViewAdvancedBorderStyle AdvancedCellBorderStyle
        {
            get
            {
                Contract.Ensures(Contract.Result<DataGridViewAdvancedBorderStyle>() != null);
                return default(DataGridViewAdvancedBorderStyle);
            }
        }

        /// <summary>
        /// Gets the border style of the column header cells in the <see cref="T:System.Windows.Forms.DataGridView"/>.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle"/> that represents the border style of the <see cref="T:System.Windows.Forms.DataGridViewColumnHeaderCell"/> objects in the <see cref="T:System.Windows.Forms.DataGridView"/>.
        /// </returns>
        public DataGridViewAdvancedBorderStyle AdvancedColumnHeadersBorderStyle
        {
            get
            {
                Contract.Ensures(Contract.Result<DataGridViewAdvancedBorderStyle>() != null);
                return default(DataGridViewAdvancedBorderStyle);
            }
        }

        /// <summary>
        /// Gets the border style of the row header cells in the <see cref="T:System.Windows.Forms.DataGridView"/>.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle"/> that represents the border style of the <see cref="T:System.Windows.Forms.DataGridViewRowHeaderCell"/> objects in the <see cref="T:System.Windows.Forms.DataGridView"/>.
        /// </returns>
        public DataGridViewAdvancedBorderStyle AdvancedRowHeadersBorderStyle
        {
            get
            {
                Contract.Ensures(Contract.Result<DataGridViewAdvancedBorderStyle>() != null);
                return default(DataGridViewAdvancedBorderStyle);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the option to add rows is displayed to the user.
        /// </summary>
        /// 
        /// <returns>
        /// true if the add-row option is displayed to the user; otherwise false. The default is true.
        /// </returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagstics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public bool AllowUserToAddRows {get; set;}

        // <summary>
        // Gets or sets a value indicating whether the user is allowed to delete rows from the <see cref="T:System.Windows.Forms.DataGridView"/>.
        // </summary>
        // 
        // <returns>
        // true if the user can delete rows; otherwise, false. The default is true.
        // </returns>
        // public bool AllowUserToDeleteRows {get; set;}

        // <summary>
        // Gets or sets a value indicating whether manual column repositioning is enabled.
        // </summary>
        // 
        // <returns>
        // true if the user can change the column order; otherwise, false. The default is false.
        // </returns>
        // public bool AllowUserToOrderColumns{get; set;}

        // <summary>
        // Gets or sets a value indicating whether users can resize columns.
        // </summary>
        // 
        // <returns>
        // true if users can resize columns; otherwise, false. The default is true.
        // </returns>
        // public bool AllowUserToResizeColumns {get; set;}

        // <summary>
        // Gets or sets a value indicating whether users can resize rows.
        // </summary>
        // 
        // <returns>
        // true if all the rows are resizable; otherwise, false. The default is true.
        // </returns>
        // public bool AllowUserToResizeRows {get; set;}

        /// <summary>
        /// Gets or sets the default cell style applied to odd-numbered rows of the <see cref="T:System.Windows.Forms.DataGridView"/>.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Windows.Forms.DataGridViewCellStyle"/> to apply to the odd-numbered rows.
        /// </returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public DataGridViewCellStyle AlternatingRowsDefaultCellStyle
        {
            get
            {
                Contract.Ensures(Contract.Result<DataGridViewCellStyle>() != null);
                return default(DataGridViewCellStyle);
            }
            set { }
        }

        // <summary>
        // Gets or sets a value indicating whether columns are created automatically when the <see cref="P:System.Windows.Forms.DataGridView.DataSource"/> or <see cref="P:System.Windows.Forms.DataGridView.DataMember"/> properties are set.
        // </summary>
        // 
        // <returns>
        // true if the columns should be created automatically; otherwise, false. The default is true.
        // </returns>
        // <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public bool AutoGenerateColumns {get; set;}

        // <returns>
        // true if enabled; otherwise, false.
        // </returns>
        // public override bool AutoSize {get;} 

        // <summary>
        // Gets or sets a value indicating how column widths are determined.
        // </summary>
        // 
        // <returns>
        // A <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnsMode"/> value. The default is <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None"/>.
        // </returns>
        // <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnsMode"/> value. </exception><exception cref="T:System.InvalidOperationException">The specified value when setting this property is <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader"/>, column headers are hidden, and at least one visible column has an <see cref="P:System.Windows.Forms.DataGridViewColumn.AutoSizeMode"/> property value of <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.NotSet"/>.-or-The specified value when setting this property is <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill"/> and at least one visible column with an <see cref="P:System.Windows.Forms.DataGridViewColumn.AutoSizeMode"/> property value of <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.NotSet"/> is frozen.</exception>
        // public DataGridViewAutoSizeColumnsMode AutoSizeColumnsMode {get; set;}

        // <summary>
        // Gets or sets a value indicating how row heights are determined.
        // </summary>
        // 
        // <returns>
        // A <see cref="T:System.Windows.Forms.DataGridViewAutoSizeRowsMode"/> value indicating the sizing mode. The default is <see cref="F:System.Windows.Forms.DataGridViewAutoSizeRowsMode.None"/>.
        // </returns>
        // <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.DataGridViewAutoSizeRowsMode"/> value. </exception><exception cref="T:System.InvalidOperationException">The specified value when setting this property is <see cref="F:System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders"/> or <see cref="F:System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedHeaders"/> and row headers are hidden. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public DataGridViewAutoSizeRowsMode AutoSizeRowsMode {get; set;}

        /// <summary>
        /// Gets or sets the background color for the control.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Drawing.Color"/> that represents the background color of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultBackColor"/> property.
        /// </returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public System.Drawing.Color BackColor
        {
            get
            {
                Contract.Ensures(Contract.Result<System.Drawing.Color>() != null);
                return default(System.Drawing.Color);
            }
            set { }
        }

        /// <summary>
        /// Gets or sets the background color of the <see cref="T:System.Windows.Forms.DataGridView"/>.
        /// </summary>
        // 
        // <returns>
        // A <see cref="T:System.Drawing.Color"/> that represents the background color of the <see cref="T:System.Windows.Forms.DataGridView"/>. The default is <see cref="P:System.Drawing.SystemColors.AppWorkspace"/>.
        // </returns>
        // <exception cref="T:System.ArgumentException">The specified value when setting this property is <see cref="F:System.Drawing.Color.Empty"/>. -or-The specified value when setting this property has a <see cref="P:System.Drawing.Color.A"/> property value that is less that 255.</exception><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        //public System.Drawing.Color BackgroundColor
        //{
        //    get { return default(System.Drawing.Color); }
        //    set
        //    {
        //        Contract.Requires(value.IsEmpty == false);
        //        Contract.Requires((int) value.A < (int) byte.MaxValue);
        //    }
        //}

        // <summary>
        // Gets or sets the background image displayed in the control.
        // </summary>
        // 
        // <returns>
        // An <see cref="T:System.Drawing.Image"/> that represents the image to display in the background of the control.
        // </returns>
        // public override Image BackgroundImage {get; set;}

        // <summary>
        // Gets or sets the background image layout as defined in the <see cref="T:System.Windows.Forms.ImageLayout"/> enumeration.
        // </summary>
        // 
        // <returns>
        // An <see cref="T:System.Windows.Forms.ImageLayout"/> value indicating the background image layout. The default is <see cref="F:System.Windows.Forms.ImageLayout.Tile"/>.
        // </returns>
        // public override ImageLayout BackgroundImageLayout {get; set;}

        // <summary>
        // Gets or sets the border style for the <see cref="T:System.Windows.Forms.DataGridView"/>.
        // </summary>
        // 
        // <returns>
        // One of the <see cref="T:System.Windows.Forms.BorderStyle"/> values. The default is <see cref="F:System.Windows.Forms.BorderStyle.FixedSingle"/>.
        // </returns>
        // <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.BorderStyle"/> value. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public BorderStyle BorderStyle {get; set;}

        // <summary>
        // Gets a value indicating whether the <see cref="P:System.Windows.Forms.Control.ImeMode"/> property can be set to an active value, to enable IME support.
        // </summary>
        // 
        // <returns>
        // true if there is an editable cell selected; otherwise, false.
        // </returns>
        // protected override bool CanEnableIme {get;}

        /// <summary>
        /// Gets the cell border style for the <see cref="T:System.Windows.Forms.DataGridView"/>.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Windows.Forms.DataGridViewCellBorderStyle"/> that represents the border style of the cells contained in the <see cref="T:System.Windows.Forms.DataGridView"/>.
        /// </returns>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.DataGridViewCellBorderStyle"/> value.</exception><exception cref="T:System.ArgumentException">The specified value when setting this property is <see cref="F:System.Windows.Forms.DataGridViewCellBorderStyle.Custom"/>.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public DataGridViewCellBorderStyle CellBorderStyle
        {
            get { return default(DataGridViewCellBorderStyle); }
            set { Contract.Requires(value != DataGridViewCellBorderStyle.Custom); }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether users can copy cell text values to the <see cref="T:System.Windows.Forms.Clipboard"/> and whether row and column header text is included.
        /// </summary>
        /// 
        /// <returns>
        /// One of the <see cref="T:System.Windows.Forms.DataGridViewClipboardCopyMode"/> values. The default is <see cref="F:System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithAutoHeaderText"/>.
        /// </returns>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.DataGridViewClipboardCopyMode"/> value.</exception>
        public DataGridViewClipboardCopyMode ClipboardCopyMode { get; set; }

        /// <summary>
        /// Gets or sets the number of columns displayed in the <see cref="T:System.Windows.Forms.DataGridView"/>.
        /// </summary>
        /// 
        /// <returns>
        /// The number of columns displayed in the <see cref="T:System.Windows.Forms.DataGridView"/>.
        /// </returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The specified value when setting this property is less than 0. </exception><exception cref="T:System.InvalidOperationException">When setting this property, the <see cref="P:System.Windows.Forms.DataGridView.DataSource"/> property has been set. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public int ColumnCount
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= 0);
                return default(int);
            }
            set
            {
                Contract.Requires(value >= 0);
                Contract.Requires(DataSource == null);
            }
        }

        // <summary>
        // Gets the border style applied to the column headers.
        // </summary>
        // 
        // <returns>
        // One of the <see cref="T:System.Windows.Forms.DataGridViewHeaderBorderStyle"/> values.
        // </returns>
        // <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.DataGridViewHeaderBorderStyle"/> value.</exception><exception cref="T:System.ArgumentException">The specified value when setting this property is <see cref="F:System.Windows.Forms.DataGridViewHeaderBorderStyle.Custom"/>.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public DataGridViewHeaderBorderStyle ColumnHeadersBorderStyle {get; set;}

        // <summary>
        // Gets or sets the default column header style.
        // </summary>
        // 
        // <returns>
        // A <see cref="T:System.Windows.Forms.DataGridViewCellStyle"/> that represents the default column header style.
        // </returns>
        // <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public DataGridViewCellStyle ColumnHeadersDefaultCellStyle { get; set; }


        /// <summary>
        /// Gets or sets the height, in pixels, of the column headers row
        /// </summary>
        /// 
        /// <returns>
        /// The height, in pixels, of the row that contains the column headers. The default is 23.
        /// </returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The specified value when setting this property is less than the minimum height of 4 pixels or is greater than the maximum height of 32768 pixels.</exception>
        public int ColumnHeadersHeight
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= 4 && Contract.Result<int>() <= 32768);
                return default(int);
            }
            set { Contract.Requires(value >= 4 && value <= 32768); }
        }

        // <summary>
        // Gets or sets a value indicating whether the height of the column headers is adjustable and whether it can be adjusted by the user or is automatically adjusted to fit the contents of the headers.
        // </summary>
        // 
        // <returns>
        // A <see cref="T:System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode"/> value indicating the mode by which the height of the column headers row can be adjusted. The default is <see cref="F:System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing"/>.
        // </returns>
        // <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode"/> value.</exception>
        // public DataGridViewColumnHeadersHeightSizeMode ColumnHeadersHeightSizeMode {get; set;}

        // <summary>
        // Gets or sets a value indicating whether the column header row is displayed.
        // </summary>
        // 
        // <returns>
        // true if the column headers are displayed; otherwise, false. The default is true.
        // </returns>
        // <exception cref="T:System.InvalidOperationException">The specified value when setting this property is false and one or more columns have an <see cref="P:System.Windows.Forms.DataGridViewColumn.InheritedAutoSizeMode"/> property value of <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader"/>.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public bool ColumnHeadersVisible {get; set;}

        /// <summary>
        /// Gets a collection that contains all the columns in the control.
        /// </summary>
        /// 
        /// <returns>
        /// The <see cref="T:System.Windows.Forms.DataGridViewColumnCollection"/> that contains all the columns in the <see cref="T:System.Windows.Forms.DataGridView"/> control.
        /// </returns>
        public DataGridViewColumnCollection Columns
        {
            get
            {
                Contract.Ensures(Contract.Result<DataGridViewColumnCollection>() != null);
                return default(DataGridViewColumnCollection);
            }
        }

        // <summary>
        // Gets or sets the currently active cell.
        // </summary>
        // 
        // <returns>
        // The <see cref="T:System.Windows.Forms.DataGridViewCell"/> that represents the current cell, or null if there is no current cell. The default is the first cell in the first column or null if there are no cells in the control.
        // </returns>
        // <exception cref="T:System.InvalidOperationException">The value of this property cannot be set because changes to the current cell cannot be committed or canceled.-or-The specified cell when setting this property is in a hidden row or column. Re-entrant calling is only allowed when the <see cref="T:System.Windows.Forms.DataGridView"/> is bound to a <see cref="P:System.Windows.Forms.DataGridView.DataSource"/>. Re-entrant calling results from a change to the underlying data.</exception><exception cref="T:System.ArgumentException">The specified cell when setting this property is not in the <see cref="T:System.Windows.Forms.DataGridView"/>.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public DataGridViewCell CurrentCell {get; set;}

        // <summary>
        // Gets the row and column indexes of the currently active cell.
        // </summary>
        // 
        // <returns>
        // A <see cref="T:System.Drawing.Point"/> that represents the row and column indexes of the currently active cell.
        // </returns>
        // public Point CurrentCellAddress { get; }

        // <summary>
        // Gets the row containing the current cell.
        // </summary>
        // 
        // <returns>
        // The <see cref="T:System.Windows.Forms.DataGridViewRow"/> that represents the row containing the current cell, or null if there is no current cell.
        // </returns>
        // public DataGridViewRow CurrentRow {get;}

        // <summary>
        // Gets or sets the name of the list or table in the data source for which the <see cref="T:System.Windows.Forms.DataGridView"/> is displaying data.
        // </summary>
        // 
        // <returns>
        // The name of the table or list in the <see cref="P:System.Windows.Forms.DataGridView.DataSource"/> for which the <see cref="T:System.Windows.Forms.DataGridView"/> is displaying data. The default is <see cref="F:System.String.Empty"/>.
        // </returns>
        // <exception cref="T:System.Exception">An error occurred in the data source and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError"/> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException"/> property to true. The exception object can typically be cast to type <see cref="T:System.FormatException"/>.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public string DataMember {get; set;}

        /// <summary>
        /// Gets or sets the data source that the <see cref="T:System.Windows.Forms.DataGridView"/> is displaying data for.
        /// </summary>
        /// 
        /// <returns>
        /// The object that contains data for the <see cref="T:System.Windows.Forms.DataGridView"/> to display.
        /// </returns>
        /// <exception cref="T:System.Exception">An error occurred in the data source and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError"/> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException"/> property to true. The exception object can typically be cast to type <see cref="T:System.FormatException"/>.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public object DataSource { get; set; }

        /// <summary>
        /// Gets or sets the default cell style to be applied to the cells in the <see cref="T:System.Windows.Forms.DataGridView"/> if no other cell style properties are set.
        /// </summary>
        /// 
        /// <returns>
        /// The <see cref="T:System.Windows.Forms.DataGridViewCellStyle"/> to be applied as the default style.
        /// </returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public DataGridViewCellStyle DefaultCellStyle
        {
            get
            {
                Contract.Ensures(Contract.Result<DataGridViewCellStyle>() != null);
                return default(DataGridViewCellStyle);
            }
            set { }
        }

        // <summary>
        // Gets the default initial size of the control.
        // </summary>
        // 
        // <returns>
        // A <see cref="T:System.Drawing.Size"/> representing the initial size of the control, which is 240 pixels wide by 150 pixels high.
        // </returns>
        // protected override Size DefaultSize {get;}

        // <summary>
        // Gets the rectangle that represents the display area of the control.
        // </summary>
        // 
        // <returns>
        // A <see cref="T:System.Drawing.Rectangle"/> that represents the display area of the control.
        // </returns>
        // public override Rectangle DisplayRectangle {get;}

        // <summary>
        // Gets or sets a value indicating how to begin editing a cell.
        // </summary>
        // 
        // <returns>
        // One of the <see cref="T:System.Windows.Forms.DataGridViewEditMode"/> values. The default is <see cref="F:System.Windows.Forms.DataGridViewEditMode.EditOnKeystrokeOrF2"/>.
        // </returns>
        // <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.DataGridViewEditMode"/> value.</exception><exception cref="T:System.Exception">The specified value when setting this property would cause the control to enter edit mode, but initialization of the editing cell value failed and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError"/> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException"/> property to true. The exception object can typically be cast to type <see cref="T:System.FormatException"/>.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public DataGridViewEditMode EditMode {get; set;}

        /// <summary>
        /// Gets the control hosted by the current cell, if a cell with an editing control is in edit mode.
        /// </summary>
        /// 
        /// <returns>
        /// The <see cref="T:System.Windows.Forms.Control"/> hosted by the current cell.
        /// </returns>
        public Control EditingControl { get; }

        /// <summary>
        /// Gets the panel that contains the <see cref="P:System.Windows.Forms.DataGridView.EditingControl"/>.
        /// </summary>
        /// 
        /// <returns>
        /// The <see cref="T:System.Windows.Forms.Panel"/> that contains the <see cref="P:System.Windows.Forms.DataGridView.EditingControl"/>.
        /// </returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>

        public Panel EditingPanel
        {
            get
            {
                Contract.Ensures(Contract.Result<Panel>() != null);
                return default(Panel);
            }
        }

        // <summary>
        // Gets or sets a value indicating whether row and column headers use the visual styles of the user's current theme if visual styles are enabled for the application.
        // </summary>
        // 
        // <returns>
        // true if visual styles are enabled for the headers; otherwise, false. The default value is true.
        // </returns>
        // <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public bool EnableHeadersVisualStyles {get; set;}

        // <summary>
        // Gets or sets the first cell currently displayed in the <see cref="T:System.Windows.Forms.DataGridView"/>; typically, this cell is in the upper left corner.
        // </summary>
        // 
        // <returns>
        // The first <see cref="T:System.Windows.Forms.DataGridViewCell"/> currently displayed in the control.
        // </returns>
        // <exception cref="T:System.ArgumentException">The specified cell when setting this property is not is not in the <see cref="T:System.Windows.Forms.DataGridView"/>. </exception><exception cref="T:System.InvalidOperationException">The specified cell when setting this property has a <see cref="P:System.Windows.Forms.DataGridViewCell.RowIndex"/> or <see cref="P:System.Windows.Forms.DataGridViewCell.ColumnIndex"/> property value of -1, indicating that it is a header cell or a shared cell. -or-The specified cell when setting this property has a <see cref="P:System.Windows.Forms.DataGridViewCell.Visible"/> property value of false.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public DataGridViewCell FirstDisplayedCell {get; set;}

        // <summary>
        // Gets the width of the portion of the column that is currently scrolled out of view..
        // </summary>
        // 
        // <returns>
        // The width of the portion of the column that is scrolled out of view.
        // </returns>
        // public int FirstDisplayedScrollingColumnHiddenWidth {get; }


        /// <summary>
        /// Gets or sets the index of the column that is the first column displayed on the <see cref="T:System.Windows.Forms.DataGridView"/>.
        /// </summary>
        /// 
        /// <returns>
        /// The index of the column that is the first column displayed on the <see cref="T:System.Windows.Forms.DataGridView"/>.
        /// </returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The specified value when setting this property is less than 0 or greater than the number of columns in the control minus 1.</exception><exception cref="T:System.InvalidOperationException">The specified value when setting this property indicates a column with a <see cref="P:System.Windows.Forms.DataGridViewColumn.Visible"/> property value of false.-or-The specified value when setting this property indicates a column with a <see cref="P:System.Windows.Forms.DataGridViewColumn.Frozen"/> property value of true.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public int FirstDisplayedScrollingColumnIndex
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() < 0 ||
                                 (Contract.Result<int>()) >= 0 && Contract.Result<int>() < Columns.Count);
                return default(int);
            }
            set
            {
                Contract.Requires(value >= 0 && value < Columns.Count);
                Contract.Assume(Columns[value] != null);
                // Contract.Requires(Columns[value].Visible && !Columns[value].Frozen);
            }
        }

        /// <summary>
        /// Gets or sets the index of the row that is the first row displayed on the <see cref="T:System.Windows.Forms.DataGridView"/>.
        /// </summary>
        /// 
        /// <returns>
        /// The index of the row that is the first row displayed on the <see cref="T:System.Windows.Forms.DataGridView"/>.
        /// </returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The specified value when setting this property is less than 0 or greater than the number of rows in the control minus 1.</exception><exception cref="T:System.InvalidOperationException">The specified value when setting this property indicates a row with a <see cref="P:System.Windows.Forms.DataGridViewRow.Visible"/> property value of false.-or-The specified value when setting this property indicates a column with a <see cref="P:System.Windows.Forms.DataGridViewRow.Frozen"/> property value of true.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public int FirstDisplayedScrollingRowIndex
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() < 0 ||
                                 (Contract.Result<int>()) >= 0 && Contract.Result<int>() < Rows.Count);
                return default(int);

            }
            set
            {
                Contract.Assume(Rows != null);
                Contract.Requires(value >= 0 && value < Rows.Count);
                Contract.Requires(
                    !((Rows.GetRowState(value) & DataGridViewElementStates.Visible) ==
                      DataGridViewElementStates.None));
                Contract.Requires(
                    !((Rows.GetRowState(value) & DataGridViewElementStates.Frozen) ==
                      DataGridViewElementStates.None));
            }
        }

        // <summary>
        // Gets or sets the foreground color of the <see cref="T:System.Windows.Forms.DataGridView"/>.
        // </summary>
        // 
        // <returns>
        // A <see cref="T:System.Drawing.Color"/> that represents the foreground color of the <see cref="T:System.Windows.Forms.DataGridView"/>. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultForeColor"/> property.
        // </returns>
        // <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public override System.Drawing.Color ForeColor {get; set;}

        /// <summary>
        /// Gets or sets the font of the text displayed by the <see cref="T:System.Windows.Forms.DataGridView"/>.
        /// </summary>
        /// 
        /// <returns>
        /// The <see cref="T:System.Drawing.Font"/> to apply to the text displayed by the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultFont"/> property.
        /// </returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public override Font Font
        {
            get
            {
                Contract.Ensures(Contract.Result<Font>() != null);
                return default(Font);
            }
            set { }
        }

        /// <summary>
        /// Gets or sets the color of the grid lines separating the cells of the <see cref="T:System.Windows.Forms.DataGridView"/>.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Drawing.Color"/> or <see cref="T:System.Drawing.SystemColors"/> that represents the color of the grid lines. The default is <see cref="F:System.Drawing.KnownColor.ControlDarkDark"/>.
        /// </returns>
        /// <exception cref="T:System.ArgumentException">The specified value when setting this property is <see cref="F:System.Drawing.Color.Empty"/>. -or-The specified value when setting this property has a <see cref="P:System.Drawing.Color.A"/> property value that is less that 255.</exception><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public System.Drawing.Color GridColor
        {
            get
            {
                return default(System.Drawing.Color);
            }
            set
            {
                Contract.Requires(value.IsEmpty == false);
                Contract.Requires((int) value.A < (int) byte.MaxValue);
            }
        }

        // <summary>
        // Gets the horizontal scroll bar of the control.
        // </summary>
        // 
        // <returns>
        // A <see cref="T:System.Windows.Forms.ScrollBar"/> representing the horizontal scroll bar.
        // </returns>
        // protected ScrollBar HorizontalScrollBar { get; }

        /// <summary>
        /// Gets or sets the number of pixels by which the control is scrolled horizontally.
        /// </summary>
        /// 
        /// <returns>
        /// The number of pixels by which the control is scrolled horizontally.
        /// </returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The specified value when setting this property is less than 0.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>    
        public int HorizontalScrollingOffset
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= 0);
                return default(int);
            }
            set { Contract.Requires(value >= 0); }
        }

        // <summary>
        // Gets a value indicating whether the current cell has uncommitted changes.
        // </summary>
        // 
        // <returns>
        // true if the current cell has uncommitted changes; otherwise, false.
        // </returns>
        // public bool IsCurrentCellDirty { get; }

        // <summary>
        // Gets a value indicating whether the currently active cell is being edited.
        // </summary>
        // 
        // <returns>
        // true if the current cell is being edited; otherwise, false.
        // </returns>
        // public bool IsCurrentCellInEditMode {get;}

        // <summary>
        // Gets a value indicating whether the current row has uncommitted changes.
        // </summary>
        // 
        // <returns>
        // true if the current row has uncommitted changes; otherwise, false.
        // </returns>
        // public bool IsCurrentRowDirty { get; }

        // <summary>
        // Gets or sets a value indicating whether the user is allowed to select more than one cell, row, or column of the <see cref="T:System.Windows.Forms.DataGridView"/> at a time.
        // </summary>
        // 
        // <returns>
        // true if the user can select more than one cell, row, or column at a time; otherwise, false. The default is true.
        // </returns>
        // <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public bool MultiSelect {get; set;}

        /// <summary>
        /// Gets the index of the row for new records.
        /// </summary>
        /// 
        /// <returns>
        /// The index of the row for new records, or -1 if <see cref="P:System.Windows.Forms.DataGridView.AllowUserToAddRows"/> is false.
        /// </returns>    
        public int NewRowIndex
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() == -1 ||
                                 (Contract.Result<int>() >= 0 && Contract.Result<int>() <= Rows.Count));
                return default(int);
            }
        }

        internal bool NoDimensionChangeAllowed { get; }
        
        // <summary>
        // This property is not relevant for this control.
        // </summary>
        // 
        // <returns>
        // A <see cref="T:System.Windows.Forms.Padding"/> instance.
        // </returns>
        // public new Padding Padding { get; set; }

        // <summary>
        // Gets or sets a value indicating whether the user can edit the cells of the <see cref="T:System.Windows.Forms.DataGridView"/> control.
        // </summary>
        // 
        // <returns>
        // true if the user cannot edit the cells of the <see cref="T:System.Windows.Forms.DataGridView"/> control; otherwise, false. The default is false.
        // </returns>
        // <exception cref="T:System.InvalidOperationException">The specified value when setting this property is true, the current cell is in edit mode, and the current cell contains changes that cannot be committed. </exception><exception cref="T:System.Exception">The specified value when setting this property would cause the control to enter edit mode, but initialization of the editing cell value failed and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError"/> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException"/> property to true. The exception object can typically be cast to type <see cref="T:System.FormatException"/>.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        //  public bool ReadOnly {get; set;}

        /// <summary>
        /// Gets or sets the number of rows displayed in the <see cref="T:System.Windows.Forms.DataGridView"/>.
        /// </summary>
        /// 
        /// <returns>
        /// The number of rows to display in the <see cref="T:System.Windows.Forms.DataGridView"/>.
        /// </returns>
        /// <exception cref="T:System.ArgumentException">The specified value when setting this property is less than 0.-or-The specified value is less than 1 and <see cref="P:System.Windows.Forms.DataGridView.AllowUserToAddRows"/> is set to true. </exception><exception cref="T:System.InvalidOperationException">When setting this property, the <see cref="P:System.Windows.Forms.DataGridView.DataSource"/> property is set. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public int RowCount
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= 0);
                return default(int);
            }
            set
            {
                Contract.Requires((AllowUserToAddRows && value >= 1) || value >= 0);
                Contract.Requires(DataSource == null);
            }
        }

        /// <summary>
        /// Gets or sets the border style of the row header cells.
        /// </summary>
        /// 
        /// <returns>
        /// One of the <see cref="T:System.Windows.Forms.DataGridViewHeaderBorderStyle"/> values.
        /// </returns>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.DataGridViewHeaderBorderStyle"/> value.</exception><exception cref="T:System.ArgumentException">The specified value when setting this property is <see cref="F:System.Windows.Forms.DataGridViewHeaderBorderStyle.Custom"/>.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public DataGridViewHeaderBorderStyle RowHeadersBorderStyle
        {
            get { return default(DataGridViewHeaderBorderStyle); }
            set { Contract.Requires(value != DataGridViewHeaderBorderStyle.Custom); }
        }

        /// <summary>
        /// Gets or sets the default style applied to the row header cells.
        /// </summary>
        /// 
        /// <returns>
        /// The <see cref="T:System.Windows.Forms.DataGridViewCellStyle"/> that represents the default style applied to the row header cells.
        /// </returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public DataGridViewCellStyle RowHeadersDefaultCellStyle
        {
            get
            {
                Contract.Ensures(Contract.Result<DataGridViewCellStyle>() != null);
                return default(DataGridViewCellStyle);
            }
            set { }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the column that contains row headers is displayed.
        /// </summary>
        /// 
        /// <returns>
        /// true if the column that contains row headers is displayed; otherwise, false. The default is true.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">The specified value when setting this property is false and the <see cref="P:System.Windows.Forms.DataGridView.AutoSizeRowsMode"/> property is set to <see cref="F:System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders"/> or <see cref="F:System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedHeaders"/>.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public bool RowHeadersVisible {get; set;}


        /// <summary>
        /// Gets or sets the width, in pixels, of the column that contains the row headers.
        /// </summary>
        /// 
        /// <returns>
        /// The width, in pixels, of the column that contains row headers. The default is 43.
        /// </returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The specified value when setting this property is less than the minimum width of 4 pixels or is greater than the maximum width of 32768 pixels.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public int RowHeadersWidth
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= 4 && Contract.Result<int>() <= 32768);
                return default(int);
            }
            set { Contract.Ensures(value >= 4 && value <= 32768); }
        }

        // <summary>
        // Gets or sets a value indicating whether the width of the row headers is adjustable and whether it can be adjusted by the user or is automatically adjusted to fit the contents of the headers.
        // </summary>
        // 
        // <returns>
        // A <see cref="T:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode"/> value indicating the mode by which the width of the row headers can be adjusted. The default is <see cref="F:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.EnableResizing"/>.
        // </returns>
        // <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode"/> value.</exception>
        // public DataGridViewRowHeadersWidthSizeMode RowHeadersWidthSizeMode {get; set;}

        /// <summary>
        /// Gets a collection that contains all the rows in the <see cref="T:System.Windows.Forms.DataGridView"/> control.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Windows.Forms.DataGridViewRowCollection"/> that contains all the rows in the <see cref="T:System.Windows.Forms.DataGridView"/>.
        /// </returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>    
        public DataGridViewRowCollection Rows
        {
            get
            {
                Contract.Ensures(Contract.Result<DataGridViewRowCollection>() != null);
                return default(DataGridViewRowCollection);
            }
        }

        /// <summary>
        /// Gets or sets the default style applied to the row cells of the <see cref="T:System.Windows.Forms.DataGridView"/>.
        /// </summary>
        /// 
        /// <returns>
        /// The <see cref="T:System.Windows.Forms.DataGridViewCellStyle"/> to apply to the row cells of the <see cref="T:System.Windows.Forms.DataGridView"/>.
        /// </returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public DataGridViewCellStyle RowsDefaultCellStyle
        {
            get
            {
                Contract.Ensures(Contract.Result<DataGridViewCellStyle>() != null);
                return default(DataGridViewCellStyle);
            }
            set { }
        }

        /// <summary>
        /// Gets or sets the row that represents the template for all the rows in the control.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Windows.Forms.DataGridViewRow"/> representing the row template.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">The specified row when setting this property has its <see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView"/> property set.</exception><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public DataGridViewRow RowTemplate
        {
            get
            {
                Contract.Ensures(Contract.Result<DataGridViewRow>() != null);
                return default(DataGridViewRow);
            }
            set { Contract.Requires(!(value != null && value.DataGridView != null)); }
        }

        // <summary>
        // Gets or sets the type of scroll bars to display for the <see cref="T:System.Windows.Forms.DataGridView"/> control.
        // </summary>
        // 
        // <returns>
        // One of the <see cref="T:System.Windows.Forms.ScrollBars"/> values. The default is <see cref="F:System.Windows.Forms.ScrollBars.Both"/>.
        // </returns>
        // <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.ScrollBars"/> value. </exception><exception cref="T:System.InvalidOperationException">The value of this property cannot be set because the <see cref="T:System.Windows.Forms.DataGridView"/> is unable to scroll due to a cell change that cannot be committed or canceled. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public ScrollBars ScrollBars {get; set;}

        /// <summary>
        /// Gets the collection of cells selected by the user.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Windows.Forms.DataGridViewSelectedCellCollection"/> that represents the cells selected by the user.
        /// </returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>    
        public DataGridViewSelectedCellCollection SelectedCells
        {
            get
            {
                Contract.Ensures(Contract.Result<DataGridViewSelectedCellCollection>() != null);
                return default(DataGridViewSelectedCellCollection);
            }
        }

        /// <summary>
        /// Gets the collection of columns selected by the user.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Windows.Forms.DataGridViewSelectedColumnCollection"/> that represents the columns selected by the user.
        /// </returns>
        /// 
        public DataGridViewSelectedColumnCollection SelectedColumns
        {
            get
            {
                Contract.Ensures(Contract.Result<DataGridViewSelectedColumnCollection>() != null);
                return default(DataGridViewSelectedColumnCollection);
            }
        }

        /// <summary>
        /// Gets the collection of rows selected by the user.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Windows.Forms.DataGridViewSelectedRowCollection"/> that contains the rows selected by the user.
        /// </returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public DataGridViewSelectedRowCollection SelectedRows
        {
            get
            {
                Contract.Ensures(Contract.Result<DataGridViewSelectedRowCollection>() != null);
                return default(DataGridViewSelectedRowCollection);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating how the cells of the <see cref="T:System.Windows.Forms.DataGridView"/> can be selected.
        /// </summary>
        /// 
        /// <returns>
        /// One of the <see cref="T:System.Windows.Forms.DataGridViewSelectionMode"/> values. The default is <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.RowHeaderSelect"/>.
        /// </returns>
        /// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.DataGridViewSelectionMode"/> value.</exception><exception cref="T:System.InvalidOperationException">The specified value when setting this property is <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.FullColumnSelect"/> or <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.ColumnHeaderSelect"/> and the <see cref="P:System.Windows.Forms.DataGridViewColumn.SortMode"/> property of one or more columns is set to <see cref="F:System.Windows.Forms.DataGridViewColumnSortMode.Automatic"/>.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public DataGridViewSelectionMode SelectionMode {get; set;}

        // <summary>
        // Gets or sets a value indicating whether to show cell errors.
        // </summary>
        // 
        // <returns>
        // true if a red glyph will appear in a cell that fails validation; otherwise, false. The default is true.
        // </returns>
        // <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public bool ShowCellErrors {get; set;}

        // <summary>
        // Gets or sets a value indicating whether or not ToolTips will show when the mouse pointer pauses on a cell.
        // </summary>
        // 
        // <returns>
        // true if cell ToolTips are enabled; otherwise, false.
        // </returns>
        // <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public bool ShowCellToolTips {get; set;}

        // <summary>
        // Gets or sets a value indicating whether or not the editing glyph is visible in the row header of the cell being edited.
        // </summary>
        // 
        // <returns>
        // true if the editing glyph is visible; otherwise, false. The default is true.
        // </returns>
        // public bool ShowEditingIcon {get; set;}

        // <summary>
        // Gets or sets a value indicating whether row headers will display error glyphs for each row that contains a data entry error.
        // </summary>
        // 
        // <returns>
        // true if the <see cref="T:System.Windows.Forms.DataGridViewRow"/> indicates there is an error; otherwise, false. The default is true.
        // </returns>
        // <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public bool ShowRowErrors {get; set;}

        // <summary>
        // Gets the column by which the <see cref="T:System.Windows.Forms.DataGridView"/> contents are currently sorted.
        // </summary>
        // 
        // <returns>
        // The <see cref="T:System.Windows.Forms.DataGridViewColumn"/> by which the <see cref="T:System.Windows.Forms.DataGridView"/> contents are currently sorted.
        // </returns> 
        // public DataGridViewColumn SortedColumn { get; }

        // <summary>
        // Gets a value indicating whether the items in the <see cref="T:System.Windows.Forms.DataGridView"/> control are sorted in ascending or descending order, or are not sorted.
        // </summary>
        // 
        // <returns>
        // One of the <see cref="T:System.Windows.Forms.SortOrder"/> values.
        // </returns>
        //  public SortOrder SortOrder { get; } 

        // <summary>
        // Gets or sets a value indicating whether the TAB key moves the focus to the next control in the tab order rather than moving focus to the next cell in the control.
        // </summary>
        // 
        // <returns>
        // true if the TAB key moves the focus to the next control in the tab order; otherwise, false.
        // </returns>
        // public bool StandardTab {get; set;}

        // <summary>
        // Gets or sets the text associated with the control.
        // </summary>
        // 
        // <returns>
        // The text associated with the control.
        // </returns>
        // public override string Text {get; set;}

        /// <summary>
        /// Provides an indexer to get or set the cell located at the intersection of the column and row with the specified indexes.
        /// </summary>
        /// 
        /// <returns>
        /// The <see cref="T:System.Windows.Forms.DataGridViewCell"/> at the specified location.
        /// </returns>
        /// <param name="columnIndex">The index of the column containing the cell.</param><param name="rowIndex">The index of the row containing the cell.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="columnIndex"/> is less than 0 or greater than the number of columns in the control minus 1.-or-<paramref name="rowIndex"/> is less than 0 or greater than the number of rows in the control minus 1.</exception>
        public DataGridViewCell this[int columnIndex, int rowIndex]
        {
            get
            {  
                Contract.Requires(rowIndex >= 0 && rowIndex < Rows.Count);
                Contract.Requires(columnIndex >= 0 && columnIndex < Rows[rowIndex].Cells.Count);
                return default(DataGridViewCell);
            }
            set { }
        }

        /// <summary>
        /// Provides an indexer to get or set the cell located at the intersection of the row with the specified index and the column with the specified name.
        /// </summary>
        /// 
        /// <returns>
        /// The <see cref="T:System.Windows.Forms.DataGridViewCell"/> at the specified location.
        /// </returns>
        /// <param name="columnName">The name of the column containing the cell.</param><param name="rowIndex">The index of the row containing the cell.</param>
        public DataGridViewCell this[string columnName, int rowIndex]
        {
            get
            {
                Contract.Requires(columnName != null);
                Contract.Requires(rowIndex >= 0 && rowIndex < Rows.Count);
                return default(DataGridViewCell);
            }
            set { }
        }

        /// <summary>
        /// Gets or sets the header cell located in the upper left corner of the <see cref="T:System.Windows.Forms.DataGridView"/> control.
        /// </summary>
        /// 
        /// <returns>
        /// The <see cref="T:System.Windows.Forms.DataGridViewHeaderCell"/> located at the upper left corner of the <see cref="T:System.Windows.Forms.DataGridView"/>.
        /// </returns>
        public DataGridViewHeaderCell TopLeftHeaderCell
        {
            get
            {
                Contract.Ensures(Contract.Result<DataGridViewHeaderCell>() != null);
                return default(DataGridViewHeaderCell);
            }
            set { }
        }

        /// <summary>
        /// Gets the default or user-specified value of the <see cref="P:System.Windows.Forms.Control.Cursor"/> property.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Windows.Forms.Cursor"/> representing the normal value of the <see cref="P:System.Windows.Forms.Control.Cursor"/> property.
        /// </returns>
        public Cursor UserSetCursor
        {
            get
            {
                Contract.Ensures(Contract.Result<Cursor>() != null);
                return default(Cursor);
            }
        }

        // <summary>
        // Gets the vertical scroll bar of the control.
        // </summary>
        // 
        // <returns>
        // A <see cref="T:System.Windows.Forms.ScrollBar"/> representing the vertical scroll bar.
        // </returns>
        // protected ScrollBar VerticalScrollBar {get;}

        /// <summary>
        /// Gets the number of pixels by which the control is scrolled vertically.
        /// </summary>
        /// 
        /// <returns>
        /// The number of pixels by which the control is scrolled vertically.
        /// </returns>
        // public int VerticalScrollingOffset {get;}

        /// <summary>
        /// Gets or sets a value indicating whether you have provided your own data-management operations for the <see cref="T:System.Windows.Forms.DataGridView"/> control.
        /// </summary>
        /// 
        /// <returns>
        /// true if the <see cref="T:System.Windows.Forms.DataGridView"/> uses data-management operations that you provide; otherwise, false. The default is false.
        /// </returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public bool VirtualMode {get; set;}

        // <summary>
        // Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridView"/> class.
        // </summary>
        // public DataGridView()

        /// <summary>
        /// Notifies the accessible client applications when a new cell becomes the current cell.
        /// </summary>
        /// <param name="cellAddress">A <see cref="T:System.Drawing.Point"/> indicating the row and column indexes of the new current cell.</param><exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Drawing.Point.X"/> property of <paramref name="cellAddress"/> is less than 0 or greater than the number of columns in the control minus 1. -or-The value of the <see cref="P:System.Drawing.Point.Y"/> property of <paramref name="cellAddress"/> is less than 0 or greater than the number of rows in the control minus 1.</exception>
        protected virtual void AccessibilityNotifyCurrentCellChanged(Point cellAddress)
        {
            Contract.Requires(cellAddress.X >= 0 || cellAddress.X < Columns.Count);
            Contract.Requires(cellAddress.Y >= 0 || cellAddress.Y < Columns.Count);
        }

        // <summary>
        // Adjusts the <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle"/> for a column header cell of a <see cref="T:System.Windows.Forms.DataGridView"/> that is currently being painted.
        // </summary>
        // 
        // <returns>
        // A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle"/> that represents the border style for the current column header.
        // </returns>
        // <param name="dataGridViewAdvancedBorderStyleInput">A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle"/> that that represents the column header border style to modify.</param><param name="dataGridViewAdvancedBorderStylePlaceholder">A <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle"/> that is used to store intermediate changes to the column header border style.</param><param name="isFirstDisplayedColumn">true to indicate that the <see cref="T:System.Windows.Forms.DataGridViewCell"/> that is currently being painted is in the first column displayed on the <see cref="T:System.Windows.Forms.DataGridView"/>; otherwise, false.</param><param name="isLastVisibleColumn">true to indicate that the <see cref="T:System.Windows.Forms.DataGridViewCell"/> that is currently being painted is in the last column in the <see cref="T:System.Windows.Forms.DataGridView"/> that has the <see cref="P:System.Windows.Forms.DataGridViewColumn.Visible"/> property set to true; otherwise, false.</param><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public virtual DataGridViewAdvancedBorderStyle AdjustColumnHeaderBorderStyle(DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStyleInput, DataGridViewAdvancedBorderStyle dataGridViewAdvancedBorderStylePlaceholder, bool isFirstDisplayedColumn, bool isLastVisibleColumn);

        // <summary>
        // Returns a value indicating whether all the <see cref="T:System.Windows.Forms.DataGridView"/> cells are currently selected.
        // </summary>
        // 
        // <returns>
        // true if all cells (or all visible cells) are selected or if there are no cells (or no visible cells); otherwise, false.
        // </returns>
        // <param name="includeInvisibleCells">true to include the rows and columns with <see cref="P:System.Windows.Forms.DataGridViewBand.Visible"/> property values of false; otherwise, false. </param><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public bool AreAllCellsSelected(bool includeInvisibleCells);

        /// <summary>
        /// Adjusts the width of the specified column to fit the contents of all its cells, including the header cell.
        /// </summary>
        /// <param name="columnIndex">The index of the column to resize.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="columnIndex"/> is not in the valid range of 0 to the number of columns minus 1. </exception>
        public void AutoResizeColumn(int columnIndex)
        {
            Contract.Requires(columnIndex >= 0 && columnIndex < Columns.Count);
        }

        /// <summary>
        /// Adjusts the width of the specified column using the specified size mode.
        /// </summary>
        /// <param name="columnIndex">The index of the column to resize. </param><param name="autoSizeColumnMode">One of the <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnMode"/> values. </param><exception cref="T:System.InvalidOperationException"><paramref name="autoSizeColumnMode"/> has the value <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader"/> and <see cref="P:System.Windows.Forms.DataGridView.ColumnHeadersVisible"/> is false. </exception><exception cref="T:System.ArgumentException"><paramref name="autoSizeColumnMode"/> has the value <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.NotSet"/>, <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.None"/>, or <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill"/>. </exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="columnIndex"/> is not in the valid range of 0 to the number of columns minus 1. </exception><exception cref="T:System.ComponentModel.InvalidEnumArgumentException"><paramref name="autoSizeColumnMode"/> is not a valid <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnMode"/> value.</exception>
        public void AutoResizeColumn(int columnIndex, DataGridViewAutoSizeColumnMode autoSizeColumnMode)
        {
            Contract.Requires(columnIndex >= 0 && columnIndex < Columns.Count);
        }

        /// <summary>
        /// Adjusts the width of the specified column using the specified size mode, optionally calculating the width with the expectation that row heights will subsequently be adjusted.
        /// </summary>
        /// <param name="columnIndex">The index of the column to resize. </param><param name="autoSizeColumnMode">One of the <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnMode"/> values. </param><param name="fixedHeight">true to calculate the new width based on the current row heights; false to calculate the width with the expectation that the row heights will also be adjusted.</param><exception cref="T:System.InvalidOperationException"><paramref name="autoSizeColumnMode"/> has the value <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader"/> and <see cref="P:System.Windows.Forms.DataGridView.ColumnHeadersVisible"/> is false. </exception><exception cref="T:System.ArgumentException"><paramref name="autoSizeColumnMode"/> has the value <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.NotSet"/>, <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.None"/>, or <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill"/>. </exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="columnIndex"/> is not in the valid range of 0 to the number of columns minus 1. </exception><exception cref="T:System.ComponentModel.InvalidEnumArgumentException"><paramref name="autoSizeColumnMode"/> is not a valid <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnMode"/> value.</exception>
        protected void AutoResizeColumn(int columnIndex, DataGridViewAutoSizeColumnMode autoSizeColumnMode,
            bool fixedHeight)
        {
            Contract.Requires(columnIndex >= 0 && columnIndex < Columns.Count);
            Contract.Requires(
                !(autoSizeColumnMode == DataGridViewAutoSizeColumnMode.NotSet ||
                  autoSizeColumnMode == DataGridViewAutoSizeColumnMode.None ||
                  autoSizeColumnMode == DataGridViewAutoSizeColumnMode.Fill));
            Contract.Requires(
                !(autoSizeColumnMode == DataGridViewAutoSizeColumnMode.ColumnHeader && !ColumnHeadersVisible));
        }

        // <summary>
        // Adjusts the height of the column headers to fit the contents of the largest column header.
        // </summary>
        // public void AutoResizeColumnHeadersHeight();

        /// <summary>
        /// Adjusts the height of the column headers based on changes to the contents of the header in the specified column.
        /// </summary>
        /// <param name="columnIndex">The index of the column containing the header with the changed content.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="columnIndex"/> is not in the valid range of 0 to the number of columns minus 1.</exception>
        public void AutoResizeColumnHeadersHeight(int columnIndex)
        {
            Contract.Requires(columnIndex >= 0 && columnIndex < Columns.Count);
        }

        // <summary>
        // Adjusts the height of the column headers to fit their contents, optionally calculating the height with the expectation that the column and/or row header widths will subsequently be adjusted.
        // </summary>
        // <param name="fixedRowHeadersWidth">true to calculate the new height based on the current width of the row headers; false to calculate the height with the expectation that the row headers width will also be adjusted. </param><param name="fixedColumnsWidth">true to calculate the new height based on the current column widths; false to calculate the height with the expectation that the column widths will also be adjusted.</param>
        // protected void AutoResizeColumnHeadersHeight(bool fixedRowHeadersWidth, bool fixedColumnsWidth)

        /// <summary>
        /// Adjusts the height of the column headers based on changes to the contents of the header in the specified column, optionally calculating the height with the expectation that the column and/or row header widths will subsequently be adjusted.
        /// </summary>
        /// <param name="columnIndex">The index of the column header whose contents should be used to determine new height.</param><param name="fixedRowHeadersWidth">true to calculate the new height based on the current width of the row headers; false to calculate the height with the expectation that the row headers width will also be adjusted.</param><param name="fixedColumnWidth">true to calculate the new height based on the current width of the specified column; false to calculate the height with the expectation that the column width will also be adjusted.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="columnIndex"/> is not in the valid range of 0 to the number of columns minus 1. </exception>
        protected void AutoResizeColumnHeadersHeight(int columnIndex, bool fixedRowHeadersWidth, bool fixedColumnWidth)
        {
            Contract.Requires(columnIndex >= 0 && columnIndex < Columns.Count);
        }

        // <summary>
        // Adjusts the width of all columns to fit the contents of all their cells, including the header cells.
        // </summary>
        // public void AutoResizeColumns()

        // <summary>
        // Adjusts the width of all columns using the specified size mode.
        // </summary>
        // <param name="autoSizeColumnsMode">One of the <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnsMode"/> values. </param><exception cref="T:System.InvalidOperationException"><paramref name="autoSizeColumnsMode"/> has the value <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader"/> and <see cref="P:System.Windows.Forms.DataGridView.ColumnHeadersVisible"/> is false. </exception><exception cref="T:System.ArgumentException"><paramref name="autoSizeColumnsMode"/> has the value <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None"/> or <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill"/>. </exception><exception cref="T:System.ComponentModel.InvalidEnumArgumentException"><paramref name="autoSizeColumnsMode"/> is not a valid <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnsMode"/> value.</exception>
        // public void AutoResizeColumns(DataGridViewAutoSizeColumnsMode autoSizeColumnsMode)

        // <summary>
        // Adjusts the width of all columns using the specified size mode, optionally calculating the widths with the expectation that row heights will subsequently be adjusted.
        // </summary>
        // <param name="autoSizeColumnsMode">One of the <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnsMode"/> values. </param><param name="fixedHeight">true to calculate the new widths based on the current row heights; false to calculate the widths with the expectation that the row heights will also be adjusted.</param><exception cref="T:System.InvalidOperationException"><paramref name="autoSizeColumnsMode"/> has the value <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader"/> and <see cref="P:System.Windows.Forms.DataGridView.ColumnHeadersVisible"/> is false. </exception><exception cref="T:System.ArgumentException"><paramref name="autoSizeColumnsMode"/> has the value <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None"/> or <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill"/>. </exception><exception cref="T:System.ComponentModel.InvalidEnumArgumentException"><paramref name="autoSizeColumnsMode"/> is not a valid <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnsMode"/> value.</exception>
        // protected void AutoResizeColumns(DataGridViewAutoSizeColumnsMode autoSizeColumnsMode, bool fixedHeight)

        /// <summary>
        /// Adjusts the height of the specified row to fit the contents of all its cells including the header cell.
        /// </summary>
        /// <param name="rowIndex">The index of the row to resize.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="rowIndex"/> is not in the valid range of 0 to the number of rows minus 1. </exception>
        public void AutoResizeRow(int rowIndex)
        {
            Contract.Requires(rowIndex >= 0 && rowIndex < Rows.Count);
        }

        /// <summary>
        /// Adjusts the height of the specified row using the specified size mode.
        /// </summary>
        /// <param name="rowIndex">The index of the row to resize. </param><param name="autoSizeRowMode">One of the <see cref="T:System.Windows.Forms.DataGridViewAutoSizeRowMode"/> values. </param><exception cref="T:System.InvalidOperationException"><paramref name="autoSizeRowMode"/> has the value <see cref="F:System.Windows.Forms.DataGridViewAutoSizeRowMode.RowHeader"/> and <see cref="P:System.Windows.Forms.DataGridView.RowHeadersVisible"/> is false. </exception><exception cref="T:System.ComponentModel.InvalidEnumArgumentException"><paramref name="autoSizeRowMode"/> is not a valid <see cref="T:System.Windows.Forms.DataGridViewAutoSizeRowMode"/> value. </exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="rowIndex"/> is not in the valid range of 0 to the number of rows minus 1.</exception>
        public void AutoResizeRow(int rowIndex, DataGridViewAutoSizeRowMode autoSizeRowMode)
        {
            Contract.Requires(rowIndex >= 0 && rowIndex < Rows.Count);
        }

        /// <summary>
        /// Adjusts the height of the specified row using the specified size mode, optionally calculating the height with the expectation that column widths will subsequently be adjusted.
        /// </summary>
        /// <param name="rowIndex">The index of the row to resize. </param><param name="autoSizeRowMode">One of the <see cref="T:System.Windows.Forms.DataGridViewAutoSizeRowMode"/> values. </param><param name="fixedWidth">true to calculate the new height based on the current width of the columns; false to calculate the height with the expectation that the column widths will also be adjusted.</param><exception cref="T:System.InvalidOperationException"><paramref name="autoSizeRowMode"/> has the value <see cref="F:System.Windows.Forms.DataGridViewAutoSizeRowMode.RowHeader"/> and <see cref="P:System.Windows.Forms.DataGridView.RowHeadersVisible"/> is false. </exception><exception cref="T:System.ComponentModel.InvalidEnumArgumentException"><paramref name="autoSizeRowMode"/> is not a valid <see cref="T:System.Windows.Forms.DataGridViewAutoSizeRowMode"/> value. </exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="rowIndex"/> is not in the valid range of 0 to the number of rows minus 1.</exception>
        protected void AutoResizeRow(int rowIndex, DataGridViewAutoSizeRowMode autoSizeRowMode, bool fixedWidth)
        {
            Contract.Requires(rowIndex >= 0 && rowIndex < Rows.Count);
            // Contract.Requires(!(autoSizeRowMode & (DataGridViewAutoSizeRowMode) -4) != (DataGridViewAutoSizeRowMode) 0));
            Contract.Requires(!((autoSizeRowMode == DataGridViewAutoSizeRowMode.RowHeader && !RowHeadersVisible)));
        }

        // <summary>
        // Adjusts the width of the row headers using the specified size mode.
        // </summary>
        // <param name="rowHeadersWidthSizeMode">One of the <see cref="T:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode"/> values.</param><exception cref="T:System.ArgumentException"><paramref name="rowHeadersWidthSizeMode"/> has the value <see cref="F:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.EnableResizing"/> or <see cref="F:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing"/>.</exception><exception cref="T:System.ComponentModel.InvalidEnumArgumentException"><paramref name="rowHeadersWidthSizeMode"/> is not a valid <see cref="T:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode"/> value. </exception>
        // public void AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode rowHeadersWidthSizeMode)

        /// <summary>
        /// Adjusts the width of the row headers using the specified size mode, optionally calculating the width with the expectation that the row and/or column header widths will subsequently be adjusted.
        /// </summary>
        /// <param name="rowHeadersWidthSizeMode">One of the <see cref="T:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode"/> values.</param><param name="fixedColumnHeadersHeight">true to calculate the new width based on the current height of the column headers; false to calculate the width with the expectation that the height of the column headers will also be adjusted.</param><param name="fixedRowsHeight">true to calculate the new width based on the current row heights; false to calculate the width with the expectation that the row heights will also be adjusted.</param><exception cref="T:System.ArgumentException"><paramref name="rowHeadersWidthSizeMode"/> has the value <see cref="F:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.EnableResizing"/> or <see cref="F:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing"/>.</exception><exception cref="T:System.ComponentModel.InvalidEnumArgumentException"><paramref name="rowHeadersWidthSizeMode"/> is not a valid <see cref="T:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode"/> value. </exception>
        protected void AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode rowHeadersWidthSizeMode,
            bool fixedColumnHeadersHeight, bool fixedRowsHeight)
        {
            Contract.Requires(
                !(rowHeadersWidthSizeMode == DataGridViewRowHeadersWidthSizeMode.EnableResizing ||
                  rowHeadersWidthSizeMode == DataGridViewRowHeadersWidthSizeMode.DisableResizing));
            Contract.Requires(
                !(rowHeadersWidthSizeMode < DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders ||
                  rowHeadersWidthSizeMode > DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader));
        }

        /// <summary>
        /// Adjusts the width of the row headers based on changes to the contents of the header in the specified row and using the specified size mode.
        /// </summary>
        /// <param name="rowIndex">The index of the row header with the changed content.</param><param name="rowHeadersWidthSizeMode">One of the <see cref="T:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode"/> values.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="rowIndex"/> is not in the valid range of 0 to the number of rows minus 1. </exception><exception cref="T:System.ArgumentException"><paramref name="rowHeadersWidthSizeMode"/> has the value <see cref="F:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.EnableResizing"/> or <see cref="F:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing"/></exception><exception cref="T:System.ComponentModel.InvalidEnumArgumentException"><paramref name="rowHeadersWidthSizeMode"/> is not a valid <see cref="T:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode"/> value. </exception>
        public void AutoResizeRowHeadersWidth(int rowIndex, DataGridViewRowHeadersWidthSizeMode rowHeadersWidthSizeMode)
        {
            Contract.Requires(rowIndex >= 0 && rowIndex < Rows.Count);
        }

        /// <summary>
        /// Adjusts the width of the row headers based on changes to the contents of the header in the specified row and using the specified size mode, optionally calculating the width with the expectation that the row and/or column header widths will subsequently be adjusted.
        /// </summary>
        /// <param name="rowIndex">The index of the row containing the header with the changed content.</param><param name="rowHeadersWidthSizeMode">One of the <see cref="T:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode"/> values.</param><param name="fixedColumnHeadersHeight">true to calculate the new width based on the current height of the column headers; false to calculate the width with the expectation that the height of the column headers will also be adjusted.</param><param name="fixedRowHeight">true to calculate the new width based on the current height of the specified row; false to calculate the width with the expectation that the row height will also be adjusted.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="rowIndex"/> is not in the valid range of 0 to the number of rows minus 1. </exception><exception cref="T:System.ArgumentException"><paramref name="rowHeadersWidthSizeMode"/> has the value <see cref="F:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.EnableResizing"/> or <see cref="F:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing"/>.</exception><exception cref="T:System.ComponentModel.InvalidEnumArgumentException"><paramref name="rowHeadersWidthSizeMode"/> is not a valid <see cref="T:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode"/> value. </exception>
        protected void AutoResizeRowHeadersWidth(int rowIndex,
            DataGridViewRowHeadersWidthSizeMode rowHeadersWidthSizeMode, bool fixedColumnHeadersHeight,
            bool fixedRowHeight)
        {
            Contract.Requires(rowIndex >= 0 && rowIndex < Rows.Count);
            Contract.Requires(
                !(rowHeadersWidthSizeMode == DataGridViewRowHeadersWidthSizeMode.EnableResizing ||
                  rowHeadersWidthSizeMode == DataGridViewRowHeadersWidthSizeMode.DisableResizing));
            Contract.Requires(
                !(rowHeadersWidthSizeMode < DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders ||
                  rowHeadersWidthSizeMode > DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader));
        }

        // <summary>
        // Adjusts the heights of all rows to fit the contents of all their cells, including the header cells.
        // </summary>
        // public void AutoResizeRows()

        // <summary>
        // Adjusts the heights of the rows using the specified size mode value.
        // </summary>
        // <param name="autoSizeRowsMode">One of the <see cref="T:System.Windows.Forms.DataGridViewAutoSizeRowsMode"/> values. </param><exception cref="T:System.InvalidOperationException"><paramref name="autoSizeRowsMode"/> has the value <see cref="F:System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders"/> or <see cref="F:System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedHeaders"/>, and <see cref="P:System.Windows.Forms.DataGridView.RowHeadersVisible"/> is false. </exception><exception cref="T:System.ComponentModel.InvalidEnumArgumentException"><paramref name="autoSizeRowsMode"/> is not a valid <see cref="T:System.Windows.Forms.DataGridViewAutoSizeRowsMode"/> value. </exception><exception cref="T:System.ArgumentException"><paramref name="autoSizeRowsMode"/> has the value <see cref="F:System.Windows.Forms.DataGridViewAutoSizeRowsMode.None"/>.</exception>
        // public void AutoResizeRows(DataGridViewAutoSizeRowsMode autoSizeRowsMode)

        /// <summary>
        /// Adjusts the heights of all rows using the specified size mode, optionally calculating the heights with the expectation that column widths will subsequently be adjusted.
        /// </summary>
        /// <param name="autoSizeRowsMode">One of the <see cref="T:System.Windows.Forms.DataGridViewAutoSizeRowsMode"/> values.</param><param name="fixedWidth">true to calculate the new heights based on the current column widths; false to calculate the heights with the expectation that the column widths will also be adjusted.</param><exception cref="T:System.InvalidOperationException"><paramref name="autoSizeRowsMode"/> has the value <see cref="F:System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders"/> or <see cref="F:System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedHeaders"/>, and <see cref="P:System.Windows.Forms.DataGridView.RowHeadersVisible"/> is false. </exception><exception cref="T:System.ComponentModel.InvalidEnumArgumentException"><paramref name="autoSizeRowsMode"/> is not a valid <see cref="T:System.Windows.Forms.DataGridViewAutoSizeRowsMode"/> value. </exception><exception cref="T:System.ArgumentException"><paramref name="autoSizeRowsMode"/> has the value <see cref="F:System.Windows.Forms.DataGridViewAutoSizeRowsMode.None"/>.</exception>
        protected void AutoResizeRows(DataGridViewAutoSizeRowsMode autoSizeRowsMode, bool fixedWidth)
        {
            Contract.Requires(autoSizeRowsMode != DataGridViewAutoSizeRowsMode.DisplayedCells ||
                              !(autoSizeRowsMode == DataGridViewAutoSizeRowsMode.None));
            Contract.Requires(autoSizeRowsMode != DataGridViewAutoSizeRowsMode.DisplayedCells ||
                              !((autoSizeRowsMode == DataGridViewAutoSizeRowsMode.AllHeaders ||
                                 autoSizeRowsMode == DataGridViewAutoSizeRowsMode.DisplayedHeaders) &&
                                !RowHeadersVisible));

        }

        /// <summary>
        /// Adjusts the heights of the specified rows using the specified size mode, optionally calculating the heights with the expectation that column widths will subsequently be adjusted.
        /// </summary>
        /// <param name="rowIndexStart">The index of the first row to resize. </param><param name="rowsCount">The number of rows to resize. </param><param name="autoSizeRowMode">One of the <see cref="T:System.Windows.Forms.DataGridViewAutoSizeRowMode"/> values. </param><param name="fixedWidth">true to calculate the new heights based on the current column widths; false to calculate the heights with the expectation that the column widths will also be adjusted.</param><exception cref="T:System.InvalidOperationException"><paramref name="autoSizeRowsMode"/> has the value <see cref="F:System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders"/> or <see cref="F:System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedHeaders"/>, and <see cref="P:System.Windows.Forms.DataGridView.RowHeadersVisible"/> is false. </exception><exception cref="T:System.ComponentModel.InvalidEnumArgumentException"><paramref name="autoSizeRowsMode"/> is not a valid <see cref="T:System.Windows.Forms.DataGridViewAutoSizeRowsMode"/> value. </exception><exception cref="T:System.ArgumentException"><paramref name="autoSizeRowsMode"/> has the value <see cref="F:System.Windows.Forms.DataGridViewAutoSizeRowsMode.None"/>.</exception><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="rowIndexStart"/> is less than 0.-or-<paramref name="rowsCount"/> is less than 0.</exception>
        protected void AutoResizeRows(int rowIndexStart, int rowsCount, DataGridViewAutoSizeRowMode autoSizeRowMode,
            bool fixedWidth)
        {
            // Contract.Requires(!(autoSizeRowMode & (DataGridViewAutoSizeRowMode) -4) != (DataGridViewAutoSizeRowMode) 0));
            Contract.Requires(!(autoSizeRowMode == DataGridViewAutoSizeRowMode.RowHeader && !RowHeadersVisible));
            Contract.Requires(rowsCount >= 0);
            Contract.Requires(rowIndexStart >= 0);
        }

        // <summary>
        // Puts the current cell in edit mode.
        // </summary>
        // 
        // <returns>
        // true if the current cell is already in edit mode or successfully enters edit mode; otherwise, false.
        // </returns>
        // <param name="selectAll">true to select all the cell's contents; false to not select any contents.</param><exception cref="T:System.InvalidOperationException"><see cref="P:System.Windows.Forms.DataGridView.CurrentCell"/> is not set to a valid cell.-or-This method was called in a handler for the <see cref="E:System.Windows.Forms.DataGridView.CellBeginEdit"/> event.</exception><exception cref="T:System.InvalidCastException">The type indicated by the cell's <see cref="P:System.Windows.Forms.DataGridViewCell.EditType"/> property does not derive from the <see cref="T:System.Windows.Forms.Control"/> type.-or-The type indicated by the cell's <see cref="P:System.Windows.Forms.DataGridViewCell.EditType"/> property does not implement the <see cref="T:System.Windows.Forms.IDataGridViewEditingControl"/> interface.</exception><exception cref="T:System.Exception">Initialization of the editing cell value failed and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError"/> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException"/> property to true. The exception object can typically be cast to type <see cref="T:System.FormatException"/>.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public virtual bool BeginEdit(bool selectAll)
        
        // <summary>
        // Clears the current selection by unselecting all selected cells.
        // </summary>
        // <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public void ClearSelection()

        /// <summary>
        /// Cancels the selection of all currently selected cells except the one indicated, optionally ensuring that the indicated cell is selected.
        /// </summary>
        /// <param name="columnIndexException">The column index to exclude.</param><param name="rowIndexException">The row index to exclude.</param><param name="selectExceptionElement">true to select the excluded cell, row, or column; false to retain its original state.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="columnIndexException"/> is greater than the highest column index.-or-<paramref name="columnIndexException"/> is less than -1 when <see cref="P:System.Windows.Forms.DataGridView.SelectionMode"/> is <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect"/>; otherwise, <paramref name="columnIndexException"/> is less than 0.-or- <paramref name="rowIndexException"/> is greater than the highest row index.-or-<paramref name="rowIndexException"/> is less than -1 when <see cref="P:System.Windows.Forms.DataGridView.SelectionMode"/> is <see cref="F:System.Windows.Forms.DataGridViewSelectionMode.FullColumnSelect"/>; otherwise, <paramref name="rowIndexException"/> is less than 0.</exception>
        protected void ClearSelection(int columnIndexException, int rowIndexException, bool selectExceptionElement)
        {
            Contract.Requires((SelectionMode != DataGridViewSelectionMode.RowHeaderSelect ||
                               SelectionMode != DataGridViewSelectionMode.ColumnHeaderSelect) ||
                              (columnIndexException >= 0 && columnIndexException < Columns.Count) &&
                              (rowIndexException >= 0 && rowIndexException < Rows.Count));
        }

        // <summary>
        // Commits changes in the current cell to the data cache without ending edit mode.
        // </summary>
        // 
        // <returns>
        // true if the changes were committed; otherwise false.
        // </returns>
        // <param name="context">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewDataErrorContexts"/> values that specifies the context in which an error can occur. </param><exception cref="T:System.Exception">The cell value could not be committed and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError"/> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException"/> property to true. </exception>
        // public bool CommitEdit(DataGridViewDataErrorContexts context)

        /// <summary>
        /// Creates a new accessible object for the <see cref="T:System.Windows.Forms.DataGridView"/>.
        /// </summary>
        /// 
        /// <returns>
        /// A new <see cref="T:System.Windows.Forms.DataGridView.DataGridViewAccessibleObject"/> for the <see cref="T:System.Windows.Forms.DataGridView"/>.
        /// </returns>
        protected AccessibleObject CreateAccessibilityInstance()
        {
            Contract.Ensures(Contract.Result<AccessibleObject>() != null);
            return default(AccessibleObject);
        }

        /// <summary>
        /// Creates and returns a new <see cref="T:System.Windows.Forms.Control.ControlCollection"/> that can be cast to type <see cref="T:System.Windows.Forms.DataGridView.DataGridViewControlCollection"/>.
        /// </summary>
        /// 
        /// <returns>
        /// An empty <see cref="T:System.Windows.Forms.Control.ControlCollection"/>.
        /// </returns>
        protected Control.ControlCollection CreateControlsInstance()
        {
            Contract.Ensures(Contract.Result<Control.ControlCollection>() != null);
            return default(Control.ControlCollection);
        }

        /// <summary>
        /// Creates and returns a new <see cref="T:System.Windows.Forms.DataGridViewColumnCollection"/>.
        /// </summary>
        /// 
        /// <returns>
        /// An empty <see cref="T:System.Windows.Forms.DataGridViewColumnCollection"/>.
        /// </returns>
        protected virtual DataGridViewColumnCollection CreateColumnsInstance()
        {
            Contract.Ensures(Contract.Result<DataGridViewColumnCollection>() != null);
            return default(DataGridViewColumnCollection);
        }

        /// <summary>
        /// Creates and returns a new <see cref="T:System.Windows.Forms.DataGridViewRowCollection"/>.
        /// </summary>
        /// 
        /// <returns>
        /// An empty <see cref="T:System.Windows.Forms.DataGridViewRowCollection"/>.
        /// </returns>
        protected virtual DataGridViewRowCollection CreateRowsInstance()
        {
            Contract.Ensures(Contract.Result<DataGridViewRowCollection>() != null);
            return default(DataGridViewRowCollection);
        }

        /// <summary>
        /// Returns the number of columns displayed to the user.
        /// </summary>
        /// 
        /// <returns>
        /// The number of columns displayed to the user.
        /// </returns>
        /// <param name="includePartialColumns">true to include partial columns in the displayed column count; otherwise, false. </param>
        public int DisplayedColumnCount(bool includePartialColumns)
        {
            Contract.Ensures(Contract.Result<int>() > 0);
            return default(int);
        }

        /// <summary>
        /// Returns the number of rows displayed to the user.
        /// </summary>
        /// 
        /// <returns>
        /// The number of rows displayed to the user.
        /// </returns>
        /// <param name="includePartialRow">true to include partial rows in the displayed row count; otherwise, false. </param>
        public int DisplayedRowCount(bool includePartialRow)
        {
            Contract.Ensures(Contract.Result<int>() > 0);
            return default(int);
        }

        // <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
        // protected override void Dispose(bool disposing)

        // <summary>
        // Commits and ends the edit operation on the current cell using the default error context.
        // </summary>
        // 
        // <returns>
        // true if the edit operation is committed and ended; otherwise, false.
        // </returns>
        // <exception cref="T:System.Exception">The cell value could not be committed and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError"/> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException"/> property to true. </exception>
        // public bool EndEdit()

        // <summary>
        // Commits and ends the edit operation on the current cell using the specified error context.
        // </summary>
        // 
        // <returns>
        // true if the edit operation is committed and ended; otherwise, false.
        // </returns>
        // <param name="context">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewDataErrorContexts"/> values that specifies the context in which an error can occur. </param><exception cref="T:System.Exception">The cell value could not be committed and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError"/> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException"/> property to true. </exception>
        // public bool EndEdit(DataGridViewDataErrorContexts context)

        /// <summary>
        /// Gets the number of cells that satisfy the provided filter.
        /// </summary>
        /// 
        /// <returns>
        /// The number of cells that match the <paramref name="includeFilter"/> parameter.
        /// </returns>
        /// <param name="includeFilter">A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewElementStates"/> values specifying the cells to count.</param><exception cref="T:System.ArgumentException"><paramref name="includeFilter"/> includes the value <see cref="F:System.Windows.Forms.DataGridViewElementStates.ResizableSet"/>.</exception>
        public int GetCellCount(DataGridViewElementStates includeFilter)
        {
            Contract.Requires(
                !((includeFilter &
                   ~(DataGridViewElementStates.Displayed | DataGridViewElementStates.Frozen |
                     DataGridViewElementStates.ReadOnly | DataGridViewElementStates.Resizable |
                     DataGridViewElementStates.Selected | DataGridViewElementStates.Visible)) !=
                  DataGridViewElementStates.None));
            return default(int);
        }

        /// <summary>
        /// Returns the rectangle that represents the display area for a cell.
        /// </summary>
        /// 
        /// <returns>
        /// The <see cref="T:System.Drawing.Rectangle"/> that represents the display rectangle of the cell.
        /// </returns>
        /// <param name="columnIndex">The column index for the desired cell. </param><param name="rowIndex">The row index for the desired cell. </param><param name="cutOverflow">true to return the displayed portion of the cell only; false to return the entire cell bounds. </param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="columnIndex"/> is less than -1 or greater than the number of columns in the control minus 1.-or-<paramref name="rowIndex"/> is less than -1 or greater than the number of rows in the control minus 1. </exception>
        public Rectangle GetCellDisplayRectangle(int columnIndex, int rowIndex, bool cutOverflow)
        {
            Contract.Requires(!(columnIndex < -1 || columnIndex >= Columns.Count));
            Contract.Requires(!(rowIndex < -1 || rowIndex >= Rows.Count));
            return default(Rectangle);
        }

        // <summary>
        // Retrieves the formatted values that represent the contents of the selected cells for copying to the <see cref="T:System.Windows.Forms.Clipboard"/>.
        // </summary>
        // 
        // <returns>
        // A <see cref="T:System.Windows.Forms.DataObject"/> that represents the contents of the selected cells.
        // </returns>
        // <exception cref="T:System.NotSupportedException"><see cref="P:System.Windows.Forms.DataGridView.ClipboardCopyMode"/> is set to <see cref="F:System.Windows.Forms.DataGridViewClipboardCopyMode.Disable"/>.</exception>
        public virtual DataObject GetClipboardContent()
        {
            Contract.Requires(ClipboardCopyMode != DataGridViewClipboardCopyMode.Disable);
            return default(DataObject);
        }

        /// <summary>
        /// Returns the rectangle that represents the display area for a column, as determined by the column index.
        /// </summary>
        /// 
        /// <returns>
        /// The <see cref="T:System.Drawing.Rectangle"/> that represents the display rectangle of the column.
        /// </returns>
        /// <param name="columnIndex">The column index for the desired cell. </param><param name="cutOverflow">true to return the column rectangle visible in the <see cref="T:System.Windows.Forms.DataGridView"/> bounds; false to return the entire column rectangle. </param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="columnIndex"/> is not in the valid range of 0 to the number of columns minus 1. </exception>
        public Rectangle GetColumnDisplayRectangle(int columnIndex, bool cutOverflow)
        {
            Contract.Requires(columnIndex >= 0 && columnIndex < Columns.Count);
            return default(Rectangle);
        }

        // <summary>
        // Returns location information, such as row and column indices, given x- and y-coordinates.
        // </summary>
        // 
        // <returns>
        // A <see cref="T:System.Windows.Forms.DataGridView.HitTestInfo"/> that contains the location information.
        // </returns>
        // <param name="x">The x-coordinate. </param><param name="y">The y-coordinate. </param>
        // public DataGridView.HitTestInfo HitTest(int x, int y);

        /// <summary>
        /// Invalidates the specified cell of the <see cref="T:System.Windows.Forms.DataGridView"/>, forcing it to be repainted.
        /// </summary>
        /// <param name="dataGridViewCell">The <see cref="T:System.Windows.Forms.DataGridViewCell"/> to invalidate. </param><exception cref="T:System.ArgumentException"><paramref name="dataGridViewCell"/> does not belong to the <see cref="T:System.Windows.Forms.DataGridView"/>. </exception><exception cref="T:System.ArgumentNullException"><paramref name="dataGridViewCell"/> is null.</exception>
        public void InvalidateCell(DataGridViewCell dataGridViewCell)
        {
            Contract.Requires(dataGridViewCell != null && dataGridViewCell.DataGridView == this);
        }

        /// <summary>
        /// Invalidates the cell with the specified row and column indexes, forcing it to be repainted.
        /// </summary>
        /// <param name="columnIndex">The column index of the cell to invalidate.</param><param name="rowIndex">The row index of the cell to invalidate. </param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="columnIndex"/> is less than -1 or greater than the number of columns in the control minus 1.-or-<paramref name="rowIndex"/> is less than -1 or greater than the number of rows in the control minus 1. </exception>
        public void InvalidateCell(int columnIndex, int rowIndex)
        {
            Contract.Requires(!(columnIndex < -1 || columnIndex >= Columns.Count));
            Contract.Requires(!(rowIndex < -1 || rowIndex >= Rows.Count));
        }

        /// <summary>
        /// Invalidates the specified column of the <see cref="T:System.Windows.Forms.DataGridView"/>, forcing it to be repainted.
        /// </summary>
        /// <param name="columnIndex">The index of the column to invalidate. </param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="columnIndex"/> is not in the valid range of 0 to the number of columns minus 1. </exception>
        public void InvalidateColumn(int columnIndex)
        {
            Contract.Requires(columnIndex >= 0 && columnIndex < Columns.Count);
        }

        /// <summary>
        /// Invalidates the specified row of the <see cref="T:System.Windows.Forms.DataGridView"/>, forcing it to be repainted.
        /// </summary>
        /// <param name="rowIndex">The index of the row to invalidate. </param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="rowIndex"/> is not in the valid range of 0 to the number of rows minus 1. </exception>
        public void InvalidateRow(int rowIndex)
        {
            Contract.Requires(rowIndex >= 0 && rowIndex < Rows.Count);
        }

        // <summary>
        // Determines whether a character is an input character that the <see cref="T:System.Windows.Forms.DataGridView"/> recognizes.
        // </summary>
        // 
        // <returns>
        // true if the character is recognized as an input character; otherwise, false.
        // </returns>
        // <param name="charCode">The character to test.</param>
        // protected override bool IsInputChar(char charCode)

        // <returns>
        // true if the specified key is a regular input key; otherwise, false.
        // </returns>
        // <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys"/> values. </param>
        // protected override bool IsInputKey(Keys keyData)

        // <summary>
        // Notifies the <see cref="T:System.Windows.Forms.DataGridView"/> that the current cell has uncommitted changes.
        // </summary>
        // <param name="dirty">true to indicate the cell has uncommitted changes; otherwise, false. </param><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public virtual void NotifyCurrentCellDirty(bool dirty)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.AllowUserToAddRowsChanged"/> event.
        // </summary>
        // <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        // protected virtual void OnAllowUserToAddRowsChanged(EventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.AllowUserToDeleteRowsChanged"/> event.
        // </summary>
        // <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        // protected virtual void OnAllowUserToDeleteRowsChanged(EventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.AllowUserToOrderColumnsChanged"/> event.
        // </summary>
        // <param name="e">A <see cref="T:System.EventArgs"/> that contains the event data. </param>
        // protected virtual void OnAllowUserToOrderColumnsChanged(EventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.AllowUserToResizeColumnsChanged"/> event.
        // </summary>
        // <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        // protected virtual void OnAllowUserToResizeColumnsChanged(EventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.AllowUserToResizeRowsChanged"/> event.
        // </summary>
        // <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        // protected virtual void OnAllowUserToResizeRowsChanged(EventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.AlternatingRowsDefaultCellStyleChanged"/> event.
        // </summary>
        // <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        // protected virtual void OnAlternatingRowsDefaultCellStyleChanged(EventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.AutoGenerateColumnsChanged"/> event.
        // </summary>
        // <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        // protected virtual void OnAutoGenerateColumnsChanged(EventArgs e)

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.AutoSizeColumnModeChanged"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnModeEventArgs"/> that contains the event data. </param><exception cref="T:System.InvalidOperationException">The value of the <see cref="P:System.Windows.Forms.DataGridViewAutoSizeColumnModeEventArgs.Column"/> property of <paramref name="e"/> is null.</exception>
        protected virtual void OnAutoSizeColumnModeChanged(DataGridViewAutoSizeColumnModeEventArgs e)
        {
            Contract.Requires(e != null && e.Column != null);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.AutoSizeColumnsModeChanged"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnsModeEventArgs"/> that contains the event data. </param><exception cref="T:System.ArgumentNullException">The value of the <see cref="P:System.Windows.Forms.DataGridViewAutoSizeColumnsModeEventArgs.PreviousModes"/> property of <paramref name="e"/> is null.</exception><exception cref="T:System.ArgumentException">The number of entries in the array returned by the <see cref="P:System.Windows.Forms.DataGridViewAutoSizeColumnsModeEventArgs.PreviousModes"/> property of <paramref name="e"/> is not equal to the number of columns in the control.</exception>
        protected virtual void OnAutoSizeColumnsModeChanged(DataGridViewAutoSizeColumnsModeEventArgs e)
        {
            Contract.Requires(e != null && e.PreviousModes != null && e.PreviousModes.Length == Columns.Count);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.AutoSizeRowsModeChanged"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewAutoSizeModeEventArgs"/> that contains the event data. </param>
        protected virtual void OnAutoSizeRowsModeChanged(DataGridViewAutoSizeModeEventArgs e)
        {
            Contract.Requires(e != null);
        }

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.BackgroundColorChanged"/> event.
        // </summary>
        // <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        // protected virtual void OnBackgroundColorChanged(EventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.Control.BindingContextChanged"/> event.
        // </summary>
        // <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        // protected override void OnBindingContextChanged(EventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.BorderStyleChanged"/> event.
        // </summary>
        // <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        // protected virtual void OnBorderStyleChanged(EventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.CancelRowEdit"/> event.
        // </summary>
        // <param name="e">A <see cref="T:System.Windows.Forms.QuestionEventArgs"/> that contains the event data. </param>
        // protected virtual void OnCancelRowEdit(QuestionEventArgs e)

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.CellBeginEdit"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellCancelEventArgs"/> that contains the event data. </param><exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellCancelEventArgs.ColumnIndex"/> property of <paramref name="e"/> is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellCancelEventArgs.RowIndex"/> property of <paramref name="e"/> is greater than the number of rows in the control minus one.</exception>
        protected virtual void OnCellBeginEdit(DataGridViewCellCancelEventArgs e)
        {
            Contract.Requires(e != null);
            Contract.Requires(e.ColumnIndex < Columns.Count);
            Contract.Requires(e.RowIndex < Rows.Count);
        }

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.CellBorderStyleChanged"/> event.
        // </summary>
        // <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        // protected virtual void OnCellBorderStyleChanged(EventArgs e)

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.CellClick"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> that contains the event data. </param><exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.ColumnIndex"/> property of <paramref name="e"/> is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.RowIndex"/> property of <paramref name="e"/> is greater than the number of rows in the control minus one.</exception>
        protected virtual void OnCellClick(DataGridViewCellEventArgs e)
        {
            Contract.Requires(e != null);
            Contract.Requires(e.ColumnIndex < Columns.Count);
            Contract.Requires(e.RowIndex < Rows.Count);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.CellContentClick"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> that contains information regarding the cell whose content was clicked.</param><exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.ColumnIndex"/> property of <paramref name="e"/> is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.RowIndex"/> property of <paramref name="e"/> is greater than the number of rows in the control minus one.</exception>
        protected virtual void OnCellContentClick(DataGridViewCellEventArgs e)
        {
            Contract.Requires(e != null);
            Contract.Requires(e.ColumnIndex < Columns.Count);
            Contract.Requires(e.RowIndex < Rows.Count);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.CellContentDoubleClick"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> that contains the event data. </param><exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.ColumnIndex"/> property of <paramref name="e"/> is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.RowIndex"/> property of <paramref name="e"/> is greater than the number of rows in the control minus one.</exception>
        protected virtual void OnCellContentDoubleClick(DataGridViewCellEventArgs e)
        {
            Contract.Requires(e != null);
            Contract.Requires(e.ColumnIndex < Columns.Count);
            Contract.Requires(e.RowIndex < Rows.Count);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.CellContextMenuStripChanged"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> that contains the event data. </param><exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.ColumnIndex"/> property of <paramref name="e"/> is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.RowIndex"/> property of <paramref name="e"/> is greater than the number of rows in the control minus one.</exception>
        protected virtual void OnCellContextMenuStripChanged(DataGridViewCellEventArgs e)
        {
            Contract.Requires(e != null);
            Contract.Requires(e.ColumnIndex < Columns.Count);
            Contract.Requires(e.RowIndex < Rows.Count);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.CellContextMenuStripNeeded"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellContextMenuStripNeededEventArgs"/> that contains the event data. </param><exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.ColumnIndex"/> property of <paramref name="e"/> is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.RowIndex"/> property of <paramref name="e"/> is greater than the number of rows in the control minus one.</exception>
        protected virtual void OnCellContextMenuStripNeeded(DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            Contract.Requires(e != null);
            Contract.Requires(e.ColumnIndex < Columns.Count);
            Contract.Requires(e.RowIndex < Rows.Count);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.CellDoubleClick"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> that contains the event data. </param><exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.ColumnIndex"/> property of <paramref name="e"/> is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.RowIndex"/> property of <paramref name="e"/> is greater than the number of rows in the control minus one.</exception>
        protected virtual void OnCellDoubleClick(DataGridViewCellEventArgs e)
        {
            Contract.Requires(e != null);
            Contract.Requires(e.ColumnIndex < Columns.Count);
            Contract.Requires(e.RowIndex < Rows.Count);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.CellEndEdit"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> that contains the event data. </param><exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.ColumnIndex"/> property of <paramref name="e"/> is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.RowIndex"/> property of <paramref name="e"/> is greater than the number of rows in the control minus one.</exception>
        protected virtual void OnCellEndEdit(DataGridViewCellEventArgs e)
        {
            Contract.Requires(e != null);
            Contract.Requires(e.ColumnIndex < Columns.Count);
            Contract.Requires(e.RowIndex < Rows.Count);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.CellEnter"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> that contains the event data. </param><exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.ColumnIndex"/> property of <paramref name="e"/> is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.RowIndex"/> property of <paramref name="e"/> is greater than the number of rows in the control minus one.</exception>
        protected virtual void OnCellEnter(DataGridViewCellEventArgs e)
        {
            Contract.Requires(e != null);
            Contract.Requires(e.ColumnIndex < Columns.Count);
            Contract.Requires(e.RowIndex < Rows.Count);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.CellErrorTextChanged"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> that contains the event data. </param><exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.ColumnIndex"/> property of <paramref name="e"/> is less than -1 or greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.RowIndex"/> property of <paramref name="e"/> is less than -1 or greater than the number of rows in the control minus one.</exception>
        protected virtual void OnCellErrorTextChanged(DataGridViewCellEventArgs e)
        {
            Contract.Requires(e != null);
            Contract.Requires(e.ColumnIndex < Columns.Count);
            Contract.Requires(e.RowIndex < Rows.Count);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.CellErrorTextNeeded"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellErrorTextNeededEventArgs"/> that contains the event data. </param><exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.ColumnIndex"/> property of <paramref name="e"/> is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.RowIndex"/> property of <paramref name="e"/> is greater than the number of rows in the control minus one.</exception>
        protected virtual void OnCellErrorTextNeeded(DataGridViewCellErrorTextNeededEventArgs e)
        {
            Contract.Requires(e != null);
            Contract.Requires(e.ColumnIndex < Columns.Count);
            Contract.Requires(e.RowIndex < Rows.Count);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.CellFormatting"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellFormattingEventArgs"/> that contains the event data. </param><exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellFormattingEventArgs.ColumnIndex"/> property of <paramref name="e"/> is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellFormattingEventArgs.RowIndex"/> property of <paramref name="e"/> is greater than the number of rows in the control minus one.</exception>
        protected virtual void OnCellFormatting(DataGridViewCellFormattingEventArgs e)
        {
            Contract.Requires(e != null);
            Contract.Requires(e.ColumnIndex < Columns.Count);
            Contract.Requires(e.RowIndex < Rows.Count);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.CellLeave"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> that contains the event data. </param><exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.ColumnIndex"/> property of <paramref name="e"/> is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.RowIndex"/> property of <paramref name="e"/> is greater than the number of rows in the control minus one.</exception>
        protected virtual void OnCellLeave(DataGridViewCellEventArgs e)
        {
            Contract.Requires(e != null);
            Contract.Requires(e.ColumnIndex < Columns.Count);
            Contract.Requires(e.RowIndex < Rows.Count);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.CellMouseClick"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> that contains the event data. </param><exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellMouseEventArgs.ColumnIndex"/> property of <paramref name="e"/> is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellMouseEventArgs.RowIndex"/> property of <paramref name="e"/> is greater than the number of rows in the control minus one.</exception>
        protected virtual void OnCellMouseClick(DataGridViewCellMouseEventArgs e)
        {
            Contract.Requires(e != null);
            Contract.Requires(e.ColumnIndex < Columns.Count);
            Contract.Requires(e.RowIndex < Rows.Count);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.CellMouseDoubleClick"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> that contains the event data.</param><exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellMouseEventArgs.ColumnIndex"/> property of <paramref name="e"/> is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellMouseEventArgs.RowIndex"/> property of <paramref name="e"/> is greater than the number of rows in the control minus one.</exception>
        protected virtual void OnCellMouseDoubleClick(DataGridViewCellMouseEventArgs e)
        {
            Contract.Requires(e != null);
            Contract.Requires(e.ColumnIndex < Columns.Count);
            Contract.Requires(e.RowIndex < Rows.Count);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.CellMouseDown"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> that contains the event data. </param><exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellMouseEventArgs.ColumnIndex"/> property of <paramref name="e"/> is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellMouseEventArgs.RowIndex"/> property of <paramref name="e"/> is greater than the number of rows in the control minus one.</exception><exception cref="T:System.Exception">This action would commit a cell value or enter edit mode, but an error in the data source prevents the action and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError"/> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException"/> property to true. </exception>
        protected virtual void OnCellMouseDown(DataGridViewCellMouseEventArgs e)
        {
            Contract.Requires(e != null);
            Contract.Requires(e.ColumnIndex < Columns.Count);
            Contract.Requires(e.RowIndex < Rows.Count);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.CellMouseEnter"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> that contains the event data. </param><exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.ColumnIndex"/> property of <paramref name="e"/> is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.RowIndex"/> property of <paramref name="e"/> is greater than the number of rows in the control minus one.</exception>
        protected virtual void OnCellMouseEnter(DataGridViewCellEventArgs e)
        {
            Contract.Requires(e != null);
            Contract.Requires(e.ColumnIndex < Columns.Count);
            Contract.Requires(e.RowIndex < Rows.Count);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.CellMouseLeave"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> that contains the event data. </param><exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.ColumnIndex"/> property of <paramref name="e"/> is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.RowIndex"/> property of <paramref name="e"/> is greater than the number of rows in the control minus one.</exception>
        protected virtual void OnCellMouseLeave(DataGridViewCellEventArgs e)
        {
            Contract.Requires(e != null);
            Contract.Requires(e.ColumnIndex < Columns.Count);
            Contract.Requires(e.RowIndex < Rows.Count);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.CellMouseMove"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> that contains the event data. </param><exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellMouseEventArgs.ColumnIndex"/> property of <paramref name="e"/> is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellMouseEventArgs.RowIndex"/> property of <paramref name="e"/> is greater than the number of rows in the control minus one.</exception>
        protected virtual void OnCellMouseMove(DataGridViewCellMouseEventArgs e)
        {
            Contract.Requires(e != null);
            Contract.Requires(e.ColumnIndex < Columns.Count);
            Contract.Requires(e.RowIndex < Rows.Count);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.CellMouseUp"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> that contains the event data. </param><exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellMouseEventArgs.ColumnIndex"/> property of <paramref name="e"/> is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellMouseEventArgs.RowIndex"/> property of <paramref name="e"/> is greater than the number of rows in the control minus one.</exception>
        protected virtual void OnCellMouseUp(DataGridViewCellMouseEventArgs e)
        {
            Contract.Requires(e != null);
            Contract.Requires(e.ColumnIndex < Columns.Count);
            Contract.Requires(e.RowIndex < Rows.Count);
        }


        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.CellPainting"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellPaintingEventArgs"/> that contains the event data. </param><exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellPaintingEventArgs.ColumnIndex"/> property of <paramref name="e"/> is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellPaintingEventArgs.RowIndex"/> property of <paramref name="e"/> is greater than the number of rows in the control minus one.</exception>
        protected internal virtual void OnCellPainting(DataGridViewCellPaintingEventArgs e)
        {
            Contract.Requires(e != null);
            Contract.Requires(e.ColumnIndex < Columns.Count);
            Contract.Requires(e.RowIndex < Rows.Count);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.CellParsing"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellParsingEventArgs"/> that contains the event data. </param><exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellParsingEventArgs.ColumnIndex"/> property of <paramref name="e"/> is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellParsingEventArgs.RowIndex"/> property of <paramref name="e"/> is greater than the number of rows in the control minus one.</exception>
        protected virtual void OnCellParsing(DataGridViewCellParsingEventArgs e)
        {
            Contract.Requires(e != null);
            Contract.Requires(e.ColumnIndex < Columns.Count);
            Contract.Requires(e.RowIndex < Rows.Count);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.CellStateChanged"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellStateChangedEventArgs"/> that contains the event data. </param>
        protected virtual void OnCellStateChanged(DataGridViewCellStateChangedEventArgs e)
        {
            Contract.Requires(e != null);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.CellStyleChanged"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> that contains the event data. </param><exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.ColumnIndex"/> property of <paramref name="e"/> is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.RowIndex"/> property of <paramref name="e"/> is greater than the number of rows in the control minus one.</exception>
        protected virtual void OnCellStyleChanged(DataGridViewCellEventArgs e)
        {
            Contract.Requires(e != null);
            Contract.Requires(e.ColumnIndex < Columns.Count);
            Contract.Requires(e.RowIndex < Rows.Count);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.CellStyleContentChanged"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellStyleContentChangedEventArgs"/> that contains the event data. </param>
        protected virtual void OnCellStyleContentChanged(DataGridViewCellStyleContentChangedEventArgs e)
        {
            Contract.Requires(e != null);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.CellToolTipTextChanged"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> that contains information about the cell.</param><exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.ColumnIndex"/> property of <paramref name="e"/> is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.RowIndex"/> property of <paramref name="e"/> is greater than the number of rows in the control minus one.</exception>
        protected virtual void OnCellToolTipTextChanged(DataGridViewCellEventArgs e)
        {
            Contract.Requires(e != null);
            Contract.Requires(e.ColumnIndex < Columns.Count);
            Contract.Requires(e.RowIndex < Rows.Count);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.CellToolTipTextNeeded"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellToolTipTextNeededEventArgs"/> that contains the event data. </param><exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.ColumnIndex"/> property of <paramref name="e"/> is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.RowIndex"/> property of <paramref name="e"/> is greater than the number of rows in the control minus one.</exception>
        protected virtual void OnCellToolTipTextNeeded(DataGridViewCellToolTipTextNeededEventArgs e)
        {
            Contract.Requires(e != null);
            Contract.Requires(e.ColumnIndex < Columns.Count);
            Contract.Requires(e.RowIndex < Rows.Count);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.CellValidated"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> that contains the event data. </param><exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.ColumnIndex"/> property of <paramref name="e"/> is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.RowIndex"/> property of <paramref name="e"/> is greater than the number of rows in the control minus one.</exception>
        protected virtual void OnCellValidated(DataGridViewCellEventArgs e)
        {
            Contract.Requires(e != null);
            Contract.Requires(e.ColumnIndex < Columns.Count);
            Contract.Requires(e.RowIndex < Rows.Count);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.CellValidating"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellValidatingEventArgs"/> that contains the event data. </param><exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellValidatingEventArgs.ColumnIndex"/> property of <paramref name="e"/> is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellValidatingEventArgs.RowIndex"/> property of <paramref name="e"/> is greater than the number of rows in the control minus one.</exception>
        protected virtual void OnCellValidating(DataGridViewCellValidatingEventArgs e)
        {
            Contract.Requires(e != null);
            Contract.Requires(e.ColumnIndex < Columns.Count);
            Contract.Requires(e.RowIndex < Rows.Count);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.CellValueChanged"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> that contains the event data. </param><exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.ColumnIndex"/> property of <paramref name="e"/> is greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellEventArgs.RowIndex"/> property of <paramref name="e"/> is greater than the number of rows in the control minus one.</exception>
        protected virtual void OnCellValueChanged(DataGridViewCellEventArgs e)
        {
            Contract.Requires(e != null);
            Contract.Requires(e.ColumnIndex < Columns.Count);
            Contract.Requires(e.RowIndex < Rows.Count);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.CellValueNeeded"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellValueEventArgs"/> that contains the event data. </param><exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellValueEventArgs.ColumnIndex"/> property of <paramref name="e"/> is less than zero or greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellValueEventArgs.RowIndex"/> property of <paramref name="e"/> is less than zero or greater than the number of rows in the control minus one.</exception>
        protected virtual void OnCellValueNeeded(DataGridViewCellValueEventArgs e)
        {
            Contract.Requires(e != null);
            Contract.Requires(e.ColumnIndex >= 0 && e.ColumnIndex < Columns.Count);
            Contract.Requires(e.RowIndex >= 0 && e.RowIndex < Rows.Count);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.CellValuePushed"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellValueEventArgs"/> that contains the event data. </param><exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellValueEventArgs.ColumnIndex"/> property of <paramref name="e"/> is less than zero or greater than the number of columns in the control minus one.-or-The value of the <see cref="P:System.Windows.Forms.DataGridViewCellValueEventArgs.RowIndex"/> property of <paramref name="e"/> is less than zero or greater than the number of rows in the control minus one.</exception>
        protected virtual void OnCellValuePushed(DataGridViewCellValueEventArgs e)
        {
            Contract.Requires(e != null);
            Contract.Requires(e.ColumnIndex < Columns.Count);
            Contract.Requires(e.RowIndex < Rows.Count);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.ColumnAdded"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewColumnEventArgs"/> that contains the event data. </param><exception cref="T:System.ArgumentException">The column indicated by the <see cref="P:System.Windows.Forms.DataGridViewColumnEventArgs.Column"/> property of <paramref name="e"/> does not belong to this <see cref="T:System.Windows.Forms.DataGridView"/> control.</exception>
        protected virtual void OnColumnAdded(DataGridViewColumnEventArgs e)
        {
            Contract.Requires(e != null);
            Contract.Requires(e.Column != null && e.Column.DataGridView == this);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.ColumnContextMenuStripChanged"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewColumnEventArgs"/> that contains the event data. </param><exception cref="T:System.ArgumentException">The column indicated by the <see cref="P:System.Windows.Forms.DataGridViewColumnEventArgs.Column"/> property of <paramref name="e"/> does not belong to this <see cref="T:System.Windows.Forms.DataGridView"/> control.</exception>
        protected virtual void OnColumnContextMenuStripChanged(DataGridViewColumnEventArgs e)
        {
            Contract.Requires(e != null);
            Contract.Requires(e.Column != null && e.Column.DataGridView != this);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.ColumnDataPropertyNameChanged"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewColumnEventArgs"/> that contains the event data. </param><exception cref="T:System.ArgumentException">The column indicated by the <see cref="P:System.Windows.Forms.DataGridViewColumnEventArgs.Column"/> property of <paramref name="e"/> does not belong to this <see cref="T:System.Windows.Forms.DataGridView"/> control.</exception>
        protected virtual void OnColumnDataPropertyNameChanged(DataGridViewColumnEventArgs e)
        {
            Contract.Requires(e != null);
            Contract.Requires(e.Column != null && e.Column.DataGridView != this);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.ColumnDefaultCellStyleChanged"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewColumnEventArgs"/> that contains the event data. </param><exception cref="T:System.ArgumentException">The column indicated by the <see cref="P:System.Windows.Forms.DataGridViewColumnEventArgs.Column"/> property of <paramref name="e"/> does not belong to this <see cref="T:System.Windows.Forms.DataGridView"/> control.</exception>
        protected virtual void OnColumnDefaultCellStyleChanged(DataGridViewColumnEventArgs e)
        {
            Contract.Requires(e != null);
            Contract.Requires(e.Column != null && e.Column.DataGridView != this);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.ColumnDisplayIndexChanged"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewColumnEventArgs"/> that contains the event data. </param><exception cref="T:System.ArgumentException">The column indicated by the <see cref="P:System.Windows.Forms.DataGridViewColumnEventArgs.Column"/> property of <paramref name="e"/> does not belong to this <see cref="T:System.Windows.Forms.DataGridView"/> control.</exception>
        protected virtual void OnColumnDisplayIndexChanged(DataGridViewColumnEventArgs e)
        {
            Contract.Requires(e != null);
            Contract.Requires(e.Column != null && e.Column.DataGridView != this);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.ColumnDividerDoubleClick"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewColumnDividerDoubleClickEventArgs"/> that contains the event data. </param>
        protected virtual void OnColumnDividerDoubleClick(DataGridViewColumnDividerDoubleClickEventArgs e)
        {
            Contract.Requires(e != null);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.ColumnDividerWidthChanged"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewColumnEventArgs"/> that contains the event data. </param><exception cref="T:System.ArgumentException">The column indicated by the <see cref="P:System.Windows.Forms.DataGridViewColumnEventArgs.Column"/> property of <paramref name="e"/> does not belong to this <see cref="T:System.Windows.Forms.DataGridView"/> control.</exception>
        protected virtual void OnColumnDividerWidthChanged(DataGridViewColumnEventArgs e)
        {
            Contract.Requires(e != null);
            Contract.Requires(e.Column != null && e.Column.DataGridView != this);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.ColumnHeaderCellChanged"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewColumnEventArgs"/> that contains the event data. </param><exception cref="T:System.ArgumentException">The column indicated by the <see cref="P:System.Windows.Forms.DataGridViewColumnEventArgs.Column"/> property of <paramref name="e"/> does not belong to this <see cref="T:System.Windows.Forms.DataGridView"/> control.</exception>
        protected virtual void OnColumnHeaderCellChanged(DataGridViewColumnEventArgs e)
        {
            Contract.Requires(e != null);
            Contract.Requires(e.Column != null && e.Column.DataGridView != this);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.ColumnHeaderMouseClick"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> that contains the event data. </param><exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Windows.Forms.DataGridViewCellMouseEventArgs.ColumnIndex"/> property of <paramref name="e"/> is less than zero or greater than the number of columns in the control minus one.</exception>
        protected virtual void OnColumnHeaderMouseClick(DataGridViewCellMouseEventArgs e)
        {
            Contract.Requires(e != null);
        }

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.ColumnHeaderMouseDoubleClick"/> event.
        // </summary>
        // <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> that contains information about the cell and the position of the mouse pointer.</param>
        // protected virtual void OnColumnHeaderMouseDoubleClick(DataGridViewCellMouseEventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.ColumnHeadersBorderStyleChanged"/> event.
        // </summary>
        // <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        // protected virtual void OnColumnHeadersBorderStyleChanged(EventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.ColumnHeadersDefaultCellStyleChanged"/> event.
        // </summary>
        // <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        // protected virtual void OnColumnHeadersDefaultCellStyleChanged(EventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.ColumnHeadersHeightChanged"/> event.
        // </summary>
        // <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        // protected virtual void OnColumnHeadersHeightChanged(EventArgs e)

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.ColumnHeadersHeightSizeModeChanged"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewAutoSizeModeEventArgs"/> that contains the event data. </param>
        protected virtual void OnColumnHeadersHeightSizeModeChanged(DataGridViewAutoSizeModeEventArgs e)
        {
            Contract.Requires(e != null);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.ColumnMinimumWidthChanged"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewColumnEventArgs"/> that contains the event data. </param><exception cref="T:System.ArgumentException">The column indicated by the <see cref="P:System.Windows.Forms.DataGridViewColumnEventArgs.Column"/> property of <paramref name="e"/> does not belong to this <see cref="T:System.Windows.Forms.DataGridView"/> control.</exception>
        protected virtual void OnColumnMinimumWidthChanged(DataGridViewColumnEventArgs e)
        {
            Contract.Requires(e != null);
            Contract.Requires(e.Column != null && e.Column.DataGridView == this);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.ColumnNameChanged"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewColumnEventArgs"/> that contains the event data. </param><exception cref="T:System.ArgumentException">The column indicated by the <see cref="P:System.Windows.Forms.DataGridViewColumnEventArgs.Column"/> property of <paramref name="e"/> does not belong to this <see cref="T:System.Windows.Forms.DataGridView"/> control.</exception>
        protected virtual void OnColumnNameChanged(DataGridViewColumnEventArgs e)
        {
            Contract.Requires(e != null);
            Contract.Requires(e.Column != null && e.Column.DataGridView == this);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.ColumnRemoved"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewColumnEventArgs"/> that contains the event data. </param>
        // protected virtual void OnColumnRemoved(DataGridViewColumnEventArgs e)

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.ColumnSortModeChanged"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewColumnEventArgs"/> that contains the event data. </param><exception cref="T:System.ArgumentException">The column indicated by the <see cref="P:System.Windows.Forms.DataGridViewColumnEventArgs.Column"/> property of <paramref name="e"/> does not belong to this <see cref="T:System.Windows.Forms.DataGridView"/> control.</exception>
        protected virtual void OnColumnSortModeChanged(DataGridViewColumnEventArgs e)
        {
            Contract.Requires(e != null);
            Contract.Requires(e.Column != null && e.Column.DataGridView == this);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.ColumnStateChanged"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewColumnStateChangedEventArgs"/> that contains the event data. </param><exception cref="T:System.InvalidCastException">The column changed from read-only to read/write, enabling the current cell to enter edit mode, but the <see cref="P:System.Windows.Forms.DataGridViewCell.EditType"/> property of the current cell does not indicate a class that derives from <see cref="T:System.Windows.Forms.Control"/> and implements <see cref="T:System.Windows.Forms.IDataGridViewEditingControl"/>.</exception>
        protected virtual void OnColumnStateChanged(DataGridViewColumnStateChangedEventArgs e)
        {
            Contract.Requires(e != null);
            Contract.Requires(e.Column != null && e.Column.DataGridView == this);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.ColumnToolTipTextChanged"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewColumnEventArgs"/> that contains information about the column.</param><exception cref="T:System.ArgumentException">The column indicated by the <see cref="P:System.Windows.Forms.DataGridViewColumnEventArgs.Column"/> property of <paramref name="e"/> does not belong to this <see cref="T:System.Windows.Forms.DataGridView"/> control.</exception>
        protected virtual void OnColumnToolTipTextChanged(DataGridViewColumnEventArgs e)
        {
            Contract.Requires(e != null);
            Contract.Requires(e.Column != null && e.Column.DataGridView != this);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.ColumnWidthChanged"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewColumnEventArgs"/> that contains the event data. </param><exception cref="T:System.ArgumentException">The column indicated by the <see cref="P:System.Windows.Forms.DataGridViewColumnEventArgs.Column"/> property of <paramref name="e"/> does not belong to this <see cref="T:System.Windows.Forms.DataGridView"/> control.</exception>
        protected virtual void OnColumnWidthChanged(DataGridViewColumnEventArgs e)
        {
            Contract.Requires(e != null);
            Contract.Requires(e.Column != null && e.Column.DataGridView != this);
        }

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.CurrentCellChanged"/> event.
        // </summary>
        // <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        // protected virtual void OnCurrentCellChanged(EventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.CurrentCellDirtyStateChanged"/> event.
        // </summary>
        // <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        // protected virtual void OnCurrentCellDirtyStateChanged(EventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.Control.CursorChanged"/> event and updates the <see cref="P:System.Windows.Forms.DataGridView.UserSetCursor"/> property if the cursor was changed in user code.
        // </summary>
        // <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        // protected override void OnCursorChanged(EventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.DataBindingComplete"/> event.
        // </summary>
        // <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewBindingCompleteEventArgs"/> that contains the event data.</param>
        // protected virtual void OnDataBindingComplete(DataGridViewBindingCompleteEventArgs e)

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.DataError"/> event.
        /// </summary>
        /// <param name="displayErrorDialogIfNoHandler">true to display an error dialog box if there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError"/> event.</param><param name="e">A <see cref="T:System.Windows.Forms.DataGridViewDataErrorEventArgs"/> that contains the event data. </param>
        protected virtual void OnDataError(bool displayErrorDialogIfNoHandler, DataGridViewDataErrorEventArgs e)
        {
            Contract.Requires(e != null);
        }

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.DataMemberChanged"/> event.
        // </summary>
        // <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        // protected virtual void OnDataMemberChanged(EventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.DataSourceChanged"/> event.
        // </summary>
        // <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        // protected virtual void OnDataSourceChanged(EventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.DefaultCellStyleChanged"/> event.
        // </summary>
        // <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        // protected virtual void OnDefaultCellStyleChanged(EventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.DefaultValuesNeeded"/> event.
        // </summary>
        // <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewRowEventArgs"/> that contains the event data. </param>
        // protected virtual void OnDefaultValuesNeeded(DataGridViewRowEventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.Control.DoubleClick"/> event.
        // </summary>
        // <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        // protected override void OnDoubleClick(EventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.EditingControlShowing"/> event.
        // </summary>
        // <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewEditingControlShowingEventArgs"/> that contains information about the editing control.</param>
        // protected virtual void OnEditingControlShowing(DataGridViewEditingControlShowingEventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.EditModeChanged"/> event.
        // </summary>
        // <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param><exception cref="T:System.InvalidCastException">When entering edit mode, the <see cref="P:System.Windows.Forms.DataGridViewCell.EditType"/> property of the current cell does not indicate a class that derives from <see cref="T:System.Windows.Forms.Control"/> and implements <see cref="T:System.Windows.Forms.IDataGridViewEditingControl"/>.</exception>
        // protected virtual void OnEditModeChanged(EventArgs e)

        // <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        // protected override void OnEnabledChanged(EventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.Control.Enter"/> event.
        // </summary>
        // <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param><exception cref="T:System.InvalidCastException">The control is configured to enter edit mode when it receives focus, but upon entering focus, the <see cref="P:System.Windows.Forms.DataGridViewCell.EditType"/> property of the current cell does not indicate a class that derives from <see cref="T:System.Windows.Forms.Control"/> and implements <see cref="T:System.Windows.Forms.IDataGridViewEditingControl"/>.</exception><exception cref="T:System.Exception">The control is configured to enter edit mode when it receives focus, but initialization of the editing cell value failed and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError"/> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException"/> property to true. The exception object can typically be cast to type <see cref="T:System.FormatException"/>.</exception>
        // protected override void OnEnter(EventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.FontChanged"/> event.
        // </summary>
        // <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        // protected override void OnFontChanged(EventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.ForeColorChanged"/> event.
        // </summary>
        // <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        // protected override void OnForeColorChanged(EventArgs e)

        // <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        // protected override void OnGotFocus(EventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.GridColorChanged"/> event.
        // </summary>
        // <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        // protected virtual void OnGridColorChanged(EventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated"/> event.
        // </summary>
        // <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        // protected override void OnHandleCreated(EventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.Control.HandleDestroyed"/> event.
        // </summary>
        // <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        // protected override void OnHandleDestroyed(EventArgs e)

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.KeyDown"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs"/> that contains the event data. </param><exception cref="T:System.Exception">This action would cause the control to enter edit mode but initialization of the editing cell value failed and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError"/> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException"/> property to true. The exception object can typically be cast to type <see cref="T:System.FormatException"/>.</exception>
        protected void OnKeyDown(KeyEventArgs e)
        {
            Contract.Requires(e != null);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.KeyPress"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.KeyPressEventArgs"/> that contains the event data. </param>
        protected void OnKeyPress(KeyPressEventArgs e)
        {
            Contract.Requires(e != null);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.KeyUp"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs"/> that contains the event data. </param>
        protected void OnKeyUp(KeyEventArgs e)
        {
            Contract.Requires(e != null);
        }

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.Control.Layout"/> event.
        // </summary>
        // <param name="e">A <see cref="T:System.Windows.Forms.LayoutEventArgs"/> that contains the event data. </param>
        // protected override void OnLayout(LayoutEventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.Control.Leave"/> event.
        // </summary>
        // <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        // protected override void OnLeave(EventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.Control.LostFocus"/> event.
        // </summary>
        // <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        // protected override void OnLostFocus(EventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.Control.MouseClick"/> event.
        // </summary>
        // <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs"/> that contains the event data. </param><exception cref="T:System.Exception">The control is configured to enter edit mode when it receives focus, but initialization of the editing cell value failed and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError"/> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException"/> property to true. The exception object can typically be cast to type <see cref="T:System.FormatException"/>.</exception>
        // protected override void OnMouseClick(MouseEventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.Control.MouseDoubleClick"/> event.
        // </summary>
        // <param name="e">An <see cref="T:System.Windows.Forms.MouseEventArgs"/> that contains the event data. </param>
        // protected override void OnMouseDoubleClick(MouseEventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.Control.MouseDown"/> event.
        // </summary>
        // <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs"/> that contains the event data. </param><exception cref="T:System.Exception">The control is configured to enter edit mode when it receives focus, but initialization of the editing cell value failed and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError"/> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException"/> property to true. The exception object can typically be cast to type <see cref="T:System.FormatException"/>.</exception>
        // protected override void OnMouseDown(MouseEventArgs e)

        // <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        // protected override void OnMouseEnter(EventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave"/> event.
        // </summary>
        // <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        // protected override void OnMouseLeave(EventArgs e)

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseMove"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs"/> that contains the event data. </param>
        protected void OnMouseMove(MouseEventArgs e)
        {
            Contract.Requires(e != null);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseUp"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs"/> that contains the event data. </param>
        protected void OnMouseUp(MouseEventArgs e)
        {
            Contract.Requires(e != null);
        }

        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs"/> that contains the event data. </param>
        protected void OnMouseWheel(MouseEventArgs e)
        {
            Contract.Requires(e != null);
        }

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.MultiSelectChanged"/> event.
        // </summary>
        // <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        // protected virtual void OnMultiSelectChanged(EventArgs e)

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.NewRowNeeded"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewRowEventArgs"/> that contains the event data. </param><exception cref="T:System.ArgumentException">The row indicated by the <see cref="P:System.Windows.Forms.DataGridViewRowEventArgs.Row"/> property of <paramref name="e"/> does not belong to this <see cref="T:System.Windows.Forms.DataGridView"/> control.</exception>
        protected virtual void OnNewRowNeeded(DataGridViewRowEventArgs e)
        {
            Contract.Requires(e != null);
            Contract.Requires(e.Row != null && e.Row.DataGridView == this);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs"/> that contains the event data. </param><exception cref="T:System.Exception">Any exceptions that occur during this method are ignored unless they are one of the following:<see cref="T:System.NullReferenceException"/><see cref="T:System.StackOverflowException"/><see cref="T:System.OutOfMemoryException"/><see cref="T:System.Threading.ThreadAbortException"/><see cref="T:System.ExecutionEngineException"/><see cref="T:System.IndexOutOfRangeException"/><see cref="T:System.AccessViolationException"/></exception>
        protected override void OnPaint(PaintEventArgs e)
        {
            Contract.Requires(e != null && e.Graphics != null);
        }

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.ReadOnlyChanged"/> event.
        // </summary>
        // <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param><exception cref="T:System.InvalidCastException">The control changed from read-only to read/write, enabling the current cell to enter edit mode, but the <see cref="P:System.Windows.Forms.DataGridViewCell.EditType"/> property of the current cell does not indicate a class that derives from <see cref="T:System.Windows.Forms.Control"/> and implements <see cref="T:System.Windows.Forms.IDataGridViewEditingControl"/>.</exception>
        // protected virtual void OnReadOnlyChanged(EventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.Control.Resize"/> event.
        // </summary>
        // <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        // protected override void OnResize(EventArgs e)

        // <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        // protected override void OnRightToLeftChanged(EventArgs e)

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.RowContextMenuStripChanged"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewRowEventArgs"/> that contains the event data. </param><exception cref="T:System.ArgumentException">The row indicated by the <see cref="P:System.Windows.Forms.DataGridViewRowEventArgs.Row"/> property of <paramref name="e"/> does not belong to this <see cref="T:System.Windows.Forms.DataGridView"/> control.</exception>
        protected virtual void OnRowContextMenuStripChanged(DataGridViewRowEventArgs e)
        {
            Contract.Requires(e != null);
            Contract.Requires(e.Row != null && e.Row.DataGridView == this);
        }

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.RowContextMenuStripNeeded"/> event.
        // </summary>
        // <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewRowContextMenuStripNeededEventArgs"/> that contains the event data. </param>
        // protected virtual void OnRowContextMenuStripNeeded(DataGridViewRowContextMenuStripNeededEventArgs e)

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.RowDefaultCellStyleChanged"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewRowEventArgs"/> that contains the event data. </param><exception cref="T:System.ArgumentException">The row indicated by the <see cref="P:System.Windows.Forms.DataGridViewRowEventArgs.Row"/> property of <paramref name="e"/> does not belong to this <see cref="T:System.Windows.Forms.DataGridView"/> control.</exception>
        protected virtual void OnRowDefaultCellStyleChanged(DataGridViewRowEventArgs e)
        {
            Contract.Requires(e != null);
            Contract.Requires(e.Row != null && e.Row.DataGridView == this);
        }

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.RowDirtyStateNeeded"/> event.
        // </summary>
        // <param name="e">A <see cref="T:System.Windows.Forms.QuestionEventArgs"/> that contains the event data. </param>
        // protected virtual void OnRowDirtyStateNeeded(QuestionEventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.RowDividerDoubleClick"/> event.
        // </summary>
        // <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewRowDividerDoubleClickEventArgs"/> that contains the event data. </param>
        // protected virtual void OnRowDividerDoubleClick(DataGridViewRowDividerDoubleClickEventArgs e)

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.RowDividerHeightChanged"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewRowEventArgs"/> that contains the event data. </param><exception cref="T:System.ArgumentException">The row indicated by the <see cref="P:System.Windows.Forms.DataGridViewRowEventArgs.Row"/> property of <paramref name="e"/> does not belong to this <see cref="T:System.Windows.Forms.DataGridView"/> control.</exception>
        protected virtual void OnRowDividerHeightChanged(DataGridViewRowEventArgs e)
        {
            Contract.Requires(e != null);
            Contract.Requires(e.Row != null && e.Row.DataGridView == this);
        }

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.RowEnter"/> event.
        // </summary>
        // <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> that contains the event data. </param>
        // protected virtual void OnRowEnter(DataGridViewCellEventArgs e)

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.RowErrorTextChanged"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewRowEventArgs"/> that contains the event data. </param><exception cref="T:System.ArgumentException">The row indicated by the <see cref="P:System.Windows.Forms.DataGridViewRowEventArgs.Row"/> property of <paramref name="e"/> does not belong to this <see cref="T:System.Windows.Forms.DataGridView"/> control.</exception>
        protected virtual void OnRowErrorTextChanged(DataGridViewRowEventArgs e)
        {
            Contract.Requires(e != null);
            Contract.Requires(e.Row != null && e.Row.DataGridView == this);
        }

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.RowErrorTextNeeded"/> event.
        // </summary>
        // <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewRowErrorTextNeededEventArgs"/> that contains the event data. </param>
        // protected virtual void OnRowErrorTextNeeded(DataGridViewRowErrorTextNeededEventArgs e)

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.RowHeaderCellChanged"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewRowEventArgs"/> that contains the event data. </param><exception cref="T:System.ArgumentException">The row indicated by the <see cref="P:System.Windows.Forms.DataGridViewRowEventArgs.Row"/> property of <paramref name="e"/> does not belong to this <see cref="T:System.Windows.Forms.DataGridView"/> control.</exception>
        protected virtual void OnRowHeaderCellChanged(DataGridViewRowEventArgs e)
        {
            Contract.Requires(e != null);
            Contract.Requires(e.Row != null && e.Row.DataGridView == this);
        }

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.RowHeaderMouseClick"/> event.
        // </summary>
        // <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> that contains information about the mouse and the header cell that was clicked.</param>
        // protected virtual void OnRowHeaderMouseClick(DataGridViewCellMouseEventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.RowHeaderMouseDoubleClick"/> event.
        // </summary>
        // <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellMouseEventArgs"/> that contains information about the mouse and the header cell that was double-clicked.</param>
        // protected virtual void OnRowHeaderMouseDoubleClick(DataGridViewCellMouseEventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.RowHeadersBorderStyleChanged"/> event.
        // </summary>
        // <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        // protected virtual void OnRowHeadersBorderStyleChanged(EventArgs e)

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.RowHeadersDefaultCellStyleChanged"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        // protected virtual void OnRowHeadersDefaultCellStyleChanged(EventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.RowHeadersWidthChanged"/> event.
        // </summary>
        // <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        // protected virtual void OnRowHeadersWidthChanged(EventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.RowHeadersWidthSizeModeChanged"/> event.
        // </summary>
        // <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewAutoSizeModeEventArgs"/> that contains the event data. </param>
        // protected virtual void OnRowHeadersWidthSizeModeChanged(DataGridViewAutoSizeModeEventArgs e)

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.RowHeightChanged"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewRowEventArgs"/> that contains the event data. </param><exception cref="T:System.ArgumentException">The row indicated by the <see cref="P:System.Windows.Forms.DataGridViewRowEventArgs.Row"/> property of <paramref name="e"/> does not belong to this <see cref="T:System.Windows.Forms.DataGridView"/> control.</exception>
        protected virtual void OnRowHeightChanged(DataGridViewRowEventArgs e)
        {
            Contract.Requires(e != null);
            Contract.Requires(e.Row != null && e.Row.DataGridView == this);
        }

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.RowHeightInfoNeeded"/> event.
        // </summary>
        // <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewRowHeightInfoNeededEventArgs"/> that contains the event data. </param>
        // protected virtual void OnRowHeightInfoNeeded(DataGridViewRowHeightInfoNeededEventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.RowHeightInfoPushed"/> event.
        // </summary>
        // <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewRowHeightInfoPushedEventArgs"/> that contains the event data. </param>
        // protected virtual void OnRowHeightInfoPushed(DataGridViewRowHeightInfoPushedEventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.RowLeave"/> event.
        // </summary>
        // <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> that contains the event data. </param>
        // protected virtual void OnRowLeave(DataGridViewCellEventArgs e)

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.RowMinimumHeightChanged"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewRowEventArgs"/> that contains the event data. </param><exception cref="T:System.ArgumentException">The row indicated by the <see cref="P:System.Windows.Forms.DataGridViewRowEventArgs.Row"/> property of <paramref name="e"/> does not belong to this <see cref="T:System.Windows.Forms.DataGridView"/> control.</exception>
        protected virtual void OnRowMinimumHeightChanged(DataGridViewRowEventArgs e)
        {
            Contract.Requires(e != null);
            Contract.Requires(e.Row != null && e.Row.DataGridView == this);
        }

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.RowPostPaint"/> event.
        // </summary>
        // <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewRowPostPaintEventArgs"/> that contains the event data. </param>
        // protected internal virtual void OnRowPostPaint(DataGridViewRowPostPaintEventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.RowPrePaint"/> event.
        // </summary>
        // <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewRowPrePaintEventArgs"/> that contains the event data. </param>
        // protected internal virtual void OnRowPrePaint(DataGridViewRowPrePaintEventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.RowsAdded"/> event.
        // </summary>
        // <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewRowsAddedEventArgs"/> that contains information about the added rows. </param>
        // protected virtual void OnRowsAdded(DataGridViewRowsAddedEventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.RowsDefaultCellStyleChanged"/> event.
        // </summary>
        // <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        // protected virtual void OnRowsDefaultCellStyleChanged(EventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.RowsRemoved"/> event.
        // </summary>
        // <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewRowsRemovedEventArgs"/> that contains information about the deleted rows. </param>
        // protected virtual void OnRowsRemoved(DataGridViewRowsRemovedEventArgs e)

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.RowStateChanged"/> event.
        /// </summary>
        /// <param name="rowIndex">The index of the row that is changing state.</param><param name="e">A <see cref="T:System.Windows.Forms.DataGridViewRowStateChangedEventArgs"/> that contains the event data. </param><exception cref="T:System.InvalidCastException">The row changed from read-only to read/write, enabling the current cell to enter edit mode, but the <see cref="P:System.Windows.Forms.DataGridViewCell.EditType"/> property of the current cell does not indicate a class that derives from <see cref="T:System.Windows.Forms.Control"/> and implements <see cref="T:System.Windows.Forms.IDataGridViewEditingControl"/>.</exception>
        protected virtual void OnRowStateChanged(int rowIndex, DataGridViewRowStateChangedEventArgs e)
        {
            Contract.Requires(e != null);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.RowUnshared"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewRowEventArgs"/> that contains the event data. </param><exception cref="T:System.ArgumentException">The row indicated by the <see cref="P:System.Windows.Forms.DataGridViewRowEventArgs.Row"/> property of <paramref name="e"/> does not belong to this <see cref="T:System.Windows.Forms.DataGridView"/> control.</exception>
        protected virtual void OnRowUnshared(DataGridViewRowEventArgs e)
        {
            Contract.Requires(e != null);
            Contract.Requires(e.Row != null && e.Row.DataGridView == this);
        }

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.RowValidating"/> event.
        // </summary>
        // <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellCancelEventArgs"/> that contains the event data. </param>
        // protected virtual void OnRowValidating(DataGridViewCellCancelEventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.RowValidated"/> event.
        // </summary>
        // <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs"/> that contains the event data. </param>
        // protected virtual void OnRowValidated(DataGridViewCellEventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.Scroll"/> event.
        // </summary>
        // <param name="e">A <see cref="T:System.Windows.Forms.ScrollEventArgs"/> that contains the event data. </param>
        // protected virtual void OnScroll(ScrollEventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.SelectionChanged"/> event.
        // </summary>
        // <param name="e">An <see cref="T:System.EventArgs"/> that contains information about the event.</param>
        // protected virtual void OnSelectionChanged(EventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.SortCompare"/> event.
        // </summary>
        // <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewSortCompareEventArgs"/> that contains the event data. </param>
        // protected virtual void OnSortCompare(DataGridViewSortCompareEventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.Sorted"/> event.
        // </summary>
        // <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
        // protected virtual void OnSorted(EventArgs e)

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.DataGridView.UserAddedRow"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewRowEventArgs"/> that contains the event data. </param><exception cref="T:System.ArgumentException">The row indicated by the <see cref="P:System.Windows.Forms.DataGridViewRowEventArgs.Row"/> property of <paramref name="e"/> does not belong to this <see cref="T:System.Windows.Forms.DataGridView"/> control.</exception>
        protected virtual void OnUserAddedRow(DataGridViewRowEventArgs e)
        {
            Contract.Requires(e != null);
            Contract.Requires(e.Row != null && e.Row.DataGridView == this);
        }

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.UserDeletedRow"/> event.
        // </summary>
        // <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewRowEventArgs"/> that contains the event data. </param>
        // protected virtual void OnUserDeletedRow(DataGridViewRowEventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.DataGridView.UserDeletingRow"/> event.
        // </summary>
        // <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewRowCancelEventArgs"/> that contains the event data. </param>
        // protected virtual void OnUserDeletingRow(DataGridViewRowCancelEventArgs e)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.Control.Validating"/> event.
        // </summary>
        // <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs"/> that contains the event data. </param><exception cref="T:System.Exception">Validation failed and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError"/> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException"/> property to true. The exception object can typically be cast to type <see cref="T:System.FormatException"/>.</exception>
        // protected override void OnValidating(CancelEventArgs e)

        /// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        // protected override void OnVisibleChanged(EventArgs e)

        // <summary>
        // Paints the background of the <see cref="T:System.Windows.Forms.DataGridView"/>.
        // </summary>
        // <param name="graphics">The <see cref="T:System.Drawing.Graphics"/> used to paint the background.</param><param name="clipBounds">A <see cref="T:System.Drawing.Rectangle"/> that represents the area of the <see cref="T:System.Windows.Forms.DataGridView"/> that needs to be painted.</param><param name="gridBounds">A <see cref="T:System.Drawing.Rectangle"/> that represents the area in which cells are drawn.</param>
        // protected virtual void PaintBackground(Graphics graphics, Rectangle clipBounds, Rectangle gridBounds)

        // <summary>
        // Processes the A key.
        // </summary>
        // 
        // <returns>
        // true if the key was processed; otherwise, false.
        // </returns>
        // <param name="keyData">A bitwise combination of <see cref="T:System.Windows.Forms.Keys"/> values that represents the key or keys to process.</param>
        // [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        // protected bool ProcessAKey(Keys keyData)

        // <summary>
        // Processes the DELETE key.
        // </summary>
        // 
        // <returns>
        // true if the key was processed; otherwise, false.
        // </returns>
        // <param name="keyData">A bitwise combination of <see cref="T:System.Windows.Forms.Keys"/> values that represents the key or keys to process.</param><exception cref="T:System.Exception">The DELETE key would delete one or more rows, but an error in the data source prevents the deletion and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError"/> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException"/> property to true. </exception>
        // [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        // protected bool ProcessDeleteKey(Keys keyData)

        // <summary>
        // Processes keys, such as the TAB, ESCAPE, ENTER, and ARROW keys, used to control dialog boxes.
        // </summary>
        // 
        // <returns>
        // true if the key was processed; otherwise, false.
        // </returns>
        // <param name="keyData">A bitwise combination of <see cref="T:System.Windows.Forms.Keys"/> values that represents the key or keys to process.</param><exception cref="T:System.InvalidCastException">The key pressed would cause the control to enter edit mode, but the <see cref="P:System.Windows.Forms.DataGridViewCell.EditType"/> property of the current cell does not indicate a class that derives from <see cref="T:System.Windows.Forms.Control"/> and implements <see cref="T:System.Windows.Forms.IDataGridViewEditingControl"/>.</exception><exception cref="T:System.Exception">This action would commit a cell value or enter edit mode, but an error in the data source prevents the action and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError"/> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException"/> property to true. </exception>
        // [UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
        // protected override bool ProcessDialogKey(Keys keyData)

        // <summary>
        // Processes the DOWN ARROW key.
        // </summary>
        // 
        // <returns>
        // true if the key was processed; otherwise, false.
        // </returns>
        // <param name="keyData">A bitwise combination of <see cref="T:System.Windows.Forms.Keys"/> values that represents the key or keys to process.</param><exception cref="T:System.InvalidCastException">The DOWN ARROW key would cause the control to enter edit mode, but the <see cref="P:System.Windows.Forms.DataGridViewCell.EditType"/> property of the new current cell does not indicate a class that derives from <see cref="T:System.Windows.Forms.Control"/> and implements <see cref="T:System.Windows.Forms.IDataGridViewEditingControl"/>.</exception><exception cref="T:System.Exception">This action would commit a cell value or enter edit mode, but an error in the data source prevents the action and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError"/> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException"/> property to true. </exception>
        // [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        // protected bool ProcessDownKey(Keys keyData)

        // <summary>
        // Processes the END key.
        // </summary>
        // 
        // <returns>
        // true if the key was processed; otherwise, false.
        // </returns>
        // <param name="keyData">A bitwise combination of <see cref="T:System.Windows.Forms.Keys"/> values that represents the key or keys to process.</param><exception cref="T:System.InvalidCastException">The END key would cause the control to enter edit mode, but the <see cref="P:System.Windows.Forms.DataGridViewCell.EditType"/> property of the new current cell does not indicate a class that derives from <see cref="T:System.Windows.Forms.Control"/> and implements <see cref="T:System.Windows.Forms.IDataGridViewEditingControl"/>.</exception><exception cref="T:System.Exception">This action would commit a cell value or enter edit mode, but an error in the data source prevents the action and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError"/> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException"/> property to true. </exception>
        //[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        // protected bool ProcessEndKey(Keys keyData)

        // <summary>
        // Processes the ENTER key.
        // </summary>
        // 
        // <returns>
        // true if the key was processed; otherwise, false.
        // </returns>
        // <param name="keyData">A bitwise combination of <see cref="T:System.Windows.Forms.Keys"/> values that represents the key or keys to process.</param><exception cref="T:System.InvalidCastException">The ENTER key would cause the control to enter edit mode, but the <see cref="P:System.Windows.Forms.DataGridViewCell.EditType"/> property of the new current cell does not indicate a class that derives from <see cref="T:System.Windows.Forms.Control"/> and implements <see cref="T:System.Windows.Forms.IDataGridViewEditingControl"/>.</exception><exception cref="T:System.Exception">This action would commit a cell value or enter edit mode, but an error in the data source prevents the action and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError"/> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException"/> property to true. </exception>
        // [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        // protected bool ProcessEnterKey(Keys keyData)

        // <summary>
        // Processes the ESC key.
        // </summary>
        // 
        // <returns>
        // true if the key was processed; otherwise, false.
        // </returns>
        // <param name="keyData">A bitwise combination of <see cref="T:System.Windows.Forms.Keys"/> values that represents the key or keys to process.</param>
        // [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        // protected bool ProcessEscapeKey(Keys keyData)

        // <summary>
        // Processes the F2 key.
        // </summary>
        // 
        // <returns>
        // true if the key was processed; otherwise, false.
        // </returns>
        // <param name="keyData">A bitwise combination of <see cref="T:System.Windows.Forms.Keys"/> values that represents the key or keys to process.</param><exception cref="T:System.InvalidCastException">The F2 key would cause the control to enter edit mode, but the <see cref="P:System.Windows.Forms.DataGridViewCell.EditType"/> property of the current cell does not indicate a class that derives from <see cref="T:System.Windows.Forms.Control"/> and implements <see cref="T:System.Windows.Forms.IDataGridViewEditingControl"/>.</exception><exception cref="T:System.Exception">The F2 key would cause the control to enter edit mode, but an error in the data source prevents the action and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError"/> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException"/> property to true. </exception>
        // [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        // protected bool ProcessF2Key(Keys keyData)

        // <summary>
        // Processes the HOME key.
        // </summary>
        // 
        // <returns>
        // true if the key was processed; otherwise, false.
        // </returns>
        // <param name="keyData">The key that was pressed.</param><exception cref="T:System.InvalidCastException">The HOME key would cause the control to enter edit mode, but the <see cref="P:System.Windows.Forms.DataGridViewCell.EditType"/> property of the new current cell does not indicate a class that derives from <see cref="T:System.Windows.Forms.Control"/> and implements <see cref="T:System.Windows.Forms.IDataGridViewEditingControl"/>.</exception><exception cref="T:System.Exception">This action would commit a cell value or enter edit mode, but an error in the data source prevents the action and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError"/> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException"/> property to true. </exception>
        // [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        // protected bool ProcessHomeKey(Keys keyData)

        // <summary>
        // Processes the INSERT key.
        // </summary>
        // 
        // <returns>
        // true if the key was processed; otherwise, false.
        // </returns>
        // <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys"/> values that represents the key to process.</param>
        // [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        // protected bool ProcessInsertKey(Keys keyData)

        // <summary>
        // Processes a key message and generates the appropriate control events.
        // </summary>
        // 
        // <returns>
        // true if the message was processed; otherwise, false.
        // </returns>
        // <param name="m">A <see cref="T:System.Windows.Forms.Message"/>, passed by reference, that represents the window message to process.</param><exception cref="T:System.InvalidCastException">The key pressed would cause the control to enter edit mode, but the <see cref="P:System.Windows.Forms.DataGridViewCell.EditType"/> property of the current cell does not indicate a class that derives from <see cref="T:System.Windows.Forms.Control"/> and implements <see cref="T:System.Windows.Forms.IDataGridViewEditingControl"/>.</exception><exception cref="T:System.Exception">This action would commit a cell value or enter edit mode, but an error in the data source prevents the action and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError"/> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException"/> property to true. </exception>
        // [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        // protected override bool ProcessKeyEventArgs(ref Message m)

        // <summary>
        // Previews a keyboard message.
        // </summary>
        // 
        // <returns>
        // true if the message was processed; otherwise, false.
        // </returns>
        // <param name="m">A <see cref="T:System.Windows.Forms.Message"/>, passed by reference, that represents the window message to process.</param><exception cref="T:System.InvalidCastException">The key pressed would cause the control to enter edit mode, but the <see cref="P:System.Windows.Forms.DataGridViewCell.EditType"/> property of the current cell does not indicate a class that derives from <see cref="T:System.Windows.Forms.Control"/> and implements <see cref="T:System.Windows.Forms.IDataGridViewEditingControl"/>.</exception><exception cref="T:System.Exception">This action would commit a cell value or enter edit mode, but an error in the data source prevents the action and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError"/> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException"/> property to true. </exception>
        // [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        // protected override bool ProcessKeyPreview(ref Message m)

        // <summary>
        // Processes the LEFT ARROW key.
        // </summary>
        // 
        // <returns>
        // true if the key was processed; otherwise, false.
        // </returns>
        // <param name="keyData">A bitwise combination of <see cref="T:System.Windows.Forms.Keys"/> values that represents the key or keys to process.</param><exception cref="T:System.InvalidCastException">The LEFT ARROW key would cause the control to enter edit mode, but the <see cref="P:System.Windows.Forms.DataGridViewCell.EditType"/> property of the new current cell does not indicate a class that derives from <see cref="T:System.Windows.Forms.Control"/> and implements <see cref="T:System.Windows.Forms.IDataGridViewEditingControl"/>.</exception><exception cref="T:System.Exception">This action would commit a cell value or enter edit mode, but an error in the data source prevents the action and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError"/> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException"/> property to true. </exception>
        // [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        // protected bool ProcessLeftKey(Keys keyData)

        // <summary>
        // Processes the PAGE DOWN key.
        // </summary>
        // 
        // <returns>
        // true if the key was processed; otherwise, false.
        // </returns>
        // <param name="keyData">A bitwise combination of <see cref="T:System.Windows.Forms.Keys"/> values that represents the key or keys to process.</param><exception cref="T:System.InvalidCastException">The PAGE DOWN key would cause the control to enter edit mode, but the <see cref="P:System.Windows.Forms.DataGridViewCell.EditType"/> property of the new current cell does not indicate a class that derives from <see cref="T:System.Windows.Forms.Control"/> and implements <see cref="T:System.Windows.Forms.IDataGridViewEditingControl"/>.</exception><exception cref="T:System.Exception">This action would commit a cell value or enter edit mode, but an error in the data source prevents the action and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError"/> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException"/> property to true. </exception>
        // [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        // protected bool ProcessNextKey(Keys keyData)

        // <summary>
        // Processes the PAGE UP key.
        // </summary>
        // 
        // <returns>
        // true if the key was processed; otherwise, false.
        // </returns>
        // <param name="keyData">A bitwise combination of <see cref="T:System.Windows.Forms.Keys"/> values that represents the key or keys to process.</param><exception cref="T:System.InvalidCastException">The PAGE UP key would cause the control to enter edit mode, but the <see cref="P:System.Windows.Forms.DataGridViewCell.EditType"/> property of the new current cell does not indicate a class that derives from <see cref="T:System.Windows.Forms.Control"/> and implements <see cref="T:System.Windows.Forms.IDataGridViewEditingControl"/>.</exception><exception cref="T:System.Exception">This action would commit a cell value or enter edit mode, but an error in the data source prevents the action and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError"/> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException"/> property to true. </exception>
        // [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        // protected bool ProcessPriorKey(Keys keyData)

        // <summary>
        // Processes the RIGHT ARROW key.
        // </summary>
        // 
        // <returns>
        // true if the key was processed; otherwise, false.
        // </returns>
        // <param name="keyData">A bitwise combination of <see cref="T:System.Windows.Forms.Keys"/> values that represents the key or keys to process.</param><exception cref="T:System.InvalidCastException">The RIGHT ARROW key would cause the control to enter edit mode, but the <see cref="P:System.Windows.Forms.DataGridViewCell.EditType"/> property of the new current cell does not indicate a class that derives from <see cref="T:System.Windows.Forms.Control"/> and implements <see cref="T:System.Windows.Forms.IDataGridViewEditingControl"/>.</exception><exception cref="T:System.Exception">This action would commit a cell value or enter edit mode, but an error in the data source prevents the action and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError"/> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException"/> property to true. </exception>
        // [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        // protected bool ProcessRightKey(Keys keyData)

        // <summary>
        // Processes the SPACEBAR.
        // </summary>
        // 
        // <returns>
        // true if the key was processed; otherwise, false.
        // </returns>
        // <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys"/> values that represents the key to process.</param>
        // [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        // protected bool ProcessSpaceKey(Keys keyData)

        // <summary>
        // Processes the TAB key.
        // </summary>
        // 
        // <returns>
        // true if the key was processed; otherwise, false.
        // </returns>
        // <param name="keyData">A bitwise combination of <see cref="T:System.Windows.Forms.Keys"/> values that represents the key or keys to process.</param><exception cref="T:System.InvalidCastException">The TAB key would cause the control to enter edit mode, but the <see cref="P:System.Windows.Forms.DataGridViewCell.EditType"/> property of the new current cell does not indicate a class that derives from <see cref="T:System.Windows.Forms.Control"/> and implements <see cref="T:System.Windows.Forms.IDataGridViewEditingControl"/>.</exception><exception cref="T:System.Exception">This action would commit a cell value or enter edit mode, but an error in the data source prevents the action and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError"/> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException"/> property to true. </exception>
        // [UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
        // protected bool ProcessTabKey(Keys keyData)

        // <summary>
        // Processes keys used for navigating in the <see cref="T:System.Windows.Forms.DataGridView"/>.
        // </summary>
        // 
        // <returns>
        // true if the key was processed; otherwise, false.
        // </returns>
        // <param name="e">Contains information about the key that was pressed.</param><exception cref="T:System.InvalidCastException">The key pressed would cause the control to enter edit mode, but the <see cref="P:System.Windows.Forms.DataGridViewCell.EditType"/> property of the current cell does not indicate a class that derives from <see cref="T:System.Windows.Forms.Control"/> and implements <see cref="T:System.Windows.Forms.IDataGridViewEditingControl"/>.</exception><exception cref="T:System.Exception">This action would commit a cell value or enter edit mode, but an error in the data source prevents the action and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError"/> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException"/> property to true.-or-The DELETE key would delete one or more rows, but an error in the data source prevents the deletion and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError"/> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException"/> property to true. </exception>
        // [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        // protected virtual bool ProcessDataGridViewKey(KeyEventArgs e)

        // <summary>
        // Processes the UP ARROW key.
        // </summary>
        // 
        // <returns>
        // true if the key was processed; otherwise, false.
        // </returns>
        // <param name="keyData">A bitwise combination of <see cref="T:System.Windows.Forms.Keys"/> values that represents the key or keys to process.</param><exception cref="T:System.InvalidCastException">The UP ARROW key would cause the control to enter edit mode, but the <see cref="P:System.Windows.Forms.DataGridViewCell.EditType"/> property of the new current cell does not indicate a class that derives from <see cref="T:System.Windows.Forms.Control"/> and implements <see cref="T:System.Windows.Forms.IDataGridViewEditingControl"/>.</exception><exception cref="T:System.Exception">This action would commit a cell value or enter edit mode, but an error in the data source prevents the action and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError"/> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException"/> property to true. </exception>
        // [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        // protected bool ProcessUpKey(Keys keyData)

        // <summary>
        // Processes the 0 key.
        // </summary>
        // 
        // <returns>
        // true if the key was processed; otherwise, false.
        // </returns>
        /// <param name="keyData">A bitwise combination of <see cref="T:System.Windows.Forms.Keys"/> values that represents the key or keys to process.</param><exception cref="T:System.InvalidCastException">The 0 key would cause the control to enter edit mode, but the <see cref="P:System.Windows.Forms.DataGridViewCell.EditType"/> property of the current cell does not indicate a class that derives from <see cref="T:System.Windows.Forms.Control"/> and implements <see cref="T:System.Windows.Forms.IDataGridViewEditingControl"/>.</exception><exception cref="T:System.Exception">This action would cause the control to enter edit mode, but an error in the data source prevents the action and either there is no handler for the <see cref="E:System.Windows.Forms.DataGridView.DataError"/> event or the handler has set the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException"/> property to true. </exception>
        // [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        // protected bool ProcessZeroKey(Keys keyData)

        // <summary>
        // Refreshes the value of the current cell with the underlying cell value when the cell is in edit mode, discarding any previous value.
        // </summary>
        // 
        // <returns>
        // true if successful; false if a <see cref="E:System.Windows.Forms.DataGridView.DataError"/> event occurred.
        // </returns>
        // <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public bool RefreshEdit()

        // <summary>
        // Resets the <see cref="P:System.Windows.Forms.DataGridView.Text"/> property to its default value.
        // </summary>
        // public override void ResetText()

        // <summary>
        // Selects all the cells in the <see cref="T:System.Windows.Forms.DataGridView"/>.
        // </summary>
        // <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public void SelectAll()

        /// <summary>
        /// Sets the currently active cell.
        /// </summary>
        /// 
        /// <returns>
        /// true if the current cell was successfully set; otherwise, false.
        /// </returns>
        /// <param name="columnIndex">The index of the column containing the cell.</param><param name="rowIndex">The index of the row containing the cell.</param><param name="setAnchorCellAddress">true to make the new current cell the anchor cell for a subsequent multicell selection using the SHIFT key; otherwise, false.</param><param name="validateCurrentCell">true to validate the value in the old current cell and cancel the change if validation fails; otherwise, false.</param><param name="throughMouseClick">true if the current cell is being set as a result of a mouse click; otherwise, false.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="columnIndex"/> is less than 0 or greater than the number of columns in the control minus 1, and <paramref name="rowIndex"/> is not -1.-or-<paramref name="rowIndex"/> is less than 0 or greater than the number of rows in the control minus 1, and <paramref name="columnIndex"/> is not -1.</exception><exception cref="T:System.InvalidOperationException">The specified cell has a <see cref="P:System.Windows.Forms.DataGridViewCell.Visible"/> property value of false.-or-This method was called for a reason other than the underlying data source being reset, and another thread is currently executing this method.</exception><exception cref="T:System.InvalidCastException">The new current cell tried to enter edit mode, but its <see cref="P:System.Windows.Forms.DataGridViewCell.EditType"/> property does not indicate a class that derives from <see cref="T:System.Windows.Forms.Control"/> and implements <see cref="T:System.Windows.Forms.IDataGridViewEditingControl"/>.</exception>
        protected virtual bool SetCurrentCellAddressCore(int columnIndex, int rowIndex, bool setAnchorCellAddress,
            bool validateCurrentCell, bool throughMouseClick)
        {
            Contract.Requires(
                !(columnIndex < -1 || columnIndex >= 0 && rowIndex == -1 || columnIndex >= Columns.Count));
            Contract.Requires(!(rowIndex < -1 || columnIndex == -1 && rowIndex >= 0 || rowIndex >= Rows.Count));
            return default(bool);
        }

        /// <summary>
        /// This member overrides <see cref="M:System.Windows.Forms.Control.SetBoundsCore(System.Int32,System.Int32,System.Int32,System.Int32,System.Windows.Forms.BoundsSpecified)"/>.
        /// </summary>
        /// <param name="x">The new <see cref="P:System.Windows.Forms.Control.Left"/> property value of the control. </param><param name="y">The new <see cref="P:System.Windows.Forms.Control.Top"/> property value of the control. </param><param name="width">The new <see cref="P:System.Windows.Forms.Control.Width"/> property value of the control. </param><param name="height">The new <see cref="P:System.Windows.Forms.Control.Height"/> property value of the control. </param><param name="specified">A bitwise combination of the <see cref="T:System.Windows.Forms.BoundsSpecified"/> values. </param><exception cref="T:System.ArgumentOutOfRangeException">One or both of the width or height values exceeds the maximum value of 8,388,607. </exception>
        protected void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            Contract.Requires(!((specified & BoundsSpecified.Width) == BoundsSpecified.Width && width > 8388607));
            Contract.Requires(!((specified & BoundsSpecified.Height) == BoundsSpecified.Height && height > 8388607));
        }

        /// <summary>
        /// Changes the selection state of the cell with the specified row and column indexes.
        /// </summary>
        /// <param name="columnIndex">The index of the column containing the cell.</param><param name="rowIndex">The index of the row containing the cell.</param><param name="selected">true to select the cell; false to cancel the selection of the cell.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="columnIndex"/> is less than 0 or greater than the number of columns in the control minus 1.-or-<paramref name="rowIndex"/> is less than 0 or greater than the number of rows in the control minus 1.</exception>
        protected virtual void SetSelectedCellCore(int columnIndex, int rowIndex, bool selected)
        {
            Contract.Requires(columnIndex >= 0 && columnIndex < Columns.Count);
            Contract.Requires(rowIndex >= 0 && rowIndex < Rows.Count);
        }

        /// <summary>
        /// Changes the selection state of the column with the specified index.
        /// </summary>
        /// <param name="columnIndex">The index of the column.</param><param name="selected">true to select the column; false to cancel the selection of the column.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="columnIndex"/> is less than 0 or greater than the number of columns in the control minus 1.</exception>
        protected virtual void SetSelectedColumnCore(int columnIndex, bool selected)
        {
            Contract.Requires(columnIndex >= 0 && columnIndex < Columns.Count);
        }

        /// <summary>
        /// Changes the selection state of the row with the specified index.
        /// </summary>
        /// <param name="rowIndex">The index of the row.</param><param name="selected">true to select the row; false to cancel the selection of the row.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="rowIndex"/> is less than 0 or greater than the number of rows in the control minus 1.</exception>
        protected virtual void SetSelectedRowCore(int rowIndex, bool selected)
        {
            Contract.Requires(rowIndex >= 0 && rowIndex < Rows.Count);
        }

        /// <summary>
        /// Sorts the contents of the <see cref="T:System.Windows.Forms.DataGridView"/> control in ascending or descending order based on the contents of the specified column.
        /// </summary>
        /// <param name="dataGridViewColumn">The column by which to sort the contents of the <see cref="T:System.Windows.Forms.DataGridView"/>. </param><param name="direction">One of the <see cref="T:System.ComponentModel.ListSortDirection"/> values. </param><exception cref="T:System.ArgumentException">The specified column is not part of this <see cref="T:System.Windows.Forms.DataGridView"/>.-or-The <see cref="P:System.Windows.Forms.DataGridView.DataSource"/> property has been set and the <see cref="P:System.Windows.Forms.DataGridViewColumn.IsDataBound"/> property of the specified column returns false.</exception><exception cref="T:System.ArgumentNullException"><paramref name="dataGridViewColumn"/> is null.</exception><exception cref="T:System.InvalidOperationException">The <see cref="P:System.Windows.Forms.DataGridView.VirtualMode"/> property is set to true and the <see cref="P:System.Windows.Forms.DataGridViewColumn.IsDataBound"/> property of the specified column returns false.-or-The object specified by the <see cref="P:System.Windows.Forms.DataGridView.DataSource"/> property does not implement the <see cref="T:System.ComponentModel.IBindingList"/> interface.-or-The object specified by the <see cref="P:System.Windows.Forms.DataGridView.DataSource"/> property has a <see cref="P:System.ComponentModel.IBindingList.SupportsSorting"/> property value of false.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public virtual void Sort(DataGridViewColumn dataGridViewColumn, ListSortDirection direction)
        {
            Contract.Requires(dataGridViewColumn != null);
            Contract.Requires(dataGridViewColumn.DataGridView == this);
            Contract.Requires(!(VirtualMode && !dataGridViewColumn.IsDataBound));
        }

        /// <summary>
        /// Sorts the contents of the <see cref="T:System.Windows.Forms.DataGridView"/> control using an implementation of the <see cref="T:System.Collections.IComparer"/> interface.
        /// </summary>
        /// <param name="comparer">An implementation of <see cref="T:System.Collections.IComparer"/> that performs the custom sorting operation. </param><exception cref="T:System.ArgumentNullException"><paramref name="comparer"/> is null.</exception><exception cref="T:System.InvalidOperationException"><see cref="P:System.Windows.Forms.DataGridView.VirtualMode"/> is set to true.-or- <see cref="P:System.Windows.Forms.DataGridView.DataSource"/> is not null.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public virtual void Sort(IComparer comparer)
        {
            Contract.Requires(comparer != null);
            Contract.Requires(!VirtualMode);
            Contract.Requires(DataSource == null);
        }

        /// <summary>
        /// Forces the cell at the specified location to update its error text.
        /// </summary>
        /// <param name="columnIndex">The column index of the cell to update, or -1 to indicate a row header cell.</param><param name="rowIndex">The row index of the cell to update, or -1 to indicate a column header cell.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="columnIndex"/> is less than -1 or greater than the number of columns in the control minus 1.-or-<paramref name="rowIndex"/> is less than -1 or greater than the number of rows in the control minus 1.</exception>
        public void UpdateCellErrorText(int columnIndex, int rowIndex)
        {
            Contract.Requires(!(columnIndex < -1 || columnIndex >= Columns.Count));
            Contract.Requires(!(rowIndex < -1 || rowIndex >= Rows.Count));
        }

        /// <summary>
        /// Forces the control to update its display of the cell at the specified location based on its new value, applying any automatic sizing modes currently in effect.
        /// </summary>
        /// <param name="columnIndex">The zero-based column index of the cell with the new value.</param><param name="rowIndex">The zero-based row index of the cell with the new value.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="columnIndex"/> is less than zero or greater than the number of columns in the control minus one.-or-<paramref name="rowIndex"/> is less than zero or greater than the number of rows in the control minus one.</exception>
        public void UpdateCellValue(int columnIndex, int rowIndex)
        {
            Contract.Requires(columnIndex >= 0 && columnIndex < Columns.Count);
            Contract.Requires(rowIndex >= 0 && rowIndex < Rows.Count);
        }

        /// <summary>
        /// Forces the row at the given row index to update its error text.
        /// </summary>
        /// <param name="rowIndex">The zero-based index of the row to update.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="rowIndex"/> is not in the valid range of 0 to the number of rows in the control minus 1.</exception>
        public void UpdateRowErrorText(int rowIndex)
        {
            Contract.Requires(rowIndex >= 0 && rowIndex < Rows.Count);
        }

        /// <summary>
        /// Forces the rows in the given range to update their error text.
        /// </summary>
        /// <param name="rowIndexStart">The zero-based index of the first row in the set of rows to update.</param><param name="rowIndexEnd">The zero-based index of the last row in the set of rows to update.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="rowIndexStart"/> is not in the valid range of 0 to the number of rows in the control minus 1.-or-<paramref name="rowIndexEnd"/> is not in the valid range of 0 to the number of rows in the control minus 1.-or-<paramref name="rowIndexEnd"/> is less than <paramref name="rowIndexStart"/>.</exception>
        public void UpdateRowErrorText(int rowIndexStart, int rowIndexEnd)
        {
            Contract.Requires(!(rowIndexStart < 0 || rowIndexStart >= Rows.Count));
            Contract.Requires(!(rowIndexEnd < 0 || rowIndexEnd >= Rows.Count));
            Contract.Requires(!(rowIndexEnd < rowIndexStart));
        }

        /// <summary>
        /// Forces the specified row or rows to update their height information.
        /// </summary>
        /// <param name="rowIndex">The zero-based index of the first row to update.</param><param name="updateToEnd">true to update the specified row and all subsequent rows.</param><exception cref="T:System.ArgumentOutOfRangeException"><paramref name="rowIndex"/> is less than 0 and <paramref name="updateToEnd"/> is true.-or-<paramref name="rowIndex"/> is less than -1 and <paramref name="updateToEnd"/> is false.-or-<paramref name="rowIndex"/> is greater than the highest row index in the <see cref="P:System.Windows.Forms.DataGridView.Rows"/> collection.</exception>
        public void UpdateRowHeightInfo(int rowIndex, bool updateToEnd)
        {
            Contract.Requires(
                !(updateToEnd && rowIndex < 0 || !updateToEnd && rowIndex < -1 || rowIndex >= Rows.Count));
        }

        // <summary>
        // Processes window messages.
        // </summary>
        // <param name="m">A <see cref="T:System.Windows.Forms.Message"/>, passed by reference, that represents the window message to process.</param>
        // [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        // protected override void WndProc(ref Message m)
    }
}
