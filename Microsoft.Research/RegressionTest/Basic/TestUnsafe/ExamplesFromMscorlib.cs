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
using System.Runtime.InteropServices;
using System.Text;
using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;

namespace Regression
{
  class DateTimeFormat
  {
    // ok
    // A simplified one. 
    // f: it does not work as we do not handle subtractions
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Localloc size ok",PrimaryILOffset=5,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.False,Message=@"Unsafe memory store IS below the lower bound",PrimaryILOffset=25,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"The upper bound of the unsafe store is correct",PrimaryILOffset=25,MethodILOffset=0)]
    public static unsafe void FormatDigits0(/*parameters removed*/)
    {
      char* chPtr = stackalloc char[0x10];
      char* chPtr2 = chPtr + 0x10;
      int i = 0x9;

      do
      {
        *(chPtr2 - i) = (char)(33);
        i--;
      }
      while (i > 0);
    }

    // ok
    // A simplified one
    // f: it does not work as we do not handle subtractions
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Localloc size ok",PrimaryILOffset=5,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.False,Message=@"Unsafe memory store IS below the lower bound",PrimaryILOffset=22,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"The upper bound of the unsafe store is correct",PrimaryILOffset=22,MethodILOffset=0)]
    public static unsafe void FormatDigits1(/*parameters removed*/)
    {
      char* chPtr = stackalloc char[0x10];
      char* chPtr2 = chPtr + 0x10;

      do
      {
        *(--chPtr2) = (char)(33);
      }
      while (chPtr2 > chPtr);
    }

    // TODO
    // The original one
    // f: it does not work as we do not handle subtractions
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Localloc size ok",PrimaryILOffset=10,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.False,Message=@"Unsafe memory store IS below the lower bound",PrimaryILOffset=37,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"The upper bound of the unsafe store is correct",PrimaryILOffset=37,MethodILOffset=0)]
#if false
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Unsafe memory store might be below the lower bound",PrimaryILOffset=76,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Unsafe memory store might be above the upper bound",PrimaryILOffset=76,MethodILOffset=0)]
#endif
    [RegressionOutcome(Outcome=ProofOutcome.Bottom,Message=@"Unsafe store never reached",PrimaryILOffset=76,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Bottom,Message=@"Unsafe store never reached",PrimaryILOffset=76,MethodILOffset=0)]  
    public static unsafe void FormatDigits(StringBuilder outputBuffer, int value, int len)
    {
      if (len > 2)
      {
        len = 2;
      }

      byte* bytes = stackalloc byte[2 * 0x10];
      char* chPtr = (char*) bytes;
      
      char* chPtr2 = chPtr + 0x10;
      
      int num = value;
      do
      {
        *(--chPtr2) = (char)((num % 10) + 0x30);
        num /= 10;
      }
      while ((num != 0) && (chPtr2 > chPtr));
      
      int count = (int)((long)(((chPtr + 0x10) - chPtr2) / 2));
      
      while ((count < len) && (chPtr2 > chPtr))
      {
        *(--chPtr2) = '0';
        count++;
      }
      // ..
    }
  }
}

 

 
