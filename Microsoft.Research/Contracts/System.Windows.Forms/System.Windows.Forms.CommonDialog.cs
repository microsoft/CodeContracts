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
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    // <summary>
    // Specifies the base class used for displaying dialog boxes on the screen.
    // </summary>
    // //
    public abstract class CommonDialog : Component
    {
        // <summary>
        // Gets or sets an object that contains data about the control.
        // </summary>
        // 
        // <returns>
        // The object that contains data about the <see cref="T:System.Windows.Forms.CommonDialog"/>.
        // </returns>
        // //
        
        // public object Tag { get; set; }
        
        // <summary>
        // Occurs when the user clicks the Help button on a common dialog box.
        // </summary>
        // //
        // public event EventHandler HelpRequest
        
        // <summary>
        // Defines the common dialog box hook procedure that is overridden to add specific functionality to a common dialog box.
        // </summary>
        // 
        // <returns>
        // A zero value if the default dialog box procedure processes the message; a nonzero value if the default dialog box procedure ignores the message.
        // </returns>
        // <param name="hWnd">The handle to the dialog box window. </param><param name="msg">The message being received. </param><param name="wparam">Additional information about the message. </param><param name="lparam">Additional information about the message. </param>
        // protected virtual IntPtr HookProc(IntPtr hWnd, int msg, IntPtr wparam, IntPtr lparam)
        
        
        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.CommonDialog.HelpRequest"/> event.
        // </summary>
        // <param name="e">An <see cref="T:System.Windows.Forms.HelpEventArgs"/> that provides the event data. </param>
        // protected virtual void OnHelpRequest(EventArgs e)
        
        // <summary>
        // Defines the owner window procedure that is overridden to add specific functionality to a common dialog box.
        // </summary>
        // 
        // <returns>
        // The result of the message processing, which is dependent on the message sent.
        // </returns>
        // <param name="hWnd">The window handle of the message to send. </param><param name="msg">The Win32 message to send. </param><param name="wparam">The <paramref name="wparam"/> to send with the message. </param><param name="lparam">The <paramref name="lparam"/> to send with the message. </param>
        // protected virtual IntPtr OwnerWndProc(IntPtr hWnd, int msg, IntPtr wparam, IntPtr lparam)
        
        // <summary>
        // When overridden in a derived class, resets the properties of a common dialog box to their default values.
        // </summary>
        // //
        // public abstract void Reset();

        // <summary>
        // When overridden in a derived class, specifies a common dialog box.
        // </summary>
        // 
        // <returns>
        // true if the dialog box was successfully run; otherwise, false.
        // </returns>
        // <param name="hwndOwner">A value that represents the window handle of the owner window for the common dialog box. </param>
        // protected abstract bool RunDialog(IntPtr hwndOwner);

        // <summary>
        // Runs a common dialog box with a default owner.
        // </summary>
        // 
        // <returns>
        // <see cref="F:System.Windows.Forms.DialogResult.OK"/> if the user clicks OK in the dialog box; otherwise, <see cref="F:System.Windows.Forms.DialogResult.Cancel"/>.
        // </returns>
        // <PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public DialogResult ShowDialog()
       
        // <summary>
        // Runs a common dialog box with the specified owner.
        // </summary>
        // 
        // <returns>
        // <see cref="F:System.Windows.Forms.DialogResult.OK"/> if the user clicks OK in the dialog box; otherwise, <see cref="F:System.Windows.Forms.DialogResult.Cancel"/>.
        // </returns>
        // <param name="owner">Any object that implements <see cref="T:System.Windows.Forms.IWin32Window"/> that represents the top-level window that will own the modal dialog box. </param>//<PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public DialogResult ShowDialog(IWin32Window owner)
    }
}

