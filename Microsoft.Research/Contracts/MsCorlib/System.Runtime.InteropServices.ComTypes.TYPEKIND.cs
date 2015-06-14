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

namespace System.Runtime.InteropServices.ComTypes {
  // Summary:
  //     Specifies various types of data and functions.
  [Serializable]
  public enum TYPEKIND {
    // Summary:
    //     A set of enumerators.
    TKIND_ENUM = 0,
    //
    // Summary:
    //     A structure with no methods.
    TKIND_RECORD = 1,
    //
    // Summary:
    //     A module that can have only static functions and data (for example, a DLL).
    TKIND_MODULE = 2,
    //
    // Summary:
    //     A type that has virtual functions, all of which are pure.
    TKIND_INTERFACE = 3,
    //
    // Summary:
    //     A set of methods and properties that are accessible through IDispatch::Invoke.
    //     By default, dual interfaces return TKIND_DISPATCH.
    TKIND_DISPATCH = 4,
    //
    // Summary:
    //     A set of implemented components interfaces.
    TKIND_COCLASS = 5,
    //
    // Summary:
    //     A type that is an alias for another type.
    TKIND_ALIAS = 6,
    //
    // Summary:
    //     A union of all members that have an offset of zero.
    TKIND_UNION = 7,
    //
    // Summary:
    //     End-of-enumeration marker.
    TKIND_MAX = 8,
  }
}
