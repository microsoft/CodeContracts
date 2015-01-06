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
  public class EqSetDom<T> where T : IComparable<T>
  {
    internal DomNode<T> root;

    public EqSetDom()
    {
      this.root = new DomNode<T>();
    }


    [Pure]
    internal IEnumerable<T> GetElements(DomNode<T> cur)
    {
      Contract.Requires(cur != null);
      yield break;
    }

    internal DomNode<T> AddElement(DomNode<T> cur, T elt)
    {
      Contract.Requires(elt != null);
      Contract.Requires(cur != null);

      Contract.Ensures(Contract.ForAll<T>(Contract.OldValue<IEnumerable<T>>(GetElements(cur)),
                              x => { return SetContains(Contract.Result<DomNode<T>>(), x); }));


      return cur;
    }

    [Pure]
    internal bool SetContains(DomNode<T> n, T elt)
    {
      return false;
    }

  }

  class DomNode<T> where T : IComparable<T>
  {
  }

  partial class TestMain
  {
    partial void Run()
    {
      var i = new EqSetDom<string>();
      if (this.behave) {
        i.AddElement(i.root, "Hello");
      }
      else {
        i.AddElement(null, null);
      }
    }
    public ContractFailureKind NegativeExpectedKind = ContractFailureKind.Precondition;
    public string NegativeExpectedCondition = "elt != null";
  }
}
