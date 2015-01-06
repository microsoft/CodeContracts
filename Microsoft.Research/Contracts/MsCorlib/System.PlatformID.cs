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

using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;

namespace System {
  // Summary:
  //     Identifies the operating system, or platform, supported by an assembly.
  [Serializable]
  [ComVisible(true)]
  public enum PlatformID {
    // Summary:
    //     The operating system is Win32s. Win32s is a layer that runs on 16-bit versions
    //     of Windows to provide access to 32-bit applications.
    Win32S = 0,
    //
    // Summary:
    //     The operating system is Windows 95 or later.
    Win32Windows = 1,
    //
    // Summary:
    //     The operating system is Windows NT or later.
    Win32NT = 2,
    //
    // Summary:
    //     The operating system is Windows CE.
    WinCE = 3,
    //
    // Summary:
    //     The operating system is Unix.
    Unix = 4,
  }
}
