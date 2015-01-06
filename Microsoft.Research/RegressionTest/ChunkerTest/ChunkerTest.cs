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

// Test of Vincent's reduction for the chuncker demo

using System;
using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;

namespace ChunkerTest
{
  class Chunker
  {
    string src;
    int ChunkSize;

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(src != null);
      Contract.Invariant(0 < ChunkSize);
      Contract.Invariant(0 <= n);
      Contract.Invariant(n <= src.Length);
    }
    int n;  // the number of characters returned so far

    /// <summary>
    /// Also tests OOB from mscorlib (on string class).
    /// </summary>
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 112, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 169, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 192, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 22, MethodILOffset = 223)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 13, MethodILOffset = 223)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 28, MethodILOffset = 223)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 46, MethodILOffset = 223)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 74, MethodILOffset = 223)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 13, MethodILOffset = 77)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 31, MethodILOffset = 77)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=54,MethodILOffset=77)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=79,MethodILOffset=77)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 13, MethodILOffset = 134)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 36, MethodILOffset = 134)]
    public string/*!*/ NextChunk()
    {
      Contract.Ensures(Contract.Result<string>().Length <= ChunkSize);
      
      string s;
      if (n + ChunkSize <= src.Length)
      {
        s = src.Substring(n, ChunkSize);

        Contract.Assert(n + s.Length <= src.Length);
      }
      else
      {
        s = src.Substring(n);
        
        Contract.Assert(n + s.Length <= src.Length);
        Contract.Assert(s.Length <= ChunkSize);
      }
      n += s.Length;

      return s;
    }

    // old version working for both cci1 and cci2
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 13, MethodILOffset = 134)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 15, MethodILOffset = 77)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 112, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 169, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 192, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 22, MethodILOffset = 223)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 13, MethodILOffset = 223)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 28, MethodILOffset = 223)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 46, MethodILOffset = 223)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 74, MethodILOffset = 223)]
    public string/*!*/ NextChunkOld()
    {
      Contract.Ensures(Contract.Result<string>().Length <= ChunkSize);

      string s;
      if (n + ChunkSize <= src.Length)
      {
        s = HelperMethods.SubstringHelper(src, n, ChunkSize);

        Contract.Assert(n + s.Length <= src.Length);
      }
      else
      {
        s = HelperMethods.SubstringHelper(src, n);

        Contract.Assert(n + s.Length <= src.Length);
        Contract.Assert(s.Length <= ChunkSize);
      }
      n += s.Length;

      return s;
    }

    // Ok: It proves all but the src != null (which is because we are doing the regression without the "-nonnull" analysis
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = (int)13, MethodILOffset = (int)53)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = (int)28, MethodILOffset = (int)53)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = (int)46, MethodILOffset = (int)53)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = (int)74, MethodILOffset = (int)53)]
    public Chunker(string/*!*/ source, int chunkSize)
    {
      Contract.Requires(0 < chunkSize);
      Contract.Requires(source != null);
      
      src = source;
      ChunkSize = chunkSize;
      n = 0;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 13, MethodILOffset = 3)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "requires unproven: startIndex <= this.Length", PrimaryILOffset = 36, MethodILOffset = 3)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 13, MethodILOffset = 12)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 28, MethodILOffset = 12)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 46, MethodILOffset = 12)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 74, MethodILOffset = 12)]
    public string NotSatisfyingOOB(string s)
    {
      return s.Substring(5);
    }
  }

  public static class HelperMethods // specific methods with contracts ; contracts are not supposed to be proven
  {
    // This is just to help the ChunckerDemo, so we do not test them 
    public static string SubstringHelper(string source, int startIndex)
    {
      Contract.Requires(startIndex <= source.Length);

      // Contract.Ensures(Contract.Result<string>() != null);
      Contract.Ensures(Contract.Result<string>().Length == source.Length - startIndex);
      // Contract.Ensures(source == Contract.OldValue(source));

      return source;
    }

    // This is just to help the ChunckerDemo, so we do not test them 
    public static string SubstringHelper(string source, int startIndex, int count)
    {
      Contract.Requires(startIndex + count <= source.Length);

      // Contract.Ensures(Contract.Result<string>() != null);
      Contract.Ensures(Contract.Result<string>().Length == count);
      // Contract.Ensures(source == Contract.OldValue(source));

      return source;
    }


    public static void PexHelper(string s, int i)
    {
      Chunker c = new Chunker(s, i);

      string chunk;
      while ((chunk = c.NextChunk()) != "")
      {
        Console.WriteLine(chunk);
      }
    }

  }

}
