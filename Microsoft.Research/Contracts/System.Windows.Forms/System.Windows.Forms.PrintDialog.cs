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
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Windows.Forms
{
    /// <summary>
    /// Lets users select a printer and choose which sections of the document to print from a Windows Forms application.
    /// </summary>
    public sealed class PrintDialog : CommonDialog
    {
        // <summary>
        // Gets or sets a value indicating whether the Current Page option button is displayed.
        // </summary>
        // <returns>
        // true if the Current Page option button is displayed; otherwise, false. The default is false.
        // </returns>
        // [DefaultValue(false)]
        // public bool AllowCurrentPage {get; set;}
       
        // <summary>
        // Gets or sets a value indicating whether the Pages option button is enabled.
        // </summary>
        // <returns>
        // true if the Pages option button is enabled; otherwise, false. The default is false.
        // </returns>
        // [DefaultValue(false)]
        // public bool AllowSomePages {get; set;}
        
        // <summary>
        // Gets or sets a value indicating whether the Print to file check box is enabled.
        // </summary>
        // 
        // <returns>
        // true if the Print to file check box is enabled; otherwise, false. The default is true.
        // </returns>
        // [DefaultValue(true)]
        // public bool AllowPrintToFile {get; set;}
        
        // <summary>
        // Gets or sets a value indicating whether the Selection option button is enabled.
        // </summary> 
        // <returns>
        // true if the Selection option button is enabled; otherwise, false. The default is false.
        // </returns>
        // [DefaultValue(false)]
        // public bool AllowSelection { get; set; }
        
        // <summary>
        // Gets or sets a value indicating the <see cref="T:System.Drawing.Printing.PrintDocument"/> used to obtain <see cref="T:System.Drawing.Printing.PrinterSettings"/>.
        // </summary>

        // <returns>
        // The <see cref="T:System.Drawing.Printing.PrintDocument"/> used to obtain <see cref="T:System.Drawing.Printing.PrinterSettings"/>. The default is null.
        // </returns>
        // [DefaultValue(null)]
        // public PrintDocument Document { get; set; }
        
        // <summary>
        // Gets or sets the printer settings the dialog box modifies.
        // </summary>
        // <returns>
        // The <see cref="T:System.Drawing.Printing.PrinterSettings"/> the dialog box modifies.
        // </returns>
        // public PrinterSettings PrinterSettings {get; set;}
        
        // <summary>
        // Gets or sets a value indicating whether the Print to file check box is selected.
        // </summary>
        // <returns>
        // true if the Print to file check box is selected; otherwise, false. The default is false.
        // </returns>
        // [DefaultValue(false)]
        // public bool PrintToFile {get; set;}
        
        // <summary>
        // Gets or sets a value indicating whether the Help button is displayed.
        // </summary>
        // <returns>
        // true if the Help button is displayed; otherwise, false. The default is false.
        // </returns>
        // [DefaultValue(false)]
        // public bool ShowHelp { get; set; }
        
        // <summary>
        // Gets or sets a value indicating whether the Network button is displayed.
        // </summary>
        // <returns>
        // true if the Network button is displayed; otherwise, false. The default is true.
        // </returns>
        // [DefaultValue(true)]
        // public bool ShowNetwork {get; set;}
       
        // <summary>
        // Gets or sets a value indicating whether the dialog should be shown in the Windows XP style for systems running Windows XP Home Edition, Windows XP Professional, Windows Server 2003 or later.
        // </summary>
        // <returns>
        // true to indicate the dialog should be shown with the Windows XP style, otherwise false. The default is false.
        // </returns>
        // [DefaultValue(false)]
        // public bool UseEXDialog {get; set;}
       
        // <summary>
        // Initializes a new instance of the <see cref="T:System.Windows.Forms.PrintDialog"/> class.
        // </summary>
        // public PrintDialog();
        // <summary>
        // Resets all options, the last selected printer, and the page settings to their default values.
        // </summary>
        
        // public override void Reset()

        protected override bool RunDialog(IntPtr hwndOwner)
        {
            return default(bool);
        }
    }
}

