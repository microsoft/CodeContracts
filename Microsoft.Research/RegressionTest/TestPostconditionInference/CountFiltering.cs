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

#define CONTRACTS_FULL
using System;
using System.Collections;
using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;


namespace Inference
{
  public class MyCollection : ICollection
  {

    int value;

    public MyCollection(int x)
    {
        this.value = x;
    }
    
    // We should filter the trivial postcondition this.Count >= 0
    [ClousotRegressionTest]
    [RegressionOutcome("Contract.Ensures(array.Rank == 1);")]
    [RegressionOutcome("Contract.Ensures(0 <= ((System.Array)array).Length);")]
    [RegressionOutcome("Contract.Ensures((index - ((System.Array)array).Length) <= 0);")]
    public void CopyTo(Array array, int index)
    {
    }

    [ClousotRegressionTest]
    public int Count
    {
      get { return value < 0? 0 : value; }
    }

    [ClousotRegressionTest]
    public bool IsSynchronized
    {
      get { return false; }
    }

    [ClousotRegressionTest]
    public object SyncRoot
    {
      get { return this; }
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.False,Message="ensures is false: Contract.Result<IEnumerator>() != null",PrimaryILOffset=17,MethodILOffset=1)]
#if CLOUSOT2
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="The normal exit of the method is unreachable. If this is wanted, consider adding Contract.Ensures(false) to document it",PrimaryILOffset=0,MethodILOffset=0)]
#else
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="The normal exit of the method is unreachable. If this is wanted, consider adding Contract.Ensures(false) to document it",PrimaryILOffset=1,MethodILOffset=0)]
#endif
    public IEnumerator GetEnumerator()
    {
      return null;
    }
  }
}
