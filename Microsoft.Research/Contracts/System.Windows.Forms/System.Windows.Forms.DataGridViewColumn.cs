using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace System.Windows.Forms
{
    /// <summary>
    /// Represents a column in a <see cref="T:System.Windows.Forms.DataGridView"/> control.
    /// </summary>
    public class DataGridViewColumn : DataGridViewBand  //, IComponent, IDisposable
    {
        // <summary>
        // Gets or sets the mode by which the column automatically adjusts its width.
        // </summary>
        // <returns>
        // A <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnMode"/> value that determines whether the column will automatically adjust its width and how it will determine its preferred width. The default is <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.NotSet"/>.
        // </returns>
        // <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is a <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnMode"/> that is not valid. </exception><exception cref="T:System.InvalidOperationException">The specified value when setting this property results in an <see cref="P:System.Windows.Forms.DataGridViewColumn.InheritedAutoSizeMode"/> value of <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader"/> for a visible column when column headers are hidden.-or-The specified value when setting this property results in an <see cref="P:System.Windows.Forms.DataGridViewColumn.InheritedAutoSizeMode"/> value of <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill"/> for a visible column that is frozen.</exception>
        // public DataGridViewAutoSizeColumnMode AutoSizeMode {get; set;}
        
        // <summary>
        // Gets or sets the template used to create new cells.
        // </summary>
        // <returns>
        // A <see cref="T:System.Windows.Forms.DataGridViewCell"/> that all other cells in the column are modeled after. The default is null.
        // </returns>
        // public virtual DataGridViewCell CellTemplate {get; set;}
       
        // <summary>
        // Gets the run-time type of the cell template.
        // </summary>
        // <returns>
        // The <see cref="T:System.Type"/> of the <see cref="T:System.Windows.Forms.DataGridViewCell"/> used as a template for this column. The default is null.
        // </returns>
        // public System.Type CellType {get;} 
        
        // <summary>
        // Gets or sets the shortcut menu for the column.
        // </summary>
        // <returns>
        // The <see cref="T:System.Windows.Forms.ContextMenuStrip"/> associated with the current <see cref="T:System.Windows.Forms.DataGridViewColumn"/>. The default is null.
        // </returns>
        // public override ContextMenuStrip ContextMenuStrip {get;}

        // <summary>
        // Gets or sets the name of the data source property or database column to which the <see cref="T:System.Windows.Forms.DataGridViewColumn"/> is bound.
        // </summary>
        // <returns>
        // The case-insensitive name of the property or database column associated with the <see cref="T:System.Windows.Forms.DataGridViewColumn"/>.
        // </returns>
        // <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public string DataPropertyName {get; set;}
       
        // <summary>
        // Gets or sets the column's default cell style.
        // </summary>
        // <returns>
        // A <see cref="T:System.Windows.Forms.DataGridViewCellStyle"/> that represents the default style of the cells in the column.
        // </returns>
        // <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // [Browsable(true)]
        // public override DataGridViewCellStyle DefaultCellStyle {get; set;}
        
        /// <summary>
        /// Gets or sets the display order of the column relative to the currently displayed columns.
        /// </summary>
        /// <returns>
        /// The zero-based position of the column as it is displayed in the associated <see cref="T:System.Windows.Forms.DataGridView"/>, or -1 if the band is not contained within a control.
        /// </returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView"/> is not null and the specified value when setting this property is less than 0 or greater than or equal to the number of columns in the control.-or-<see cref="P:System.Windows.Forms.DataGridViewElement.DataGridView"/> is null and the specified value when setting this property is less than -1.-or-The specified value when setting this property is equal to <see cref="F:System.Int32.MaxValue"/>. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public int DisplayIndex
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= -1);
                return default(int);
            }
            set
            {
            }
        }

        // <summary>
        // Gets or sets the width, in pixels, of the column divider.
        // </summary>
        // <returns>
        // The thickness, in pixels, of the divider (the column's right margin).
        // </returns>
        // public int DividerWidth {get; set;}

        // <summary>
        // Gets or sets a value that represents the width of the column when it is in fill mode relative to the widths of other fill-mode columns in the control.
        // </summary>
        // <returns>
        // A <see cref="T:System.Single"/> representing the width of the column when it is in fill mode relative to the widths of other fill-mode columns. The default is 100.
        // </returns>
        // <exception cref="T:System.ArgumentOutOfRangeException">The specified value when setting this property is less than or equal to 0. </exception>
        // public float FillWeight {get; set;}

        // <summary>
        // Gets or sets a value indicating whether a column will move when a user scrolls the <see cref="T:System.Windows.Forms.DataGridView"/> control horizontally.
        // </summary>
        // <returns>
        // true to freeze the column; otherwise, false.
        // </returns>
        // <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public override bool Frozen {get; set;}

        // <summary>
        // Gets or sets the <see cref="T:System.Windows.Forms.DataGridViewColumnHeaderCell"/> that represents the column header.
        // </summary>
        // <returns>
        // A <see cref="T:System.Windows.Forms.DataGridViewColumnHeaderCell"/> that represents the header cell for the column.
        // </returns>
        // public DataGridViewColumnHeaderCell HeaderCell {get; set;}

        // <summary>
        // Gets or sets the caption text on the column's header cell.
        // </summary>
        // <returns>
        // A <see cref="T:System.String"/> with the desired text. The default is an empty string ("").
        // </returns>
        // <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public string HeaderText {get; set;}

        // <summary>
        // Gets the sizing mode in effect for the column.
        // </summary>
        // <returns>
        // The <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnMode"/> value in effect for the column.
        // </returns>
        // public DataGridViewAutoSizeColumnMode InheritedAutoSizeMode {get;}

        // <summary>
        // Gets the cell style currently applied to the column.
        // </summary>
        // <returns>
        // A <see cref="T:System.Windows.Forms.DataGridViewCellStyle"/> that represents the cell style used to display the column.
        // </returns>
        // [Browsable(false)]
        // public override DataGridViewCellStyle InheritedStyle { get; }

        /// <summary>
        /// Gets a value indicating whether the column is bound to a data source.
        /// </summary>
        /// <returns>
        /// true if the column is connected to a data source; otherwise, false.
        /// </returns>
        public bool IsDataBound {get { return default(bool); } }

        /// <summary>
        /// Gets or sets the minimum width, in pixels, of the column.
        /// </summary>
        /// <returns>
        /// The number of pixels, from 2 to <see cref="F:System.Int32.MaxValue"/>, that specifies the minimum width of the column. The default is 5.
        /// </returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The value is less than 2 or greater than <see cref="F:System.Int32.MaxValue"/>.</exception><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        /// [DefaultValue(5)]
        public int MinimumWidth
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= 2);
                return default(int);
            }
            set { Contract.Requires(value >= 2); }
        }

        // <summary>
        // Gets or sets the name of the column.
        // </summary>
        // <returns>
        // A <see cref="T:System.String"/> that contains the name of the column. The default is an empty string ("").
        // </returns>
        // <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public string Name {get; set;}
               
        // <summary>
        // Gets or sets a value indicating whether the user can edit the column's cells.
        // </summary>
        // <returns>
        // true if the user cannot edit the column's cells; otherwise, false.
        // </returns>
        // <exception cref="T:System.InvalidOperationException">This property is set to false for a column that is bound to a read-only data source. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public override bool ReadOnly {get; set;}
        
        // <summary>
        // Gets or sets a value indicating whether the column is resizable.
        // </summary>
        // <returns>
        // One of the <see cref="T:System.Windows.Forms.DataGridViewTriState"/> values. The default is <see cref="F:System.Windows.Forms.DataGridViewTriState.True"/>.
        // </returns>
        // <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public override DataGridViewTriState Resizable {get; set;}
        
        // <summary>
        // Gets or sets the site of the column.
        // </summary>
        // <returns>
        // The <see cref="T:System.ComponentModel.ISite"/> associated with the column, if any.
        // </returns>
        // public ISite Site {get; set;}
       
        // <summary>
        // Gets or sets the sort mode for the column.
        // </summary>
        // <returns>
        // A <see cref="T:System.Windows.Forms.DataGridViewColumnSortMode"/> that specifies the criteria used to order the rows based on the cell values in a column.
        // </returns>
        // <exception cref="T:System.InvalidOperationException">The value assigned to the property conflicts with <see cref="P:System.Windows.Forms.DataGridView.SelectionMode"/>. </exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // [DefaultValue(DataGridViewColumnSortMode.NotSortable)]
        // public DataGridViewColumnSortMode SortMode {get; set;}
        
        // <summary>
        // Gets or sets the text used for ToolTips.
        // </summary>
        // <returns>
        // The text to display as a ToolTip for the column.
        // </returns>
        //[DefaultValue("")]
        // public string ToolTipText {get; set;}
        
        // <summary>
        // Gets or sets the data type of the values in the column's cells.
        // </summary>
        // <returns>
        // A <see cref="T:System.Type"/> that describes the run-time class of the values stored in the column's cells.
        // </returns>
        // [DefaultValue(null)]
        // public System.Type ValueType { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether the column is visible.
        /// </summary>
        /// <returns>
        /// true if the column is visible; otherwise, false.
        /// </returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // [DefaultValue(true)]
        public override bool Visible { get; set; }

        /// <summary>
        /// Gets or sets the current width of the column.
        /// </summary>
        /// <returns>
        /// The width, in pixels, of the column. The default is 100.
        /// </returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">The specified value when setting this property is greater than 65536.</exception><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public int Width
        {
            get { return default(int); }
            set { Contract.Requires(value <= 65536); }
        }

        // <summary>
        // Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewColumn"/> class to the default state.
        // </summary>
        // public DataGridViewColumn()
        //   : this((DataGridViewCell)null)
        
        // <summary>
        // Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewColumn"/> class using an existing <see cref="T:System.Windows.Forms.DataGridViewCell"/> as a template.
        // </summary>
        // <param name="cellTemplate">An existing <see cref="T:System.Windows.Forms.DataGridViewCell"/> to use as a template. </param>
        // public DataGridViewColumn(DataGridViewCell cellTemplate)
        
        // <returns>
        // An <see cref="T:System.Object"/> that represents the cloned <see cref="T:System.Windows.Forms.DataGridViewBand"/>.
        // </returns>
        // <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public override object Clone()
        
        // <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        // protected override void Dispose(bool disposing)
        
        /// <summary>
        /// Calculates the ideal width of the column based on the specified criteria.
        /// </summary>
        /// <returns>
        /// The ideal width, in pixels, of the column.
        /// </returns>
        /// <param name="autoSizeColumnMode">A <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnMode"/> value that specifies an automatic sizing mode. </param><param name="fixedHeight">true to calculate the width of the column based on the current row heights; false to calculate the width with the expectation that the row heights will be adjusted.</param><exception cref="T:System.ArgumentException"><paramref name="autoSizeColumnMode"/> is <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.NotSet"/>, <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.None"/>, or <see cref="F:System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill"/>. </exception><exception cref="T:System.ComponentModel.InvalidEnumArgumentException"><paramref name="autoSizeColumnMode"/> is not a valid <see cref="T:System.Windows.Forms.DataGridViewAutoSizeColumnMode"/> value. </exception>
        public virtual int GetPreferredWidth(DataGridViewAutoSizeColumnMode autoSizeColumnMode, bool fixedHeight)
        {
            Contract.Requires(
                autoSizeColumnMode != DataGridViewAutoSizeColumnMode.NotSet &&
                autoSizeColumnMode != DataGridViewAutoSizeColumnMode.None &&
                autoSizeColumnMode != DataGridViewAutoSizeColumnMode.Fill);
            return default(int);
        }

        // <summary>
        // Gets a string that describes the column.
        // </summary>
        // <returns>
        // A <see cref="T:System.String"/> that describes the column.
        // </returns>
        // public override string ToString()
    }
}

