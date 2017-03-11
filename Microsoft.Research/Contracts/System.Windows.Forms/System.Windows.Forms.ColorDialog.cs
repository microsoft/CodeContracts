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
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Windows.Forms
{
    // <summary>
    // Represents a common dialog box that displays available colors along with controls that enable the user to define custom colors.
    // </summary>
    // <filterpriority>1</filterpriority>
    public class ColorDialog : CommonDialog
    {
        // <summary>
        // Gets or sets a value indicating whether the user can use the dialog box to define custom colors.
        // </summary>
        //
        // <returns>
        // true if the user can define custom colors; otherwise, false. The default is true.
        // </returns>
        // [DefaultValue(true)]
        // public virtual bool AllowFullOpen { get; set; }
        
        // <summary>
        // Gets or sets a value indicating whether the dialog box displays all available colors in the set of basic colors.
        // </summary>
        // <returns>
        // true if the dialog box displays all available colors in the set of basic colors; otherwise, false. The default value is false.
        // </returns>
        // public virtual bool AnyColor {get; set;}
       
        // <summary>
        // Gets or sets the color selected by the user.
        // </summary>
        //
        // <returns>
        // The color selected by the user. If a color is not selected, the default value is black.
        // </returns>
        // public Color Color {get; set;}
       
        /// <summary>
        /// Gets or sets the set of custom colors shown in the dialog box.
        /// </summary>
        /// 
        /// <returns>
        /// A set of custom colors shown by the dialog box. The default value is null.
        /// </returns>
        public int[] CustomColors
        {
            get
            {
                Contract.Ensures(Contract.Result<int[]>() != null);
                return default(int[]);
            }
        }

        // <summary>
        // Gets or sets a value indicating whether the controls used to create custom colors are visible when the dialog box is opened
        // </summary>
        // 
        // <returns>
        // true if the custom color controls are available when the dialog box is opened; otherwise, false. The default value is false.
        // </returns>
        
        // [DefaultValue(false)]
        // public virtual bool FullOpen {get; set}
        
        // <summary>
        // Gets the underlying window instance handle (HINSTANCE).
        // </summary>
        // 
        // <returns>
        // An <see cref="T:System.IntPtr"/> that contains the HINSTANCE value of the window handle.
        // </returns>
        // protected virtual IntPtr Instance {get;}

        // <summary>
        // Gets values to initialize the <see cref="T:System.Windows.Forms.ColorDialog"/>.
        // </summary>
        // 
        // <returns>
        // A bitwise combination of internal values that initializes the <see cref="T:System.Windows.Forms.ColorDialog"/>.
        // </returns>
        // protected virtual int Options {get;}
        
        // <summary>
        // Gets or sets a value indicating whether a Help button appears in the color dialog box.
        // </summary>
        // 
        // <returns>
        // true if the Help button is shown in the dialog box; otherwise, false. The default value is false.
        // </returns>
        // [DefaultValue(false)]
        // public virtual bool ShowHelp { get; set; }
        
        // <summary>
        // Gets or sets a value indicating whether the dialog box will restrict users to selecting solid colors only.
        // </summary>
        // 
        // <returns>
        // true if users can select only solid colors; otherwise, false. The default value is false.
        // </returns>
        // [DefaultValue(false)]
        // public virtual bool SolidColorOnly {get; set;}
        
        // <summary>
        // Initializes a new instance of the <see cref="T:System.Windows.Forms.ColorDialog"/> class.
        // </summary>
        // public ColorDialog()
        
        // <summary>
        // Resets all options to their default values, the last selected color to black, and the custom colors to their default values.
        // </summary>

        // public override void Reset()

        /// <returns>
        /// true if the dialog box was successfully run; otherwise, false.
        /// </returns>
        /// <param name="hwndOwner">A value that represents the window handle of the owner window for the common dialog box. </param>
        protected override bool RunDialog(IntPtr hwndOwner)
        {
            return default(bool);
        }
        
        // <summary>
        // Returns a string that represents the <see cref="T:System.Windows.Forms.ColorDialog"/>.
        // </summary>
        // 
        // <returns>
        // A <see cref="T:System.String"/> that represents the current <see cref="T:System.Windows.Forms.ColorDialog"/>.
        // </returns>
        // <PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public override string ToString()
    }
}
