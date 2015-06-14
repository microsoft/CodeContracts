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

// File System.Windows.Media.Imaging.WriteableBitmap.cs
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
  sealed public partial class WriteableBitmap : BitmapSource
  {
    #region Methods and constructors
    public void AddDirtyRect(System.Windows.Int32Rect dirtyRect)
    {
    }

    public System.Windows.Media.Imaging.WriteableBitmap Clone()
    {
      return default(System.Windows.Media.Imaging.WriteableBitmap);
    }

    protected override void CloneCore(System.Windows.Freezable sourceFreezable)
    {
    }

    public System.Windows.Media.Imaging.WriteableBitmap CloneCurrentValue()
    {
      return default(System.Windows.Media.Imaging.WriteableBitmap);
    }

    protected override void CloneCurrentValueCore(System.Windows.Freezable sourceFreezable)
    {
    }

    protected override System.Windows.Freezable CreateInstanceCore()
    {
      return default(System.Windows.Freezable);
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

    public void Lock()
    {
    }

    public bool TryLock(System.Windows.Duration timeout)
    {
      return default(bool);
    }

    public void Unlock()
    {
    }

    public WriteableBitmap(int pixelWidth, int pixelHeight, double dpiX, double dpiY, System.Windows.Media.PixelFormat pixelFormat, BitmapPalette palette)
    {
    }

    public WriteableBitmap(BitmapSource source)
    {
    }

    public void WritePixels(System.Windows.Int32Rect sourceRect, Array sourceBuffer, int sourceBufferStride, int destinationX, int destinationY)
    {
    }

    public void WritePixels(System.Windows.Int32Rect sourceRect, IntPtr sourceBuffer, int sourceBufferSize, int sourceBufferStride, int destinationX, int destinationY)
    {
    }

    public void WritePixels(System.Windows.Int32Rect sourceRect, IntPtr buffer, int bufferSize, int stride)
    {
    }

    public void WritePixels(System.Windows.Int32Rect sourceRect, Array pixels, int stride, int offset)
    {
    }
    #endregion

    #region Properties and indexers
    public IntPtr BackBuffer
    {
      get
      {
        return default(IntPtr);
      }
      private set
      {
      }
    }

    public int BackBufferStride
    {
      get
      {
        return default(int);
      }
    }
    #endregion
  }
}
