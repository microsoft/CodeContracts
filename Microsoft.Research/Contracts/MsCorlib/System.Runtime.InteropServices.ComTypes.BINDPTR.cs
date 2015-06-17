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

namespace System.Runtime.InteropServices.ComTypes {
  // Summary:
  //     Contains a pointer to a bound-to System.Runtime.InteropServices.FUNCDESC
  //     structure, System.Runtime.InteropServices.VARDESC structure, or an ITypeComp
  //     interface.
  public struct BINDPTR {
    // Summary:
    //     Represents a pointer to a System.Runtime.InteropServices.FUNCDESC structure.
    public IntPtr lpfuncdesc;
    //
    // Summary:
    //     Represents a pointer to an System.Runtime.InteropServices.ComTypes.ITypeComp
    //     interface.
    public IntPtr lptcomp;
    //
    // Summary:
    //     Represents a pointer to a System.Runtime.InteropServices.VARDESC structure.
    public IntPtr lpvardesc;
  }
}
