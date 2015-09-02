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
using System.Drawing.Printing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace System.Windows.Forms
{
    /// <summary>
    /// Enables users to change page-related print settings, including margins and paper orientation. This class cannot be inherited.
    /// </summary>

    public sealed class PageSetupDialog : CommonDialog
    {
     
        // <summary>
        // Gets or sets a value indicating whether the margins section of the dialog box is enabled.
        // </summary>
        // 
        // <returns>
        // true if the margins section of the dialog box is enabled; otherwise, false. The default is true.
        // </returns>
        // [DefaultValue(true)]
        // public bool AllowMargins {get; set;}
        
        // <summary>
        // Gets or sets a value indicating whether the orientation section of the dialog box (landscape versus portrait) is enabled.
        // </summary>
        // 
        // <returns>
        // true if the orientation section of the dialog box is enabled; otherwise, false. The default is true.
        // </returns>

        // [DefaultValue(true)]
        // public bool AllowOrientation {get; set;}
        
        // <summary>
        // Gets or sets a value indicating whether the paper section of the dialog box (paper size and paper source) is enabled.
        // </summary>
        // 
        // <returns>
        // true if the paper section of the dialog box is enabled; otherwise, false. The default is true.
        // </returns>
        // [DefaultValue(true)]
        // public bool AllowPaper {get; set;}
        
        // <summary>
        // Gets or sets a value indicating whether the Printer button is enabled.
        // </summary>
        // 
        // <returns>
        // true if the Printer button is enabled; otherwise, false. The default is true.
        // </returns>

        // [DefaultValue(true)]
        // public bool AllowPrinter {get; set;}
       
        // <summary>
        // Gets or sets a value indicating the <see cref="T:System.Drawing.Printing.PrintDocument"/> to get page settings from.
        // </summary>
        // 
        // <returns>
        // The <see cref="T:System.Drawing.Printing.PrintDocument"/> to get page settings from. The default is null.
        // </returns>

        // [DefaultValue(null)]
        // public PrintDocument Document {get; set;}
       
        // <summary>
        // Gets or sets a value indicating whether the margin settings, when displayed in millimeters, should be automatically converted to and from hundredths of an inch.
        // </summary>
        // 
        // <returns>
        // true if the margins should be automatically converted; otherwise, false. The default is false.
        // </returns>
        // [DefaultValue(false)]
        // public bool EnableMetric {get; set;}
        
        // <summary>
        // Gets or sets a value indicating the minimum margins, in hundredths of an inch, the user is allowed to select.
        // </summary>
        // 
        // <returns>
        // The minimum margins, in hundredths of an inch, the user is allowed to select. The default is null.
        // </returns>
        public Margins MinMargins
        {
            get
            {
                Contract.Ensures(Contract.Result<Margins>() != null);
                return default(Margins);
            }
            set
            {
               
            }
        }

        /// <summary>
        /// Gets or sets a value indicating the page settings to modify.
        /// </summary>
        /// 
        /// <returns>
        /// The <see cref="T:System.Drawing.Printing.PageSettings"/> to modify. The default is null.
        /// </returns>
        /// [DefaultValue(null)]
        public PageSettings PageSettings {get; set;}
       
        // <summary>
        // Gets or sets the printer settings that are modified when the user clicks the Printer button in the dialog.
        // </summary>
        // 
        // <returns>
        // The <see cref="T:System.Drawing.Printing.PrinterSettings"/> to modify when the user clicks the Printer button. The default is null.
        // </returns>

        [DefaultValue(null)]
        // public PrinterSettings PrinterSettings {get; set;}
        
        // <summary>
        // Gets or sets a value indicating whether the Help button is visible.
        // </summary>
        // 
        // <returns>
        // true if the Help button is visible; otherwise, false. The default is false.
        // </returns>
        // [DefaultValue(false)]
        // public bool ShowHelp {get; set;}
       
        // <summary>
        // Gets or sets a value indicating whether the Network button is visible.
        // </summary>
        // 
        // <returns>
        // true if the Network button is visible; otherwise, false. The default is true.
        // </returns>

        // [DefaultValue(true)]
        // public bool ShowNetwork {get; set;}
       
        // <summary>
        // Initializes a new instance of the <see cref="T:System.Windows.Forms.PageSetupDialog"/> class.
        // </summary>
        // public PageSetupDialog()
        
        // <summary>
        // Resets all options to their default values.
        // </summary>

        // public override void Reset()
        
        protected bool RunDialog(IntPtr hwndOwner)
        {
            Contract.Requires(this.PageSettings != null);
            return default(bool);
        }
    }
}

