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
using System.Runtime.InteropServices;

namespace System.Configuration.Assemblies {
  // Summary:
  //     Specifies all the hash algorithms used for hashing files and for generating
  //     the strong name.
  [Serializable]
  [ComVisible(true)]
  public enum AssemblyHashAlgorithm {
    // Summary:
    //     A mask indicating that there is no hash algorithm. If you specify None for
    //     a multi-module assembly, the common language runtime defaults to the SHA1
    //     algorithm, since multi-module assemblies need to generate a hash.
    None = 0,
    //
    // Summary:
    //     Retrieves the MD5 message-digest algorithm. MD5 was developed by Rivest in
    //     1991. It is basically MD4 with safety-belts and while it is slightly slower
    //     than MD4, it helps provide more security. The algorithm consists of four
    //     distinct rounds, which has a slightly different design from that of MD4.
    //     Message-digest size, as well as padding requirements, remain the same.
    MD5 = 32771,
    //
    // Summary:
    //     A mask used to retrieve a revision of the Secure Hash Algorithm that corrects
    //     an unpublished flaw in SHA.
    SHA1 = 32772,
  }
}
