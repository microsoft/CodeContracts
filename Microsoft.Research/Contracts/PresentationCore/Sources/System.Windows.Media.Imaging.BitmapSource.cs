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

// File System.Windows.Media.Imaging.BitmapSource.cs
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
  abstract public partial class BitmapSource : System.Windows.Media.ImageSource, System.Windows.Media.Composition.DUCE.IResource
  {
    #region Methods and constructors
    protected BitmapSource()
    {
    }

    protected void CheckIfSiteOfOrigin()
    {
    }

    public System.Windows.Media.Imaging.BitmapSource Clone()
    {
      return default(System.Windows.Media.Imaging.BitmapSource);
    }

    protected override void CloneCore(System.Windows.Freezable sourceFreezable)
    {
    }

    public System.Windows.Media.Imaging.BitmapSource CloneCurrentValue()
    {
      return default(System.Windows.Media.Imaging.BitmapSource);
    }

    protected override void CloneCurrentValueCore(System.Windows.Freezable sourceFreezable)
    {
    }

    public virtual new void CopyPixels(System.Windows.Int32Rect sourceRect, IntPtr buffer, int bufferSize, int stride)
    {
    }

    public virtual new void CopyPixels(System.Windows.Int32Rect sourceRect, Array pixels, int stride, int offset)
    {
    }

    public virtual new void CopyPixels(Array pixels, int stride, int offset)
    {
    }

    public static BitmapSource Create(int pixelWidth, int pixelHeight, double dpiX, double dpiY, System.Windows.Media.PixelFormat pixelFormat, BitmapPalette palette, Array pixels, int stride)
    {
      return default(BitmapSource);
    }

    public static BitmapSource Create(int pixelWidth, int pixelHeight, double dpiX, double dpiY, System.Windows.Media.PixelFormat pixelFormat, BitmapPalette palette, IntPtr buffer, int bufferSize, int stride)
    {
      return default(BitmapSource);
    }

    protected override bool FreezeCore(bool isChecking)
    {
      return default(bool);
    }

    protected override void GetAsFrozenCore(System.Windows.Freezable sourceFreezable)
    {
    }

    protected override void GetCurrentValueAsFrozenCore(System.Windows.Freezable sourceFreezable)
    {
    }

    int System.Windows.Media.Composition.DUCE.IResource.GetChannelCount()
    {
      return default(int);
    }
    #endregion

    #region Properties and indexers
    public virtual new double DpiX
    {
      get
      {
        return default(double);
      }
    }

    public virtual new double DpiY
    {
      get
      {
        return default(double);
      }
    }

    public virtual new System.Windows.Media.PixelFormat Format
    {
      get
      {
        return default(System.Windows.Media.PixelFormat);
      }
    }

    public override double Height
    {
      get
      {
        return default(double);
      }
    }

    public virtual new bool IsDownloading
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

    public virtual new BitmapPalette Palette
    {
      get
      {
        return default(BitmapPalette);
      }
    }

    public virtual new int PixelHeight
    {
      get
      {
        return default(int);
      }
    }

    public virtual new int PixelWidth
    {
      get
      {
        return default(int);
      }
    }

    public override double Width
    {
      get
      {
        return default(double);
      }
    }
    #endregion

    #region Events
    public virtual new event EventHandler<System.Windows.Media.ExceptionEventArgs> DecodeFailed
    {
      add
      {
      }
      remove
      {
      }
    }

    public virtual new event EventHandler DownloadCompleted
    {
      add
      {
      }
      remove
      {
      }
    }

    public virtual new event EventHandler<System.Windows.Media.ExceptionEventArgs> DownloadFailed
    {
      add
      {
      }
      remove
      {
      }
    }

    public virtual new event EventHandler<DownloadProgressEventArgs> DownloadProgress
    {
      add
      {
      }
      remove
      {
      }
    }
    #endregion
  }
}
