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
        public static async Task<int> CountThem<T>(T[] values, T other)
        {
            Contract.Requires(values != null);
            Contract.Requires(Contract.ForAll(0, values.Length, i => !values[i].Equals(other)));
            Contract.Ensures(Contract.Result<Task<int>>().Result > 0);

            var x = values.Length;
            if (x == 4) throw new ArgumentException();
            return x;
        }
  }

  class TestMeGen<R>
  {
        public async Task<IEnumerable<int>> TPTest<T, Q>(T[] values, Q value, int limit)
            where Q : T
        {
            Contract.Requires(values != null);
            Contract.Requires(Contract.ForAll(0, values.Length, i => !values[i].Equals(value)));
            Contract.Ensures(Contract.ForAll(Contract.Result<Task<IEnumerable<int>>>().Result, i => i < limit && Contract.Result<Task<IEnumerable<int>>>() != null));
            Contract.EnsuresOnThrow<ArgumentException>(limit == 10);

            if (values.Length == 3) throw new ArgumentException();

            var x = await NonGen.CountThem(values, value);

            if (x == 5) throw new ArgumentException();

            return GetValues(x, limit);
        }

        IEnumerable<int> GetValues(int x, int limit)
        {
            Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<int>>(), i => i < limit));

            for (int i = 0; i < limit; i++)
            {
                if (x == 6) throw new ArgumentException();
                yield return i;
            }
        }
  }

  partial class TestMain
  {
    void TestMe(TestMeGen<DateTime> g, string[] args, bool mustThrow) {
      try {
        var z = g.TPTest<string,string>(args, "d", 9);
        foreach (var i in z.Result) {
          Console.WriteLine("Result = {0}", i);
        }
        if (mustThrow) {
          throw new Exception("failed to throw");
        }
      }
      catch (AggregateException ae) {
        if (ae.InnerException is ArgumentException) {
          if (!mustThrow){
            throw;
          }
        }
        else {
          throw;
        }
      }
    }

    void Test()
    {
      var g = new TestMeGen<DateTime>();
      TestMe(g, new[]{"a"}, false);
      TestMe(g, new[]{"a", "b"}, false);
      TestMe(g, new[]{"a", "b"}, false);

      if (!behave) {
        TestMe(g, new[]{"a", "b", "c", "e", "f", "g"}, true);
      }
    }

    partial void Run()
    {
      this.Test();
    }

    public ContractFailureKind NegativeExpectedKind = ContractFailureKind.PostconditionOnException;
    public string NegativeExpectedCondition = "limit == 10";
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
