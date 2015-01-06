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

#if NETFRAMEWORK_3_5
namespace System.Threading.Tasks {
  public delegate T Func<T>();

  public class Task<T> {
      T value;
      public Task(Func<T> make) {
        value = make();
      }
      public void Wait() {}
      public void Start() {}
      public T Result { get { return this.value; } }
  }
}
#endif

namespace Tests.Sources
{

  using System.Threading.Tasks;

  class GenAsync<T> {

#if NETFRAMEWORK_4_5
    public async Task<int> Test(int[] times) {
      Contract.Requires(times != null);
      Contract.Requires(Contract.ForAll(times, ms => ms >= 0));
      Contract.Requires(Contract.ForAll(0, times.Length, i => times[i] >= 0));

      foreach (var ms in times) {
        await TaskEx.Delay(ms);
      }

      return 7;
    }
#else
    public Task<int> Test(int[] times) {
      Contract.Requires(times != null);
      Contract.Requires(Contract.ForAll(times, ms => ms >= 0));
      Contract.Requires(Contract.ForAll(0, times.Length, i => times[i] >= 0));

      var t = new Task<int>(() => 7);
      t.Start();
      return t;
    }
#endif
  }
    
  partial class TestMain
  {
    void Test(int[] ms)
    {
      var g = new GenAsync<string>();
      var t = g.Test(ms);
      t.Wait();
      Console.WriteLine("Result = {0}", t.Result);
    }

    partial void Run()
    {
      if (behave)
      {
        this.Test(new[]{50,30,10});
      }
      else
      {
        this.Test(new[]{50, -1, 10});
      }
    }

    public ContractFailureKind NegativeExpectedKind = ContractFailureKind.Precondition;
    public string NegativeExpectedCondition = "Contract.ForAll(times, ms => ms >= 0)";
  }
}
