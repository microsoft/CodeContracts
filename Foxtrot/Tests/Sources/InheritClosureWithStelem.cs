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
  public delegate bool D<T>(T t);

  public class BaseWithClosureWithStelem<T>
  {
    public bool Foo(D<T> action, T data)
    {
      action(data);
      action(data);
      return true;
    }

    public virtual void M(T[] ts, int i)
    {
      Contract.Requires(ts != null);
      Contract.Requires(i + 1 < ts.Length);
      Contract.Ensures(ts[i].Equals(ts[0]));
      Contract.Ensures(ts[i+1].Equals(ts[0]));

      int index = i;
      Foo(delegate(T t) { ts[index++] = t; return true; }, ts[0]);
    }
  }

  public class DerivedOfClosureWithStelem<V> : BaseWithClosureWithStelem<V>
  {
    public override void M(V[] vs, int j)
    {
      base.M(vs, j);
    }
  }


  partial class TestMain
  {

    partial void Run()
    {
      if (behave)
      {
        var st = new DerivedOfClosureWithStelem<string>();
        st.M(new string[] { "a", "b", "c" }, 1);
      }
      else
      {
        var st = new DerivedOfClosureWithStelem<string>();
        st.M(new string[] { "a", "b", "c" }, 2);
      }
    }
    public ContractFailureKind NegativeExpectedKind = ContractFailureKind.Precondition;
    public string NegativeExpectedCondition = "i + 1 < ts.Length";
    
  }
}
