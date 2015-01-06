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
using System.Data.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace System.Runtime.InteropServices {
  // Summary:
  //     Specifies the calling convention required to call methods implemented in
  //     unmanaged code.
  [Serializable]
  [ComVisible(true)]
  public enum CallingConvention {
    // Summary:
    //     This member is not actually a calling convention, but instead uses the default
    //     platform calling convention. For example, on Windows the default is System.Runtime.InteropServices.CallingConvention.StdCall
    //     and on Windows CE.NET it is System.Runtime.InteropServices.CallingConvention.Cdecl.
    Winapi = 1,
    //
    // Summary:
    //     The caller cleans the stack. This enables calling functions with varargs,
    //     which makes it appropriate to use for methods that accept a variable number
    //     of parameters, such as Printf.
    Cdecl = 2,
    //
    // Summary:
    //     The callee cleans the stack. This is the default convention for calling unmanaged
    //     functions with platform invoke.
    StdCall = 3,
    //
    // Summary:
    //     The first parameter is the this pointer and is stored in register ECX. Other
    //     parameters are pushed on the stack. This calling convention is used to call
    //     methods on classes exported from an unmanaged DLL.
    ThisCall = 4,
    //
    // Summary:
    //     This calling convention is not supported.
    FastCall = 5,
  }
}