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
  [ContractClass(typeof(GenericAbstractClassContracts<,>))]
  public abstract class GenericAbstractClass<A,B> where A: class,B
  {
    public abstract bool IsMatch(B b, A a);

    public abstract B ReturnFirst(B[] args, A match, bool behave);

    public abstract A[][] Collection(int x, int y);

    public abstract A FirstNonNullMatch(bool behave, A[] elems);

    public abstract C[] GenericMethod<C>(A[] elems);
  }

  [ContractClassFor(typeof(GenericAbstractClass<,>))]
  internal abstract class GenericAbstractClassContracts<A,B> : GenericAbstractClass<A,B>
    where A : class, B
  {
    public override bool IsMatch(B b, A a)
    {
      throw new NotImplementedException();
    }

    public override B ReturnFirst(B[] args, A match, bool behave)
    {
      Contract.Requires(args != null);
      Contract.Requires(args.Length > 0);
      Contract.Ensures(Contract.Exists(0, args.Length, i => args[i].Equals(Contract.Result<B>()) && IsMatch(args[i], match)));
      return default(B);
    }

    public override A[][] Collection(int x, int y)
    {
      Contract.Ensures(Contract.ForAll(Contract.Result<A[][]>(), nested => nested != null && nested.Length == y && Contract.ForAll(nested, elem => elem != null)));
      Contract.Ensures(Contract.ForAll(0, x, index => Contract.Result<A[][]>()[index] != null));
 	    throw new NotImplementedException();
    }

    public override A FirstNonNullMatch(bool behave, A[] elems)
    {
      // meaningless, but testing our closures, in particular inner one with a static closure referring to result.
      Contract.Ensures(Contract.Exists(0, elems.Length, index => elems[index] != null && elems[index] == Contract.Result<A>() && 
                                          Contract.ForAll(0, index, prior => Contract.Result<A>() != null)));
      // See if we are properly sharing fields.
      Contract.Ensures(Contract.Exists(0, elems.Length, index => elems[index] != null && elems[index] == Contract.Result<A>() &&
                                          Contract.ForAll(0, index, prior => Contract.Result<A>() != null)));
      // See if we are properly sharing fields.
      Contract.Ensures(Contract.Exists(0, elems.Length, index => elems[index] != null &&
                                          Contract.ForAll(0, index, prior => Contract.Result<A>() != null)));

      throw new NotImplementedException();
    }

    public override C[] GenericMethod<C>(A[] elems)
    {
      Contract.Requires(elems != null);
      Contract.Ensures(Contract.Result<C[]>() != null);
      Contract.Ensures(Contract.ForAll(Contract.Result<C[]>(), resultElem => Contract.Exists(elems, orig => resultElem.Equals(orig))));


      throw new NotImplementedException();
    }
  }

  public class ImplForGenericAbstractClass : GenericAbstractClass<string, string>
  {
    public override bool IsMatch(string b, string a)
    {
      return b == a;
    }

    public override string ReturnFirst(string[] args, string match, bool behave)
    {
      for (int i = 0; i < args.Length; i++)
      {
        if (IsMatch(args[i], match)) return args[i];
      }
      return default(string);
    }

    public override string[][] Collection(int x, int y)
    {
      var result = new string[x][];
      for (int i=0; i<result.Length; i++) {
        result[i] = new string[y];
        for (int j = 0; j < y; j++)
        {
          if (x == 5 && y == 5 && i == 4 && j == 4)
          {
            // behave badly
            continue;
          }
          result[i][j] = "Foo";
        }
      }
      return result;
    }

    public override string FirstNonNullMatch(bool behave, string[] elems)
    {
      if (!behave) return "foobar";
      for (int i = 0; i < elems.Length; i++)
      {
        if (elems[i] != null) return elems[i];
      }
      return null;
    }

    public override C[] GenericMethod<C>(string[] elems)
    {
      List<C> result = new List<C>();

      foreach (var elem in elems) {
        if (elem is C) {
          result.Add((C)(object)elem);
        }
      }
      if (typeof(C) == typeof(int)) {
        // behave badly
        result.Add((C)(object)55);
      }
      return result.ToArray();
    }
  }

  partial class TestMain
  {
    partial void Run()
    {
      var i = new ImplForGenericAbstractClass();
      i.FirstNonNullMatch(behave, new string[]{null, "a",null,"b"});
    }
    public ContractFailureKind NegativeExpectedKind = ContractFailureKind.Postcondition;
    public string NegativeExpectedCondition = "Contract.Exists(0, elems.Length, index => elems[index] != null && elems[index] == Contract.Result<A>() && Contract.ForAll(0, index, prior => Contract.Result<A>() != null))";
  }
}
