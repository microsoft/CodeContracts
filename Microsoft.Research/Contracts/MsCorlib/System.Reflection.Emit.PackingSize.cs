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

namespace System.Reflection.Emit {
  // Summary:
  //     Specifies one of two factors that determine the memory alignment of fields
  //     when a type is marshaled.
  [Serializable]
  [ComVisible(true)]
  public enum PackingSize {
    // Summary:
    //     The packing size is not specified.
    Unspecified = 0,
    //
    // Summary:
    //     The packing size is 1 byte.
    Size1 = 1,
    //
    // Summary:
    //     The packing size is 2 bytes.
    Size2 = 2,
    //
    // Summary:
    //     The packing size is 4 bytes.
    Size4 = 4,
    //
    // Summary:
    //     The packing size is 8 bytes.
    Size8 = 8,
    //
    // Summary:
    //     The packing size is 16 bytes.
    Size16 = 16,
    //
    // Summary:
    //     The packing size is 32 bytes.
    Size32 = 32,
    //
    // Summary:
    //     The packing size is 64 bytes.
    Size64 = 64,
    //
    // Summary:
    //     The packing size is 128 bytes.
    Size128 = 128,
  }
}
