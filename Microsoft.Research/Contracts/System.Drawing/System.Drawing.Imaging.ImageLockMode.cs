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

namespace System.Drawing.Imaging {
  // Summary:
  //     Specifies flags that are passed to the flags parameter of the Overload:System.Drawing.Bitmap.LockBits
  //     method. The Overload:System.Drawing.Bitmap.LockBits method locks a portion
  //     of an image so that you can read or write the pixel data.
  public enum ImageLockMode {
    // Summary:
    //     Specifies that a portion of the image is locked for reading.
    ReadOnly = 1,
    //
    // Summary:
    //     Specifies that a portion of the image is locked for writing.
    WriteOnly = 2,
    //
    // Summary:
    //     Specifies that a portion of the image is locked for reading or writing.
    ReadWrite = 3,
    //
    // Summary:
    //     Specifies that the buffer used for reading or writing pixel data is allocated
    //     by the user. If this flag is set, the flags parameter of the Overload:System.Drawing.Bitmap.LockBits
    //     method serves as an input parameter (and possibly as an output parameter).
    //     If this flag is cleared, then the flags parameter serves only as an output
    //     parameter.
    UserInputBuffer = 4,
  }
}
