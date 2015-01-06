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

// File System.Windows.Media.Imaging.BitmapImage.cs
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


namespace System.Windows.Media.Imaging
{
  sealed public partial class BitmapImage : BitmapSource, System.ComponentModel.ISupportInitialize, System.Windows.Markup.IUriContext
  {
    #region Methods and constructors
    public void BeginInit()
    {
    }

    public BitmapImage()
    {
    }

    public BitmapImage(Uri uriSource)
    {
    }

    public BitmapImage(Uri uriSource, System.Net.Cache.RequestCachePolicy uriCachePolicy)
    {
    }

    public System.Windows.Media.Imaging.BitmapImage Clone()
    {
      return default(System.Windows.Media.Imaging.BitmapImage);
    }

    protected override void CloneCore(System.Windows.Freezable source)
    {
    }

    public System.Windows.Media.Imaging.BitmapImage CloneCurrentValue()
    {
      return default(System.Windows.Media.Imaging.BitmapImage);
    }

    protected override void CloneCurrentValueCore(System.Windows.Freezable source)
    {
    }

    protected override System.Windows.Freezable CreateInstanceCore()
    {
      return default(System.Windows.Freezable);
    }

    public void EndInit()
    {
    }

    protected override void GetAsFrozenCore(System.Windows.Freezable source)
    {
    }

    protected override void GetCurrentValueAsFrozenCore(System.Windows.Freezable source)
    {
    }
    #endregion

    #region Properties and indexers
    public Uri BaseUri
    {
      get
      {
        return default(Uri);
      }
      set
      {
      }
    }

    public BitmapCacheOption CacheOption
    {
      get
      {
        return default(BitmapCacheOption);
      }
      set
      {
      }
    }

    public BitmapCreateOptions CreateOptions
    {
      get
      {
        return default(BitmapCreateOptions);
      }
      set
      {
      }
    }

    public int DecodePixelHeight
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public int DecodePixelWidth
    {
      get
      {
        return default(int);
      }
      set
      {
      }
    }

    public override bool IsDownloading
    {
      get
      {
        return default(bool);
      }
    }

    public override System.Windows.Media.ImageMetadata Metadata
    {
      get
      {
        return default(System.Windows.Media.ImageMetadata);
      }
    }

    public Rotation Rotation
    {
      get
      {
        return default(Rotation);
      }
      set
      {
      }
    }

    public System.Windows.Int32Rect SourceRect
    {
      get
      {
        return default(System.Windows.Int32Rect);
      }
      set
      {
      }
    }

    public Stream StreamSource
    {
      get
      {
        return default(Stream);
      }
      set
      {
      }
    }

    public System.Net.Cache.RequestCachePolicy UriCachePolicy
    {
      get
      {
        return default(System.Net.Cache.RequestCachePolicy);
      }
      set
      {
      }
    }

    public Uri UriSource
    {
      get
      {
        return default(Uri);
      }
      set
      {
      }
    }
    #endregion

    #region Fields
    public readonly static System.Windows.DependencyProperty CacheOptionProperty;
    public readonly static System.Windows.DependencyProperty CreateOptionsProperty;
    public readonly static System.Windows.DependencyProperty DecodePixelHeightProperty;
    public readonly static System.Windows.DependencyProperty DecodePixelWidthProperty;
    public readonly static System.Windows.DependencyProperty RotationProperty;
    public readonly static System.Windows.DependencyProperty SourceRectProperty;
    public readonly static System.Windows.DependencyProperty StreamSourceProperty;
    public readonly static System.Windows.DependencyProperty UriCachePolicyProperty;
    public readonly static System.Windows.DependencyProperty UriSourceProperty;
    #endregion
  }
}
