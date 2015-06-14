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
  //     Defines the valid calling conventions for a method.
  [Serializable]
  [Flags]
  [ComVisible(true)]
  public enum CallingConventions {
    // Summary:
    //     Specifies the default calling convention as determined by the common language
    //     runtime. Use this calling convention for static methods. For instance or
    //     virtual methods use HasThis.
    Standard = 1,
    //
    // Summary:
    //     Specifies the calling convention for methods with variable arguments.
    VarArgs = 2,
    //
    // Summary:
    //     Specifies that either the Standard or the VarArgs calling convention may
    //     be used.
    Any = 3,
    //
    // Summary:
    //     Specifies an instance or virtual method (not a static method). At run-time,
    //     the called method is passed a pointer to the target object as its first argument
    //     (the this pointer). The signature stored in metadata does not include the
    //     type of this first argument, because the method is known and its owner class
    //     can be discovered from metadata.
    HasThis = 32,
    //
    // Summary:
    //     Specifies that the signature is a function-pointer signature, representing
    //     a call to an instance or virtual method (not a static method). If ExplicitThis
    //     is set, HasThis must also be set. The first argument passed to the called
    //     method is still a this pointer, but the type of the first argument is now
    //     unknown. Therefore, a token that describes the type (or class) of the this
    //     pointer is explicitly stored into its metadata signature.
    ExplicitThis = 64,
  }
}
