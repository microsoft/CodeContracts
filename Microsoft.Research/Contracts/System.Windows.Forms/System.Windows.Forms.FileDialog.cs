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

// Decompiled with JetBrains decompiler
// Type: System.Windows.Forms.FileDialog
// Assembly: System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: E99EB19F-1780-41E7-95D6-F9A9962C7B8D
// Assembly location: C:\WINDOWS\assembly\GAC_MSIL\System.Windows.Forms\2.0.0.0__b77a5c561934e089\System.Windows.Forms.dll

using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Threading;

namespace System.Windows.Forms
{
    // <summary>
    // Displays a dialog box from which the user can select a file.
    // </summary>
    public abstract class FileDialog : CommonDialog
    {
        // <summary>
        // Owns the <see cref="E:System.Windows.Forms.FileDialog.FileOk"/> event.
        // </summary>

        // <summary>
        // Gets or sets a value indicating whether the dialog box automatically adds an extension to a file name if the user omits the extension.
        // </summary>
        // 
        // <returns>
        // true if the dialog box adds an extension to a file name if the user omits the extension; otherwise, false. The default value is true.
        // </returns>
        // public bool AddExtension {get; set;}

        // <summary>
        // Gets or sets a value indicating whether the dialog box displays a warning if the user specifies a file name that does not exist.
        // </summary>
        // 
        // <returns>
        // true if the dialog box displays a warning if the user specifies a file name that does not exist; otherwise, false. The default value is false.
        // </returns>
        // [DefaultValue(false)]
        // public virtual bool CheckFileExists { get; set; }

        // <summary>
        // Gets or sets a value indicating whether the dialog box displays a warning if the user specifies a path that does not exist.
        // </summary>
        // 
        // <returns>
        // true if the dialog box displays a warning when the user specifies a path that does not exist; otherwise, false. The default value is true.
        // </returns>
        //
        // public bool CheckPathExists {get; set;}
        // <summary>
        // Gets or sets the default file name extension.
        // </summary>
        // 
        // <returns>
        // The default file name extension. The returned string does not include the period. The default value is an empty string ("").
        // </returns>
        //
        // public string DefaultExt {get; set;}

        // <summary>
        // Gets or sets a value indicating whether the dialog box returns the location of the file referenced by the shortcut or whether it returns the location of the shortcut (.lnk).
        // </summary>
        // 
        // <returns>
        // true if the dialog box returns the location of the file referenced by the shortcut; otherwise, false. The default value is true.
        // </returns>
        // public bool DereferenceLinks {get; set;}

        // <summary>
        // Gets or sets a string containing the file name selected in the file dialog box.
        // </summary>
        // 
        // <returns>
        // The file name selected in the file dialog box. The default value is an empty string ("").
        // </returns>
        //<PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public string FileName {get; set;}

        /// <summary>
        /// Gets the file names of all selected files in the dialog box.
        /// </summary>
        /// 
        /// <returns>
        /// An array of type <see cref="T:System.String"/>, containing the file names of all selected files in the dialog box.
        /// </returns>
        ///<PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public string[] FileNames
        {
            get
            {
                Contract.Ensures(Contract.Result<string[]>() != null);
                return default(string[]);
            }
        }

        // <summary>
        // Gets or sets the current file name filter string, which determines the choices that appear in the "Save as file type" or "Files of type" box in the dialog box.
        // </summary>
        // 
        // <returns>
        // The file filtering options available in the dialog box.
        // </returns>
        // <exception cref="T:System.ArgumentException"><paramref name="Filter"/> format is invalid. </exception><filterpriority>1</filterpriority>
        // public string Filter {get; set;}

        // <summary>
        // Gets or sets the index of the filter currently selected in the file dialog box.
        // </summary>
        // 
        // <returns>
        // A value containing the index of the filter currently selected in the file dialog box. The default value is 1.
        // </returns>
        //
        // [DefaultValue(1)]
        // public int FilterIndex {get; set;}

        // <summary>
        // Gets or sets the initial directory displayed by the file dialog box.
        // </summary>
        // 
        // <returns>
        // The initial directory displayed by the file dialog box. The default is an empty string ("").
        // </returns>
        //
        // [DefaultValue("")]
        public string InitialDirectory
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                return default(string);
            }
            set { }
        }

        // <summary>
        // Gets the Win32 instance handle for the application.
        // </summary>
        // 
        // <returns>
        // A Win32 instance handle for the application.
        // </returns>
        // protected virtual IntPtr Instance {get;}

        // <summary>
        // Gets values to initialize the <see cref="T:System.Windows.Forms.FileDialog"/>.
        // </summary>
        // 
        // <returns>
        // A bitwise combination of internal values that initializes the <see cref="T:System.Windows.Forms.FileDialog"/>.
        // </returns>
        // protected int Options {get;}

        // <summary>
        // Gets or sets a value indicating whether the dialog box restores the current directory before closing.
        // </summary>
        // 
        // <returns>
        // true if the dialog box restores the current directory to its original value if the user changed the directory while searching for files; otherwise, false. The default value is false.
        // </returns>
        // [DefaultValue(false)]
        public bool RestoreDirectory { get; set; }

        // <summary>
        // Gets or sets a value indicating whether the Help button is displayed in the file dialog box.
        // </summary>
        // 
        // <returns>
        // true if the dialog box includes a help button; otherwise, false. The default value is false.
        // </returns>
        //
        // [DefaultValue(false)]
        // public bool ShowHelp {get; set;}

        // <summary>
        // Gets or sets whether the dialog box supports displaying and saving files that have multiple file name extensions.
        // </summary>
        // 
        // <returns>
        // true if the dialog box supports multiple file name extensions; otherwise, false. The default is false.
        // </returns>
        // [DefaultValue(false)]
        // public bool SupportMultiDottedExtensions {get; set;}

        /// <summary>
        /// Gets or sets the file dialog box title.
        /// </summary>
        /// 
        /// <returns>
        /// The file dialog box title. The default value is an empty string ("").
        /// </returns>
        ///
        // [DefaultValue("")]
        public string Title
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                return default(string);
            }
            set { }
        }

        // <summary>
        // Gets or sets a value indicating whether the dialog box accepts only valid Win32 file names.
        // </summary>
        // 
        // <returns>
        // true if the dialog box accepts only valid Win32 file names; otherwise, false. The default value is true.
        // </returns>
        //
        // [DefaultValue(true)]
        // public bool ValidateNames { get; set; }

        // <summary>
        // Gets the custom places collection for this <see cref="T:System.Windows.Forms.FileDialog"/> instance.
        // </summary>
        // 
        // <returns>
        // The custom places collection for this <see cref="T:System.Windows.Forms.FileDialog"/> instance.
        // </returns>
        // public FileDialogCustomPlacesCollection CustomPlaces {get;}

        // <summary>
        // Gets or sets a value indicating whether this <see cref="T:System.Windows.Forms.FileDialog"/> instance should automatically upgrade appearance and behavior when running on Windows Vista.
        // </summary>
        // 
        // <returns>
        // true if this <see cref="T:System.Windows.Forms.FileDialog"/> instance should automatically upgrade appearance and behavior when running on Windows Vista; otherwise, false. The default is true.
        // </returns>
        // [DefaultValue(true)]
        // public bool AutoUpgradeEnabled {get; set;}

        // <summary>
        // Occurs when the user clicks on the Open or Save button on a file dialog box.
        // </summary>
        //
        // public event CancelEventHandler FileOk

        // <summary>
        // Defines the common dialog box hook procedure that is overridden to add specific functionality to the file dialog box.
        // </summary>
        // 
        // <returns>
        // Returns zero if the default dialog box procedure processes the message; returns a nonzero value if the default dialog box procedure ignores the message.
        // </returns>
        // <param name="hWnd">The handle to the dialog box window. </param><param name="msg">The message received by the dialog box. </param><param name="wparam">Additional information about the message. </param><param name="lparam">Additional information about the message. </param>
        // [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        // protected override IntPtr HookProc(IntPtr hWnd, int msg, IntPtr wparam, IntPtr lparam)

        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.FileDialog.FileOk"/> event.
        // </summary>
        // <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs"/> that contains the event data. </param>
        // protected void OnFileOk(CancelEventArgs e)

        /// <summary>
        /// Specifies a common dialog box.
        /// </summary>
        /// 
        /// <returns>
        /// true if the file could be opened; otherwise, false.
        /// </returns>
        /// <param name="hWndOwner">A value that represents the window handle of the owner window for the common dialog box. </param>
        protected bool RunDialog(IntPtr hWndOwner)
        {
            Contract.Requires(!Control.CheckForIllegalCrossThreadCalls ||
                              Application.OleRequired() == ApartmentState.STA);
            return default(bool);

        }

        // <summary>
        // Provides a string version of this object.
        // </summary>
        // 
        // <returns>
        // A string version of this object.
        // </returns>
        //<PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public override string ToString()
    }
}
