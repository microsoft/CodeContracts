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
using System.Runtime.InteropServices;

namespace System.Reflection
{
  // Summary:
  //     Provides information about an System.Reflection.Assembly reference.
  public enum AssemblyNameFlags
  {
    // Summary:
    //     Specifies that no flags are in effect.
    None = 0,
    //
    // Summary:
    //     Specifies that a public key is formed from the full public key rather than
    //     the public key token.
    PublicKey = 1,
    //
    // Summary:
    //     Specifies that the assembly can be retargeted at runtime to an assembly from
    //     a different publisher.
    Retargetable = 256,
    //
    // Summary:
    //     Specifies that just-in-time (JIT) compiler optimization is enabled for the
    //     assembly.
    EnableJITcompileOptimizer = 16384,
    //
    // Summary:
    //     Specifies that just-in-time (JIT) compiler tracking is enabled for the assembly.
    EnableJITcompileTracking = 32768,
  }
}
