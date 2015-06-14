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

namespace OutOfBand {
  public class C {
    private bool b = false;
    protected bool c = false;

    public C() {
      this.b = true;
      this.c = true;
    }

    public C(bool b, bool c)
    {
      this.b = b;
      this.c = c;
    }


    public C(bool satisfyInvariant) { b = satisfyInvariant; }

    [ContractInvariantMethod]
    private void InvariantMethod()
    {
      Contract.Invariant(b);
      Contract.Invariant(c, "c must be true");

    }
    public int Inc(int y)
    {
      Contract.Requires(y >= 0, "y must be non-negative");
      Contract.Requires(y == 3);
      return default(int);
    }

    public int FooMethodTakingTypeFromSameAssembly(Foo foo)
    {
      Contract.Requires(foo != null);
      Contract.Ensures(Contract.Result<int>() >= 0, "result is non-negative");
      Contract.Ensures(Contract.Result<int>() >= 0);

      return foo.Bar;
    }

    public int TestRequiresWithException(int x)
    {
      Contract.Requires<IndexOutOfRangeException>(x >= 0);
      Contract.Requires<ArgumentOutOfRangeException>(x < 10);

      return x;
    }

  }

  public class Foo {
    int data;

    public Foo(int data)
    {
      this.data = data;
    }

    public int Bar { get { return data; } }
  }
}


namespace ManualInheritanceChain {
  /// <summary>
  /// This class/assembly exists so there is an assembly that generates a reference
  /// assembly from which other assemblies can inherit contracts.
  /// </summary>
  public class BaseOfChain {
    public virtual void Test(int x)
    {
      Contract.Requires<ArgumentException>(x > 0, "x must be positive");
    }
  }

}