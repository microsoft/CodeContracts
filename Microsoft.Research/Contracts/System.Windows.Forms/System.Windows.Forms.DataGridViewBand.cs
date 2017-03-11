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
using System.Globalization;
using System.Text;

namespace System.Windows.Forms
{
    /// <summary>
    /// Represents a linear collection of elements in a <see cref="T:System.Windows.Forms.DataGridView"/> control.
    /// </summary>
    public class DataGridViewBand : DataGridViewElement // ,ICloneable, IDisposable
    {
        // <summary>
        // Gets or sets the shortcut menu for the band.
        // </summary>
        // <returns>
        // The <see cref="T:System.Windows.Forms.ContextMenuStrip"/> associated with the current <see cref="T:System.Windows.Forms.DataGridViewBand"/>. The default is null.
        // </returns>
        // [DefaultValue(null)]
        // public virtual ContextMenuStrip ContextMenuStrip { get; set; }
        
        /// <summary>
        /// Gets or sets the default cell style of the band.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Windows.Forms.DataGridViewCellStyle"/> associated with the <see cref="T:System.Windows.Forms.DataGridViewBand"/>.
        /// </returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public virtual DataGridViewCellStyle DefaultCellStyle { get; set; }
        
        // <summary>
        // Gets or sets the run-time type of the default header cell.
        // </summary>
        // <returns>
        // A <see cref="T:System.Type"/> that describes the run-time class of the object used as the default header cell.
        // </returns>
        // <exception cref="T:System.ArgumentException">The specified value when setting this property is not a <see cref="T:System.Type"/> representing <see cref="T:System.Windows.Forms.DataGridViewHeaderCell"/> or a derived type. </exception>
        // public System.Type DefaultHeaderCellType {get; set;}
       
        /// <summary>
        /// Gets a value indicating whether the band is currently displayed onscreen.
        /// </summary>
        /// <returns>
        /// true if the band is currently onscreen; otherwise, false.
        /// </returns>
        public virtual bool Displayed {get { return default(bool); } }
       
        /// <summary>
        /// Gets or sets a value indicating whether the band will move when a user scrolls through the <see cref="T:System.Windows.Forms.DataGridView"/>.
        /// </summary>
        /// <returns>
        /// true if the band cannot be scrolled from view; otherwise, false. The default is false.
        /// </returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        ///[DefaultValue(false)]
        public virtual bool Frozen {get; set;}
       
        // <summary>
        // Gets a value indicating whether the <see cref="P:System.Windows.Forms.DataGridViewBand.DefaultCellStyle"/> property has been set.
        // </summary>
        // <returns>
        // true if the <see cref="P:System.Windows.Forms.DataGridViewBand.DefaultCellStyle"/> property has been set; otherwise, false.
        // </returns>
        // [Browsable(false)]
        // public bool HasDefaultCellStyle {get;}
        
        // <summary>
        // Gets or sets the header cell of the <see cref="T:System.Windows.Forms.DataGridViewBand"/>.
        // </summary>
        // <returns>
        // A <see cref="T:System.Windows.Forms.DataGridViewHeaderCell"/> representing the header cell of the <see cref="T:System.Windows.Forms.DataGridViewBand"/>.
        // </returns>
        // <exception cref="T:System.ArgumentException">The specified value when setting this property is not a <see cref="T:System.Windows.Forms.DataGridViewRowHeaderCell"/> and this <see cref="T:System.Windows.Forms.DataGridViewBand"/> instance is of type <see cref="T:System.Windows.Forms.DataGridViewRow"/>.-or-The specified value when setting this property is not a <see cref="T:System.Windows.Forms.DataGridViewColumnHeaderCell"/> and this <see cref="T:System.Windows.Forms.DataGridViewBand"/> instance is of type <see cref="T:System.Windows.Forms.DataGridViewColumn"/>.</exception>
        // protected DataGridViewHeaderCell HeaderCellCore {get; set;}
        
        /// <summary>
        /// Gets the relative position of the band within the <see cref="T:System.Windows.Forms.DataGridView"/> control.
        /// </summary>
        /// <returns>
        /// The zero-based position of the band in the <see cref="T:System.Windows.Forms.DataGridViewRowCollection"/> or <see cref="T:System.Windows.Forms.DataGridViewColumnCollection"/> that it is contained within. The default is -1, indicating that there is no associated <see cref="T:System.Windows.Forms.DataGridView"/> control.
        /// </returns>
        /// 
        // [Browsable(false)]
        public int Index
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= 0);
                return default(int);
            }
        }
        
        /// <summary>
        /// Gets the cell style in effect for the current band, taking into account style inheritance.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Windows.Forms.DataGridViewCellStyle"/> associated with the <see cref="T:System.Windows.Forms.DataGridViewBand"/>. The default is null.
        /// </returns>
        public virtual DataGridViewCellStyle InheritedStyle {get { return default(DataGridViewCellStyle); } }
        
        // <summary>
        // Gets a value indicating whether the band represents a row.
        // </summary>
        // <returns>
        // true if the band represents a <see cref="T:System.Windows.Forms.DataGridViewRow"/>; otherwise, false.
        // </returns>
        // protected bool IsRow {get;}
        
        /// <summary>
        /// Gets or sets a value indicating whether the user can edit the band's cells.
        /// </summary>
        /// <returns>
        /// true if the user cannot edit the band's cells; otherwise, false. The default is false.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">When setting this property, this <see cref="T:System.Windows.Forms.DataGridViewBand"/> instance is a shared <see cref="T:System.Windows.Forms.DataGridViewRow"/>.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        /// [DefaultValue(false)]
        public virtual bool ReadOnly {get; set;}
        
        /// <summary>
        /// Gets or sets a value indicating whether the band can be resized in the user interface (UI).
        /// </summary>
        /// <returns>
        /// One of the <see cref="T:System.Windows.Forms.DataGridViewTriState"/> values. The default is <see cref="F:System.Windows.Forms.DataGridViewTriState.True"/>.
        /// </returns>
        /// <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        /// [Browsable(true)]
        public virtual DataGridViewTriState Resizable {get; set;}
        
        /// <summary>
        /// Gets or sets a value indicating whether the band is in a selected user interface (UI) state.
        /// </summary>
        /// <returns>
        /// true if the band is selected; otherwise, false.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">The specified value when setting this property is true, but the band has not been added to a <see cref="T:System.Windows.Forms.DataGridView"/> control. -or-This property is being set on a shared <see cref="T:System.Windows.Forms.DataGridViewRow"/>.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public virtual bool Selected {get; set;}
        
        // <summary>
        // Gets or sets the object that contains data to associate with the band.
        // </summary>
        // <returns>
        // An <see cref="T:System.Object"/> that contains information associated with the band. The default is null.
        // </returns>
        // public object Tag {get; set;}
        
        /// <summary>
        /// Gets or sets a value indicating whether the band is visible to the user.
        /// </summary>
        /// <returns>
        /// true if the band is visible; otherwise, false. The default is true.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">The specified value when setting this property is false and the band is the row for new records.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        /// [DefaultValue(true)]
        public virtual bool Visible {get; set;}
       
        // <summary>
        // Creates an exact copy of this band.
        // </summary>
        // <returns>
        // An <see cref="T:System.Object"/> that represents the cloned <see cref="T:System.Windows.Forms.DataGridViewBand"/>.
        // </returns>
        // <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public virtual object Clone()
        
        // <summary>
        // Releases all resources used by the <see cref="T:System.Windows.Forms.DataGridViewBand"/>.
        // </summary>
        // public void Dispose()
      
        // <summary>
        // Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.DataGridViewBand"/> and optionally releases the managed resources.
        // </summary>
        // <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        // protected virtual void Dispose(bool disposing)
        
        // <summary>
        // Called when the band is associated with a different <see cref="T:System.Windows.Forms.DataGridView"/>.
        // </summary>
        // protected override void OnDataGridViewChanged()
        
        // <summary>
        // Returns a string that represents the current band.
        // </summary>
        // <returns>
        // A <see cref="T:System.String"/> that represents the current <see cref="T:System.Windows.Forms.DataGridViewBand"/>.
        // </returns>
        // public override string ToString()

        internal DataGridViewBand()
        {
        }
    }
}
