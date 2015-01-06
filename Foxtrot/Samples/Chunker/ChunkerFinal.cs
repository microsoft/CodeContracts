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

namespace ChunkerTest
{
  /// <summary>
  /// A chunker object holds a string and a chunkSize. It breaks the string into equal sized 
  /// substrings of chunkSize. Each substring is provided by repeated calls to NextChunk.
  /// </summary>
  class Chunker
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
    /// The number of characters in stringData we already returned in chunks
    /// </summary>
    int returnedCount;

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(chunkSize > 0);
      Contract.Invariant(returnedCount >= 0);
      Contract.Invariant(stringData != null);
      Contract.Invariant(returnedCount <= stringData.Length);
    }

    public string NextChunk()
    {
      string s;
      if (returnedCount <= stringData.Length - chunkSize)
      {
        s = stringData.Substring(returnedCount, chunkSize);
      }
      else
      {
        s = stringData.Substring(returnedCount);
      }
      returnedCount += s.Length;
      return s;
    }

    public Chunker(string source, int chunkSize)
    {
      Contract.Requires(chunkSize>0);
      Contract.Requires(source != null);

      this.stringData = source;
      this.chunkSize = chunkSize;
      returnedCount = 0;
    }
  }



}
