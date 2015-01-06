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

#if !NETFRAMEWORK_4_0
#define WRITABLE_BYTES
#endif

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;

namespace SafeWrapper
{
  class SafeWrapper
  {
#if WRITABLE_BYTES
    // ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The lower bound of the unsafe load is correct", PrimaryILOffset = 92, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The upper bound of the unsafe load is correct", PrimaryILOffset = 92, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The lower bound of the unsafe store is correct", PrimaryILOffset = 93, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The upper bound of the unsafe store is correct", PrimaryILOffset = 93, MethodILOffset = 0)]
    static unsafe private void CopyMemoryProxy(char* pdst, char* psrc,
                                                    int size_dst, int size_src)
    {
      Contract.Requires(size_dst >= 0);
      Contract.Requires(size_src >= 0);
      Contract.Requires(size_dst >= size_src);
      Contract.Requires(Contract.WritableBytes(pdst) >= (uint)size_dst * sizeof(char));
      Contract.Requires(Contract.WritableBytes(psrc) >= (uint)size_src * sizeof(char));

      for (int i = 0; i < size_src; i++) {
        pdst[i] = psrc[i];
      }
    }

    // ok
    [ClousotRegressionTest("clousot1")][ClousotRegressionTest("clousot2")]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 7, MethodILOffset = 96)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 19, MethodILOffset = 96)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 31, MethodILOffset = 96)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 51, MethodILOffset = 96)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 71, MethodILOffset = 96)]
    public unsafe static void FastCopy(char[] d, char[] s)
    {
      Contract.Requires(d != null);
      Contract.Requires(s != null);
      Contract.Requires(d.Length >= s.Length);

      fixed (char* pdst = d)
      fixed (char* psrc = s)
      {
        CopyMemoryProxy(pdst, psrc, d.Length, s.Length);
      }
    }
#else // no writeable bytes
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"The lower bound of the unsafe load is correct",PrimaryILOffset=52,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Unsafe memory load might be above the upper bound",PrimaryILOffset=52,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"The lower bound of the unsafe store is correct",PrimaryILOffset=53,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Unsafe memory store might be above the upper bound",PrimaryILOffset=53,MethodILOffset=0)]
    static unsafe private void CopyMemoryProxy(char* pdst, char* psrc,
                                                    int size_dst, int size_src)
    {
      Contract.Requires(size_dst >= 0);
      Contract.Requires(size_src >= 0);
      Contract.Requires(size_dst >= size_src);
      //Contract.Requires(Contract.WritableBytes(pdst) >= (uint)size_dst * sizeof(char));
      //Contract.Requires(Contract.WritableBytes(psrc) >= (uint)size_src * sizeof(char));

      for (int i = 0; i < size_src; i++) {
        pdst[i] = psrc[i];
      }
    }

#endif
  }
}
