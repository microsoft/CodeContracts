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

// File System.Windows.Documents.Glyphs.cs
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


namespace System.Windows.Documents
{
  sealed public partial class Glyphs : System.Windows.FrameworkElement, System.Windows.Markup.IUriContext
  {
    #region Methods and constructors
    protected override System.Windows.Size ArrangeOverride(System.Windows.Size finalSize)
    {
      return default(System.Windows.Size);
    }

    public Glyphs()
    {
    }

    protected override System.Windows.Size MeasureOverride(System.Windows.Size constraint)
    {
      return default(System.Windows.Size);
    }

    protected override void OnRender(System.Windows.Media.DrawingContext context)
    {
    }

    public System.Windows.Media.GlyphRun ToGlyphRun()
    {
      return default(System.Windows.Media.GlyphRun);
    }
    #endregion

    #region Properties and indexers
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

    public string CaretStops
    {
      get
      {
        return default(string);
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

    public System.Windows.Media.Brush Fill
    {
      get
      {
        return default(System.Windows.Media.Brush);
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

    public string Indices
    {
      get
      {
        return default(string);
      }
      set
      {
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

    public double OriginX
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public double OriginY
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public System.Windows.Media.StyleSimulations StyleSimulations
    {
      get
      {
        return default(System.Windows.Media.StyleSimulations);
      }
      set
      {
      }
    }

    Uri System.Windows.Markup.IUriContext.BaseUri
    {
      get
      {
        return default(Uri);
      }
      set
      {
      }
    }

    public string UnicodeString
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }
    #endregion

    #region Fields
    public readonly static System.Windows.DependencyProperty BidiLevelProperty;
    public readonly static System.Windows.DependencyProperty CaretStopsProperty;
    public readonly static System.Windows.DependencyProperty DeviceFontNameProperty;
    public readonly static System.Windows.DependencyProperty FillProperty;
    public readonly static System.Windows.DependencyProperty FontRenderingEmSizeProperty;
    public readonly static System.Windows.DependencyProperty FontUriProperty;
    public readonly static System.Windows.DependencyProperty IndicesProperty;
    public readonly static System.Windows.DependencyProperty IsSidewaysProperty;
    public readonly static System.Windows.DependencyProperty OriginXProperty;
    public readonly static System.Windows.DependencyProperty OriginYProperty;
    public readonly static System.Windows.DependencyProperty StyleSimulationsProperty;
    public readonly static System.Windows.DependencyProperty UnicodeStringProperty;
    #endregion
  }
}
