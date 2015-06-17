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

// File System.Windows.Media.Imaging.BitmapFrame.cs
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
  abstract public partial class BitmapFrame : BitmapSource, System.Windows.Markup.IUriContext
  {
    #region Methods and constructors
    protected BitmapFrame()
    {
    }

    public static System.Windows.Media.Imaging.BitmapFrame Create(BitmapSource source, BitmapSource thumbnail, BitmapMetadata metadata, System.Collections.ObjectModel.ReadOnlyCollection<System.Windows.Media.ColorContext> colorContexts)
    {
      return default(System.Windows.Media.Imaging.BitmapFrame);
    }

    public static System.Windows.Media.Imaging.BitmapFrame Create(BitmapSource source, BitmapSource thumbnail)
    {
      return default(System.Windows.Media.Imaging.BitmapFrame);
    }

    public static System.Windows.Media.Imaging.BitmapFrame Create(BitmapSource source)
    {
      return default(System.Windows.Media.Imaging.BitmapFrame);
    }

    public static System.Windows.Media.Imaging.BitmapFrame Create(Uri bitmapUri)
    {
      return default(System.Windows.Media.Imaging.BitmapFrame);
    }

    public static System.Windows.Media.Imaging.BitmapFrame Create(Uri bitmapUri, System.Net.Cache.RequestCachePolicy uriCachePolicy)
    {
      return default(System.Windows.Media.Imaging.BitmapFrame);
    }

    public static System.Windows.Media.Imaging.BitmapFrame Create(Uri bitmapUri, BitmapCreateOptions createOptions, BitmapCacheOption cacheOption, System.Net.Cache.RequestCachePolicy uriCachePolicy)
    {
      return default(System.Windows.Media.Imaging.BitmapFrame);
    }

    public static System.Windows.Media.Imaging.BitmapFrame Create(Uri bitmapUri, BitmapCreateOptions createOptions, BitmapCacheOption cacheOption)
    {
      return default(System.Windows.Media.Imaging.BitmapFrame);
    }

    public static System.Windows.Media.Imaging.BitmapFrame Create(Stream bitmapStream)
    {
      return default(System.Windows.Media.Imaging.BitmapFrame);
    }

    public static System.Windows.Media.Imaging.BitmapFrame Create(Stream bitmapStream, BitmapCreateOptions createOptions, BitmapCacheOption cacheOption)
    {
      return default(System.Windows.Media.Imaging.BitmapFrame);
    }

    public abstract InPlaceBitmapMetadataWriter CreateInPlaceBitmapMetadataWriter();
    #endregion

    #region Properties and indexers
    public abstract Uri BaseUri
    {
      get;
      set;
    }

    public abstract System.Collections.ObjectModel.ReadOnlyCollection<System.Windows.Media.ColorContext> ColorContexts
    {
      get;
    }

    public abstract BitmapDecoder Decoder
    {
      get;
    }

    public abstract BitmapSource Thumbnail
    {
      get;
    }
    #endregion
  }
}
