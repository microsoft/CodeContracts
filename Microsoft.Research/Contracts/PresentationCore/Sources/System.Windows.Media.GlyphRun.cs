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

// File System.Windows.Media.GlyphRun.cs
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
  public partial class GlyphRun : System.Windows.Media.Composition.DUCE.IResource, System.ComponentModel.ISupportInitialize
  {
    #region Methods and constructors
    public Geometry BuildGeometry()
    {
      return default(Geometry);
    }

    public System.Windows.Rect ComputeAlignmentBox()
    {
      return default(System.Windows.Rect);
    }

    public System.Windows.Rect ComputeInkBoundingBox()
    {
      return default(System.Windows.Rect);
    }

    public System.Windows.Media.TextFormatting.CharacterHit GetCaretCharacterHitFromDistance(double distance, out bool isInside)
    {
      isInside = default(bool);

      return default(System.Windows.Media.TextFormatting.CharacterHit);
    }

    public double GetDistanceFromCaretCharacterHit(System.Windows.Media.TextFormatting.CharacterHit characterHit)
    {
      return default(double);
    }

    public System.Windows.Media.TextFormatting.CharacterHit GetNextCaretCharacterHit(System.Windows.Media.TextFormatting.CharacterHit characterHit)
    {
      return default(System.Windows.Media.TextFormatting.CharacterHit);
    }

    public System.Windows.Media.TextFormatting.CharacterHit GetPreviousCaretCharacterHit(System.Windows.Media.TextFormatting.CharacterHit characterHit)
    {
      return default(System.Windows.Media.TextFormatting.CharacterHit);
    }

    public GlyphRun(GlyphTypeface glyphTypeface, int bidiLevel, bool isSideways, double renderingEmSize, IList<ushort> glyphIndices, System.Windows.Point baselineOrigin, IList<double> advanceWidths, IList<System.Windows.Point> glyphOffsets, IList<char> characters, string deviceFontName, IList<ushort> clusterMap, IList<bool> caretStops, System.Windows.Markup.XmlLanguage language)
    {
    }

    public GlyphRun()
    {
    }

    void System.ComponentModel.ISupportInitialize.BeginInit()
    {
    }

    void System.ComponentModel.ISupportInitialize.EndInit()
    {
    }

    int System.Windows.Media.Composition.DUCE.IResource.GetChannelCount()
    {
      return default(int);
    }
    #endregion

    #region Properties and indexers
    public IList<double> AdvanceWidths
    {
      get
      {
        return default(IList<double>);
      }
      set
      {
      }
    }

    public System.Windows.Point BaselineOrigin
    {
      get
      {
        return default(System.Windows.Point);
      }
      set
      {
      }
    }

    public int BidiLevel
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public IList<bool> CaretStops
    {
      get
      {
        return default(IList<bool>);
      }
      set
      {
      }
    }

    public IList<char> Characters
    {
      get
      {
        return default(IList<char>);
      }
      set
      {
      }
    }

    public IList<ushort> ClusterMap
    {
      get
      {
        return default(IList<ushort>);
      }
      set
      {
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

    public double FontRenderingEmSize
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public IList<ushort> GlyphIndices
    {
      get
      {
        return default(IList<ushort>);
      }
      set
      {
      }
    }

    public IList<System.Windows.Point> GlyphOffsets
    {
      get
      {
        return default(IList<System.Windows.Point>);
      }
      set
      {
      }
    }

    public GlyphTypeface GlyphTypeface
    {
      get
      {
        return default(GlyphTypeface);
      }
      set
      {
      }
    }

    public bool IsHitTestable
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsSideways
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public System.Windows.Markup.XmlLanguage Language
    {
      get
      {
        return default(System.Windows.Markup.XmlLanguage);
      }
      set
      {
      }
    }
    #endregion
  }
}
