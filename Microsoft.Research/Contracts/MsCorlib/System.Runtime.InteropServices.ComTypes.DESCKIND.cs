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
using System.Diagnostics.Contracts;

namespace System.Runtime.InteropServices.ComTypes {
  // Summary:
  //     Identifies the type description being bound to.
  [Serializable]
  public enum DESCKIND {
    // Summary:
    //     Indicates that no match was found.
    DESCKIND_NONE = 0,
    //
    // Summary:
    //     Indicates that a System.Runtime.InteropServices.FUNCDESC structure was returned.
    DESCKIND_FUNCDESC = 1,
    //
    // Summary:
    //     Indicates that a VARDESC was returned.
    DESCKIND_VARDESC = 2,
    //
    // Summary:
    //     Indicates that a TYPECOMP was returned.
    DESCKIND_TYPECOMP = 3,
    //
    // Summary:
    //     Indicates that an IMPLICITAPPOBJ was returned.
    DESCKIND_IMPLICITAPPOBJ = 4,
    //
    // Summary:
    //     Indicates an end-of-enumeration marker.
    DESCKIND_MAX = 5,
  }
}
