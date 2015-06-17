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
using Contracts.Regression;

namespace ChunkerTest
{
  /// <summary>
  /// A chunker object holds a string and a chunkSize. It breaks the string into equal sized 
  /// substrings of chunkSize. Each substring is provided by repeated calls to NextChunk.
  /// </summary>
  public class Chunker
  {
    /// <summary>
    /// the string from which we return chunks
    /// </summary>
    readonly string stringData;

    /// <summary>
    /// the size of each chunk
    /// </summary>
    readonly int chunkSize;

    /// <summary>
    /// Returns the length of each chunk produced by this chunker.
    /// </summary>
    public int ChunkSize {
      [ClousotRegressionTest]
      get
      {
        //Contract.Ensures(Contract.Result<int>() == this.chunkSize);

        return this.chunkSize;
      }
    }

    /// <summary>
    /// The number of characters in stringData we already returned in chunks
    /// </summary>
    int returnedCount;

    /// <summary>
    /// Object invariant method
    /// </summary>
    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(chunkSize > 0);
      Contract.Invariant(returnedCount >= 0);
      Contract.Invariant(stringData != null);
      Contract.Invariant(returnedCount <= stringData.Length);
    }

    /// <summary>
    /// Returns the next chunk
    /// </summary>
    /// <returns></returns>
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 13, MethodILOffset = 86)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 31, MethodILOffset = 86)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 56, MethodILOffset = 86)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 109, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 13, MethodILOffset = 128)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 36, MethodILOffset = 128)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 151, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 11, MethodILOffset = 176)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 37, MethodILOffset = 176)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"invariant is valid", PrimaryILOffset = 9, MethodILOffset = 176)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"invariant is valid", PrimaryILOffset = 26, MethodILOffset = 176)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"invariant is valid", PrimaryILOffset = 43, MethodILOffset = 176)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"invariant is valid", PrimaryILOffset = 70, MethodILOffset = 176)]
    public virtual string NextChunk()
    {
      Contract.Ensures(Contract.Result<string>() != null);
      Contract.Ensures(Contract.Result<string>().Length <= ChunkSize);


      string s;
      if (returnedCount + chunkSize <= stringData.Length)
      {
        s = stringData.Substring(returnedCount, chunkSize);
        Contract.Assert(s.Length <= ChunkSize);
      }
      else
      {
        s = stringData.Substring(returnedCount);
        Contract.Assert(s.Length <= ChunkSize);
      }
      returnedCount += s.Length;
      return s;
    }

    /// <summary>
    /// Creates a new chunker from given source string and with the given chunkSize
    /// </summary>
    /// <param name="source">string to cut into chunks</param>
    /// <param name="chunkSize">length in characters of each chunk</param>
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"invariant is valid", PrimaryILOffset = 9, MethodILOffset = 48)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"invariant is valid", PrimaryILOffset = 26, MethodILOffset = 48)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"invariant is valid", PrimaryILOffset = 43, MethodILOffset = 48)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"invariant is valid", PrimaryILOffset = 70, MethodILOffset = 48)]
    public Chunker(string source, int chunkSize)
    {
      Contract.Requires(chunkSize>0);
      Contract.Requires(source != null);

      this.stringData = source;
      this.chunkSize = chunkSize;
      returnedCount = 0;
    }

    /// <summary>
    /// Chunker's string representation
    /// </summary>
    /// <returns></returns>
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"ensures is valid", PrimaryILOffset = 17, MethodILOffset = 6)]
    public override string ToString()
    {
      return base.ToString();
    }
  }



}
