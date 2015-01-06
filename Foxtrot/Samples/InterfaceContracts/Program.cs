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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Contracts.Samples
{
  class Program
  {
    static void Main(string[] args)
    {
      var f1 = new FooImplementation1();
      int r1 = f1.Foo(0);
      Contract.Assert(r1 > 0);

      IFoo f2 = new FooImplementation2();
      int r2 = f2.Foo(1);
    }
  }

  [ContractClass(typeof(IFooContract))]
  interface IFoo
  {
    int Foo(int x);
  }

  [ContractClassFor(typeof(IFoo))]
  abstract class IFooContract : IFoo
  {
    int IFoo.Foo(int x)
    {
      Contract.Requires(x > 0);
      Contract.Ensures(Contract.Result<int>() > 0);

      throw new NotImplementedException();
    }
  }

  public class FooImplementation1 : IFoo
  {
    public int Foo(int x)
    {
      return x;
    }
  }

  public class FooImplementation2 : IFoo
  {
    /// <summary>
    /// Bad implementation of IFoo.Foo which does not always satisfy post condition.
    /// </summary>
    int IFoo.Foo(int x)
    {
      return x - 1;
    }
  }
}
