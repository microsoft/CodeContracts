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
namespace System.Runtime.InteropServices {
  // Summary:
  //     Dictates which character set marshaled strings should use.
  [Serializable]
  [ComVisible(true)]
  public enum CharSet {
    // Summary:
    //     This value is obsolete and has the same behavior as System.Runtime.InteropServices.CharSet.Ansi.
    None = 1,
    //
    // Summary:
    //     Marshal strings as multiple-byte character strings.
    Ansi = 2,
    //
    // Summary:
    //     Marshal strings as Unicode 2-byte characters.
    Unicode = 3,
    //
    // Summary:
    //     Automatically marshal strings appropriately for the target operating system.
    //     The default is System.Runtime.InteropServices.CharSet.Unicode on Windows
    //     NT, Windows 2000, Windows XP, and the Windows Server 2003 family; the default
    //     is System.Runtime.InteropServices.CharSet.Ansi on Windows 98 and Windows
    //     Me. Although the common language runtime default is System.Runtime.InteropServices.CharSet.Auto,
    //     languages may override this default. For example, by default C# marks all
    //     methods and types as System.Runtime.InteropServices.CharSet.Ansi.
    Auto = 4,
  }
}
