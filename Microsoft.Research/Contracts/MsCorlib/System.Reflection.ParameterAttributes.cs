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

namespace System.Reflection {
  // Summary:
  //     Defines the attributes that can be associated with a parameter. These are
  //     defined in CorHdr.h.
  public enum ParameterAttributes {
    // Summary:
    //     Specifies that there is no parameter attribute.
    None = 0,
    //
    // Summary:
    //     Specifies that the parameter is an input parameter.
    In = 1,
    //
    // Summary:
    //     Specifies that the parameter is an output parameter.
    Out = 2,
    //
    // Summary:
    //     Specifies that the parameter is a locale identifier (lcid).
    Lcid = 4,
    //
    // Summary:
    //     Specifies that the parameter is a return value.
    Retval = 8,
    //
    // Summary:
    //     Specifies that the parameter is optional.
    Optional = 16,
    //
    // Summary:
    //     Specifies that the parameter has a default value.
    HasDefault = 4096,
    //
    // Summary:
    //     Specifies that the parameter has field marshaling information.
    HasFieldMarshal = 8192,
    //
    // Summary:
    //     Reserved.
    Reserved3 = 16384,
    //
    // Summary:
    //     Reserved.
    Reserved4 = 32768,
    //
    // Summary:
    //     Specifies that the parameter is reserved.
    ReservedMask = 61440,
  }
}
