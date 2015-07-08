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
#if NETFRAMEWORK_4_5
  using System.Threading.Tasks;

  class NonGen {
        public async Task<List<int>> FooAsync(int limit)
        {
            Contract.Ensures(Contract.ForAll(Contract.Result<List<int>>(), i => i < limit));

            await Task.Delay(42);

            return new List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
        }
  }
  partial class TestMain
  {
    void TestMe(NonGen g, int limit, bool mustThrow) {
      try {
        var z = g.FooAsync(limit);
 
        foreach (var i in z.Result) {
          Console.WriteLine("Result = {0}", i);
        }

        if (mustThrow) {
          throw new Exception("failed to throw");
        }
      }
      catch (AggregateException ae) {
        if (!mustThrow) {
            throw;
        }
      }
    }

    void Test()
    {
      var g = new NonGen();
      TestMe(g, 20, false);
      TestMe(g, 5, true);

      if (!behave) {
        TestMe(g, 5, false);
      }
    }

    partial void Run()
    {
      this.Test();
    }

    public ContractFailureKind NegativeExpectedKind = ContractFailureKind.Postcondition;
    public string NegativeExpectedCondition = "Contract.ForAll(Contract.Result<List<int>>(), i => i < limit)";
  }
#else
  // Dummy for Pre 4.5
  partial class TestMain 
  {

    void Test() {
      Contract.Requires(false);
    }

    partial void Run()
    {
      if (!behave) {
        this.Test();
      }
    }

    public ContractFailureKind NegativeExpectedKind = ContractFailureKind.Precondition;
    public string NegativeExpectedCondition = "false";
  }
#endif
}
