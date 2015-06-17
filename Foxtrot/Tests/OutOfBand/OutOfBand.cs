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
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace OutOfBand {
  public class C {
    private bool b = true;
    protected bool c = true;

    public C () { }
    public C(bool b, bool c)
    {
      this.b = b;
      this.c = c;
    }


    public C (bool satisfyInvariant) { b = satisfyInvariant; }
    public int Inc (int y) {
      return y + 1;
    }

    public int FooMethodTakingTypeFromSameAssembly(Foo foo)
    {
      return foo.Bar;
    }

    public int TestRequiresWithException(int x)
    {
      return x;
    }
  }

  public class Foo
  {
    int data;

    public Foo(int data)
    {
      this.data = data;
    }

    public int Bar { get { return data; } }
  }
}

namespace ManualInheritanceChain
{
  public class BaseOfChain
  {
    public virtual void Test(int x)
    {
    }
  }
}