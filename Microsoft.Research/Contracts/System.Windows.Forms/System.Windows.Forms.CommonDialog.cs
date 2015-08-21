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
    public abstract class CommonDialog // : System.ComponentModel.Component
    {
        // <summary>
        // Gets or sets an object that contains data about the control.
        // </summary>
        // 
        // <returns>
        // The object that contains data about the <see cref="T:System.Windows.Forms.CommonDialog"/>.
        // </returns>
        // public object Tag { get; set; }

        // <summary>
        // When overridden in a derived class, resets the properties of a common dialog box to their default values.
        // </summary>
        // public abstract void Reset();

        // <summary>
        // Runs a common dialog box with a default owner.
        // </summary>
        // 
        // <returns>
        // <see cref="F:System.Windows.Forms.DialogResult.OK"/> if the user clicks OK in the dialog box; otherwise, <see cref="F:System.Windows.Forms.DialogResult.Cancel"/>.
        // </returns>
        // public DialogResult ShowDialog() {}

        // <summary>
        // Runs a common dialog box with the specified owner.
        // </summary>
        // 
        // <returns>
        // <see cref="F:System.Windows.Forms.DialogResult.OK"/> if the user clicks OK in the dialog box; otherwise, <see cref="F:System.Windows.Forms.DialogResult.Cancel"/>.
        // </returns>
        // <param name="owner">Any object that implements <see cref="T:System.Windows.Forms.IWin32Window"/> that represents the top-level window that will own the modal dialog box. </param><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public DialogResult ShowDialog(IWin32Window owner)
        {
            Contract.Requires(owner != null);
            return default(DialogResult);
        }
    }
}
