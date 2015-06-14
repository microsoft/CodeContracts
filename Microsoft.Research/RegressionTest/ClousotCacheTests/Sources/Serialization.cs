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
using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;

class Test
{
  [ClousotRegressionTest]
#if FIRST
  [RegressionOutcome("No entry found in the cache")]
#else
  [RegressionOutcome("An entry has been found in the cache")]
#endif
  bool Type_Primitive()
  {
    Contract.Ensures(Contract.Result<bool>() == true);
    return true;
  }

  [ClousotRegressionTest]
#if FIRST
  [RegressionOutcome("No entry found in the cache")]
#else
  [RegressionOutcome("An entry has been found in the cache")]
#endif
  object Type_Object()
  {
    Contract.Ensures(Contract.Result<object>() != null);
    return new Object();
  }

  [ClousotRegressionTest]
#if FIRST
  [RegressionOutcome("No entry found in the cache")]
#else
  [RegressionOutcome("An entry has been found in the cache")]
#endif
  T Type_FormalMethodParamter<A,C,G,T>()
    where T : new()
  {
    Contract.Ensures(Contract.Result<T>() != null);
    return new T();
  }

  [ClousotRegressionTest]
#if FIRST
  [RegressionOutcome("No entry found in the cache")]
#else
  [RegressionOutcome("An entry has been found in the cache")]
#endif
  int[] Type_Array()
  {
    Contract.Ensures(Contract.Result<int[]>() != null);
    return new int[0];
  }

  [ClousotRegressionTest]
#if FIRST
  [RegressionOutcome("No entry found in the cache")]
#else
  [RegressionOutcome("An entry has been found in the cache")]
#endif
  int[,,] Type_Array3()
  {
    Contract.Ensures(Contract.Result<int[,,]>() != null);
    return new int[0,0,0];
  }

  [ClousotRegressionTest]
#if FIRST
  [RegressionOutcome("No entry found in the cache")]
#else
  [RegressionOutcome("An entry has been found in the cache")]
#endif
  List<int> Type_Specialized()
  {
    Contract.Ensures(Contract.Result<List<int>>() != null);
    return new List<int>();
  }

  class DummyClass
  {
    public class NestedClass
    {
      public object DummyField;
    }
  }

  [ClousotRegressionTest]
#if FIRST
  [RegressionOutcome("No entry found in the cache")]
#else
  [RegressionOutcome("An entry has been found in the cache")]
#endif
  DummyClass.NestedClass Type_NestedType()
  {
    Contract.Ensures(Contract.Result<DummyClass.NestedClass>() != null);
    return new DummyClass.NestedClass();
  }

  [ClousotRegressionTest]
#if FIRST
  [RegressionOutcome("No entry found in the cache")]
#else
  [RegressionOutcome("An entry has been found in the cache")]
#endif
  object Type_NestedType(DummyClass.NestedClass x)
  {
    Contract.Requires(x != null);
    Contract.Ensures(Contract.Result<object>() == x.DummyField);
    return x.DummyField;
  }

  [ClousotRegressionTest]
#if FIRST
  [RegressionOutcome("No entry found in the cache")]
#else
  [RegressionOutcome("An entry has been found in the cache")]
#endif
  bool Parameters(object x, object y)
  {
    Contract.Ensures(Contract.Result<bool>() == ((x == this) || (x == y)));
    return x == this || x == y;
  }
}

public class GenericClass<A, C, G, T>
  where A : class, C
  where C : class
  where T : class
{
  [ClousotRegressionTest]
#if FIRST
    [RegressionOutcome("No entry found in the cache")]
#else
  [RegressionOutcome("An entry has been found in the cache")]
#endif
  T Type_FormalTypeParameter()
  {
    Contract.Ensures(Contract.Result<T>() == default(T));
    return default(T);
  }

  [Pure]
  C c(A a) { return a; }

  [Pure]
  static C sc(A a) { return a; }

  [ClousotRegressionTest]
#if FIRST
    [RegressionOutcome("No entry found in the cache")]
#else
  [RegressionOutcome("An entry has been found in the cache")]
#endif
  bool Method_TypeArguments(A a, C c)
  {
    Contract.Ensures(Contract.Result<bool>() == (this.c(a) == c));
    return this.c(a) == c;
  }

  [ClousotRegressionTest]
#if FIRST
    [RegressionOutcome("No entry found in the cache")]
#else
  [RegressionOutcome("An entry has been found in the cache")]
#endif
  bool Method_Static(A a, C c)
  {
    Contract.Ensures(Contract.Result<bool>() == (sc(a) == c));
    return sc(a) == c;
  }

  [Pure]
  U f<U>(bool b, U x) { return b ? x : default(U); }

  [ClousotRegressionTest]
#if FIRST
    [RegressionOutcome("No entry found in the cache")]
#else
  [RegressionOutcome("An entry has been found in the cache")]
#endif
  A Method_Specialized(bool b, A a)
  {
    Contract.Ensures(Contract.Result<A>() == this.f<A>(b, a));
    return this.f(b, a);
  }

  [ClousotRegressionTest]
#if FIRST
    [RegressionOutcome("No entry found in the cache")]
#else
  [RegressionOutcome("An entry has been found in the cache")]
#endif
  U Method_Generic<X, U, Y>(bool b, U u)
    where U : class
  {
    Contract.Ensures(Contract.Result<U>() == this.f<U>(b, u));
    return this.f(b, u);
  }
}
