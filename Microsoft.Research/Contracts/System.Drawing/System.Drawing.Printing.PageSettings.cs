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

#if !SILVERLIGHT
using System;
using System.Drawing;

namespace System.Drawing.Printing
{
  public class PageSettings // : ICloneable
  {
    public PageSettings() { }

    public PageSettings(PrinterSettings printerSettings) { }

    // Summary:
    //     Gets the size of the page, taking into account the page orientation specified
    //     by the System.Drawing.Printing.PageSettings.Landscape property.
    //
    // Returns:
    //     A System.Drawing.Rectangle that represents the length and width, in hundredths
    //     of an inch, of the page.
    //
    // Exceptions:
    //   System.Drawing.Printing.InvalidPrinterException:
    //     The printer named in the System.Drawing.Printing.PrinterSettings.PrinterName
    //     property does not exist.
    //public Rectangle Bounds { get; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the page should be printed in color.
    //
    // Returns:
    //     true if the page should be printed in color; otherwise, false. The default
    //     is determined by the printer.
    //
    // Exceptions:
    //   System.Drawing.Printing.InvalidPrinterException:
    //     The printer named in the System.Drawing.Printing.PrinterSettings.PrinterName
    //     property does not exist.
    //public bool Color { get; set; }
    //
    // Summary:
    //     Gets the x-coordinate, in hundredths of an inch, of the hard margin at the
    //     left of the page.
    //
    // Returns:
    //     The x-coordinate, in hundredths of an inch, of the left-hand hard margin.
    //public float HardMarginX { get; }
    //
    // Summary:
    //     Gets the y-coordinate, in hundredths of an inch, of the hard margin at the
    //     top of the page.
    //
    // Returns:
    //     The y-coordinate, in hundredths of an inch, of the hard margin at the top
    //     of the page.
    //public float HardMarginY { get; }
    //
    // Summary:
    //     Gets or sets a value indicating whether the page is printed in landscape
    //     or portrait orientation.
    //
    // Returns:
    //     true if the page should be printed in landscape orientation; otherwise, false.
    //     The default is determined by the printer.
    //
    // Exceptions:
    //   System.Drawing.Printing.InvalidPrinterException:
    //     The printer named in the System.Drawing.Printing.PrinterSettings.PrinterName
    //     property does not exist.
    //public bool Landscape { get; set; }
    //
    // Summary:
    //     Gets or sets the margins for this page.
    //
    // Returns:
    //     A System.Drawing.Printing.Margins that represents the margins, in hundredths
    //     of an inch, for the page. The default is 1-inch margins on all sides.
    //
    // Exceptions:
    //   System.Drawing.Printing.InvalidPrinterException:
    //     The printer named in the System.Drawing.Printing.PrinterSettings.PrinterName
    //     property does not exist.
    //public Margins Margins { get; set; }
    //
    // Summary:
    //     Gets or sets the paper size for the page.
    //
    // Returns:
    //     A System.Drawing.Printing.PaperSize that represents the size of the paper.
    //     The default is the printer's default paper size.
    //
    // Exceptions:
    //   System.Drawing.Printing.InvalidPrinterException:
    //     The printer named in the System.Drawing.Printing.PrinterSettings.PrinterName
    //     property does not exist or there is no default printer installed.
    //public PaperSize PaperSize { get; set; }
    //
    // Summary:
    //     Gets or sets the page's paper source; for example, the printer's upper tray.
    //
    // Returns:
    //     A System.Drawing.Printing.PaperSource that specifies the source of the paper.
    //     The default is the printer's default paper source.
    //
    // Exceptions:
    //   System.Drawing.Printing.InvalidPrinterException:
    //     The printer named in the System.Drawing.Printing.PrinterSettings.PrinterName
    //     property does not exist or there is no default printer installed.
    //public PaperSource PaperSource { get; set; }
    //
    // Summary:
    //     Gets the bounds of the printable area of the page for the printer.
    //
    // Returns:
    //     A System.Drawing.RectangleF representing the length and width, in hundredths
    //     of an inch, of the area the printer is capable of printing in.
    //public RectangleF PrintableArea { get; }
    //
    // Summary:
    //     Gets or sets the printer resolution for the page.
    //
    // Returns:
    //     A System.Drawing.Printing.PrinterResolution that specifies the printer resolution
    //     for the page. The default is the printer's default resolution.
    //
    // Exceptions:
    //   System.Drawing.Printing.InvalidPrinterException:
    //     The printer named in the System.Drawing.Printing.PrinterSettings.PrinterName
    //     property does not exist or there is no default printer installed.
    //public PrinterResolution PrinterResolution { get; set; }
    //
    // Summary:
    //     Gets or sets the printer settings associated with the page.
    //
    // Returns:
    //     A System.Drawing.Printing.PrinterSettings that represents the printer settings
    //     associated with the page.
    //public PrinterSettings PrinterSettings { get; set; }

    //
    // Summary:
    //     Copies the relevant information from the System.Drawing.Printing.PageSettings
    //     to the specified DEVMODE structure.
    //
    // Parameters:
    //   hdevmode:
    //     The handle to a Win32 DEVMODE structure.
    //
    // Exceptions:
    //   System.Drawing.Printing.InvalidPrinterException:
    //     The printer named in the System.Drawing.Printing.PrinterSettings.PrinterName
    //     property does not exist or there is no default printer installed.
    //public void CopyToHdevmode(IntPtr hdevmode);
    //
    // Summary:
    //     Copies relevant information to the System.Drawing.Printing.PageSettings from
    //     the specified DEVMODE structure.
    //
    // Parameters:
    //   hdevmode:
    //     The handle to a Win32 DEVMODE structure.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The printer handle is not valid.
    //
    //   System.Drawing.Printing.InvalidPrinterException:
    //     The printer named in the System.Drawing.Printing.PrinterSettings.PrinterName
    //     property does not exist or there is no default printer installed.
    //public void SetHdevmode(IntPtr hdevmode);
  }
}
#endif