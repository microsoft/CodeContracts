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

namespace System.Windows.Forms
{
    /// <summary>
    /// Contains border styles for the cells in a <see cref="T:System.Windows.Forms.DataGridView"/> control.
    /// </summary>
    public sealed class DataGridViewAdvancedBorderStyle // : ICloneable
    {
        // <summary>
        // Gets or sets the border style for all of the borders of a cell.
        // </summary>
        // 
        // <returns>
        // One of the <see cref="T:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle"/> values.
        // </returns>
        // <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle"/> values.</exception><exception cref="T:System.ArgumentException">The specified value when setting this property is <see cref="F:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle.NotSet"/>.-or-The specified value when setting this property is <see cref="F:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle.OutsetDouble"/>, <see cref="F:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle.OutsetPartial"/>, or <see cref="F:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle.InsetDouble"/> and this <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle"/> instance was retrieved through the <see cref="P:System.Windows.Forms.DataGridView.AdvancedCellBorderStyle"/> property.</exception><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public DataGridViewAdvancedCellBorderStyle All
        
        // <summary>
        // Gets or sets the style for the bottom border of a cell.
        // </summary>
        // 
        // <returns>
        // One of the <see cref="T:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle"/> values.
        // </returns>
        // <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle"/> values.</exception><exception cref="T:System.ArgumentException">The specified value when setting this property is <see cref="F:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle.NotSet"/>.</exception>
        // public DataGridViewAdvancedCellBorderStyle Bottom
        
        // <summary>
        // Gets the style for the left border of a cell.
        // </summary>
        // 
        // <returns>
        // One of the <see cref="T:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle"/> values.
        // </returns>
        // <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle"/>.</exception><exception cref="T:System.ArgumentException">The specified value when setting this property is <see cref="F:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle.NotSet"/>.-or-The specified value when setting this property is <see cref="F:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle.InsetDouble"/> or <see cref="F:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle.OutsetDouble"/> and this <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle"/> instance has an associated <see cref="T:System.Windows.Forms.DataGridView"/> control with a <see cref="P:System.Windows.Forms.Control.RightToLeft"/> property value of true.</exception>
        // public DataGridViewAdvancedCellBorderStyle Left
        
        // <summary>
        // Gets the style for the right border of a cell.
        // </summary>
        // 
        // <returns>
        // One of the <see cref="T:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle"/> values.
        // </returns>
        // <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle"/>.</exception><exception cref="T:System.ArgumentException">The specified value when setting this property is <see cref="F:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle.NotSet"/>.-or-The specified value when setting this property is <see cref="F:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle.InsetDouble"/> or <see cref="F:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle.OutsetDouble"/> and this <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle"/> instance has an associated <see cref="T:System.Windows.Forms.DataGridView"/> control with a <see cref="P:System.Windows.Forms.Control.RightToLeft"/> property value of false.</exception>
        // public DataGridViewAdvancedCellBorderStyle Right
        
        // <summary>
        // Gets the style for the top border of a cell.
        // </summary>
        // 
        // <returns>
        // One of the <see cref="T:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle"/> values.
        // </returns>
        // <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle"/>.</exception><exception cref="T:System.ArgumentException">The specified value when setting this property is <see cref="F:System.Windows.Forms.DataGridViewAdvancedCellBorderStyle.NotSet"/>.</exception>
        // public DataGridViewAdvancedCellBorderStyle Top
        
        // <summary>
        // Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle"/> class.
        // </summary>
        // public DataGridViewAdvancedBorderStyle()
        //  : this((DataGridView)null, DataGridViewAdvancedCellBorderStyle.NotSet, DataGridViewAdvancedCellBorderStyle.NotSet, DataGridViewAdvancedCellBorderStyle.NotSet)
        
        // <summary>
        // Determines whether the specified object is equal to the current <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle"/>.
        // </summary>
        // 
        // <returns>
        // true if <paramref name="other"/> is a <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle"/> and the values for the <see cref="P:System.Windows.Forms.DataGridViewAdvancedBorderStyle.Top"/>, <see cref="P:System.Windows.Forms.DataGridViewAdvancedBorderStyle.Bottom"/>, <see cref="P:System.Windows.Forms.DataGridViewAdvancedBorderStyle.Left"/>, and <see cref="P:System.Windows.Forms.DataGridViewAdvancedBorderStyle.Right"/> properties are equal to their counterpart in the current <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle"/>; otherwise, false.
        // </returns>
        // <param name="other">An <see cref="T:System.Object"/> to be compared.</param>
        // public override bool Equals(object other)
        
        // public override int GetHashCode()
       
        /// <summary>
        /// Returns a string that represents the <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle"/>.
        /// </summary>
        /// 
        /// <returns>
        /// A string that represents the <see cref="T:System.Windows.Forms.DataGridViewAdvancedBorderStyle"/>.
        /// </returns>
        // public override string ToString()
    }
}

