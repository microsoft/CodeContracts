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

    partial void Run()
    {
      I<int> d = new Derived();
      if (behave)
      {
        d.M(5, "hello", d);
      }
      else
      {
        d.M<string>(5, "hello", null);
      }
    }

    public ContractFailureKind NegativeExpectedKind = ContractFailureKind.Precondition;
    public string NegativeExpectedCondition = "z != null";
  }

  [ContractClass(typeof(IContract<>))]
  public interface I<T> {
    T M<Q>(T x, string y, Q z);
  }

  [ContractClassFor(typeof(I<>))]
  abstract class IContract<T> : I<T> {
    public T M<Q>(T x, string y, Q z) {
      Contract.Requires(z != null);
      Contract.Requires(y != null);
      return default(T);
    }
  }

  public class Base {

    public int M<Q>(int x, string y, Q z) {
      return x;
    }
  }

  public class Derived : Base, I<int> {
  }

}
