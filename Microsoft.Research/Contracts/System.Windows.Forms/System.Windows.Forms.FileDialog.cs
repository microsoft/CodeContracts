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
    public abstract class FileDialog // : CommonDialog
    {
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
        // public virtual bool CheckFileExists {get; set;}

        // <summary>
        // Gets or sets a value indicating whether the dialog box displays a warning if the user specifies a path that does not exist.
        // </summary>
        // 
        // <returns>
        // true if the dialog box displays a warning when the user specifies a path that does not exist; otherwise, false. The default value is true.
        // </returns>
        // public bool CheckPathExists {get; set;}

        /// <summary>
        /// Gets or sets the default file name extension.
        /// </summary>
        /// 
        /// <returns>
        /// The default file name extension. The returned string does not include the period. The default value is an empty string ("").
        /// </returns>
        public string DefaultExt
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                return default(string);
            }
            set
            {
                
            }
        }

        // <summary>
        // Gets or sets a value indicating whether the dialog box returns the location of the file referenced by the shortcut or whether it returns the location of the shortcut (.lnk).
        // </summary>
        // 
        // <returns>
        // true if the dialog box returns the location of the file referenced by the shortcut; otherwise, false. The default value is true.
        // </returns>
        // <filterpriority>1</filterpriority>
        // public bool DereferenceLinks {get; set}

        /// <summary>
        /// Gets or sets a string containing the file name selected in the file dialog box.
        /// </summary>
        /// 
        /// <returns>
        /// The file name selected in the file dialog box. The default value is an empty string ("").
        /// </returns>
        public string FileName
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                return default(string);
            }
            set
            {
            
            }
        }

        /// <summary>
        /// Gets the file names of all selected files in the dialog box.
        /// </summary>
        /// 
        /// <returns>
        /// An array of type <see cref="T:System.String"/>, containing the file names of all selected files in the dialog box.
        /// </returns>
        public string[] FileNames
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                return default(string[]);
            }
            set
            {
                
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
        public string Filter
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                return default(string);
            }
            set
            {

            }
        }

        // <summary>
        // Gets or sets the index of the filter currently selected in the file dialog box.
        // </summary>
        // 
        // <returns>
        // A value containing the index of the filter currently selected in the file dialog box. The default value is 1.
        // </returns>
        // public int FilterIndex {get; set;}

        /// <summary>
        /// Gets or sets the initial directory displayed by the file dialog box.
        /// </summary>
        /// 
        /// <returns>
        /// The initial directory displayed by the file dialog box. The default is an empty string ("").
        /// </returns>
      
        public string InitialDirectory
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                return default(string);
            }
            set
            {

            }
        }

        // <summary>
        // Gets or sets a value indicating whether the dialog box restores the current directory before closing.
        // </summary>
        // 
        // <returns>
        // true if the dialog box restores the current directory to its original value if the user changed the directory while searching for files; otherwise, false. The default value is false.
        // </returns>
        // public bool RestoreDirectory {get; set;}

        // <summary>
        // Gets or sets a value indicating whether the Help button is displayed in the file dialog box.
        // </summary>
        // 
        // <returns>
        // true if the dialog box includes a help button; otherwise, false. The default value is false.
        // </returns>
        // public bool ShowHelp {get; set;}
        
        // <summary>
        // Gets or sets whether the dialog box supports displaying and saving files that have multiple file name extensions.
        // </summary>
        // 
        // <returns>
        // true if the dialog box supports multiple file name extensions; otherwise, false. The default is false.
        // </returns>
        // public bool SupportMultiDottedExtensions {get; set;}
        
        /// <summary>
        /// Gets or sets the file dialog box title.
        /// </summary>
        /// 
        /// <returns>
        /// The file dialog box title. The default value is an empty string ("").
        /// </returns>
        /// <filterpriority>1</filterpriority>   
        public string Title
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                return default(string);
            }
            set
            {

            }
        }

        // <summary>
        // Gets or sets a value indicating whether the dialog box accepts only valid Win32 file names.
        // </summary>
        // 
        // <returns>
        // true if the dialog box accepts only valid Win32 file names; otherwise, false. The default value is true.
        // </returns>
        // <filterpriority>1</filterpriority>
        // public bool ValidateNames {get; set;}

        /// <summary>
        /// Gets the custom places collection for this <see cref="T:System.Windows.Forms.FileDialog"/> instance.
        /// </summary>
        /// 
        /// <returns>
        /// The custom places collection for this <see cref="T:System.Windows.Forms.FileDialog"/> instance.
        /// </returns>
        public FileDialogCustomPlacesCollection CustomPlaces
        {
            get
            {
                Contract.Ensures(Contract.Result<FileDialogCustomPlacesCollection>() != null);
                return default(FileDialogCustomPlacesCollection);
            }
            set
            {
                
            }
        }

        // <summary>
        // Gets or sets a value indicating whether this <see cref="T:System.Windows.Forms.FileDialog"/> instance should automatically upgrade appearance and behavior when running on Windows Vista.
        // </summary>
        // 
        // <returns>
        // true if this <see cref="T:System.Windows.Forms.FileDialog"/> instance should automatically upgrade appearance and behavior when running on Windows Vista; otherwise, false. The default is true.
        // </returns> 
        // public bool AutoUpgradeEnabled {get; set;}

        // <summary>
        // Resets all properties to their default values.
        // </summary>
        // public override void Reset() { }

        // <summary>
        // Provides a string version of this object.
        // </summary>
        // 
        // <returns>
        // A string version of this object.
        // </returns>
        // public override string ToString()
    }
}
