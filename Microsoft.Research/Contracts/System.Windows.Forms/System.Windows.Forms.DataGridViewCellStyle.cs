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
using System.Drawing;

namespace System.Windows.Forms
{
    /// <summary>
    /// Represents the formatting and style information applied to individual cells within a <see cref="T:System.Windows.Forms.DataGridView"/> control.
    /// </summary>
    public class DataGridViewCellStyle // : ICloneable
    {
        // <summary>
        // Gets or sets a value indicating the position of the cell content within a <see cref="T:System.Windows.Forms.DataGridView"/> cell.
        // </summary>
        // <returns>
        // One of the <see cref="T:System.Windows.Forms.DataGridViewContentAlignment"/> values. The default is <see cref="F:System.Windows.Forms.DataGridViewContentAlignment.NotSet"/>.
        // </returns>
        // <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The property value is not a valid <see cref="T:System.Windows.Forms.DataGridViewContentAlignment"/> value. </exception>
        // public DataGridViewContentAlignment Alignment {get; set;}
       
        // <summary>
        // Gets or sets the background color of a <see cref="T:System.Windows.Forms.DataGridView"/> cell.
        // </summary>
        // <returns>
        // A <see cref="T:System.Drawing.Color"/> that represents the background color of a cell. The default is <see cref="F:System.Drawing.Color.Empty"/>.
        // </returns>
        // <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public Color BackColor { get; set; }
        
        // <summary>
        // Gets or sets the value saved to the data source when the user enters a null value into a cell.
        // </summary>
        // <returns>
        // The value saved to the data source when the user specifies a null cell value. The default is <see cref="F:System.DBNull.Value"/>.
        // </returns>
        // public object DataSourceNullValue {get; set;}
        
        // <summary>
        // Gets or sets the font applied to the textual content of a <see cref="T:System.Windows.Forms.DataGridView"/> cell.
        // </summary>
        // <returns>
        // The <see cref="T:System.Drawing.Font"/> applied to the cell text. The default is null.
        // </returns>
        // public Font Font {get; set;}

        // <summary>
        // Gets or sets the foreground color of a <see cref="T:System.Windows.Forms.DataGridView"/> cell.
        // </summary>
        // <returns>
        // A <see cref="T:System.Drawing.Color"/> that represents the foreground color of a cell. The default is <see cref="F:System.Drawing.Color.Empty"/>.
        // </returns>
        // public Color ForeColor {get; set;}
       
        /// <summary>
        /// Gets or sets the format string applied to the textual content of a <see cref="T:System.Windows.Forms.DataGridView"/> cell.
        /// </summary>
        /// <returns>
        /// A string that indicates the format of the cell value. The default is <see cref="F:System.String.Empty"/>.
        /// </returns>
        public string Format
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                return default(string);
            }
            set { }
        }

        /// <summary>
        /// Gets or sets the object used to provide culture-specific formatting of <see cref="T:System.Windows.Forms.DataGridView"/> cell values.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.IFormatProvider"/> used for cell formatting. The default is <see cref="P:System.Globalization.CultureInfo.CurrentUICulture"/>.
        /// </returns>
       
        public IFormatProvider FormatProvider
        {
            get
            {
                Contract.Ensures(Contract.Result<IFormatProvider>() != null);
                return default(IFormatProvider);
            }
            set
            {

            }
        }

        // <summary>
        // Gets a value indicating whether the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.DataSourceNullValue"/> property has been set.
        // </summary>
        // <returns>
        // true if the value of the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.DataSourceNullValue"/> property is the default value; otherwise, false.
        // </returns>
        // public bool IsDataSourceNullValueDefault { get;}
        
        // <summary>
        // Gets a value that indicates whether the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.FormatProvider"/> property has been set.
        // </summary>
        // <returns>
        // true if the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.FormatProvider"/> property is the default value; otherwise, false.
        // </returns>
        // public bool IsFormatProviderDefault { get; }
        
        // <summary>
        // Gets a value indicating whether the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.NullValue"/> property has been set.
        // </summary>
        // <returns>
        // true if the value of the <see cref="P:System.Windows.Forms.DataGridViewCellStyle.NullValue"/> property is the default value; otherwise, false.
        // </returns>
        // public bool IsNullValueDefault { get; }
        
        // <summary>
        // Gets or sets the <see cref="T:System.Windows.Forms.DataGridView"/> cell display value corresponding to a cell value of <see cref="F:System.DBNull.Value"/> or null.
        // </summary>
        // <returns>
        // The object used to indicate a null value in a cell. The default is <see cref="F:System.String.Empty"/>.
        // </returns>
        // public object NullValue { get; set; }
        
        // <summary>
        // Gets or sets the space between the edge of a <see cref="T:System.Windows.Forms.DataGridViewCell"/> and its content.
        // </summary>
        // <returns>
        // A <see cref="T:System.Windows.Forms.Padding"/> that represents the space between the edge of a <see cref="T:System.Windows.Forms.DataGridViewCell"/> and its content.
        // </returns>
        // public Padding Padding {get; set;}
       
        // <summary>
        // Gets or sets the background color used by a <see cref="T:System.Windows.Forms.DataGridView"/> cell when it is selected.
        // </summary>
        // <returns>
        // A <see cref="T:System.Drawing.Color"/> that represents the background color of a selected cell. The default is <see cref="F:System.Drawing.Color.Empty"/>.
        // </returns>
        // public Color SelectionBackColor { get; set; }
        
        // <summary>
        // Gets or sets the foreground color used by a <see cref="T:System.Windows.Forms.DataGridView"/> cell when it is selected.
        // </summary>
        // <returns>
        // A <see cref="T:System.Drawing.Color"/> that represents the foreground color of a selected cell. The default is <see cref="F:System.Drawing.Color.Empty"/>.
        // </returns>
        // public Color SelectionForeColor {get; set;}
        
        // <summary>
        // Gets or sets an object that contains additional data related to the <see cref="T:System.Windows.Forms.DataGridViewCellStyle"/>.
        // </summary>
        // <returns>
        // An object that contains additional data. The default is null.
        // </returns>
        // public object Tag {get; set;}
        
        // <summary>
        // Gets or sets a value indicating whether textual content in a <see cref="T:System.Windows.Forms.DataGridView"/> cell is wrapped to subsequent lines or truncated when it is too long to fit on a single line.
        // </summary>
        // <returns>
        // One of the <see cref="T:System.Windows.Forms.DataGridViewTriState"/> values. The default is <see cref="F:System.Windows.Forms.DataGridViewTriState.NotSet"/>.
        // </returns>
        // <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The property value is not a valid <see cref="T:System.Windows.Forms.DataGridViewTriState"/> value. </exception>
        // public DataGridViewTriState WrapMode { get; set; }
        
        // <summary>
        // Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCellStyle"/> class using default property values.
        // </summary>
        // public DataGridViewCellStyle()
        
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewCellStyle"/> class using the property values of the specified <see cref="T:System.Windows.Forms.DataGridViewCellStyle"/>.
        /// </summary>
        /// <param name="dataGridViewCellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle"/> used as a template to provide initial property values. </param><exception cref="T:System.ArgumentNullException"><paramref name="dataGridViewCellStyle"/> is null.</exception>
        public DataGridViewCellStyle(DataGridViewCellStyle dataGridViewCellStyle)
        {
            Contract.Requires(dataGridViewCellStyle != null);
        }
        
        /// <summary>
        /// Applies the specified <see cref="T:System.Windows.Forms.DataGridViewCellStyle"/> to the current <see cref="T:System.Windows.Forms.DataGridViewCellStyle"/>.
        /// </summary>
        /// <param name="dataGridViewCellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle"/> to apply to the current <see cref="T:System.Windows.Forms.DataGridViewCellStyle"/>.</param><exception cref="T:System.ArgumentNullException"><paramref name="dataGridViewCellStyle"/> is null.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public virtual void ApplyStyle(DataGridViewCellStyle dataGridViewCellStyle)
        {
            Contract.Requires(dataGridViewCellStyle != null);
        }

        /// <summary>
        /// Creates an exact copy of this <see cref="T:System.Windows.Forms.DataGridViewCellStyle"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Windows.Forms.DataGridViewCellStyle"/> that represents an exact copy of this cell style.
        /// </returns>
        public virtual DataGridViewCellStyle Clone()
        {
            Contract.Ensures(Contract.Result<DataGridViewCellStyle>() != null);
            return default(DataGridViewCellStyle);
        }

        // <summary>
        // Returns a value indicating whether this instance is equivalent to the specified object.
        // </summary>
        // <returns>
        // true if <paramref name="o"/> is a <see cref="T:System.Windows.Forms.DataGridViewCellStyle"/> and has the same property values as this instance; otherwise, false.
        // </returns>
        // <param name="o">An object to compare with this instance, or null. </param>
        // public override bool Equals(object o);
        
        // <returns>
        // A hash code for the current object.
        // </returns>
        // public override int GetHashCode();
        
        // <summary>
        // Returns a string indicating the current property settings of the <see cref="T:System.Windows.Forms.DataGridViewCellStyle"/>.
        // </summary>
        // <returns>
        // A string indicating the current property settings of the <see cref="T:System.Windows.Forms.DataGridViewCellStyle"/>.
        // </returns>
        // <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public override string ToString();
    }
}
