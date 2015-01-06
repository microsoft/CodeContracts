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
using System.Text;
using System.Diagnostics.Contracts;

namespace Tests.Sources
{

  partial class TestMain
  {
    IEnumerable<int> GetInts(int[] values) {
      Contract.Requires(values != null);
      Contract.Requires(Contract.ForAll(values, v => v >= 0));
      Contract.Requires(Contract.ForAll(0, values.Length, i => values[i] >= 0));

      yield return 0;
      yield return 1;
      yield return 2;

      foreach (var value in values) { yield return value; }


      yield return 10;
      yield return 11;
      yield return 12;
    }

    void Test(int[] values)
    {
      foreach (var x in GetInts(values)) {
        Console.WriteLine(x);
      }
    }

    partial void Run()
    {
      if (behave)
      {
        this.Test(new[]{0,1,2,3,4,5});
      }
      else
      {
        this.Test(new[]{0,1,2,-1,4,5});
      }
    }

    public ContractFailureKind NegativeExpectedKind = ContractFailureKind.Precondition;
    public string NegativeExpectedCondition = "Contract.ForAll(values, v => v >= 0)";
  }
}
