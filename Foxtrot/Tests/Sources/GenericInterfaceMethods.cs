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
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace Tests.Sources
{

  partial class TestMain
  {
    partial void Run()
    {
      var c = new TestClass();

      if (behave) {
        c.Bar<int>(0);
      }
      else {
        c.Bar<string>(null);
      }
    }


    public ContractFailureKind NegativeExpectedKind = ContractFailureKind.Postcondition;
    public string NegativeExpectedCondition = "Foo1<T>(arg)";

  }

  [ContractClass(typeof(TestCC))]
  interface ITest
  {
    bool Foo1<T>(T arg);
    bool Foo2();
    void Bar<T>(T arg);
  }

  [ContractClassFor(typeof(ITest))]
  abstract class TestCC : ITest
  {
    [Pure]
    public bool Foo1<T>(T arg)
    {
      throw new NotImplementedException();
    }

    [Pure]
    public bool Foo2()
    {
      throw new NotImplementedException();
    }

    public void Bar<T>(T arg)
    {
      Contract.Ensures(Foo1<T>(arg));
      Contract.Ensures(Foo2());
      throw new NotImplementedException();
    }
  }

  class TestClass : ITest
  {
    #region ITest Members

    public bool Foo1<T>(T arg)
    {
      return arg != null;
    }

    public bool Foo2()
    {
      return true;
    }

    public void Bar<T>(T arg)
    {
    }

    #endregion
  }
}

