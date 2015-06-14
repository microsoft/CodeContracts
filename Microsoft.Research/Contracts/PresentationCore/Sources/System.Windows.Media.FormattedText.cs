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

// File System.Windows.Media.FormattedText.cs
// Automatically generated contract file.
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics.Contracts;
using System;

// Disable the "this variable is not used" warning as every field would imply it.
#pragma warning disable 0414
// Disable the "this variable is never assigned to".
#pragma warning disable 0067
// Disable the "this event is never assigned to".
#pragma warning disable 0649
// Disable the "this variable is never used".
#pragma warning disable 0169
// Disable the "new keyword not required" warning.
#pragma warning disable 0109
// Disable the "extern without DllImport" warning.
#pragma warning disable 0626
// Disable the "could hide other member" warning, can happen on certain properties.
#pragma warning disable 0108


namespace System.Windows.Media
{
  public partial class FormattedText
  {
    #region Methods and constructors
    public Geometry BuildGeometry(System.Windows.Point origin)
    {
      return default(Geometry);
    }

    public Geometry BuildHighlightGeometry(System.Windows.Point origin)
    {
      return default(Geometry);
    }

    public Geometry BuildHighlightGeometry(System.Windows.Point origin, int startIndex, int count)
    {
      return default(Geometry);
    }

    public FormattedText(string textToFormat, System.Globalization.CultureInfo culture, System.Windows.FlowDirection flowDirection, Typeface typeface, double emSize, Brush foreground)
    {
    }

    public FormattedText(string textToFormat, System.Globalization.CultureInfo culture, System.Windows.FlowDirection flowDirection, Typeface typeface, double emSize, Brush foreground, NumberSubstitution numberSubstitution)
    {
    }

    public FormattedText(string textToFormat, System.Globalization.CultureInfo culture, System.Windows.FlowDirection flowDirection, Typeface typeface, double emSize, Brush foreground, NumberSubstitution numberSubstitution, TextFormattingMode textFormattingMode)
    {
    }

    public double[] GetMaxTextWidths()
    {
      return default(double[]);
    }

    public void SetCulture(System.Globalization.CultureInfo culture)
    {
    }

    public void SetCulture(System.Globalization.CultureInfo culture, int startIndex, int count)
    {
    }

    public void SetFontFamily(string fontFamily)
    {
    }

    public void SetFontFamily(FontFamily fontFamily, int startIndex, int count)
    {
    }

    public void SetFontFamily(string fontFamily, int startIndex, int count)
    {
    }

    public void SetFontFamily(FontFamily fontFamily)
    {
    }

    public void SetFontSize(double emSize)
    {
    }

    public void SetFontSize(double emSize, int startIndex, int count)
    {
    }

    public void SetFontStretch(System.Windows.FontStretch stretch, int startIndex, int count)
    {
    }

    public void SetFontStretch(System.Windows.FontStretch stretch)
    {
    }

    public void SetFontStyle(System.Windows.FontStyle style, int startIndex, int count)
    {
    }

    public void SetFontStyle(System.Windows.FontStyle style)
    {
    }

    public void SetFontTypeface(Typeface typeface)
    {
    }

    public void SetFontTypeface(Typeface typeface, int startIndex, int count)
    {
    }

    public void SetFontWeight(System.Windows.FontWeight weight)
    {
    }

    public void SetFontWeight(System.Windows.FontWeight weight, int startIndex, int count)
    {
    }

    public void SetForegroundBrush(Brush foregroundBrush)
    {
    }

    public void SetForegroundBrush(Brush foregroundBrush, int startIndex, int count)
    {
    }

    public void SetMaxTextWidths(double[] maxTextWidths)
    {
    }

    public void SetNumberSubstitution(NumberSubstitution numberSubstitution)
    {
    }

    public void SetNumberSubstitution(NumberSubstitution numberSubstitution, int startIndex, int count)
    {
    }

    public void SetTextDecorations(System.Windows.TextDecorationCollection textDecorations, int startIndex, int count)
    {
    }

    public void SetTextDecorations(System.Windows.TextDecorationCollection textDecorations)
    {
    }
    #endregion

    #region Properties and indexers
    public double Baseline
    {
      get
      {
        return default(double);
      }
    }

    public double Extent
    {
      get
      {
        return default(double);
      }
    }

    public System.Windows.FlowDirection FlowDirection
    {
      get
      {
        return default(System.Windows.FlowDirection);
      }
      set
      {
      }
    }

    public double Height
    {
      get
      {
        Contract.Ensures(Contract.Result<double>() >= 0);
        return default(double);
      }
    }

    public double LineHeight
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public int MaxLineCount
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public double MaxTextHeight
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public double MaxTextWidth
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public double MinWidth
    {
      get
      {
        return default(double);
      }
    }

    public double OverhangAfter
    {
      get
      {
        return default(double);
      }
    }

    public double OverhangLeading
    {
      get
      {
        return default(double);
      }
    }

    public double OverhangTrailing
    {
      get
      {
        return default(double);
      }
    }

    public string Text
    {
      get
      {
        return default(string);
      }
    }

    public System.Windows.TextAlignment TextAlignment
    {
      get
      {
        return default(System.Windows.TextAlignment);
      }
      set
      {
      }
    }

    public System.Windows.TextTrimming Trimming
    {
      get
      {
        return default(System.Windows.TextTrimming);
      }
      set
      {
      }
    }

    public double Width
    {
      get
      {
          Contract.Ensures(Contract.Result<double>() >= 0);
          return default(double);
      }
    }

    public double WidthIncludingTrailingWhitespace
    {
      get
      {
        return default(double);
      }
    }
    #endregion
  }
}
