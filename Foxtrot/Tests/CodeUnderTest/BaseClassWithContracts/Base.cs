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

#define CONTRACTS_FULL
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace BaseClassWithContracts {
  public class Base {
    internal static string ResourceMessage { get { return "x must be positive"; } }

    public virtual int M(int x, bool behave)
    {
      Contract.Requires(0 < x);
      Contract.Ensures(Contract.Result<int>() == -x);
      Contract.EnsuresOnThrow<ApplicationException>(x == 0);

      return behave ? -x : 3;
    }

    public virtual int WithUserStrings(int x, bool behave)
    {
      Contract.Requires(0 < x, ResourceMessage);
      Contract.Requires<ArgumentException>(1 < x, "x must be greater than 1");
      Contract.Ensures(Contract.Result<int>() == -x, "result is negated x");
      Contract.EnsuresOnThrow<ApplicationException>(x == 0, "Throws only if x == 0");

      return behave ? -x : 3;
    }
  }
}

namespace TypeParameterWritingTest {
  public interface IDal<TType1, TType2> {
    TEntity Load<TEntity, TKey>(TKey key) where TEntity : TKey;
    TEntity Load<TEntity, TKey>(TEntity key) where TEntity : IEntity<TKey>;
    TType1 Load<TEntity, TKey>(TType1 key) where TEntity : TKey;
    TType1 Load<TEntity, TKey>(TType2 key) where TEntity : IEntity<TKey>;
    TType2 Load<TEntity, TKey>(TKey key, bool b) where TEntity : TKey;
    TType2 Load<TEntity, TKey>(TEntity key, bool b) where TEntity : IEntity<TKey>;
    TKey Load<TEntity, TKey>(TType1 key, bool b) where TEntity : TKey;
    TKey Load<TEntity, TKey>(TType2 key, bool b) where TEntity : IEntity<TKey>;
  }

  public interface IEntity<TKey> {
  }
}

// The code in Onur isn't used but appears in the contract reference assembly. When
// extracted and copied when referenced from a third assembly, this caused copying problems in the past
// Thus it appears here to be part of the regular regression.
namespace Onurg {
  public class Foo : IFoo {
    IEnumerable<T> IFoo.Bar<T>()
    {
      return null;
    }
  }

  [ContractClass(typeof(FooContract))]
  public interface IFoo {
    IEnumerable<T> Bar<T>();
  }

  [ContractClassFor(typeof(IFoo))]
  abstract class FooContract : IFoo {
    IEnumerable<T> IFoo.Bar<T>()
    {
      Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);
      Contract.Ensures(Contract.ForAll(Contract.Result<IEnumerable<T>>(), x => x != null));
      return default(IEnumerable<T>);
    }
  }

}
