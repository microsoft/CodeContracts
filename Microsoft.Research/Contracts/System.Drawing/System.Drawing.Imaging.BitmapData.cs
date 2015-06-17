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

#region Assembly System.Drawing.dll, v4.0.30319
// C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.0\System.Drawing.dll
#endregion

using System;
using System.Runtime;

namespace System.Drawing.Imaging {
  // Summary:
  //     Specifies the attributes of a bitmap image. The System.Drawing.Imaging.BitmapData
  //     class is used by the Overload:System.Drawing.Bitmap.LockBits and System.Drawing.Bitmap.UnlockBits(System.Drawing.Imaging.BitmapData)
  //     methods of the System.Drawing.Bitmap class. Not inheritable.
  public sealed class BitmapData {
    // Summary:
    //     Initializes a new instance of the System.Drawing.Imaging.BitmapData class.
    //[TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
    //public BitmapData();

    // Summary:
    //     Gets or sets the pixel height of the System.Drawing.Bitmap object. Also sometimes
    //     referred to as the number of scan lines.
    //
    // Returns:
    //     The pixel height of the System.Drawing.Bitmap object.
    public int Height { get { return default(int); } set { } }
    //
    // Summary:
    //     Gets or sets the format of the pixel information in the System.Drawing.Bitmap
    //     object that returned this System.Drawing.Imaging.BitmapData object.
    //
    // Returns:
    //     A System.Drawing.Imaging.PixelFormat that specifies the format of the pixel
    //     information in the associated System.Drawing.Bitmap object.
    public PixelFormat PixelFormat { get { return default(PixelFormat); } set { } }
    //
    // Summary:
    //     Reserved. Do not use.
    //
    // Returns:
    //     Reserved. Do not use.
    public int Reserved { get { return default(int); } set { } }
    //
    // Summary:
    //     Gets or sets the address of the first pixel data in the bitmap. This can
    //     also be thought of as the first scan line in the bitmap.
    //
    // Returns:
    //     The address of the first pixel data in the bitmap.
    public IntPtr Scan0 { get { return default(IntPtr); } set { } }
    //
    // Summary:
    //     Gets or sets the stride width (also called scan width) of the System.Drawing.Bitmap
    //     object.
    //
    // Returns:
    //     The stride width, in bytes, of the System.Drawing.Bitmap object.
    public int Stride { get { return default(int); } set { } }
    //
    // Summary:
    //     Gets or sets the pixel width of the System.Drawing.Bitmap object. This can
    //     also be thought of as the number of pixels in one scan line.
    //
    // Returns:
    //     The pixel width of the System.Drawing.Bitmap object.
    public int Width { get { return default(int); } set { } }
  }
}
