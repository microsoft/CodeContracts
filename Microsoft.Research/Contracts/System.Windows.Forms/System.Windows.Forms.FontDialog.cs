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

namespace System.Windows.Forms
{
    // <summary>
    // Prompts the user to choose a font from among those installed on the local computer.
    // </summary>
    public class FontDialog : CommonDialog
    {
        // <summary>
        // Owns the <see cref="E:System.Windows.Forms.FontDialog.Apply"/> event.
        // </summary>
        // protected static readonly object EventApply = new object();
        
        // <summary>
        // Gets or sets a value indicating whether the dialog box allows graphics device interface (GDI) font simulations.
        // </summary>
        // 
        // <returns>
        // true if font simulations are allowed; otherwise, false. The default value is true.
        // </returns>
        // [DefaultValue(true)]
        // public bool AllowSimulations {get; set;}
       
        // <summary>
        // Gets or sets a value indicating whether the dialog box allows vector font selections.
        // </summary>
        // 
        // <returns>
        // true if vector fonts are allowed; otherwise, false. The default value is true.
        // </returns>
        // [DefaultValue(true)]
        // public bool AllowVectorFonts { get; set; }
       
        // <summary>
        // Gets or sets a value indicating whether the dialog box displays both vertical and horizontal fonts or only horizontal fonts.
        // </summary>
        // 
        // <returns>
        // true if both vertical and horizontal fonts are allowed; otherwise, false. The default value is true.
        // </returns>
        // [DefaultValue(true)]
        // public bool AllowVerticalFonts {get; set;}
        
        // <summary>
        // Gets or sets a value indicating whether the user can change the character set specified in the Script combo box to display a character set other than the one currently displayed.
        // </summary>
        // 
        // <returns>
        // true if the user can change the character set specified in the Script combo box; otherwise, false. The default value is true.
        // </returns>
        // [DefaultValue(true)]
        // public bool AllowScriptChange {get; set;}
       
        // <summary>
        // Gets or sets the selected font color.
        // </summary>
        // 
        // <returns>
        // The color of the selected font. The default value is <see cref="P:System.Drawing.Color.Black"/>.
        // </returns>
        // [DefaultValue(typeof(Color), "Black")]
        // public Color Color {get; set;}
        
        // <summary>
        // Gets or sets a value indicating whether the dialog box allows only the selection of fixed-pitch fonts.
        // </summary>
        // 
        // <returns>
        // true if only fixed-pitch fonts can be selected; otherwise, false. The default value is false.
        // </returns>
        //[DefaultValue(false)]
        // public bool FixedPitchOnly {get; set;}
        
        /// <summary>
        /// Gets or sets the selected font.
        /// </summary>
        /// 
        /// <returns>
        /// The selected font.
        /// </returns>
        public Font Font
        {
            get
            {
                Contract.Ensures(Contract.Result<Font>() != null);
                return default(Font);
            }
            set
            {
                
            }
        }

        // <summary>
        // Gets or sets a value indicating whether the dialog box specifies an error condition if the user attempts to select a font or style that does not exist.
        // </summary>
        // 
        // <returns>
        // true if the dialog box specifies an error condition when the user tries to select a font or style that does not exist; otherwise, false. The default is false.
        // </returns>
        // [DefaultValue(false)]
        // public bool FontMustExist {get; set;}
        
        /// <summary>
        /// Gets or sets the maximum point size a user can select.
        /// </summary>
        /// 
        /// <returns>
        /// The maximum point size a user can select. The default is 0.
        /// </returns>
        /// [DefaultValue(0)]
        public int MaxSize
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= 0);
                return default(int);
            }
            set
            {

            }
        }

        /// <summary>
        /// Gets or sets the minimum point size a user can select.
        /// </summary>
        /// 
        /// <returns>
        /// The minimum point size a user can select. The default is 0.
        /// </returns>
        ///[DefaultValue(0)]
        public int MinSize
        {
            get
            {
                Contract.Ensures(Contract.Result<int>() >= 0);
                return default(int);
            }
            set
            {

            }
        }

        // <summary>
        // Gets values to initialize the <see cref="T:System.Windows.Forms.FontDialog"/>.
        // </summary>
        // 
        // <returns>
        // A bitwise combination of internal values that initializes the <see cref="T:System.Windows.Forms.FontDialog"/>.
        // </returns>
        // protected int Options {get;}
        
        // <summary>
        // Gets or sets a value indicating whether the dialog box allows selection of fonts for all non-OEM and Symbol character sets, as well as the ANSI character set.
        // </summary>
        // 
        // <returns>
        // true if selection of fonts for all non-OEM and Symbol character sets, as well as the ANSI character set, is allowed; otherwise, false. The default value is false.
        // </returns>
        // [DefaultValue(false)]
        // public bool ScriptsOnly {get; set;}
        
        // <summary>
        // Gets or sets a value indicating whether the dialog box contains an Apply button.
        // </summary>
        // 
        // <returns>
        // true if the dialog box contains an Apply button; otherwise, false. The default value is false.
        // </returns>
        // [DefaultValue(false)]
        // public bool ShowApply {get; set;}
        
        // <summary>
        // Gets or sets a value indicating whether the dialog box displays the color choice.
        // </summary>
        // 
        // <returns>
        // true if the dialog box displays the color choice; otherwise, false. The default value is false.
        // </returns>
        // [DefaultValue(false)]
        // public bool ShowColor {get; set;}
        
        // <summary>
        // Gets or sets a value indicating whether the dialog box contains controls that allow the user to specify strikethrough, underline, and text color options.
        // </summary>
        // 
        // <returns>
        // true if the dialog box contains controls to set strikethrough, underline, and text color options; otherwise, false. The default value is true.
        // </returns>
        // [DefaultValue(true)]
        // public bool ShowEffects { get; set; }
        
        // <summary>
        // Gets or sets a value indicating whether the dialog box displays a Help button.
        // </summary>
        // 
        // <returns>
        // true if the dialog box displays a Help button; otherwise, false. The default value is false.
        // </returns>
        // [DefaultValue(false)]
        // public bool ShowHelp {get; set;}
        
        // <summary>
        // Occurs when the user clicks the Apply button in the font dialog box.
        // </summary>
        // public event EventHandler Apply
        
        // <summary>
        // Initializes a new instance of the <see cref="T:System.Windows.Forms.FontDialog"/> class.
        // </summary>
        // public FontDialog()
        
        // <summary>
        // Specifies the common dialog box hook procedure that is overridden to add specific functionality to a common dialog box.
        // </summary>
        // 
        // <returns>
        // A zero value if the default dialog box procedure processes the message; a nonzero value if the default dialog box procedure ignores the message.
        // </returns>
        // <param name="hWnd">The handle to the dialog box window. </param><param name="msg">The message being received. </param><param name="wparam">Additional information about the message. </param><param name="lparam">Additional information about the message. </param>
        // [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        // protected override IntPtr HookProc(IntPtr hWnd, int msg, IntPtr wparam, IntPtr lparam)
        
        // <summary>
        // Raises the <see cref="E:System.Windows.Forms.FontDialog.Apply"/> event.
        // </summary>
        // <param name="e">An <see cref="T:System.EventArgs"/> that contains the data. </param>
        // protected virtual void OnApply(EventArgs e)
        
        // <summary>
        // Resets all dialog box options to their default values.
        // </summary>
        // public override void Reset()
        
        // <summary>
        // Specifies a file dialog box.
        // </summary>
        // 
        // <returns>
        // true if the dialog box was successfully run; otherwise, false.
        // </returns>
        // <param name="hWndOwner">The window handle of the owner window for the common dialog box.</param>
        // protected override bool RunDialog(IntPtr hWndOwner)
        
        // <summary>
        // Retrieves a string that includes the name of the current font selected in the dialog box.
        // </summary>
        // 
        // <returns>
        // A string that includes the name of the currently selected font.
        // </returns>
        
        // public override string ToString()
    }
}

