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
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using System.Runtime;
using System.Diagnostics.Contracts;

namespace System.Drawing.Printing
{
  // Summary:
  //     Specifies the printer's duplex setting.
  [Serializable]
  public enum Duplex
  {
    // Summary:
    //     The printer's default duplex setting.
    Default = -1,
    //
    // Summary:
    //     Single-sided printing.
    Simplex = 1,
    //
    // Summary:
    //     Double-sided, vertical printing.
    Vertical = 2,
    //
    // Summary:
    //     Double-sided, horizontal printing.
    Horizontal = 3,
  }


  // Summary:
  //     Specifies the part of the document to print.
  [Serializable]
  public enum PrintRange
  {
    // Summary:
    //     All pages are printed.
    AllPages = 0,
    //
    // Summary:
    //     The selected pages are printed.
    Selection = 1,
    //
    // Summary:
    //     The pages between System.Drawing.Printing.PrinterSettings.FromPage and System.Drawing.Printing.PrinterSettings.ToPage
    //     are printed.
    SomePages = 2,
    //
    // Summary:
    //     The currently displayed page is printed
    CurrentPage = 4194304,
  }

  public class PrinterSettings // : ICloneable
  {
    public PrinterSettings() { }

    extern public bool CanDuplex { get; }

    extern public bool Collate { get; set; }

    public short Copies
    {
      get
      {
        Contract.Ensures(Contract.Result<short>() >= 0);
        return 0;
      }
      set
      {
        Contract.Requires(value >= 0);
      }
    }

    public PageSettings DefaultPageSettings { get { Contract.Ensures(Contract.Result<PageSettings>() != null); return null; } }

    //public Duplex Duplex { get; set; }
    public int FromPage
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);
        return 0;
      }
      set
      {
        Contract.Requires(value >= 0);
      }
    }

    public static PrinterSettings.StringCollection InstalledPrinters 
    {
      get
      {
        Contract.Ensures(Contract.Result<PrinterSettings.StringCollection>() != null);

        return null;
      }
    }

    extern public bool IsDefaultPrinter { get; }

    extern public bool IsPlotter { get; }
    extern public bool IsValid { get; }

    public int LandscapeAngle
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);
        Contract.Ensures(Contract.Result<int>() <= 270);

        return 0;
      }
    }
    public int MaximumCopies
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);

        return 0;
      }
    }

    public int MaximumPage
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);
        return 0;
      }
      set
      {
        Contract.Requires(value >= 0);
      }
    }

    public int MinimumPage
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);
        return 0;
      }
      set
      {
        Contract.Requires(value >= 0);
      }
    }

    public PrinterSettings.PaperSizeCollection PaperSizes
    {
      get
      {
        Contract.Ensures(Contract.Result<PrinterSettings.PaperSizeCollection>() != null);

        return null;
      }
    }

    public PrinterSettings.PaperSourceCollection PaperSources
    {
      get
      {
        Contract.Ensures(Contract.Result<PrinterSettings.PaperSourceCollection>() != null);

        return null;
      }
    }

    public string PrinterName 
    {
      get { Contract.Ensures(Contract.Result<string>() != null); return null; }
      set { }
    }

    public PrinterSettings.PrinterResolutionCollection PrinterResolutions
    {
      get
      {
        Contract.Ensures(Contract.Result<PrinterSettings.PrinterResolutionCollection>() != null);

        return null;
      }
    }

    public string PrintFileName
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null);

        return null;
      }
      set
      {
        Contract.Requires(!string.IsNullOrEmpty(value));
      }
    }

    extern public PrintRange PrintRange { get; set; }
    extern public bool PrintToFile { get; set; }
    extern public bool SupportsColor { get; }
    public int ToPage
    {
      get
        {
      Contract.Ensures(Contract.Result<int>() >= 0);
      return 0;
  }
  set
  {
    Contract.Requires(value != 0);
  }
    }

    [Pure]
    public Graphics CreateMeasurementGraphics()
    {
      Contract.Ensures(Contract.Result<Graphics>() != null);

      return null;
    }

    [Pure]
    public Graphics CreateMeasurementGraphics(bool honorOriginAtMargins)
    {
      Contract.Ensures(Contract.Result<Graphics>() != null);

      return null;
    }

    [Pure]
    public Graphics CreateMeasurementGraphics(PageSettings pageSettings)
    {
      Contract.Requires(pageSettings != null);
      Contract.Ensures(Contract.Result<Graphics>() != null);

      return null;
    }

    [Pure]
    public Graphics CreateMeasurementGraphics(PageSettings pageSettings, bool honorOriginAtMargins)
    {
      Contract.Requires(pageSettings != null);
      Contract.Ensures(Contract.Result<Graphics>() != null);

      return null;
    }

    [Pure]
    public IntPtr GetHdevmode() { return default(IntPtr); }

    [Pure]
    public IntPtr GetHdevmode(PageSettings pageSettings)
    {
      Contract.Requires(pageSettings != null);

      return default(IntPtr);
    }

    [Pure]
    extern public IntPtr GetHdevnames();

    [Pure]
    public bool IsDirectPrintingSupported(Image image)
    {
      Contract.Requires(image != null);

      return false;
    }

    [Pure]
    public bool IsDirectPrintingSupported(ImageFormat imageFormat)
    {
      Contract.Requires(imageFormat != null);

      return false;
    }

    [Pure]
    public void SetHdevmode(IntPtr hdevmode)
    {
      Contract.Requires(hdevmode != IntPtr.Zero);
    }

    [Pure]
    public void SetHdevnames(IntPtr hdevnames)
    {
      Contract.Requires(hdevnames != IntPtr.Zero);
    }
    // Summary:
    //     Contains a collection of System.Drawing.Printing.PaperSize objects.
    public class PaperSizeCollection // : ICollection, IEnumerable
    {
      /*
      public PaperSizeCollection(PaperSize[] array);

      public int Count { get; }

      public virtual PaperSize this[int index] { get; }

      public int Add(PaperSize paperSize);

      public IEnumerator GetEnumerator();
       */
    }

    public class PaperSourceCollection //: ICollection, IEnumerable
    {
      /*
      public PaperSourceCollection(PaperSource[] array);

      public int Count { get; }
      public virtual PaperSource this[int index] { get; }

      public int Add(PaperSource paperSource);
      public void CopyTo(PaperSource[] paperSources, int index);
      public IEnumerator GetEnumerator();
       */
    }

    public class PrinterResolutionCollection // : ICollection, IEnumerable
    {
      /*
      public PrinterResolutionCollection(PrinterResolution[] array);

      public int Count { get; }
      public virtual PrinterResolution this[int index] { get; }
      public int Add(PrinterResolution printerResolution);
      public void CopyTo(PrinterResolution[] printerResolutions, int index);
      public IEnumerator GetEnumerator();
       */
    }

    public class StringCollection // : ICollection, IEnumerable
    {
      /*
      public StringCollection(string[] array);

      public int Count { get; }
      public virtual string this[int index] { get; }

      public int Add(string value);
      public void CopyTo(string[] strings, int index);
       */
      public IEnumerator GetEnumerator()
      {
        Contract.Ensures(Contract.Result<IEnumerator>() != null);

        return null;
      }
    }
  }
}
