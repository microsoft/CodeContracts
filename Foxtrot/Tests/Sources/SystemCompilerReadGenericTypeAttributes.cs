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
using System.Threading;

namespace Tests.Sources
{
    // a dummy class, just to make the test framework happy 
    public class Foo
    {
        public void Method(params string[] strings)
        {
            Contract.Requires(Contract.ForAll(strings, s => s.Length > 0));
            Console.WriteLine("Method");
        }
    } 
    
    public class C1
    {
    }
    public class C2
    {
    }
    public class C3
    {
    }
    
    public interface I1<T1, T2, T3>
    {
    }
    
    public class A1 : System.Attribute
    {
        public A1(Type t)
        {
            T = t;
        }
        
        public Type T {get;set;}
    }

  partial class TestMain
  {
    partial void Run()
    {
      if (behave)
      {
        new Foo().Method("1", "2", "3");
      }
      else
      {
        new Foo().Method("1", "");
      }
    }
    
    private interface I2<T1, T2, T3>
    {
    }
    
    [A1(typeof(I1<C1, C2, C3>))]
    private class Inner1 : I1<C1, C2, C3>
    {
    }
    
    [A1(typeof(I2<C1, C2, C3>))]
    private class Inner2 : I2<C1, C2, C3>
    {
    }

    public ContractFailureKind NegativeExpectedKind = ContractFailureKind.Precondition;
    public string NegativeExpectedCondition = "Contract.ForAll(strings, s => s.Length > 0)";
  }
}
