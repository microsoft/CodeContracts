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

// File System.Windows.Media.GlyphTypeface.cs
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
  public partial class GlyphTypeface : MS.Internal.FontFace.ITypefaceMetrics, System.ComponentModel.ISupportInitialize
  {
    #region Methods and constructors
    public byte[] ComputeSubset(ICollection<ushort> glyphs)
    {
      return default(byte[]);
    }

    public override bool Equals(Object o)
    {
      return default(bool);
    }

    public Stream GetFontStream()
    {
      return default(Stream);
    }

    public Geometry GetGlyphOutline(ushort glyphIndex, double renderingEmSize, double hintingEmSize)
    {
      return default(Geometry);
    }

    public override int GetHashCode()
    {
      return default(int);
    }

    public GlyphTypeface()
    {
    }

    public GlyphTypeface(Uri typefaceSource)
    {
    }

    public GlyphTypeface(Uri typefaceSource, StyleSimulations styleSimulations)
    {
    }

    void System.ComponentModel.ISupportInitialize.BeginInit()
    {
    }

    void System.ComponentModel.ISupportInitialize.EndInit()
    {
    }
    #endregion

    #region Properties and indexers
    public IDictionary<ushort, double> AdvanceHeights
    {
      get
      {
        return default(IDictionary<ushort, double>);
      }
    }

    public IDictionary<ushort, double> AdvanceWidths
    {
      get
      {
        return default(IDictionary<ushort, double>);
      }
    }

    public double Baseline
    {
      get
      {
        return default(double);
      }
    }

    public IDictionary<ushort, double> BottomSideBearings
    {
      get
      {
        return default(IDictionary<ushort, double>);
      }
    }

    public double CapsHeight
    {
      get
      {
        return default(double);
      }
    }

    public IDictionary<int, ushort> CharacterToGlyphMap
    {
      get
      {
        return default(IDictionary<int, ushort>);
      }
    }

    public IDictionary<System.Globalization.CultureInfo, string> Copyrights
    {
      get
      {
        return default(IDictionary<System.Globalization.CultureInfo, string>);
      }
    }

    public IDictionary<System.Globalization.CultureInfo, string> Descriptions
    {
      get
      {
        return default(IDictionary<System.Globalization.CultureInfo, string>);
      }
    }

    public IDictionary<System.Globalization.CultureInfo, string> DesignerNames
    {
      get
      {
        return default(IDictionary<System.Globalization.CultureInfo, string>);
      }
    }

    public IDictionary<System.Globalization.CultureInfo, string> DesignerUrls
    {
      get
      {
        return default(IDictionary<System.Globalization.CultureInfo, string>);
      }
    }

    public IDictionary<ushort, double> DistancesFromHorizontalBaselineToBlackBoxBottom
    {
      get
      {
        return default(IDictionary<ushort, double>);
      }
    }

    public FontEmbeddingRight EmbeddingRights
    {
      get
      {
        return default(FontEmbeddingRight);
      }
    }

    public IDictionary<System.Globalization.CultureInfo, string> FaceNames
    {
      get
      {
        return default(IDictionary<System.Globalization.CultureInfo, string>);
      }
    }

    public IDictionary<System.Globalization.CultureInfo, string> FamilyNames
    {
      get
      {
        return default(IDictionary<System.Globalization.CultureInfo, string>);
      }
    }

    public Uri FontUri
    {
      get
      {
        return default(Uri);
      }
      set
      {
      }
    }

    public int GlyphCount
    {
      get
      {
        return default(int);
      }
    }

    public double Height
    {
      get
      {
        return default(double);
      }
    }

    public IDictionary<ushort, double> LeftSideBearings
    {
      get
      {
        return default(IDictionary<ushort, double>);
      }
    }

    public IDictionary<System.Globalization.CultureInfo, string> LicenseDescriptions
    {
      get
      {
        return default(IDictionary<System.Globalization.CultureInfo, string>);
      }
    }

    public IDictionary<System.Globalization.CultureInfo, string> ManufacturerNames
    {
      get
      {
        return default(IDictionary<System.Globalization.CultureInfo, string>);
      }
    }

    IDictionary<System.Windows.Markup.XmlLanguage, string> MS.Internal.FontFace.ITypefaceMetrics.AdjustedFaceNames
    {
      get
      {
        return default(IDictionary<System.Windows.Markup.XmlLanguage, string>);
      }
    }

    double MS.Internal.FontFace.ITypefaceMetrics.CapsHeight
    {
      get
      {
        return default(double);
      }
    }

    double MS.Internal.FontFace.ITypefaceMetrics.StrikethroughPosition
    {
      get
      {
        return default(double);
      }
    }

    double MS.Internal.FontFace.ITypefaceMetrics.StrikethroughThickness
    {
      get
      {
        return default(double);
      }
    }

    bool MS.Internal.FontFace.ITypefaceMetrics.Symbol
    {
      get
      {
        return default(bool);
      }
    }

    double MS.Internal.FontFace.ITypefaceMetrics.UnderlinePosition
    {
      get
      {
        return default(double);
      }
    }

    double MS.Internal.FontFace.ITypefaceMetrics.UnderlineThickness
    {
      get
      {
        return default(double);
      }
    }

    double MS.Internal.FontFace.ITypefaceMetrics.XHeight
    {
      get
      {
        return default(double);
      }
    }

    public IDictionary<ushort, double> RightSideBearings
    {
      get
      {
        return default(IDictionary<ushort, double>);
      }
    }

    public IDictionary<System.Globalization.CultureInfo, string> SampleTexts
    {
      get
      {
        return default(IDictionary<System.Globalization.CultureInfo, string>);
      }
    }

    public System.Windows.FontStretch Stretch
    {
      get
      {
        return default(System.Windows.FontStretch);
      }
    }

    public double StrikethroughPosition
    {
      get
      {
        return default(double);
      }
    }

    public double StrikethroughThickness
    {
      get
      {
        return default(double);
      }
    }

    public System.Windows.FontStyle Style
    {
      get
      {
        return default(System.Windows.FontStyle);
      }
    }

    public StyleSimulations StyleSimulations
    {
      get
      {
        return default(StyleSimulations);
      }
      set
      {
      }
    }

    public bool Symbol
    {
      get
      {
        return default(bool);
      }
    }

    public IDictionary<ushort, double> TopSideBearings
    {
      get
      {
        return default(IDictionary<ushort, double>);
      }
    }

    public IDictionary<System.Globalization.CultureInfo, string> Trademarks
    {
      get
      {
        return default(IDictionary<System.Globalization.CultureInfo, string>);
      }
    }

    public double UnderlinePosition
    {
      get
      {
        return default(double);
      }
    }

    public double UnderlineThickness
    {
      get
      {
        return default(double);
      }
    }

    public IDictionary<System.Globalization.CultureInfo, string> VendorUrls
    {
      get
      {
        return default(IDictionary<System.Globalization.CultureInfo, string>);
      }
    }

    public double Version
    {
      get
      {
        return default(double);
      }
    }

    public IDictionary<System.Globalization.CultureInfo, string> VersionStrings
    {
      get
      {
        return default(IDictionary<System.Globalization.CultureInfo, string>);
      }
    }

    public System.Windows.FontWeight Weight
    {
      get
      {
        return default(System.Windows.FontWeight);
      }
    }

    public IDictionary<System.Globalization.CultureInfo, string> Win32FaceNames
    {
      get
      {
        return default(IDictionary<System.Globalization.CultureInfo, string>);
      }
    }

    public IDictionary<System.Globalization.CultureInfo, string> Win32FamilyNames
    {
      get
      {
        return default(IDictionary<System.Globalization.CultureInfo, string>);
      }
    }

    public double XHeight
    {
      get
      {
        return default(double);
      }
    }
    #endregion
  }
}
