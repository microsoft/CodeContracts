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
using System.Linq;
using System.Text;
using Contracts.Regression;
using System.Diagnostics.Contracts;
using Protocols;

namespace SampleClients
{
  class Program
  {
    public static void Main() { }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.False, Message = "requires is false: State == S.Computed (object must be in Computed state)", PrimaryILOffset = 19, MethodILOffset = 22)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"requires is valid", PrimaryILOffset = 15, MethodILOffset = 16)]

    static void TestApiProtocol(string[] args)
    {
      var c = new ClassWithProtocol();

      if (args[0] == null) return;
      c.Initialize(args[0]);

      var data = c.ComputedData;

      Console.WriteLine(data.ToString());

    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 24, MethodILOffset = 0)]
    static void TestChunker(ChunkerTest.Chunker chunker)
    {
      string s = chunker.NextChunk();
      Contract.Assert(s.Length <= chunker.ChunkSize);
    }

  }
}

