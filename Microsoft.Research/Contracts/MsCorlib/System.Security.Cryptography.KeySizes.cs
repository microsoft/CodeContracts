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

namespace System.Security.Cryptography {
  // Summary:
  //     Determines the set of valid key sizes for the symmetric cryptographic algorithms.
  [ComVisible(true)]
  public sealed class KeySizes {
    // Summary:
    //     Initializes a new instance of the System.Security.Cryptography.KeySizes class
    //     with the specified key values.
    //
    // Parameters:
    //   minSize:
    //     The minimum valid key size.
    //
    //   maxSize:
    //     The maximum valid key size.
    //
    //   skipSize:
    //     The interval between valid key sizes.
    //
    // Returns:
    //     The newly created instance of System.Security.Cryptography.KeySizes.
    extern public KeySizes(int minSize, int maxSize, int skipSize);

    // Summary:
    //     Specifies the maximum key size in bits.
    //
    // Returns:
    //     The maximum key size in bits.
    extern public int MaxSize { get; }
    //
    // Summary:
    //     Specifies the minimum key size in bits.
    //
    // Returns:
    //     The minimum key size in bits.
    extern public int MinSize { get; }
    //
    // Summary:
    //     Specifies the interval between valid key sizes in bits.
    //
    // Returns:
    //     The interval between valid key sizes in bits.
    extern public int SkipSize { get; }
  }
}
