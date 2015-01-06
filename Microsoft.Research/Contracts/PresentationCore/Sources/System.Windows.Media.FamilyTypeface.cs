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

// File System.Windows.Media.FamilyTypeface.cs
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
  public partial class FamilyTypeface : MS.Internal.FontFace.IDeviceFont, MS.Internal.FontFace.ITypefaceMetrics
  {
    #region Methods and constructors
    public override bool Equals(Object o)
    {
      return default(bool);
    }

    public bool Equals(System.Windows.Media.FamilyTypeface typeface)
    {
      return default(bool);
    }

    public FamilyTypeface()
    {
    }

    public override int GetHashCode()
    {
      return default(int);
    }

    bool MS.Internal.FontFace.IDeviceFont.ContainsCharacter(int unicodeScalar)
    {
      return default(bool);
    }

    unsafe void MS.Internal.FontFace.IDeviceFont.GetAdvanceWidths(char* characterString, int characterLength, double emSize, int* pAdvances)
    {
    }
    #endregion

    #region Properties and indexers
    public IDictionary<System.Windows.Markup.XmlLanguage, string> AdjustedFaceNames
    {
      get
      {
        return default(IDictionary<System.Windows.Markup.XmlLanguage, string>);
      }
    }

    public double CapsHeight
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public CharacterMetricsDictionary DeviceFontCharacterMetrics
    {
      get
      {
        return default(CharacterMetricsDictionary);
      }
    }

    public string DeviceFontName
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    string MS.Internal.FontFace.IDeviceFont.Name
    {
      get
      {
        return default(string);
      }
    }

    StyleSimulations MS.Internal.FontFace.ITypefaceMetrics.StyleSimulations
    {
      get
      {
        return default(StyleSimulations);
      }
    }

    bool MS.Internal.FontFace.ITypefaceMetrics.Symbol
    {
      get
      {
        return default(bool);
      }
    }

    public System.Windows.FontStretch Stretch
    {
      get
      {
        return default(System.Windows.FontStretch);
      }
      set
      {
      }
    }

    public double StrikethroughPosition
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public double StrikethroughThickness
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public System.Windows.FontStyle Style
    {
      get
      {
        return default(System.Windows.FontStyle);
      }
      set
      {
      }
    }

    public double UnderlinePosition
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public double UnderlineThickness
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public System.Windows.FontWeight Weight
    {
      get
      {
        return default(System.Windows.FontWeight);
      }
      set
      {
      }
    }

    public double XHeight
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }
    #endregion
  }
}
