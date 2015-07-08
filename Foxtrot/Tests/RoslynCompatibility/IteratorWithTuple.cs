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
using System.Collections;
using System.Diagnostics.Contracts;
using System.Text;
using System.Threading;

namespace Tests.Sources
{
    public class TestYield : System.Collections.Generic.IEnumerable<Tuple<int, int>>
    {
        IEnumerator<Tuple<int, int>> System.Collections.Generic.IEnumerable<Tuple<int, int>>.GetEnumerator()
        {
            yield return Tuple.Create(42, 42);
        }

        IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            yield break;
        }

        // This will proof that only new Roslyn-based compiler can compile this
        public int X => 42;
    }


  partial class TestMain
  {
    partial void Run()
    {
      if (behave)
      {
        foreach(var e in new TestYield()) {Console.WriteLine(e);}
      }
      else
      {
        // For compatibility reasons when behave is false the test should fail. The only way to do this - throw ANE.
        throw new ArgumentNullException();
      }
    }

    public ContractFailureKind NegativeExpectedKind = ContractFailureKind.Precondition;
    public string NegativeExpectedCondition = "Value cannot be null.";
  }
}
