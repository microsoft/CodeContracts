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
using System.Threading.Tasks;

namespace Tests.Sources
{

    [ContractClass(typeof(FooContracts))]
    public interface IFoo
    {
        string Method(params string[] strings);
        string Method2(params string[] strings);
    }

    [ContractClassFor(typeof(IFoo))]
    public abstract class FooContracts : IFoo
    {
        public string Method(params string[] strings)
        {
            Contract.Requires(Contract.ForAll(strings, s => s.Length > 0));
            Contract.Ensures(Contract.Exists(0, strings.Length, index => strings[index] != null && strings[index] == "42" && 
                                          Contract.ForAll(0, index, prior => Contract.Result<string>() != null)));

            return "42";
        }
        
        public string Method2(params string[] strings)
        {
            Contract.Requires(Contract.ForAll(strings, s => s.Length > 0));
            Contract.Ensures(Contract.Exists(0, strings.Length, index => strings[index] != null && strings[index] == "43" && 
                                          Contract.ForAll(0, index, prior => Contract.Result<string>() != null)));
            return "";
        }

    }

    public class Foo : IFoo
    {
        public string Method(params string[] strings)
        {
            Console.WriteLine("Method({0}", string.Join(", ", strings));
return "";
        }

        public string Method2(params string[] strings)
        {
            Console.WriteLine("Method2({0}", string.Join(", ", strings));
return "";
        }
    }


    public class Booo
    {
        public void Method(params string[] strings)
        {
            Contract.Requires(Contract.ForAll(strings, s => s.Length > 0));
        }
    }

  partial class TestMain
  {
    partial void Run()
    {
      if (behave)
      {
        new Foo().Method("42", "42", "42");
        new Foo().Method2("43", "43", "43");
      }
      else
      {
        new Foo().Method("11", "11");
      }
    }

    public ContractFailureKind NegativeExpectedKind = ContractFailureKind.Postcondition;
    public string NegativeExpectedCondition = "Contract.Exists(0, strings.Length, index => strings[index] != null && strings[index] == \"42\" && Contract.ForAll(0, index, prior => Contract.Result<string>() != null))";
  }
}
