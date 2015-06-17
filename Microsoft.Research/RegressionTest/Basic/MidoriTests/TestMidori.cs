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

#define FEATURE_FULL_CONTRACTS

using System;
using Microsoft.Contracts;
using Microsoft.Research.ClousotRegression;
using Microsoft.Research.CodeAnalysis;

namespace MidoriTests
{
  public class TestMidori
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Array creation : ok", PrimaryILOffset = 19, MethodILOffset = 19)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 10, MethodILOffset = 28)]
    private char[] GetBytes(int a)
    {
      Contract.Ensures(Contract.Result<char[]>() != null);
      return new char[4];
      // Infers \result.Length == 4
    }

    // Ok
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null pointer use (as receiver)", PrimaryILOffset = 13, MethodILOffset = 13)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null pointer use (as receiver)", PrimaryILOffset = 64, MethodILOffset = 64)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null pointer use (as array)", PrimaryILOffset = 83, MethodILOffset = 83)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null pointer use (as array)", PrimaryILOffset = 84, MethodILOffset = 84)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null pointer use (as array)", PrimaryILOffset = 93, MethodILOffset = 93)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null pointer use (as array)", PrimaryILOffset = 94, MethodILOffset = 94)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null pointer use (as array)", PrimaryILOffset = 32, MethodILOffset = 32)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null pointer use (as array)", PrimaryILOffset = 33, MethodILOffset = 33)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null pointer use (as array)", PrimaryILOffset = 42, MethodILOffset = 42)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null pointer use (as array)", PrimaryILOffset = 43, MethodILOffset = 43)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Array creation : ok", PrimaryILOffset = 5, MethodILOffset = 5)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 83, MethodILOffset = 83)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 83, MethodILOffset = 83)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 84, MethodILOffset = 84)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Array access might be above the upper bound", PrimaryILOffset = 84, MethodILOffset = 84)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 93, MethodILOffset = 93)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 93, MethodILOffset = 93)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 94, MethodILOffset = 94)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Array access might be above the upper bound", PrimaryILOffset = 94, MethodILOffset = 94)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 32, MethodILOffset = 32)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 32, MethodILOffset = 32)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 33, MethodILOffset = 33)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Array access might be above the upper bound", PrimaryILOffset = 33, MethodILOffset = 33)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 42, MethodILOffset = 42)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 42, MethodILOffset = 42)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Array access might be above the upper bound", PrimaryILOffset = 43, MethodILOffset = 43)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 43, MethodILOffset = 43)]
    public void M(int a)
    {
      int offset = 0;
      char[] guidChars = new char[0x20];
      char[] tmp = GetBytes(a);
      
      for (int i = 3; i >= 0; i = (int)(i - 1))
      {

        guidChars[offset++] = tmp[i]; 

        // guidChars.Length == 32

        // 0 <= i <= 3
        // 0 <= offset 
        // i + offset == 3

        guidChars[offset++] = tmp[i]; 
        // guidChars[offset++] = tmp[i];
        // guidChars.Length == 32, offset <= guidChars.Length 
      }

#if false
      for (int i = 3; i >= 0; i = (int)(i - 1))
      {
        // Entry
        // i == 3
        // offset == 0
        // i + offset == 3

        // One iteration
        // i == 2
        // offset == 2
        // i + offset == 4

        // Join of Karr
        // 2 * i + offset == 6

        // In the following iteration
        // 2 * (i -1) + (offset -2) == 6  

	guidChars[offset++] = tmp[i];
        guidChars[offset++] = tmp[i];
      }
#endif

      // 2 * i + offset == 6
      // -1 <= i <= 3 

      tmp = GetBytes(a);
      for (int i = 1; i >= 0; i = (int)(i - 1))
      {
        guidChars[offset++] = tmp[i] ;
        guidChars[offset++] = tmp[i];
      }
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null pointer use (as array)", PrimaryILOffset = 28, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null pointer use (as array)", PrimaryILOffset = 40, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null pointer use (as array)", PrimaryILOffset = 56, MethodILOffset = 56)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null pointer use (as array)", PrimaryILOffset = 69, MethodILOffset = 69)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null pointer use (as array)", PrimaryILOffset = 88, MethodILOffset = 88)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Array creation : ok", PrimaryILOffset = 73, MethodILOffset = 73)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 88, MethodILOffset = 88)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 88, MethodILOffset = 88)]
    public void GrowWithTimes(int[] a, int index)
    {
      // <Clousot inferred>
      Contract.Requires(a != null);
      // </Clousot inferred>
      Contract.Requires(index >= 0);
      Contract.Requires(a.Length > 0);
      Contract.Requires(a.Length >= index);

      if (index == a.Length)
      {
        a = new int[a.Length * 2];
      }
      a[index] = -876;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null pointer use (as array)", PrimaryILOffset = 28, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null pointer use (as array)", PrimaryILOffset = 44, MethodILOffset = 44)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null pointer use (as array)", PrimaryILOffset = 57, MethodILOffset = 57)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null pointer use (as array)", PrimaryILOffset = 76, MethodILOffset = 76)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Array creation : ok", PrimaryILOffset = 61, MethodILOffset = 61)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 76, MethodILOffset = 76)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 76, MethodILOffset = 76)]
    public void GrowWithPlus(int[] a, int index)
    {
      // <Clousot inferred>
      Contract.Requires(a != null);
      // </Clousot inferred>
      Contract.Requires(index >= 0);
      Contract.Requires(a.Length >= index);

      if (index == a.Length)
      {
        a = new int[a.Length +1];
      }
      a[index] = -876;
    }

  }
}
