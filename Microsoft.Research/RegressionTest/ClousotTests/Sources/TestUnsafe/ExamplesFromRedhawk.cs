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


using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;
using System;

namespace ExamplesFromRedHawk
{
  // Mainly examples involving strings
  class MafExamples
  {
    // ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The lower bound of the unsafe store is correct", PrimaryILOffset = (int)43, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The upper bound of the unsafe store is correct", PrimaryILOffset = (int)43, MethodILOffset = 0)]
    public static unsafe void StringTest0(string s)
    {
      Contract.Requires(s != null);
      Contract.Requires(s.Length > 0);

      fixed (char* ptr = s)
      {
        ptr[0] = 'f';
      }
    }

    // ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The lower bound of the unsafe store is correct", PrimaryILOffset = (int)37, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The upper bound of the unsafe store is correct", PrimaryILOffset = (int)37, MethodILOffset = 0)]
    public static unsafe void StringTest1(string s)
    {
      Contract.Requires(s != null);

      fixed (char* ptr = s)
      {
        for (int i = 0; i < s.Length; i++)
        {
          ptr[i] = '\0';
        }
      }
    }

    // ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The lower bound of the unsafe load is correct", PrimaryILOffset = (int)42, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "The upper bound of the unsafe load is correct", PrimaryILOffset = (int)42, MethodILOffset = 0)]
    public static unsafe void Write1(string str)
    {
      // hacky conversion to utf8
      int cch = str.Length;
      byte[] utf8 = new byte[cch];

      fixed (char* pCh = str)
      {
        for (int i = 0; i < cch; i++)
        {
          utf8[i] = unchecked((byte)pCh[i]);
        }
      }
      // ...
    }

  }
}
