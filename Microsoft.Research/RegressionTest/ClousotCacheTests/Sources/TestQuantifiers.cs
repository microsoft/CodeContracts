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
using Microsoft.Research.ClousotRegression;

namespace Quantifiers {
  public class ExampleWithForAll
  {
    [ClousotRegressionTest]
#if FIRST
    [RegressionOutcome("No entry found in the cache")]
#else
    [RegressionOutcome("No entry found in the cache")] // it should not be found
    [RegressionOutcome(Outcome=ProofOutcome.False,Message=@"assert is false",PrimaryILOffset=91,MethodILOffset=0)]
#endif
    // no warning
    public void UseIncorrect(string[] array)
    {
      Contract.Requires(array != null);
      Contract.Requires(array.Length > 0);
#if FIRST
      Contract.Requires(Contract.ForAll(0, array.Length, i => array[i] != null));
#else
      Contract.Requires(Contract.ForAll(0, array.Length, i => array[i] == null));
#endif
      Contract.Assert(array[0] != null);
    }


    [ClousotRegressionTest]
#if FIRST
    [RegressionOutcome("No entry found in the cache")]
#else
    [RegressionOutcome("An entry has been found in the cache")]
#endif
    // no warning
    public void UseCorrect(string[] array)
    {
      Contract.Requires(array != null);
      Contract.Requires(array.Length > 0);
      Contract.Requires(Contract.ForAll(0, array.Length, i => array[i] != null));

      Contract.Assert(array[0] != null);
    }

  }
}
