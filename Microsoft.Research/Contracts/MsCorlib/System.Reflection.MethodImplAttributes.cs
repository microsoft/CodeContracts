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
  //     Specifies flags for the attributes of a method implementation.
  public enum MethodImplAttributes {
    // Summary:
    //     Specifies that the method implementation is managed, otherwise unmanaged.
    Managed = 0,
    //
    // Summary:
    //     Specifies that the method implementation is in Microsoft intermediate language
    //     (MSIL).
    IL = 0,
    //
    // Summary:
    //     Specifies that the method implementation is native.
    Native = 1,
    //
    // Summary:
    //     Specifies that the method implementation is in Optimized Intermediate Language
    //     (OPTIL).
    OPTIL = 2,
    //
    // Summary:
    //     Specifies that the method implementation is provided by the runtime.
    Runtime = 3,
    //
    // Summary:
    //     Specifies flags about code type.
    CodeTypeMask = 3,
    //
    // Summary:
    //     Specifies whether the code is managed or unmanaged.
    ManagedMask = 4,
    //
    // Summary:
    //     Specifies that the method implementation is unmanaged, otherwise managed.
    Unmanaged = 4,
    //
    // Summary:
    //     Specifies that the method cannot be inlined.
    NoInlining = 8,
    //
    // Summary:
    //     Specifies that the method is not defined.
    ForwardRef = 16,
    //
    // Summary:
    //     Specifies that the method is single-threaded through the body. Static methods
    //     (Shared in Visual Basic) lock on the type, while instance methods lock on
    //     the instance. You can also use the C# lock statement or the Visual Basic
    //     Lock function for this purpose.
    Synchronized = 32,
    //
    // Summary:
    //     Specifies that the method signature is exported exactly as declared.
    PreserveSig = 128,
    //
    // Summary:
    //     Specifies an internal call.
    InternalCall = 4096,
    //
    // Summary:
    //     Specifies a range check value.
    MaxMethodImplVal = 65535,
  }
}
