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
    /// Used to group collections of controls.
    /// </summary>
    public class Panel : ScrollableControl
    {
        // <summary>
        // Gets or sets a value that indicates whether the control resizes based on its contents.
        // </summary>
        // <returns>
        // true if the control automatically resizes based on its contents; otherwise, false. The default is true.
        // </returns>
        // public override bool AutoSize {get; set;}
       
        // <summary>
        // Indicates the automatic sizing behavior of the control.
        // </summary>
        // <returns>
        // One of the <see cref="T:System.Windows.Forms.AutoSizeMode"/> values.
        // </returns>
        // <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.AutoSizeMode"/> values.</exception>
        // public virtual AutoSizeMode AutoSizeMode {get; set;}
        
        // <summary>
        // Indicates the border style for the control.
        // </summary>
        // <returns>
        // One of the <see cref="T:System.Windows.Forms.BorderStyle"/> values. The default is BorderStyle.None.
        // </returns>
        // <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.BorderStyle"/> value.</exception><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public BorderStyle BorderStyle {get; set;}
        
        // <summary>
        // Gets the required creation parameters when the control handle is created.
        // </summary>
        // <returns>
        // A <see cref="T:System.Windows.Forms.CreateParams"/> that contains the required creation parameters when the handle to the control is created.
        // </returns>
        // protected override CreateParams CreateParams {get;}
        
        // <summary>
        // Gets the default size of the control.
        // </summary>
        // <returns>
        // The default <see cref="T:System.Drawing.Size"/> of the control.
        // </returns>
        // protected override Size DefaultSize { get; }
        
        // <summary>
        // Gets or sets a value indicating whether the user can give the focus to this control using the TAB key.
        // </summary>
        // <returns>
        // true if the user can give the focus to the control using the TAB key; otherwise, false. The default is false.
        // </returns>
        // <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // [DefaultValue(false)]
        // public new bool TabStop {get; set;}
        
        // <summary>
        // This member is not meaningful for this control.
        // </summary>
        // <returns>
        // The text associated with this control.
        // </returns>
        // 
        // public override string Text {get; set;}
       
        // <summary>
        // Occurs when the value of the <see cref="P:System.Windows.Forms.Panel.AutoSize"/> property has changed.
        // </summary>
        // public new event EventHandler AutoSizeChanged
        
        // <summary>
        // This member is not meaningful for this control.
        // </summary>
        // public new event KeyEventHandler KeyUp
        
        // <summary>
        // This member is not meaningful for this control.
        // </summary>
        // public new event KeyEventHandler KeyDown
        
        // <summary>
        // This member is not meaningful for this control.
        // </summary>
        // public new event KeyPressEventHandler KeyPress
        
        // <summary>
        // This member is not meaningful for this control.
        // </summary>
        // public new event EventHandler TextChanged
        
        // <summary>
        // Initializes a new instance of the <see cref="T:System.Windows.Forms.Panel"/> class.
        // </summary>
        // public Panel()
        
        // <summary>
        // Fires the event indicating that the panel has been resized. Inheriting controls should use this in favor of actually listening to the event, but should still call base.onResize to ensure that the event is fired for external listeners.
        // </summary>
        // <param name="eventargs">An <see cref="T:System.EventArgs"/> that contains the event data. </param>
        // protected override void OnResize(EventArgs eventargs)
        
        // <summary>
        // Returns a string representation for this control.
        // </summary>
        // <returns>
        // A <see cref="T:System.String"/> representation of the control.
        // </returns>
        // public override string ToString()
    }
}
