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
  //     Retrieves the mapping of an interface into the actual methods on a class
  //     that implements that interface.
  [ComVisible(true)]
  public struct InterfaceMapping {
    // Summary:
    //     Shows the methods that are defined on the interface.
    [ComVisible(true)]
    public MethodInfo[] InterfaceMethods;
    //
    // Summary:
    //     Shows the type that represents the interface.
    [ComVisible(true)]
    public Type InterfaceType;
    //
    // Summary:
    //     Shows the methods that implement the interface.
    [ComVisible(true)]
    public MethodInfo[] TargetMethods;
    //
    // Summary:
    //     Represents the type that was used to create the interface mapping.
    [ComVisible(true)]
    public Type TargetType;
  }
}

