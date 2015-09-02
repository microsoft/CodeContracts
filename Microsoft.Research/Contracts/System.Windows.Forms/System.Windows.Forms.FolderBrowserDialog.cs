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
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;

namespace System.Windows.Forms
{
    // <summary>
    // Prompts the user to select a folder. This class cannot be inherited.
    // </summary>
    public sealed class FolderBrowserDialog : CommonDialog
    {
        // <summary>
        // Gets or sets a value indicating whether the New Folder button appears in the folder browser dialog box.
        // </summary>
        // 
        // <returns>
        // true if the New Folder button is shown in the dialog box; otherwise, false. The default is true.
        // </returns>

        // [DefaultValue(true)]
        // public bool ShowNewFolderButton {get; set;}
        
        // <summary>
        // Gets or sets the path selected by the user.
        // </summary>
        // 
        // <returns>
        // The path of the folder first selected in the dialog box or the last folder selected by the user. The default is an empty string ("").
        // </returns>
        // <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // [DefaultValue("")]
        // public string SelectedPath {get; set;}
        
        // <summary>
        // Gets or sets the root folder where the browsing starts from.
        // </summary>
        // 
        // <returns>
        // One of the <see cref="T:System.Environment.SpecialFolder"/> values. The default is Desktop.
        // </returns>
        // <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Environment.SpecialFolder"/> values. </exception><filterpriority>1</filterpriority>
        // [DefaultValue(Environment.SpecialFolder.Desktop)]
        // public Environment.SpecialFolder RootFolder { get; set; }
        

        /// <summary>
        /// Gets or sets the descriptive text displayed above the tree view control in the dialog box.
        /// </summary>
        /// 
        /// <returns>
        /// The description to display. The default is an empty string ("").
        /// </returns>
        public string Description
        {
            get
            {
                Contract.Requires(Contract.Result<string>() != null);
                return default(string);
            }
            set
            {
               
            }
        }

        // <summary>
        // Occurs when the user clicks the Help button on the dialog box.
        // </summary>
        // public new event EventHandler HelpRequest
        
        // <summary>
        // Initializes a new instance of the <see cref="T:System.Windows.Forms.FolderBrowserDialog"/> class.
        // </summary>
        // public FolderBrowserDialog()
        
        // <summary>
        // Resets properties to their default values.
        // </summary>

        // public override void Reset()
        
        // protected override bool RunDialog(IntPtr hWndOwner)
        
    }
}
